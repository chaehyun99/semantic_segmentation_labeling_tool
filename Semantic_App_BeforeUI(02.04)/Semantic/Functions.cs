using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Python.Runtime;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;//솔루션에서 PresentationCore.dll 참조추가 -JSW
using System.Text.RegularExpressions;

namespace Semantic
{
    partial class UI_Main
    {
        public void Load_()
        {
            // 이미지 리스트 및 경로 초기화
            this.LeftDock_flowPanel_Thumbnail.Controls.Clear();
            picBox_Origin.Image = null;
            imgList = null;

            picBox_Rgb.Parent = picBox_Origin;
            picBox_Rgb.BackColor = Color.Transparent;
            //thePointRelativeToTheBackImage;
            picBox_Rgb.Location = new Point(0, 0);

            Console.WriteLine("pbox2위치;" + Convert.ToString(picBox_Cursor.Location));

            //커서그려줄 패널 겹치기



            gray_imglist.Clear();
            rgb_imglist.Clear();

            string[] files = Directory.GetFiles(input_file_path.SelectedPath);

            imgList = files.Where(x => x.IndexOf(".bmp", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();

            for (int index = 0; index < imgList.Count(); index++)
                imgList[index] = imgList[index].Remove(0, input_file_path.SelectedPath.Count());

            //썸넬 목록 갱신
            for (int i = 0; i < imgList.Count; i++)
            {
                Image img = Image.FromFile(input_file_path.SelectedPath + imgList[i]);

                Panel panelThumb = new Panel();
                panelThumb.BackColor = Color.Black;
                panelThumb.Size = new Size(Constants.Thumbnail_Width, Constants.Thumbnail_Height);
                panelThumb.Padding = new System.Windows.Forms.Padding(4);

                PictureBox pBoxThumb = new PictureBox();
                pBoxThumb.BackColor = Color.DimGray;
                pBoxThumb.Dock = DockStyle.Fill;
                pBoxThumb.SizeMode = PictureBoxSizeMode.Zoom;
                pBoxThumb.Image = img.GetThumbnailImage(Constants.Thumbnail_Width, Constants.Thumbnail_Height, null, IntPtr.Zero);
                pBoxThumb.Click += PBoxThumbnail_Click;
                pBoxThumb.Tag = i.ToString();
                panelThumb.Controls.Add(pBoxThumb);

                this.LeftDock_flowPanel_Thumbnail.Controls.Add(panelThumb);
            }



            if (imgList.Count <= 0)
            {
                return;
            }
            else
            {
                Panel pnl = this.LeftDock_flowPanel_Thumbnail.Controls[0] as Panel;
                PictureBox pb = pnl.Controls[0] as PictureBox;
                PBoxThumbnail_Click(pb, null);
            }

            //if (null == rgb_imglist || 0 == rgb_imglist.Count()){}
        }

        public void Network_route_settings()
        {
            Network_route_settings setting_Form = new Network_route_settings();

            setting_Form.ShowDialog();
            if (whether_to_save_)
            {
                Load_();
                whether_to_save_ = false;
            }
        }

        public static void AddEnvPath(params string[] paths)
        {
            // PC에 설정되어 있는  환경 변수를 가져온다.
            var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
            // 중복 환경 변수가 없으면 list에 넣는다.
            envPaths.InsertRange(0, paths.Where(x => x.Length > 0 && !envPaths.Contains(x)).ToArray());
            // 환경 변수를 다시 설정한다.
            Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
        }

        public static void Find_conda(ref string PYTHON_HOME)
        {
            string result = "";
            string[] words;
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();
            Console.WriteLine("loading");
            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.CreateNoWindow = true;
            cmd.UseShellExecute = false;

            cmd.RedirectStandardInput = true;
            cmd.RedirectStandardError = true;
            cmd.RedirectStandardOutput = true;


            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();

            process.StandardInput.Write("where conda" + Environment.NewLine);
            process.StandardInput.Write("conda update --all -y" + Environment.NewLine);
            process.StandardInput.Write("conda env create -f environment.yml" + Environment.NewLine);

            process.StandardInput.Close();
            result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(result);

            words = Regex.Split(result, "\r\n");
            if (!words[4].Contains("conda") || !words[5].Contains("conda"))
            {
                MessageBox.Show("확인되는 conda 가 없습니다 프로그램을 종료합니다");
                Application.ExitThread();
                Environment.Exit(0);
            }

            result = words[4];
            string tmp = words[5];
            int index;
            for (index = 0; index < words[4].Length; index++)
            {
                if (result[index] != tmp[index]) break;
            }
            PYTHON_HOME = result.Substring(0, index);

            process.WaitForExit();
            process.Close();

        }

        public static void Pythonnet_(string input_path, string output_path, List<string> imgList)
        {
            // python 설치 경로
            var PYTHON_HOME = "";
            //var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"\anaconda\");

            // 환경 변수 설정
            Find_conda(ref PYTHON_HOME);
            AddEnvPath(PYTHON_HOME, Path.Combine(PYTHON_HOME, @"envs\deeplab\Library\bin"));

            // Python 홈 설정.
            PythonEngine.PythonHome = PYTHON_HOME;

            // 모듈 패키지 패스 설정.
            PythonEngine.PythonPath = string.Join(
            Path.PathSeparator.ToString(),
            new string[] {
                PythonEngine.PythonPath,
                // pip하면 설치되는 패키지 폴더.
                Path.Combine(PYTHON_HOME, @"envs\deeplab\Lib\site-packages"),
                // 개인 패키지 폴더
                Environment.CurrentDirectory
            }
            );
            // Python 엔진 초기화
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic deeplab = Py.Import("python.deeplab");
                OpenFileDialog dialog = new OpenFileDialog();
                dynamic func = deeplab.Caclulating(input_path, output_path, imgList);
                func.cal();

            }

            PythonEngine.Shutdown();
        }

        private void ClearStack()
        {
            stackUndo.Clear();
            stackRedo.Clear();
        }

        private void Input_Action(Bitmap src4Input)
        {
            if (null == src4Input)
            {
                return;
            }

            //1.새로운 수정내용이 생겼으므로 기존의 redo스택을 날린다.
            stackRedo.Clear();

            //2.입력받은 액션(src4Input)을 stackUndo에 저장.
            stackUndo.AddLast(new Bitmap(src4Input));

            //ETC.모든 AddLast이후, 저장개수 검사 후 지정한 최댓값을 넘으면 오래된 순으로 제거.
            if (stackUndo.Count > _maxHistory_)
            {
                stackUndo.RemoveFirst();
            }
        }

        public void PBoxThumbnail_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.LeftDock_flowPanel_Thumbnail.Controls.Count; i++)
            {
                if (this.LeftDock_flowPanel_Thumbnail.Controls[i] is Panel)
                {
                    Panel pnl = this.LeftDock_flowPanel_Thumbnail.Controls[i] as Panel;
                    pnl.BackColor = Color.Black;
                }
            }

