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
    public partial class Main_form : Form
    {
        #region Field        

        List<string> imgList = null;

        List<Bitmap> gray_imglist = new List<Bitmap>();
        List<Bitmap> rgb_imglist = new List<Bitmap>();

        ///original_opac -> sourceBitmapRgb로 변경됨
        private Bitmap sourceBitmapRgb = null;

        private Bitmap sourceBitmapOrigin = null;

        public static bool whether_to_save_ = false;
        public static FolderBrowserDialog input_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog gray_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog rgb_file_path = new FolderBrowserDialog();

        private int cursor_mode = 1, brush_Size = 1;
        private Color brush_Color = Color.Black;


        private bool isScroll = false, isPaint = false;
        private Point move_startpt, move_endpt, pen_startpt, pen_endpt;


        private Rectangle targetImgRect = new Rectangle(0, 0, 100, 100);
        private int zoomLevel = 0;
        private double zoomScale = 1.0f;

        bool ctrlKeyDown;

        private ImageAttributes imageAtt = new ImageAttributes();

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
                /*
                */
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

        }
        #endregion

        #region 생성자 - MainForm()
        public Main_form()
        {
            this.KeyPreview = true;
            InitializeComponent();

        }
        #endregion

        #region Python Env Set
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
        #endregion

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

       

        #region Save Img
        public static FolderBrowserDialog GetSaveFolderDialog()
        {
            FolderBrowserDialog saveFolderDig = new FolderBrowserDialog();
            saveFolderDig.RootFolder = Environment.SpecialFolder.Desktop;

            return saveFolderDig;
        }
        /*
        private void Image_Save_Click(Object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDiag = GetSaveFolderDialog())
            {
                if (folderDiag.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string dir = folderDiag.SelectedPath;

                    Image image = pictureBox1.Image;

                    string fileName = "test.jpg".ToString();

                    if (image != null)
                    {
                        string imageSavePath = string.Format(@"{0}\{1}", dir, fileName);
                        image.Save(imageSavePath);
                        MessageBox.Show("저장 완료");
                    }



                    this.Cursor = Cursors.Default;

                }

            }

        }
        */
        #endregion

        #region GrayScale <-> RGB Function
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
                if (ColorTable.Entry_byKnownName[i].ToArgb().Equals(co_RGB.ToArgb()))
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
        #endregion

        #region Opacity Function
        private void SetAlpha(int alpha)
        {

            float a = alpha / (float)trackBar1.Maximum;

            //투명도 표시 인터페이스
            lable_Opacity.Tag = (int)Math.Round(a * 100);
            lable_Opacity.Refresh();

            float[][] matrixItems = {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, a, 0},
                new float[] {0, 0, 0, 0, 1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            return;
        }

        //트레이스바(투명도 비율 정보) 조정 - 투명도 조절용
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (null == sourceBitmapRgb)
            {
                return;
            }
            SetAlpha(trackBar1.Value);

            RefreshAllPictureBox();

        }
        #endregion


        #region <그리기: 선>

        /// <summary>
        /// pt2pt의 선
        /// </summary>
        /// <param name="myPen"></param>
        /// <param name="g"></param>
        private void DrawFreeLine(Pen myPen, Graphics g)
        {
            g.DrawLine(myPen, pen_startpt, pen_endpt);
        }

        /// <summary>
        /// isPaint를 확인하고 값이 True면,
        /// 펜 생성->그리기->새로고침 실행합니다.
        /// </summary>
        private void DrawShape() 
        {
            if (false == isPaint)
            {
                return;
            }

            Color brush_Color = Color.Black;
            Pen myPen = new Pen(brush_Color, brush_Size);                
            //TODO: myPen의 수명이 언제 끝나는지 확인해서 Dispose처리 해주기.
            //TODO: 모든 disposable의 수명이 언제 끝나는지 미리 확인해서 라이프사이클 관리. 

            //GC에 모든걸 맡기면 느려지거나 크래시날 확률 존재
            
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;


            using (Graphics g = Graphics.FromImage(sourceBitmapRgb)) 
            {
                DrawFreeLine(myPen, g);
            }


            RefreshAllPictureBox();
        }

        //브러시 크기조절
        private void SetBrushSize(int newbrushSize)
        {
            brush_Size = newbrushSize;

            //만약 브러시모양을 표시해줄거면 여기서 갱신
        }

        #endregion

        #region <이미지 배율- 확대/축소/직접지정>

        private void SetScale(double scale) // 
        {
            if (0 == scale)
            {
                return;
            }
            zoomScale = scale;
            RefreshAllPictureBox();
            lable_ImgScale.Refresh();

            /*
             * 배율 조정하는 인터페이스(트랙바, 텍스트박스 등)의 변동이 아래에 들어가면 됨.
            this.ignoreChanges = true;
            this.ignoreChanges = false;
            */
        }

        #endregion


        #region Event Control




        private void 경로설정FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Network_route_settings();
        }

        //좌측 이미지 목록 클릭시 동작
        private void uiPanelThumbnail_Paint(object sender, PaintEventArgs e){ }



        private void Network_operation_Click(object sender, EventArgs e)
        {
            if (imgList == null || imgList.Count() == 0)
            {
                MessageBox.Show("불러올 이미지가 없습니다.");
                return;
            }

            if (gray_file_path.SelectedPath == string.Empty)
            {
                MessageBox.Show("그레이 스케일 저장 경로가 없습니다.");
                Network_route_settings();
                return;
            }

            if (Constants.isTest_NoLabel_mode == true)
            {

                MessageBox.Show("테스트모드-RGB Swap only로 실행합니다. 모델 구동만 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {

                    //new Bitmap(경로)로 바꿀예정
                    gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));
                    Console.WriteLine("그레이경로: " + gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png");                    
                    rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));

                    //아예 rgb변환도 생략.
                    //rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image

                    /*테스트용 RGB 이미지 생성할때만 풀면됨
                    rgb_imglist[0].Save(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");
                    MessageBox.Show("rgb테스트용 이미지 저장 완료");
                    */
                }
                RefreshAllPictureBox();
                return;
            }
            else if (Constants.isTestmode == true)
            {
                MessageBox.Show("테스트모드로 실행합니다. 모델 구동, GrayToRGB변환을 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {

                    //new Bitmap(경로)로 바꿀예정
                    gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));
                    rgb_imglist.Add(new Bitmap(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png"));
                    Console.WriteLine("그레이경로: " + gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png");
                    Console.WriteLine("RGB 경로: " + rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");

                    //아예 rgb변환도 생략.
                    //rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image

                    /*테스트용 RGB 이미지 생성할때만 풀면됨
                    rgb_imglist[0].Save(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");
                    MessageBox.Show("rgb테스트용 이미지 저장 완료");
                    */
                }


                RefreshAllPictureBox();
                return;
            }
            
            //테스트 아닐때

            MessageBox.Show("그레이 스케일 변환 중 입니다.");
            Pythonnet_(input_file_path.SelectedPath, gray_file_path.SelectedPath, imgList);
            MessageBox.Show("그레이 스케일 변환 완료 !");

            //GrayScale 이미지 변수에 저장
            //Gray2RGB 이미지 변수에 저장
            for (int index = 0; index < imgList.Count(); index++)
            {
                gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));
                rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index])); 
            }


            RefreshAllPictureBox();

            return;
        }

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F) Network_route_settings();
            this.ctrlKeyDown = e.Control;
        }

        private void Main_form_KeyUp(object sender, KeyEventArgs e)
        {
            this.ctrlKeyDown = e.Control;
        }

        //pictureBox1_Paint() -> 수정을 위해 Main_Form_partial_zoomAtCursor.cs로 이동.

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

            if (null == rgb_imglist || 0 == rgb_imglist.Count || null == sourceBitmapRgb)
            {
                return;
            }

            // 보간 방식 지정. 
            if (zoomScale < 1)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            }
            else
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.

            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            e.Graphics.DrawImage(
                sourceBitmapRgb,
                targetImgRect,
                0,
                0,
                sourceBitmapOrigin.Width,
                sourceBitmapOrigin.Height,
                GraphicsUnit.Pixel,
                imageAtt
                );

        }

        private void Button_ZoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel++;
            SetScale(Math.Pow(Constants.ratioPerLevel, zoomLevel)); //윈도우 그림판은 첫번째 인자가 2로 잡혀있는 셈( 25%/ 50%/ 100%/ 200%/ 400%)
        }

        private void Button_ZoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel--;
            SetScale(Math.Pow(Constants.ratioPerLevel, zoomLevel));
        }
        private void button_ZoomReset_Click(object sender, EventArgs e)
        {
            //최초 위치로 되돌림.
            targetImgRect.X = 0;
            targetImgRect.Y = 0;

            zoomLevel = 0;
            SetScale(Math.Pow(1.5, zoomLevel));

        }



        private void lable_ImgScale_Paint(object sender, PaintEventArgs e)
        {
            lable_ImgScale.Text =
                "배율: "
                + Convert.ToString(Math.Round(zoomScale * 100))
                + "%"
                ;
        }

        private void lable_Opacity_Paint(object sender, PaintEventArgs e)
        {
            lable_Opacity.Text =
                "불투명도: "
                + Convert.ToString(lable_Opacity.Tag)
                + "%"
                ;
        }

        private void button_setscrollmode_Click(object sender, EventArgs e)
        {
            cursor_mode = 1;
        }

        private void button_setPaintmode_Click(object sender, EventArgs e)
        {
            cursor_mode = 2;
        }

        private void button_BrushsizeUp_Click(object sender, EventArgs e)
        {
            if (Constants.Max_brush_Size == brush_Size)
            {
                return;
            }
            SetBrushSize(brush_Size + 1);
        }

        private void button_BrushsizeDown_Click(object sender, EventArgs e)
        {
            if (1 == brush_Size)
            {
                return;
            }
            SetBrushSize(brush_Size - 1);
        }


        //이미지 저장버튼
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("t");
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1: //scroll mode
                    isScroll = true;
                    move_startpt = e.Location;
                    break;

                case 2: //paint mode

                    if (sourceBitmapRgb == null)
                    {
                        return;
                    }

                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion

                    //그리기 활성화
                    isPaint = true;

                    //펜 시작점 갱신.
                    pen_startpt.X = (int)Math.Round((mousePos.X - targetImgRect.X) / zoomScale);
                    pen_startpt.Y = (int)Math.Round((mousePos.Y - targetImgRect.Y) / zoomScale);

                    
                    /// pen좌표*zoom배율+src의 좌표 = mosPos이니까 우변으로 넘기고 나누면 동일
                    /// = 2배로 보고있을땐 10cm 이동해도 5cm 이동한 셈밖에 안됨
                    

                    #region 클릭만 했을때도 점(원) 그려짐.
                    using (Pen myPen = new Pen(brush_Color, 1))
                    using (Graphics g = Graphics.FromImage(sourceBitmapRgb))
                    {
                        Brush aBrush = new SolidBrush(brush_Color);

                        /* //Create Proper Circle */
                        Rectangle rectDot = new Rectangle(pen_startpt.X - (brush_Size + 2) / 2, pen_startpt.Y - (brush_Size + 2) / 2, brush_Size + 1, brush_Size + 1);

                        if (brush_Size == 1)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size, brush_Size);
                            g.FillRectangle(aBrush, rectDot);
                        }
                        else if (brush_Size == 2)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size - 1, brush_Size - 1);
                            g.DrawRectangle(myPen, rectDot);
                        }
                        else
                        {
                            g.FillEllipse(aBrush, rectDot);
                        }

                        RefreshAllPictureBox();
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {

            switch (cursor_mode)
            {
                case 1: //scroll mode
                    move_endpt = e.Location;
                    Move_targetRect_location();
                    move_startpt = move_endpt;
                    break;

                case 2: //paint mode
                    if (sourceBitmapRgb == null)
                    {
                        return;
                    }
                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion

                    //이미지의 확대,이동을 역산: 소스비트맵의 해당 좌표
                    pen_endpt = new Point((int)Math.Round((mousePos.X - targetImgRect.X) / zoomScale), (int)Math.Round((mousePos.Y - targetImgRect.Y) / zoomScale));
                   
                    DrawShape();
                    pen_startpt = pen_endpt;

                    break;
                default:
                    break;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1: //scroll mode

                    isScroll = false;
                    break;

                case 2: //paint mode

                    if (sourceBitmapRgb == null)
                    {
                        return;
                    }

                    #region <sender 캐스팅 & 좌표지정>

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;

                    #endregion

                    //그리기 비활성화.
                    isPaint = false;

                    ////
                    ///TODO: mouse_Down-> Move -> Up까지 한 번 그린 분량의 이미지를 값복사하여 stackUndo에 저장.
                    ///그리고 그리기 액션이 있을대마다 redoStack.Clear()해주기.
                    ////


                    break;
                default:
                    break;
            }
        }
        #endregion

    }


    public partial class Main_form : Form
    {
        //-----------------------------------------------------------------------------------------------속성

        //브러시 그리기 관련
        bool isOnPicBox3 = false;
        private Bitmap cursorBoardBitmap = null;

        //-----------------------------------------------------------------------------------------------메서드
        ///
        private void DrawCursor(Control control_, MouseEventArgs e)
        {

            using (Pen myPen = new Pen(brush_Color, 1))
            using (Graphics g = Graphics.FromImage(cursorBoardBitmap))
            {

                Brush aBrush = new SolidBrush(brush_Color);

                //이전에 그려진 브러시커서 처리
                //g.Clear(Color.Transparent);

                /* //Create Proper Circle */
                Point ptOnSrcBitmap = new Point((int)Math.Round((e.X - targetImgRect.X) / zoomScale), (int)Math.Round((e.Y - targetImgRect.Y) / zoomScale));

                Rectangle rectDot = new Rectangle(ptOnSrcBitmap.X - ((brush_Size + 2) / 2), ptOnSrcBitmap.Y - ((brush_Size + 2) / 2), brush_Size + 1, brush_Size + 1);

                Console.WriteLine("브러시 pen_startpt 좌표: " + Convert.ToString(e.Location));

                g.Clear(Color.Transparent);
                if (brush_Size == 1)
                {
                    rectDot = new Rectangle(ptOnSrcBitmap.X - (brush_Size) / 2, ptOnSrcBitmap.Y - (brush_Size) / 2, brush_Size, brush_Size);
                    g.FillRectangle(aBrush, rectDot);
                }
                else if (brush_Size == 2)
                {
                    rectDot = new Rectangle(ptOnSrcBitmap.X - (brush_Size) / 2, ptOnSrcBitmap.Y - (brush_Size) / 2, brush_Size - 1, brush_Size - 1);
                    g.DrawRectangle(myPen, rectDot);
                }
                else
                {
                    g.FillEllipse(aBrush, rectDot);
                }

                control_.Refresh();
            }
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            this.ctrlKeyDown = false;

            //rgb픽쳐박스와 커서픽쳐박스의 이벤트 전달여부 (picBox에 가려진 이벤트 발동)
            this.pBox3_CursorBoard.MouseDown += pictureBox2_MouseDown;
            this.pBox3_CursorBoard.MouseUp += pictureBox2_MouseUp;

            //호출순서 변경(정석은 아닌듯)
            this.pBox3_CursorBoard.MouseMove -= pBox3_CursorBoard_MouseMove;
            this.pBox3_CursorBoard.MouseMove += pictureBox2_MouseMove;
            this.pBox3_CursorBoard.MouseMove += pBox3_CursorBoard_MouseMove;

            this.pBox3_CursorBoard.MouseWheel += pictureBox1_MouseWheel;
        }

        public void Load_()
        {
            // 이미지 리스트 및 경로 초기화
            this.listPanelThumb.Controls.Clear();
            pictureBox1.Image = null;
            imgList = null;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
            //thePointRelativeToTheBackImage;
            pictureBox2.Location = new Point(0, 0);

            Console.WriteLine("pbox2위치;" + Convert.ToString(pictureBox2.Location));

            //커서그려줄 패널 겹치기

            pBox3_CursorBoard.Parent = pictureBox2;
            pBox3_CursorBoard.BackColor = Color.Transparent;
            //thePointRelativeToTheBackImage;
            pBox3_CursorBoard.Location = new Point(0, 0);
            Console.WriteLine("pbox3위치;" + Convert.ToString(pBox3_CursorBoard.Location));

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

                this.listPanelThumb.Controls.Add(panelThumb);
            }



            if (imgList.Count <= 0)
            {
                return;
            }
            else
            {
                Panel pnl = this.listPanelThumb.Controls[0] as Panel;
                PictureBox pb = pnl.Controls[0] as PictureBox;
                PBoxThumbnail_Click(pb, null);
            }

            //if (null == rgb_imglist || 0 == rgb_imglist.Count()){}
        }

        //-----------------------------------------------------------------------------------------------------------------이벤트
        ///


        private void pBox3_CursorBoard_Paint(object sender, PaintEventArgs e)
        {
            //1.보간처리 pictureBox2와 동일하게
            //2.현재 픽쳐박스와 동일한 크기의 패널로 구현 및갱신. //추후 부분만 갱신하는 방법으로 구현해보기

            //rgb이미지가 없을때.
            if (null == rgb_imglist || 0 == rgb_imglist.Count || null == sourceBitmapRgb || null == cursorBoardBitmap)
            {
                return;
            }

            // 보간 방식 지정. 
            if (zoomScale < 1)
            {
                //어느정도 중간값 사용. -> 확대시에 픽셀이 흐릿.
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            }
            else
            {
                //픽셀을 그대로 확대 -> 이미지 축소시에 1pixel짜리 곡선같은건 끊어져보이거나 사라짐.
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.

            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            e.Graphics.DrawImage(
                cursorBoardBitmap,
                targetImgRect,
                0,
                0,
                sourceBitmapOrigin.Width,
                sourceBitmapOrigin.Height,
                GraphicsUnit.Pixel,
                imageAtt
                );

            //브러시에 투명도 미적용 하려면 imageAtt만 제외.
        }

        private void pBox3_CursorBoard_MouseEnter(object sender, EventArgs e)
        {
            Console.WriteLine("커서표시영역 진입");

            Console.WriteLine("커서모드" + Convert.ToString(cursor_mode));
            Console.WriteLine(
                "커서비트맵:"
                + ((null == cursorBoardBitmap) ? "No" : "Yes")
                );

            //화면에 띄울 커서의 종류, 픽쳐박스에서 따라다닐 브러시의 표시유무(isOnpicBox3).
            //둘 다 isPaint, isScroll, cursor_mode에 따라 변경.

            isOnPicBox3 = true;
        }

        private void pBox3_CursorBoard_MouseLeave(object sender, EventArgs e)
        {
            Console.WriteLine("커서표시영역 탈출");

            isOnPicBox3 = false;


            PictureBox picBox = (PictureBox)sender;
            if ((2 != cursor_mode) || (null == cursorBoardBitmap))
            {
                return;
            }

            using (Graphics g = Graphics.FromImage(cursorBoardBitmap))
            {
                g.Clear(Color.Transparent);
            }
            picBox.Refresh();

        }

        private void pBox3_CursorBoard_MouseMove(object sender, MouseEventArgs e)
        {
            //picBox3.MouseMove 의 delegate로 picBox2_MouseMove를 줘도 되고,
            //아예 여기서 picBox2_MouseMove를 발생시켜도 됨.

            if ((2 != cursor_mode) || (null == cursorBoardBitmap))
            {
                return;
            }
            DrawCursor(pBox3_CursorBoard, e);
        }


        public void PBoxThumbnail_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.listPanelThumb.Controls.Count; i++)
            {
                if (this.listPanelThumb.Controls[i] is Panel)
                {
                    Panel pnl = this.listPanelThumb.Controls[i] as Panel;
                    pnl.BackColor = Color.Black;
                }
            }

            PictureBox pb = sender as PictureBox;
            pb.Parent.BackColor = Color.Red;


            ///////픽쳐박스 이미지 띄울 때 인덱스는 어디서?
            /// 
            /// pb.Tag = i.Tostring();            
            /// > pBoxThumbnail_click(pb, e);            
            /// >> pBox = sender as picturebox;
            /// >>> int idx = Convert.ToInt32(pb.Tag.ToString());
            /// >>>> sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);
            /// 
            /// /// /// /// 
            /// TODO?
            /// => i값 저장시 string으로 변환 안해도 됨.
            /// 
            /////////////////

            int idx = Convert.ToInt32(pb.Tag.ToString());
            //Image img = Image.FromFile(input_file_path.SelectedPath + imgList[idx]);
            sourceBitmapOrigin = new Bitmap(input_file_path.SelectedPath + imgList[idx]);

            //픽쳐박스2에 띄워질 비트맵 변경. (+ 커서가 그려질 비트맵 크기조절)
            if (null == rgb_imglist || 0 == rgb_imglist.Count())
            {
                Console.WriteLine("rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                return;
            }
            else
            {
                //pictureBox2.Image = rgb_imglist[idx];
                //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                //커서가 그려질 보드의 비트맵 갱신(크기)
                cursorBoardBitmap = new Bitmap(sourceBitmapRgb.Width, sourceBitmapRgb.Height);
                cursorBoardBitmap.MakeTransparent();

                SetAlpha(trackBar1.Value);

                RefreshAllPictureBox();

                Console.WriteLine("");
                Console.WriteLine("tagRect(XYWH)/src.WH: "
                    + Convert.ToString(targetImgRect.X)
                    + "/"
                    + Convert.ToString(targetImgRect.Y)
                    + "/"
                    + Convert.ToString(targetImgRect.Width)
                    + "/"
                    + Convert.ToString(targetImgRect.Height)
                    + "/"
                    + Convert.ToString(sourceBitmapRgb.Width)
                    + "/"
                    + Convert.ToString(sourceBitmapRgb.Height));
            }


            //UiTxt_File.Text = this.imgList[idx];
        }

    }


    public partial class Main_form : Form
    {
        //---------------------------------------------------------------속성

        enum zoomMode
        {
            Center,
            Cursor,
            TopLeft
        }



        // ------------------------------------------------------------- 메소드

        public void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                // WheelUp
                zoomLevel++;
            }
            else
            {
                // WheelDown
                zoomLevel--;
            }

            zoomScale = Math.Pow(Constants.ratioPerLevel, zoomLevel);

            if (true == ctrlKeyDown)
            {
                SetTargetRectByZoomAt(zoomMode.Cursor, e);
            }
            else
            {
                SetTargetRectByZoomAt(zoomMode.Center, e);
            }

            RefreshAllPictureBox();
            lable_ImgScale.Refresh();
        }

        private void SetTargetRectByZoomAt(zoomMode zoomOrigin, MouseEventArgs e)
        {
            Point zoomOrigin_pt = new Point();

            switch (zoomOrigin)
            {
                case zoomMode.Center:
                    zoomOrigin_pt.X = pictureBox1.Width / 2;
                    zoomOrigin_pt.Y = pictureBox1.Height / 2;
                    break;
                case zoomMode.Cursor:
                    zoomOrigin_pt = e.Location;
                    break;
                case zoomMode.TopLeft:
                    zoomOrigin_pt = new Point(0, 0);
                    break;
                default:
                    break;
            }

            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - zoomOrigin_pt.X) * Constants.ratioPerLevel) + zoomOrigin_pt.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - zoomOrigin_pt.Y) * Constants.ratioPerLevel) + zoomOrigin_pt.Y;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - zoomOrigin_pt.X) / Constants.ratioPerLevel) + zoomOrigin_pt.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - zoomOrigin_pt.Y) / Constants.ratioPerLevel) + zoomOrigin_pt.Y;
            }

            //"zoom [??] ByWheelFrom [??]"
            Console.WriteLine(
                "zoom "
                + (e.Delta > 0 ? "[IN]" : "[OUT]")
                + " ByWheelFrom ["
                + zoomOrigin.ToString()
                + "]"
                );

            #region 픽쳐박스의 특정 지점으로부터 zoom : 좌표계산
            /*

                A.TargetRect_New___________________________________
                |                                                 |
                |          B.TargetRect_Old_______                |
                |          |                      |               |
                |          |         C。          |               |
                |          |       (zoomOrigin)   |               |
                |          |______________________|               |
                |_________________________________________________|

            AC / BC = zoomRatio
            //상대좌표로 계산시 원점이 동일해야함. 이 경우엔 Control인 pictureBox1 기준.
            AC = zoomOrigin - TargetRect_New.Location
            BC = zoomOrigin - TargetRect_Old.Location

            A = C - (C - B) * zoomRatio
              = C * (1 - zoomRatio) + B * zoomRatio

            ___________________코드_____________________

            Point ZoomOrigin;
                       
            Double zoomRatio = TargetRectNew.Size / TargetRectOld.Size;
            //Width or Height.
            //계산량을 줄이고 싶으면 e.Delta의 부호를 조건으로 나누고, ZoomRatio를 상수로 지정해서 계산식을 나누면 됨.
           
            TargetRect.Location.X = ZoomOrigin.X * (1 - zoomRatio) + TargetRect.Location.X * zoomRatio;
            TargetRect.Location.Y = ZoomOrigin.Y * (1 - zoomRatio) + TargetRect.Location.Y * zoomRatio;

            //이하 AC > BC일때 ( Zoom in)
            targetImgRect.X = (int)Math.Round((targetImgRect.X - e.X) * Constants._ScaleIncreaseRatio) + e.X;
            targetImgRect.Y = (int)Math.Round((targetImgRect.Y - e.Y) * Constants._ScaleIncreaseRatio) + e.Y;

            */

            #endregion

        }

        #region <이미지 스크롤 by 마우스 드래그>
        /// <summary>
        /// 타겟이미지의 위치 변화 벡터 = 커서의 위치 변화벡터.
        /// 이동할때마다 이미지를 갱신합니다.
        /// </summary>
        private void Move_targetRect_location()
        {
            if (false == isScroll)
            {
                return;
            }

            targetImgRect.X += move_endpt.X - move_startpt.X;
            targetImgRect.Y += move_endpt.Y - move_startpt.Y;

            RefreshAllPictureBox();
        }
        #endregion


        private void SetTargetRectByZoomAtCenter(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - pictureBox1.Width / 2) * Constants.ratioPerLevel) + pictureBox1.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - pictureBox1.Height / 2) * Constants.ratioPerLevel) + pictureBox1.Height / 2;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - pictureBox1.Width / 2) / Constants.ratioPerLevel) + pictureBox1.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - pictureBox1.Height / 2) / Constants.ratioPerLevel) + pictureBox1.Height / 2;
            }

            Console.WriteLine("휠.줌.중앙_targetImgRect: " + Convert.ToString(targetImgRect));
        }

        private void SetTargetRectByZoomAtCursor(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - e.X) * Constants.ratioPerLevel) + e.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - e.Y) * Constants.ratioPerLevel) + e.Y;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - e.X) / Constants.ratioPerLevel) + e.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - e.Y) / Constants.ratioPerLevel) + e.Y;
            }

            Console.WriteLine("휠.줌.커서_targetImgRect: " + Convert.ToString(targetImgRect));
        }



        //-----------------------------------------------------------------------------이벤트


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //ORIGIN 이미지가 없을때.
            if (null == imgList || 0 == imgList.Count || null == sourceBitmapOrigin)
            {
                return;
            }

            // 보간 방식 지정. 축소시엔 부드럽게, 확대했을땐 픽셀 뚜렷하게.
            if (zoomScale < 1)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            }
            else
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.
            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            //---------------------------------------------------------------------------------
            ///이미지가 픽쳐박스 탈출하는 것 방지.
            //---------------------------------------------------------------------------------

            //적용하는 순서에 따라 zoomOut시에 어느 모서리로 붙을지 결정.
            //중앙에 띄우고싶으면..?

            if (targetImgRect.X > 0) targetImgRect.X = 0;
            if (targetImgRect.Y > 0) targetImgRect.Y = 0;
            if (targetImgRect.X + targetImgRect.Width < pictureBox1.Width) targetImgRect.X = pictureBox1.Width - targetImgRect.Width;
            if (targetImgRect.Y + targetImgRect.Height < pictureBox1.Height) targetImgRect.Y = pictureBox1.Height - targetImgRect.Height;

            //---------------------------------------------------------------------------------

            e.Graphics.DrawImage(
                sourceBitmapOrigin,
                targetImgRect,
                0,
                0,
                sourceBitmapOrigin.Width,
                sourceBitmapOrigin.Height,
                GraphicsUnit.Pixel
                );
        }

        //TODO: 일단은 모든 상황에 각 픽쳐박스가 다 refreash되게 해둔것 고치기
        // -> 전체적인 프로그램 흐름을 파악한 뒤에 지울거 지워서 불필요한 연산 줄이기.

        //TODO: 각 _paint 이벤트 함수에서 축소시 사용하는 보간방식 적절히 변경
        //확대는 아무리해도 그냥 래스터 그대로 보여주니까 안느려짐. 어짜피 축소했을떈 수정안하니까 타협필요. 
        private void RefreshAllPictureBox()
        {
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pBox3_CursorBoard.Refresh();
        }
    }

    static class Constants
    {
        public const int Thumbnail_Width = 300;
        public const int Thumbnail_Height = 150;

        public const double ratioPerLevel = 1.1;

        public const int Max_brush_Size = 10;

        //
        //public const bool isTestmode = false;                     //원본이미지만 필요
        //
        //public const bool isTest_NoLabel_mode = true;               //모델구동 생략. 레이블링해놓은 GrayScale 이미지를 경로에 둔채로 테스트.                          
        //
        public const bool isTest_NoLabel_mode = false;
        public const bool isTestmode = true;                       //모델구동+rgb 전부 생략. GraysCale&RGB 이미지를 경로에 둔채로 테스트.                   
        //

        ///모델구동, rgb변환없이 작업 시작할 때 키고, 
        ///모델구동, rgb변환해야되거나 공식적으로 올릴땐 false
    }


}
