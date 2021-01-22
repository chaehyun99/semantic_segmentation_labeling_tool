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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;//솔루션에서 PresentationCore.dll 참조추가 -JSW
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Semantic
{

    public partial class Main_form : Form
    {
        #region Field
        static class Constants
        {
            public const int Image_width = 300;
            public const int Image_height = 150;
        }

        List<string> imgList = null;
        List<Image> gray_imglist = new List<Image>();
        List<Image> rgb_imglist = new List<Image>();

        Bitmap sourceBitmapRgb = new Bitmap(200, 200);
        Bitmap sourceBitmapOrigin = null;

        //이미지 그릴때 속성. 투명도, 색상 필터링시 사용.
        ImageAttributes ImageAtt_pictureBox2 = new ImageAttributes();

        public static bool whether_to_save_ = false;
        public static FolderBrowserDialog input_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog gray_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog rgb_file_path = new FolderBrowserDialog();

        private Rectangle targetImgRect = new Rectangle(0, 0, 1, 1);
        private int cursor_mode = 2, brush_Size = 1;
        private bool isScroll = false, isPaint = false;
        private Point move_startpt, move_endpt, pen_startpt, pen_endpt;
        private double zoomScale=1;

        /// <summary>
        /// 배율 입출력 인터페이스 변경할때 Lock
        /// </summary>
        private bool ignoreChanges = false;

        #region UNDO 및 REDO - 필드

        ///     ///     ///     ///     ///     ///
        /// 최대 갯수가 제한된 stack처럼 사용.
        /// 삽입과 출력은 last에서, 최대개수 초과시에만 first에서 제거.
        /// 연결리스트의 Last를 스택의 Top처럼 사용.
        ///     ///     ///    ///      ///     ///

        LinkedList<Bitmap> stackUndo = new LinkedList<Bitmap>();
        LinkedList<Bitmap> stackRedo = new LinkedList<Bitmap>();

        /// <summary>
        /// 이 값으로 저장할수 있는 UNDO,REDO횟수의 최댓값 조절.
        /// 필요시 UNDO와 REDO에 각각 부여해도 무관.
        /// </summary>
        int _maxHistory_ = 20;
        #endregion

        public static class ColorTable
        {

            //c#에 맞게 사용. 색상표는 r언어나 matlab에서 쓰는것 중에 적당히
            public static int Entry_Length = 40;
            public static Color[] Entry =
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
                ///이하 규격외 이미지 대상 테스트용 인덱스.
                ///(20개 이상을 분류한 모델의 출력물을 변환 할 때)
                ///ColorTable의 인덱스 관련된 오류가 있을때 주석 풀고 테스트.
                /*
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
                */
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

        public static void Pythonnet_(string input_path, string output_path ,List<string> imgList)
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
                dynamic func = deeplab.Caclulating(input_path,output_path,imgList);
                func.cal();

            }

            PythonEngine.Shutdown();
        }
        #endregion

        #region Load Image By Path
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
        
        public void Load_()
        {
            // 이미지 리스트 및 경로 초기화
            this.uiFp_Image.Controls.Clear();
            pictureBox1.Image = null;
            imgList = null;

            //pictureBox2 겹치기
            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Location = new Point(0, 0);//thePointRelativeToTheBackImage;

            gray_imglist.Clear();
            rgb_imglist.Clear();

            string[] files = Directory.GetFiles(input_file_path.SelectedPath);

            imgList = files.Where(x => x.IndexOf(".bmp", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();

            for (int index = 0; index < imgList.Count(); index++)
                imgList[index] = imgList[index].Remove(0, input_file_path.SelectedPath.Count());

            for (int i = 0; i < imgList.Count; i++)
            {
                Image img = Image.FromFile(input_file_path.SelectedPath+imgList[i]);

                Panel pPanel = new Panel();
                pPanel.BackColor = Color.Black;
                pPanel.Size = new Size(Constants.Image_width, Constants.Image_height);
                pPanel.Padding = new System.Windows.Forms.Padding(4);

                PictureBox pBox = new PictureBox();
                pBox.BackColor = Color.DimGray;
                pBox.Dock = DockStyle.Fill;
                pBox.SizeMode = PictureBoxSizeMode.Zoom;
                pBox.Image = img.GetThumbnailImage(Constants.Image_width, Constants.Image_height, null, IntPtr.Zero);
                pBox.Click += PBox_Click;
                pBox.Tag = i.ToString();
                pPanel.Controls.Add(pBox);

                this.uiFp_Image.Controls.Add(pPanel);
            }

            if (imgList.Count > 0)
            {
                Panel pnl = this.uiFp_Image.Controls[0] as Panel;
                PictureBox pb = pnl.Controls[0] as PictureBox;
                PBox_Click(pb, null);
            }
        }
        #endregion

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
<<<<<<< Updated upstream

            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///bool a = Color.Black == Color.FromArgb(255, 0, 0, 0);
            /// MessageBox.Show("답은? " + a); // False. 이유는 아래에
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///[참고] Color.Black과  Color.FromArgb(255,0,0,0)의 ARGB값 동일, Name 다름
            ///후자는 #FF000000, 전자는 Black. 따라서 비교할때는 Color.ToArgb()씌워서 비교
            /////////////////////////////////////////////////////////////////////////////////////////////////
          

=======
>>>>>>> Stashed changes
            Color Ret_Color = co_RGB;

            for (int i = 0; i < ColorTable.Entry_Length; i++)
            {

                if (ColorTable.Entry[i].ToArgb().Equals(co_RGB.ToArgb()))
                {
                    Ret_Color = Color.FromArgb(255, i, i, i); //n번째 인덱스의 색상과 일치하면 그 인덱스값이 곧 회색 값
                    break;
                }
            }
           

            if ((Ret_Color.ToArgb().Equals(co_RGB.ToArgb()))&& !(Ret_Color.ToArgb().Equals(Color.Black.ToArgb())))
            {
                MessageBox.Show("ERR: 해당 픽셀의 적절한 인덱스값을 찾지못했습니다."+Convert.ToString(co_RGB.Name));
                Ret_Color = Color.NavajoWhite;
            }

            return Ret_Color;
        }

        private Image Gray2RGB_Click(Image image_)
        {

            Bitmap img2Convert = image_ as Bitmap;
            
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
        
        private Image RGB2Gray_Click(Image image_)
        {
            Bitmap img2Convert = image_ as Bitmap;

            int x, y;


            // Loop through the images pixels to reset color.
            for (x = 0; x < img2Convert.Width; x++)
            {
                for (y = 0; y < img2Convert.Height; y++)
                {
                    ///퍼포먼스 개선방법 1.팔레트만 씌우는방법(불확실) 
                    ///2.getpixel쓰지말고 lockbit해서 메모리접근(공부필요,그러나 유의미) 3.둘 다 적용

                    
                    Color pixelColor = img2Convert.GetPixel(x, y);

                   // MessageBox.Show("받아온 픽셀의 색상" + Convert.ToString(pixelColor));
                    Color newColor_RGB = Swap_RGB2G(pixelColor);
                    if (newColor_RGB== Color.NavajoWhite)
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
            //변환된 이미지를 띄울 창(pictureBox1)에 갱신해주면됨.
        }
        #endregion

        #region Opacity Function
        //pictureBox2 알파값 조정 - 투명도 조절용
        private void SetAlpha(int alpha)
        {
            float a = alpha / (float)trackBar1.Maximum;

            float[][] matrixItems = {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, a, 0},
                new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);


            ImageAtt_pictureBox2.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //using (Graphics g = Graphics.FromImage(bmpOut))
              //  g.DrawImage(bmpNotTransparent, r, r.X, r.Y, r.Width, r.Height, GraphicsUnit.Pixel, imageAtt);


            //TODO: SetAlpha가 ImageAtt만 다루도록 수정 현재 출력까지 같이하고 있음. (Jan.22)
        }

        //트레이스바(투명도 비율 정보) 조정 - 투명도 조절용
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (sourceBitmapRgb != null)
            {
<<<<<<< Updated upstream
                pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value);
=======
                SetAlpha(trackBar1.Value);
                pictureBox2.Refresh();
>>>>>>> Stashed changes
            }
        }
        #endregion



        #region Event Control
        private void 경로설정FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Network_route_settings();
        }

        //좌측 이미지 목록 클릭시 동작
        private void uiFp_Image_Paint(object sender, PaintEventArgs e) {}
        public void PBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.uiFp_Image.Controls.Count; i++)
            {
                if (this.uiFp_Image.Controls[i] is Panel)
                {
                    Panel pnl = this.uiFp_Image.Controls[i] as Panel;
                    pnl.BackColor = Color.Black;
                }
            }

            PictureBox pb = sender as PictureBox;
            pb.Parent.BackColor = Color.Red;

            int idx = Convert.ToInt32(pb.Tag.ToString());