            PictureBox pb = sender as PictureBox;
            pb.Parent.BackColor = Color.Red;

            int idx = Convert.ToInt32(pb.Tag.ToString());
            sourceBitmapOrigin = new Bitmap(input_file_path.SelectedPath + imgList[idx]);

            //픽쳐박스2에 띄워질 비트맵 변경. (+ 커서가 그려질 비트맵 크기조절)
            if (null == rgb_imglist || 0 == rgb_imglist.Count())
            {
                Console.WriteLine("rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                return;
            }
            else
            {
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                //커서가 그려질 보드의 비트맵 갱신(크기)
                cursorBoardBitmap = new Bitmap(sourceBitmapRgb.Width, sourceBitmapRgb.Height);
                cursorBoardBitmap.MakeTransparent();

                //TODO: ㅁㄴㅇㄹ 컬러슬라이더 라이브러리로 바뀐것에 대응하기.
                SetAlpha(trackBar1.Value);

                // 픽쳐박스에 올라온 이미지가 바뀔때마다 
                // 1. 기존 스택을 비우고,
                ClearStack();

                // 2.UndoStack에 +1을 시도.(rgb변환이 되었든 안되었든)
                Input_Action(sourceBitmapRgb);

                RefreshAllPictureBox();

            }

        }

        public static class ColorTable
        {

            //c#에 맞게 사용. 색상표는 r언어나 matlab에서 쓰는것 중에 적당히
            public static int Entry_Length = 40;

