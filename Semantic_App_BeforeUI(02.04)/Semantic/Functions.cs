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

        private void Network_route_settings()
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
            //TODO: 여기 if 한줄 더 들어가야되는지 아닌지 확인. (Branch_ SeongWoon에서 Undo_redo.cs 확인)

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
                Console.WriteLine("ERR: rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                return;
            }
            else
            {
                Console.WriteLine("rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                //커서가 그려질 보드의 비트맵 갱신(크기)
                cursorBoardBitmap = new Bitmap(sourceBitmapRgb.Width, sourceBitmapRgb.Height);
                cursorBoardBitmap.MakeTransparent();

                //TODO: ㅁㄴㅇㄹ 컬러슬라이더 라이브러리로 바뀐것에 대응하기.
                SetAlpha((int)colorSlider_Opacity.Value);

                // 픽쳐박스에 올라온 이미지가 바뀔때마다 
                // 1. 기존 스택을 비우고,
                ClearStack();

                // 2.UndoStack에 +1을 시도.(rgb변환이 되었든 안되었든)
                Input_Action(sourceBitmapRgb);

                RefreshAllPictureBox();

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

            //TODO: 모든 상황에 각 픽쳐박스가 다 refreash되게 해둔것 최적화하기.
            // == 전체적인 프로그램 흐름을 파악한 뒤에 지울거 지워서 불필요한 연산 줄이기.
        }

        private void SetScale(double scale) // 
        {
            if (0 == scale)
            {
                return;
            }
            zoomScale = scale;
            RefreshAllPictureBox();

            //TODO: Scale_toolStripStatusLabel 작동원리 파악해서 값 변하게 해주기.

            /*
             * 배율 조정하는 인터페이스(트랙바, 텍스트박스 등)의 변동이 아래에 들어가면 됨.
            this.ignoreChanges = true;
            this.ignoreChanges = false;
            */
        }

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

                //Console.WriteLine("브러시 pen_startpt 좌표: " + Convert.ToString(e.Location));
                //TODO: 이거 나중에 하단에 좌표 띄워주기 -> CursorPosition_toolStripStatusLabel

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

        private void SetTargetRectByZoomAt(zoomMode zoomOrigin, MouseEventArgs e)
        {
            Point zoomOrigin_pt = new Point();

            switch (zoomOrigin)
            {
                case zoomMode.Center:
                    zoomOrigin_pt.X = picBox_Origin.Width / 2;
                    zoomOrigin_pt.Y = picBox_Origin.Height / 2;
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

        private void SetTargetRectByZoomAtCenter(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - picBox_Origin.Width / 2) * Constants.ratioPerLevel) + picBox_Origin.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - picBox_Origin.Height / 2) * Constants.ratioPerLevel) + picBox_Origin.Height / 2;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - picBox_Origin.Width / 2) / Constants.ratioPerLevel) + picBox_Origin.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - picBox_Origin.Height / 2) / Constants.ratioPerLevel) + picBox_Origin.Height / 2;
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


        //브러시 크기조절
        private void SetBrushSize(int brushSize)
        {
            this.brush_Size = brushSize;

            //만약 브러시모양을 표시해줄거면 여기서 갱신
        }

        private void SetAlpha(int alpha)
        {

            float a = alpha / (float)colorSlider_Opacity.Maximum;

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
    }


}