<<<<<<< Updated upstream
            Image img = Image.FromFile(input_file_path.SelectedPath+imgList[idx]);

=======
>>>>>>> Stashed changes
            //선택된 원본 이미지로 변경
            //sourceBitmapOrigin = new Bitmap(Image.FromFile(input_file_path.SelectedPath + imgList[idx]));
            sourceBitmapOrigin = new Bitmap(input_file_path.SelectedPath + imgList[idx]);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            ///TODO: pictureBox1에 이미지 불러올때도 비트맵 하나에 따로 받아서 관리.
            AutoFit(pictureBox1, sourceBitmapOrigin, false);
            //TODO:새로 지정한 비트맵으로 인자 바꿔주기.
            pictureBox1.Refresh(); //새로 이미지를 불러왔으니 갱신.


            //선택된 레이블링 이미지로 변경
            if (rgb_imglist != null && rgb_imglist.Count() != 0)
            {
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                ///<해설> Clone()으로 넣으면 얕은 복사. 두 객체의 저장값이 공유되버림.
                ///따라서 새 비트맵을 생성하여 값만 복사해줌.

                //픽쳐박스에 새로 이미지 올라올때마다
                //stackUndo, stackRedo 비우고,
                stackUndo.Clear();
                stackRedo.Clear();
                //UNDO스택에 이미지 추가해주기.
                stackUndo.AddLast(new Bitmap(sourceBitmapRgb));

                pictureBox2.Refresh();

            }
            //UiTxt_File.Text = this.imgList[idx];
        }

        private void Network_operation_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(Environment.CurrentDirectory);
            if (imgList == null || imgList.Count() == 0)
            {
                MessageBox.Show("불러올 이미지가 없습니다.");
                return;
            }

            if(gray_file_path.SelectedPath == string.Empty)
            {
                MessageBox.Show("그레이 스케일 저장 경로가 없습니다.");
                Network_route_settings();
                return;
            }