            public static Color[] Entry =
            {
                Color.FromArgb(0, 0, 0),            // 0=background    
                Color.FromArgb(128, 0, 0),          // 1=aeroplane     
                Color.FromArgb(0, 128, 0),          // 2=bicycle       
                Color.FromArgb(128, 128, 0),        // 3=bird          
                Color.FromArgb(0, 0, 128),          // 4=boat          
                Color.FromArgb(128, 0, 128),        // 5=bottle        
                Color.FromArgb(0, 128, 128),        // 6=bus           
                Color.FromArgb(128, 128, 128),      // 7=car           
                Color.FromArgb(255, 255, 255),      // 8=cat           
                Color.FromArgb(192, 0, 0),          // 9=chair         
                Color.FromArgb(64, 128, 0),         // 10=cow          
                Color.FromArgb(192, 128, 0),        // 11=dining table 
                Color.FromArgb(64, 0, 128),         // 12=dog          
                Color.FromArgb(192, 0, 128),        // 13=horse        
                Color.FromArgb(64, 128, 128),       // 14=motorbike    
                Color.FromArgb(192, 128, 128),      // 15=person       
                Color.FromArgb(0, 64, 0),           // 16=potted plant 
                Color.FromArgb(128, 64, 0),         // 17=sheep        
                Color.FromArgb(0, 192, 0),          // 18=sofa         
                Color.FromArgb(128, 192, 0),        // 19=train        
                Color.FromArgb(0, 64, 128),          // 20=tv/monitor
                
                //규격외 파일 테스트용 더미 컬러
                Color.FromArgb(0,0, 64),            // 0=background    
                Color.FromArgb(128, 0, 64),          // 1=aeroplane     
                Color.FromArgb(0, 128, 64),          // 2=bicycle       
                Color.FromArgb(128, 128, 64),        // 3=bird          
                Color.FromArgb(0, 192, 128),          // 4=boat          
                Color.FromArgb(128, 64, 128),        // 5=bottle        
                Color.FromArgb(0, 128, 128),        // 6=bus           
                Color.FromArgb(128, 0, 128),      // 7=car           
                Color.FromArgb(255, 255, 255),      // 8=cat           
                Color.FromArgb(192, 0, 0),          // 9=chair         
                Color.FromArgb(64, 128, 0),         // 10=cow          
                Color.FromArgb(192, 128, 0),        // 11=dining table 
                Color.FromArgb(64, 0, 128),         // 12=dog          
                Color.FromArgb(192, 0, 128),        // 13=horse        
                Color.FromArgb(64, 128, 128),       // 14=motorbike    
                Color.FromArgb(192, 128, 128),      // 15=person       
                Color.FromArgb(0, 64, 0),           // 16=potted plant 
                Color.FromArgb(128, 64, 0),         // 17=sheep        
                Color.FromArgb(0, 192, 0),          // 18=sofa         
                Color.FromArgb(128, 192, 0),        // 19=train        
                Color.FromArgb(0, 64, 128),

            };
            /*
            public static Color[] Entry_byKnownName =
             {
                //20번까지는 python 모델에서 사용하는 클래스 순서 그대로 사용.
                Color.Black,       // 0=background
                Color.LightPink,        // 1=aeroplane
                Color.DarkGreen,       //2=bicycle
                Color.LightBlue,        // 3=bird
                Color.LawnGreen,        // 4=boat
                Color.Lavender,         // 5=bottle
                Color.Khaki,            // 6=bus
                Color.Ivory,            // 7=car
                Color.IndianRed,        // 8=cat
                Color.HotPink,          // 9=chair
                Color.Lime,             // 10=cow
                Color.LightCoral,        // 11=dining table
                Color.LightSalmon,      // 12=dog
                Color.SlateBlue,        // 13=horse
                Color.OrangeRed,        // 14=motorbike
                Color.Yellow,           // 15=person
                Color.Navy,             // 16=potted plant
                Color.Wheat,            // 17=sheep
                Color.MediumTurquoise,  // 18=sofa
                Color.Magenta,          // 19=train
                Color.Gray,             // 20=tv/monitor
                ///이하 규격외 이미지 대상 테스트 전용.
                ///ColorTable의 인덱스 관련된 오류가 있을때 주석 풀고 테스트.
                Color.Tan,
                Color.Aqua,
                Color.DarkCyan,
                Color.DarkKhaki,
                Color.LemonChiffon,
                Color.DeepPink,
                Color.Pink,
                Color.LightCoral,
                Color.AntiqueWhite,
                Color.AliceBlue, //30        
                Color.DarkCyan,
                Color.BlueViolet,
                Color.Chartreuse,
                Color.DarkOliveGreen,
                Color.Cornsilk,
                Color.DarkOrange,
                Color.Gainsboro,
                Color.Blue,
                Color.Bisque,
                Color.DarkGoldenrod//40
             };
             */
        }

        public void Image_Save()
        {
            if (null == rgb_imglist || 0 == rgb_imglist.Count)
            {
                MessageBox.Show("저장 할 이미지가 없습니다 !! ");
                return;
            }

            if (sourceBitmapRgb == null)
            {
                MessageBox.Show("수정 된 이미지가 없습니다 !! ");
                return;
            }

            if (gray_file_path.SelectedPath == string.Empty)
            {
                MessageBox.Show("그레이 스케일 저장 경로가 없습니다.");
                Network_route_settings();
                return;
            }

            // index 넣어서 저장
            //rgb_imglist[index] = sourceBitmapRgb.;
            MessageBox.Show(current_idx.ToString());
            rgb_imglist[current_idx] = new Bitmap(sourceBitmapRgb);

            for (int index = 0; index < rgb_imglist.Count(); index++)
            {
                gray_imglist[index] = RGB2Gray_Click(rgb_imglist[index]);
                gray_imglist[index].Save(gray_file_path.SelectedPath + imgList[index].Remove(imgList[0].Count() - 4, 4) + "_gray_img.png");
            }
        }

        public Color Swap_G2RGB(Color co_Gray)
        {
            byte value2Swap = co_Gray.R; //R이든 G든 B든 상관x.
            Color Ret_Color = ColorTable.Entry[value2Swap];
            return Ret_Color;
        }

        public Color Swap_RGB2G(Color co_RGB)
        {


            Color Ret_Color = co_RGB;
            for (int i = 0; i < ColorTable.Entry_Length; i++)
            {
                if (ColorTable.Entry[i].ToArgb().Equals(co_RGB.ToArgb()))
                {
                    Ret_Color = Color.FromArgb(255, i, i, i); //n번째 인덱스의 색상과 일치하면 그 인덱스값이 곧 회색 값
                    break;
                }

            }


            if ((Ret_Color.ToArgb().Equals(co_RGB.ToArgb())) && !(Ret_Color.ToArgb().Equals(Color.Black.ToArgb())))
            {
                MessageBox.Show("ERR: 해당 픽셀의 적절한 인덱스값을 찾지못했습니다." + Convert.ToString(co_RGB.Name));
                Ret_Color = Color.NavajoWhite;
            }

            return Ret_Color;
        }

        private Bitmap Gray2RGB_Click(Bitmap bmp_)
        {
            Bitmap img2Convert = new Bitmap(bmp_);

            int x, y;


            // Loop through the images pixels to reset color.
            for (x = 0; x < img2Convert.Width; x++)
            {
                for (y = 0; y < img2Convert.Height; y++)
                {
                    Color pixelColor = img2Convert.GetPixel(x, y);
                    Color newColor_gray = Swap_G2RGB(pixelColor);
                    img2Convert.SetPixel(x, y, newColor_gray);
                }
            }
            return img2Convert;

        }

        private Bitmap RGB2Gray_Click(Bitmap bmp_)
        {
            Bitmap img2Convert = new Bitmap(bmp_);

            int x, y;


            // Loop through the images pixels to reset color.
            for (x = 0; x < img2Convert.Width; x++)
            {
                for (y = 0; y < img2Convert.Height; y++)
                {
                    ///현재:픽셀의 rgb값을 테이블의 값과 비교해서 찾는 방식
                    ///->속도가 느림->index+palette 구조를 사용한다면 처음에 한번빼고는 헤더만 건드리니까 좋음.
                    //////->브러시툴의 구현방법이 어떻게 되는지랑은 관련있을수도.
                    ///&&실제로 퍼포먼스가 현저히 떨어질 정도인지 확인해보고 생각예정.
                    ///
                    ///퍼포먼스 개선방법 1.팔레트만 씌우는방법(불확실) ---->>Colormap 관련 MSDN 찾아볼것. 거의 실마리 찾았는데 적용할 시간없어서 Skip.
                    ///2.getpixel쓰지말고 lockbit해서 메모리접근(공부필요,그러나 유의미) 3.둘 다 적용(ㄴㄴ. 1이 가능하면 2가 필요없음)


                    Color pixelColor = img2Convert.GetPixel(x, y);

                    // MessageBox.Show("받아온 픽셀의 색상" + Convert.ToString(pixelColor));
                    Color newColor_RGB = Swap_RGB2G(pixelColor);
                    if (newColor_RGB == Color.NavajoWhite)
                    {
                        MessageBox.Show("에러발생 X:" + x.ToString() + ",y:" + y.ToString());
                    }
                    img2Convert.SetPixel(x, y, newColor_RGB);
                }
            }
            /*
            for (int i = 0; i < img2Convert.Width; i++)
            {
                img2Convert.SetPixel(i, 40, Color.Red);
                img2Convert.SetPixel(i, 41, Color.Red);
            }*/
            return img2Convert;
        }

        private void RefreshAllPictureBox()
        {
            picBox_Origin.Refresh();
            picBox_Rgb.Refresh();
            picBox_Cursor.Refresh();
        }

    }


}