<<<<<<< Updated upstream
=======
            if (Constants.isTestmode == true)
            {
                MessageBox.Show("테스트모드로 실행합니다. 모델 구동을 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {
                    gray_imglist.Add(Image.FromFile(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));


                    rgb_imglist.Add(Image.FromFile(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png"));


                    //아예 rgb변환도 생략.
                    //rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image

                    /*테스트용 RGB 이미지 생성할때만 풀면됨
                    rgb_imglist[0].Save(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");
                    MessageBox.Show("rgb테스트용 이미지 저장 완료");
                    */
                }
                sourceBitmapRgb = new Bitmap(rgb_imglist[0]);
                Console.WriteLine("rgb 소스 로딩 확인 리프레시전" + Convert.ToString(sourceBitmapRgb.Equals(pictureBox2.Image)));
                //픽쳐박스에 새로 이미지 올라올때마다
                //stackUndo, stackRedo 비우고,
                stackUndo.Clear();
                stackRedo.Clear();
                //UNDO스택에 이미지 추가해주기.
                stackUndo.AddLast(new Bitmap(sourceBitmapRgb));
                pictureBox2.Refresh();
                Console.WriteLine("rgb 소스 로딩 확인 리프 후" + Convert.ToString(sourceBitmapRgb.Equals(pictureBox2.Image)));

                return;
            }

>>>>>>> Stashed changes
            MessageBox.Show("그레이 스케일 변환 중 입니다.");
            Pythonnet_(input_file_path.SelectedPath,gray_file_path.SelectedPath,imgList);
            MessageBox.Show("그레이 스케일 변환 완료 !");

            //GrayScale 이미지 변수에 저장
            //Gray2RGB 이미지 변수에 저장
            for (int index = 0; index < imgList.Count(); index++) {
                gray_imglist.Add(Image.FromFile(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count()-4,4)+ "_gray_img.png"));
                rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image
            }

            //변환된 이미지 픽쳐박스에 띄우기
            sourceBitmapRgb = new Bitmap(rgb_imglist[0]);


            //픽쳐박스에 새로 이미지 올라올때마다
            //stackUndo, stackRedo 비우고,
            stackUndo.Clear();
            stackRedo.Clear();
            //UNDO스택에 이미지 추가해주기.
            stackUndo.AddLast(new Bitmap(sourceBitmapRgb));
            pictureBox2.Refresh();
        }

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F) Network_route_settings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("t");
        }

        #region <이미지 스크롤 by 마우스 드래그>

        /// <summary>
        /// 타겟이미지의 위치 변화 벡터 = 커서의 위치 변화벡터.
        /// 이동할때마다 이미지를 갱신합니다.
        /// </summary>
        private void Move_srcImg_location()
        {
            if (false == isScroll)
            {
                return;
            }

            //targetImgRect.X += move_endpt.X - move_startpt.X; 
            //targetImgRect.Y += move_endpt.Y - move_startpt.Y;

            targetImgRect.Offset(move_endpt.X - move_startpt.X, move_endpt.Y - move_startpt.Y);

            pictureBox2.Refresh();
        }

        #region 이미지를 픽쳐박스에 맞게 자동으로 조절.

        ////////////////////////////////Base on this Article. 
        ///https://stackoverflow.com/questions/6565703/math-algorithm-fit-image-to-screen-retain-aspect-ratio        ///
        ////////////////////////////////



        /// <summary>
        /// 이미지가 픽쳐박스에 완전히 들어가도록 비율을 조정.
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="srcImg"></param>
        /// <param name="isAlignCenter">이미지의 정렬방식 지정. 비활성화 시 좌상단 정렬.</param>
        public void AutoFit(PictureBox picBox, Bitmap srcImg, bool isAlignCenter)
        {
            /* Fix
             * 1.받아온 속성값으로 zoomScale을 새로 계산해서 저장.
             * 2. 이 계산을 GetScale_AutoFit이 함.
             * 3. 픽쳐박스 갱신은 나중에 한번에 조정
             */

            int W_origin = srcImg.Width;
            int H_origin = srcImg.Height;
            int W_screen = picBox.Width;
            int H_screen = picBox.Height;

            //픽쳐박스의 최소(+최대) 크기가 지정되어있으면 오류상황 x
            zoomScale = GetScale_AutoFit(picBox, sourceBitmapRgb);

            //TODO:아래 두줄 필요한지 아닌지 확실해지면 정리.
            targetImgRect.Width = (int)Math.Round(zoomScale * W_origin);
            targetImgRect.Height = (int)Math.Round(zoomScale * H_origin);

            //이미지 정렬 옵션
            if (true == isAlignCenter)
            {
                //중앙정렬
                targetImgRect.X = (int)((W_screen - targetImgRect.Width) / 2);
                targetImgRect.Y = (int)((H_screen - targetImgRect.Height) / 2);

            }
            else
            {
                //픽쳐박스 좌상단
                targetImgRect.X = 0;
                targetImgRect.Y = 0;
            }

            /////////////////////////////////////////////////////////////////////////////////
            this.ignoreChanges = true;

            //여기서 배율 입출력 인터페이스 갱신: 
            
            //ex) 텍스트박스(넓이/높이/비율)
            /*
            this.widthTextBox.Text = targetImgRect.Width.ToString("0");
            this.heightTextBox.Text = targetImgRect.Height.ToString("0");
            int percent = (int)(zoomScale * 100);
            this.percentTextBox.Text = percent.ToString("0");
            */

            /*
            if (showWidth)
            {
                this.widthTextBox.Text = width.ToString("0");
            }

            if (showHeight)
            {
                this.heightTextBox.Text = height.ToString("0");
            }

            if (showPercent)
            {
                int percent = (int)(scale * 100);

                this.percentTextBox.Text = percent.ToString("0");
            }
            */

            this.ignoreChanges = false;

        }

        #endregion

        /// <summary>
        /// 스케일 변경하고, 배율 입출력 인터페이스 갱신.
        /// </summary>
        /// <param name="scale">스케일</param>
        /// <param name="showWidth">너비 표시 여부</param>
        /// <param name="showHeight">높이 표시 여부</param>
        /// <param name="showPercent">비율 표시 여부</param>
        private void SetImageScale(double scale, bool showWidth, bool showHeight, bool showPercent)
        {
            //1.
            int width = (int)(this.sourceBitmapRgb.Width * scale);
            int height = (int)(this.sourceBitmapRgb.Height * scale);

            if ((width < 1) || (height < 1)) // 값이 너무 작아서 1x1보다 작아진 경우
            {
                return;
            }
            //2. !! 굳이 먼저 저장안하는 이유. 가로세로 1보다 작으면 저장 x.
            zoomScale = scale;


            //3. 출력할떄 항상 소스이미지의 값 * zoomScale을 계산해야 함-> 여기서 안바꿔도 됨.
            /*
            targetImgRect.Width = width;
            targetImgRect.Height = height;
            */

            pictureBox1.Refresh();
            pictureBox2.Refresh();

            ////////////////////////////////////
            this.ignoreChanges = true;

            //여기에 배율 조절 인터페이스 내용 삽입( 트랙바, 텍스트박스 기타 등등)

            /*
            if (showWidth)
            {
                this.widthTextBox.Text = width.ToString("0");
            }

            if (showHeight)
            {
                this.heightTextBox.Text = height.ToString("0");
            }

            if (showPercent)
            {
                int percent = (int)(scale * 100);

                this.percentTextBox.Text = percent.ToString("0");
            }
            */
            this.ignoreChanges = false;
            ////////////////////////////////////
        }


        /// <summary>
        /// pictureBox와 Bitmap 객체의 넓이비 또는 높이비 중 큰쪽을 반환합니다.
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="srcImg"></param>
        /// <returns></returns>
        public static Double GetScale_AutoFit(PictureBox picBox, Bitmap srcImg)
        {
            Double ret_ratio;

            int W_origin = srcImg.Width;
            int H_origin = srcImg.Height;
            int W_screen = picBox.Width;
            int H_screen = picBox.Height;

            ret_ratio = (((double)W_screen / (double)W_origin) < ((double)H_screen / (double)H_origin)) ? ((double)W_screen / (double)W_origin) : ((double)H_screen / (double)H_origin);
            Console.WriteLine("ret_ratio:" + Convert.ToString(ret_ratio) + "/W_screen:" + Convert.ToString((double)W_screen / (double)W_origin) + "H_screen" + Convert.ToString((double)H_screen / (double)H_origin));
            Console.WriteLine("h_origin: " + Convert.ToString(H_origin));
            return ret_ratio;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            ///이 메서드 pictureBox1에서 살짝만 바꿔서 써먹을수 있을듯.
            ///TODO: pictureBox1에 이미지 불러올때도 비트맵 하나에 따로 받아서 관리.

            #region 보간 방식 지정. 

            //scale 1을 기준으로 변경
            if (zoomScale < 1)
            {
                //어느정도 중간값 사용. -> 확대시에 픽셀이 흐릿.
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            }
            else
            {
                //픽셀을 그대로 확대 -> 이미지 축소시에 1pixel짜리 곡선같은건 끊어져보이거나 사라짐.
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            #endregion

            //타겟영역 갱신.
            //e.Graphics.DrawImage(this.sourceBitmap, targetImgRect, ImageAtt_pictureBox2);
            e.Graphics.DrawImage(this.sourceBitmapRgb, targetImgRect, 0, 0, (int)(zoomScale * this.sourceBitmapRgb.Width),(int)(zoomScale * this.sourceBitmapRgb.Height), GraphicsUnit.Pixel, ImageAtt_pictureBox2);
            Console.WriteLine("zoomscale" + Convert.ToString(zoomScale) + "Width" + Convert.ToString(this.sourceBitmapRgb.Width) + "Height"+Convert.ToString(this.sourceBitmapRgb.Height));
            //e.Graphics.DrawImage(this.sourceBitmapRgb, targetImgRect);

        }
        #endregion

        #region <그리기: 선>

        /// <summary>
        /// 브러시의 크기를 변경합니다.
        /// </summary>
        /// <param name="new_size">변경될 브러시 사이즈</param>
        private void SetBrush_Size(int new_size)
        {
            if (0 == new_size)
            {
                return;
            }
            brush_Size = new_size;
            //만약 커서 표시해줄거면 여기서 갱신해줘야됨.

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
        private void DrawShape() //Q: 나중에 인자로 brush& shape 받기?
        {
            if (false == isPaint)
            {
                return;
            }

            Color brush_Color = Color.Black;
            Pen myPen = new Pen(brush_Color, brush_Size);                // myPen

            using (Graphics g = Graphics.FromImage(sourceBitmapRgb)) //sourceBitmap sourceBitmap sourceBitmap 픽처박스에 뜨는부분만 가져와서 그리는게 아니고 전체 맵에서 뜨는걸 가져와야되나?
            {

                DrawFreeLine(myPen, g);
                pictureBox2.Refresh();
            }
        }
        #endregion

        #region UNDO 및 REDO - Method

        /////////////// Undo,Redo 작동방식//////
        /// 
        ///1.1. 제일 최근에 수정한 이미지는 항상 stackUndo에 저장되있고 pictureBox2에도 떠있다.
        ///
        ///2.1. Undo()를 호출하면 stackUndo의 last를 복제해서 stackRedo에 삽입한다.
        ///2.2. 그 다음 stackUndo의 새로운 last을 (pictureBox2)에 띄운다.
        ///
        ///etc. '이미지를 띄운다'고 함은 sourceBitmap(혹은 Original_opac)를 바꿔준 뒤 
        ///     pictureBox2.Refresh()를 통해 pictureBox_Paint 이벤트를 발생시키는 것.
        /// 
        ///3.1. 마우스 이벤트(그리기)를 통해 stackUndo에 스냅샷이 추가 될때, redoStack은 비운다.(저장할 이유가 없다)
        ///
        ///4.1. Redo()를 호출하면 stackRedo의 Last를 복제해서 stackUndo에 삽입한다.
        ///4.2. 그 다음 stackUndo의 새로운 Last를 pictureBox2에 띄운다.
        ///
        ///5.1. 다른썸네일을 클릭하는 순간(이미지를 새로 불러올때)마다 두 저장소를 비워준 뒤 stackUndo에 원본이미지를 복제해서 삽입한다. 
        ///////////////////
        ///

        //1. UNDO 구현
        private void UNDO()
        {
            //되돌리기가 수행가능한 상태인지 확인.
            if (stackUndo.Count <= 1) //UNDO의 Last를 항상 화면에 띄우는 이미지와 같게할 것이므로 최소 1스택.
            {
                return;
            }
            //1.최신 작업내역을 꺼내어 stackRedo에 저장한 뒤   
            stackRedo.AddLast(new Bitmap(stackUndo.Last.Value));
            stackUndo.RemoveLast();

            //ETC.모든 AddLast이후, 저장개수 검사 후 지정한 최댓값을 넘으면 오래된 순으로 제거.
            if (stackRedo.Count > _maxHistory_)
            {
                stackRedo.RemoveFirst();
            }

            //2.stackUndo에서 새로운 Top을 화면에 뿌려줌. = 비트맵 변경후 refresh.

            this.sourceBitmapRgb = new Bitmap(stackUndo.Last.Value);

            pictureBox2.Refresh();

        }

        //2. REDO 구현

        private void REDO()
        {
            //되돌리기가 수행가능한 상태인지 확인.
            if (stackRedo.Count <= 0)
            {
                return;
            }
            //1.최신 Undo내역을 꺼내어 stackUndo에 저장한 뒤   
            stackUndo.AddLast(new Bitmap(stackRedo.Last.Value));
            stackRedo.RemoveLast();
            //ETC.모든 AddLast이후, 저장개수 검사 후 지정한 최댓값을 넘으면 오래된 순으로 제거.
            if (stackUndo.Count > _maxHistory_)
            {
                stackUndo.RemoveFirst();
            }

            //2.stackUndo에서 새로운 Top을 화면에 뿌려줌. = 비트맵 변경후 refresh.
            this.sourceBitmapRgb = new Bitmap(stackUndo.Last.Value);
            pictureBox2.Refresh();

        }

        /// <summary>
        /// 이미지가 수정될 때마다 stackUndo에 복제본 삽입.
        /// </summary>
        /// <param name="src4Input"></param>
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
        #endregion


        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (sourceBitmapRgb == null)
                return;
            switch (cursor_mode)
            {
                case 1: //scroll mode
                    isScroll = true;
                    move_startpt = e.Location;
                    break;

                case 2: //paint mode

                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion

                    //그리기 활성화
                    isPaint = true;

                    //펜 시작점 갱신.
                    pen_startpt.X = (int)((mousePos.X - targetImgRect.X) / zoomScale);
                    pen_startpt.Y = (int)((mousePos.Y - targetImgRect.Y) / zoomScale);

                    ///<해설>
                    ///pen좌표*zoom배율+src의 좌표 = mosPos이니까 우변으로 넘기고 나누면 같아짐.
                    ///= 2배로 보고있을땐 10cm 이동해도 5cm 이동한 셈밖에 안된단 뜻.
                    /// </해설>

                    using (Graphics g = Graphics.FromImage(sourceBitmapRgb))//sourceBitmap sourceBitmap sourceBitmap
                    {
                        Pen blackPen = new Pen(Color.Black, brush_Size);
                        // Create rectangle.
                        Rectangle rect = new Rectangle(pen_startpt.X, pen_startpt.Y, 1, 1);
                        Console.WriteLine("pen_start" + pen_startpt.X.ToString() + " " + pen_startpt.Y.ToString());
                        Brush aBrush = (Brush)Brushes.Black;
                        g.FillRectangle(aBrush, rect);

                        pictureBox2.Refresh();
                    }
                    break;
                default:
                    break;
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (sourceBitmapRgb == null)
                return;
            switch (cursor_mode)
            {
                case 1: //scroll mode
                    move_endpt = e.Location;
                    Move_srcImg_location();
                    move_startpt = move_endpt;
                    break;

                case 2: //paint mode
                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion


                    pen_endpt = new Point((int)((mousePos.X - targetImgRect.X) / zoomScale), (int)((mousePos.Y - targetImgRect.Y) / zoomScale));
                    Console.WriteLine("Move" + pen_startpt.X.ToString() +" "+ pen_startpt.Y.ToString());
                    DrawShape();
                    pen_startpt = pen_endpt;

                    break;
                default:
                    break;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (sourceBitmapRgb == null)
                return;
            switch (cursor_mode)
            {
                case 1: //scroll mode

                    isScroll = false;
                    break;

                case 2: //paint mode

                    #region <sender 캐스팅 & 좌표지정>

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;

                    #endregion

                    //그리기 비활성화.
                    isPaint = false;

                    /////////////////////////////////////
<<<<<<< Updated upstream
                    ///TODO: mouse_Down-> Move -> Up까지 한 번 그린 분량의 이미지를 clone하여 stack에 저장.

=======
                    ///TODO: mouse_Down-> Move -> Up까지 한 번 그린 분량의 그래픽+원본 비트맵을 복사하여 stackUndo에 저장.
                    ///(clone()말고 값으로 복사하기 -> new bitmap()
                    ///그리고 그리기 액션이 있을대마다 redoStack.Clear()해주기.
>>>>>>> Stashed changes
                    ////////////////////////////////////

                    Input_Action(sourceBitmapRgb);

                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
