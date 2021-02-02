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
#pragma warning disable CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Collections.Generic;
#pragma warning restore CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
#pragma warning disable CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Linq;
#pragma warning restore CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
#pragma warning disable CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Text;
#pragma warning restore CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;//솔루션에서 PresentationCore.dll 참조추가 -JSW
#pragma warning disable CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Diagnostics;
#pragma warning restore CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System.Text.RegularExpressions;

namespace Semantic
{
    public partial class Main_form : Form
    {
        #region Field        

        List<string> imgList = null;
        List<Bitmap> gray_imglist = new List<Bitmap>();
        List<Bitmap> rgb_imglist = new List<Bitmap>();
        private Bitmap sourceBitmapRgb = null;
        private Bitmap sourceBitmapOrigin = null;

        public static bool whether_to_save_ = false;
        public static FolderBrowserDialog input_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog gray_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog rgb_file_path = new FolderBrowserDialog();

        private Rectangle targetImgRect = new Rectangle(0, 0, 100, 100);
        private int cursor_mode = 2, brush_Size = 1;
        private bool isScroll = false, isPaint = false;
        private Point move_startpt, move_endpt, pen_startpt, pen_endpt;
        private double zoomScale = 1.0f;
        private int zoomLevel = 0;


        enum zoomMode
        {
            Center,
            Cursor,
            TopLeft
        }

        bool ctrlKeyDown;

        bool isOnPicBox3 = false;
        private Bitmap cursorBoardBitmap = null;
        private Color brush_Color = Color.Black;

        private ImageAttributes imageAtt = new ImageAttributes();

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
            ////Console.WriteLine("loading");
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
            ////Console.WriteLine(result);

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
            ////Console.WriteLine("index확인: " + Convert.ToString(value2Swap));
            Color Ret_Color = ColorTable.Entry[value2Swap];
            return Ret_Color;
        }

        public Color Swap_RGB2G(Color co_RGB)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///bool a = Color.Black == Color.FromArgb(255, 0, 0, 0);
            /// MessageBox.Show("답은? " + a); // False. 이유는 아래에
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///[참고] Color.Black과  Color.FromArgb(255,0,0,0)의 ARGB값 동일, Name 다름
            ///후자는 #FF000000, 전자는 Black. 따라서 비교할때는 Color.ToArgb()씌워서 비교
            /////////////////////////////////////////////////////////////////////////////////////////////////


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

        private Bitmap Gray2RGB_Click(Bitmap image_)
        {

            Bitmap img2Convert = new Bitmap(image_);

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
            //변환된 이미지를 띄울 창(pictureBox1)에 갱신해주면됨.

        }

        private Bitmap RGB2Gray_Click(Bitmap image_)
        {
            Bitmap img2Convert = new Bitmap(image_);

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
                    ///퍼포먼스 개선방법 1.팔레트만 씌우는방법(불확실) 
                    ///2.getpixel쓰지말고 lockbit해서 메모리접근(공부필요,그러나 유의미) 3.둘 다 적용


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

            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            return;
        }

        //트레이스바(투명도 비율 정보) 조정 - 투명도 조절용
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (sourceBitmapOrigin != null)
            {
                //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value); //TODO: SetAlpha개선하고 picturebox2.refresh()넣어주기.
                SetAlpha(trackBar1.Value);
                RefreshPictureBoxes();

            }
        }
        #endregion

        #region zoom in/out
        public static Double GetScale_fitImg2PicBox(PictureBox picBox, Bitmap srcImg)
        {
            Double ret_ratio;

            int W_origin = srcImg.Width;
            int H_origin = srcImg.Height;
            int W_screen = picBox.Width;
            int H_screen = picBox.Height;

            ret_ratio = (((double)W_screen / (double)W_origin) < ((double)H_screen / (double)H_origin)) ? ((double)W_screen / (double)W_origin) : ((double)H_screen / (double)H_origin);
            return ret_ratio;
        }

        private void SetScale(double scale) // 
        {
            //TODO:(구현에 따라 조건문 추가)
            if (0 == scale)
            {
                return;
            }
            zoomScale = scale;
            RefreshPictureBoxes();
            //lable_ImgScale.Refresh();

            /*
             * 배율 조정하는 인터페이스(트랙바, 텍스트박스 등)의 변동이 아래에 들어가면 됨.
            this.ignoreChanges = true;
            this.ignoreChanges = false;
            */
        }
        
        private void SetScale2(double scale, bool showWidth, bool showHeight, bool showPercent)
        {
            //1.
            int width = (int)Math.Round(this.sourceBitmapOrigin.Width * scale);
            int height = (int)Math.Round(this.sourceBitmapOrigin.Height * scale);

            if ((0 == scale)|(width < 1) || (height < 1)) // 값이 너무 작아서 1x1보다 작아진 경우
            {
                return;
            }
            //2.
            zoomScale = scale;
            //3.
            RefreshPictureBoxes();
            //
            //this.ignoreChanges = true;

            if (showWidth)
            {
                //this.widthTextBox.Text = width.ToString("0");
            }

            if (showHeight)
            {
                //this.heightTextBox.Text = height.ToString("0");
            }

            if (showPercent)
            {
                int percent = (int)Math.Round(scale * 100);

                this.percenttextBox.Text = percent.ToString("0");
            }

            //this.ignoreChanges = false;
        }
        
        #endregion

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

            targetImgRect.X += move_endpt.X - move_startpt.X;
            targetImgRect.Y += move_endpt.Y - move_startpt.Y;

            //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
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
        private void DrawShape() //Q: 나중에 인자로 brush& shape 받기?
        {
            if (false == isPaint)
            {
                return;
            }

            Color brush_Color = this.brush_Color;
            Pen myPen = new Pen(brush_Color, brush_Size);                // myPen

            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            using (Graphics g = Graphics.FromImage(sourceBitmapRgb)) //sourceBitmap sourceBitmap sourceBitmap 픽처박스에 뜨는부분만 가져와서 그리는게 아니고 전체 맵에서 뜨는걸 가져와야되나?
            {

                DrawFreeLine(myPen, g);
                //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value);
                
                pictureBox2.Refresh();
                pictureBox3.Refresh();
            }
        }

        private void SetBrushSize(int newbrushSize)
        {
            brush_Size = newbrushSize;

            //만약 브러시모양을 표시해줄거면 여기서 갱신
        }
        #endregion

        #region 2021-02-02 커서따라다니는 브러시 & 마우스휠 Zoom 작업

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////메서드
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

                //Console.WriteLine("브러시 pen_startpt 좌표: " + Convert.ToString(e.Location));

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
            this.pictureBox3.MouseDown += pictureBox2_MouseDown;
            this.pictureBox3.MouseUp += pictureBox2_MouseUp;

            //호출순서 변경(정석은 아닌듯)
            this.pictureBox3.MouseMove -= pictureBox3_MouseMove;
            this.pictureBox3.MouseMove += pictureBox2_MouseMove;
            this.pictureBox3.MouseMove += pictureBox3_MouseMove;

            this.pictureBox3.MouseWheel += pictureBox1_MouseWheel;
        }

        public void Load_()
        {
            // 이미지 리스트 및 경로 초기화
            this.uiPanelThumbnail.Controls.Clear();
            pictureBox1.Image = null;
            imgList = null;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
            //thePointRelativeToTheBackImage;
            pictureBox2.Location = new Point(0, 0);

            //Console.WriteLine("pbox2위치;" + Convert.ToString(pictureBox2.Location));

            //커서그려줄 패널 겹치기

            pictureBox3.Parent = pictureBox2;
            pictureBox3.BackColor = Color.Transparent;
            //thePointRelativeToTheBackImage;
            pictureBox3.Location = new Point(0, 0);
            //Console.WriteLine("pbox3위치;" + Convert.ToString(pictureBox3.Location));

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

                this.uiPanelThumbnail.Controls.Add(panelThumb);
            }



            if (imgList.Count <= 0)
            {
                return;
            }
            else
            {
                Panel pnl = this.uiPanelThumbnail.Controls[0] as Panel;
                PictureBox pb = pnl.Controls[0] as PictureBox;
                PBoxThumbnail_Click(pb, null);
            }

            //if (null == rgb_imglist || 0 == rgb_imglist.Count()){}
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////이벤트
        ///


        private void pictureBox3_Paint(object sender, PaintEventArgs e)
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
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            }
            else
            {
                //픽셀을 그대로 확대 -> 이미지 축소시에 1pixel짜리 곡선같은건 끊어져보이거나 사라짐.
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.

            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            /*
            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///이미지가 픽쳐박스 탈출하는 것 방지
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (targetImgRect.X > 0) targetImgRect.X = 0;
            if (targetImgRect.Y > 0) targetImgRect.Y = 0;
            if (targetImgRect.X + targetImgRect.Width < pictureBox1.Width) targetImgRect.X = pictureBox1.Width - targetImgRect.Width;
            if (targetImgRect.Y + targetImgRect.Height < pictureBox1.Height) targetImgRect.Y = pictureBox1.Height - targetImgRect.Height;
            ///////////////////////////////////////////////
            */

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

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            //Console.WriteLine("커서표시영역 진입");

            //Console.WriteLine("커서모드" + Convert.ToString(cursor_mode));
            Console.WriteLine(
                "커서비트맵:"
                + ((null == cursorBoardBitmap) ? "No" : "Yes")
                );

            //화면에 띄울 커서의 종류, 픽쳐박스에서 따라다닐 브러시의 표시유무(isOnpicBox3).
            //둘 다 isPaint, isScroll, cursor_mode에 따라 변경.

            isOnPicBox3 = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            //Console.WriteLine("커서표시영역 탈출");

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

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            //picBox3.MouseMove 의 delegate로 picBox2_MouseMove를 줘도 되고,
            //아예 여기서 picBox2_MouseMove를 발생시켜도 됨.

            if ((2 != cursor_mode) || (null == cursorBoardBitmap))
            {
                return;
            }
            DrawCursor(pictureBox3, e);
        }


        public void PBoxThumbnail_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.uiPanelThumbnail.Controls.Count; i++)
            {
                if (this.uiPanelThumbnail.Controls[i] is Panel)
                {
                    Panel pnl = this.uiPanelThumbnail.Controls[i] as Panel;
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
                //Console.WriteLine("rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                return;
            }
            else
            {
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                //커서가 그려질 보드의 비트맵 갱신(크기)
                cursorBoardBitmap = new Bitmap(sourceBitmapRgb.Width, sourceBitmapRgb.Height);
                cursorBoardBitmap.MakeTransparent();

                SetAlpha(trackBar1.Value);

                RefreshPictureBoxes();

                /*
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
                */
            }

            RefreshPictureBoxes();
            //UiTxt_File.Text = this.imgList[idx];
        }

        #endregion

        private void RefreshPictureBoxes()
        {
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
        }

        #region Event Control
        private void 경로설정FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Network_route_settings();
        }

        #region 좌측 썸네일 동작
      
        #endregion

        private void Network_operation_Click(object sender, EventArgs e)
        {
            ////Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            ////Console.WriteLine(Environment.CurrentDirectory);
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

            if (Constants.isTestmode == true)
            {
                MessageBox.Show("테스트모드로 실행합니다. 모델 구동을 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {
                    gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));


                    rgb_imglist.Add(new Bitmap(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png"));

                }

                //sourceBitmapOrigin = (Bitmap)rgb_imglist[0].Clone();
                //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value); //TODO: SetAlpha를 바꾸고 pictureBox2.refresh()넣기
                //SetScale2(GetScale_fitImg2PicBox(pictureBox2, (Bitmap)sourceBitmapOrigin), true, true, true);
                RefreshPictureBoxes();

                return;
            }

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


            //pictureBox2.Image = rgb_imglist[0];
            //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            sourceBitmapOrigin = (Bitmap)rgb_imglist[0].Clone();
            //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value);
            SetScale2(GetScale_fitImg2PicBox(pictureBox2, (Bitmap)sourceBitmapOrigin), true, true, true);
            RefreshPictureBoxes();

        }

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F) 
                Network_route_settings();
            else if (e.KeyCode == Keys.M)
            {
                if (cursor_mode == 1)
                {
                    cursor_mode = 2;
                    //Console.WriteLine("cursor mode set draw");
                }
                else
                {
                    cursor_mode = 1;
                    //Console.WriteLine("cursor mode set move");
                }
            }
        }

        //이미지 저장 버튼.
        private void button2_Click(object sender, EventArgs e)
        {

        }


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

            RefreshPictureBoxes();
           // lable_ImgScale.Refresh();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("t");
        }

        #region picturebox paint option. call by picturebox.refresh()

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
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            }
            else
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.
            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///이미지가 픽쳐박스 탈출하는 것 방지.
            ////////////////////////////////////////////////////////////////////////////////////////////////

            //적용하는 순서에 따라 zoomOut시에 어느 모서리로 붙을지 결정.
            //중앙에 띄우고싶으면..?

            if (targetImgRect.X > 0) targetImgRect.X = 0;
            if (targetImgRect.Y > 0) targetImgRect.Y = 0;
            if (targetImgRect.X + targetImgRect.Width < pictureBox1.Width) targetImgRect.X = pictureBox1.Width - targetImgRect.Width;
            if (targetImgRect.Y + targetImgRect.Height < pictureBox1.Height) targetImgRect.Y = pictureBox1.Height - targetImgRect.Height;

            ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

            if (null == rgb_imglist || 0 == rgb_imglist.Count || null == sourceBitmapRgb)
            {
                return;
            }

            // 보간 방식 지정. 
            if (zoomScale < 1)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            }
            else
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.

            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            /* draw 영역 조절은 한번만

            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///이미지가 픽쳐박스 탈출하는 것 방지
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (targetImgRect.X > 0) targetImgRect.X = 0;
            if (targetImgRect.Y > 0) targetImgRect.Y = 0;
            if (targetImgRect.X + targetImgRect.Width < pictureBox1.Width) targetImgRect.X = pictureBox1.Width - targetImgRect.Width;
            if (targetImgRect.Y + targetImgRect.Height < pictureBox1.Height) targetImgRect.Y = pictureBox1.Height - targetImgRect.Height;
            ///////////////////////////////////////////////
            */
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
        #endregion

        #region picturebox Mouse Event(For Draw)
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

                    ///<해설>
                    ///pen좌표*zoom배율+src의 좌표 = mosPos이니까 우변으로 넘기고 나누면 같아짐.
                    ///= 2배로 보고있을땐 10cm 이동해도 5cm 이동한 셈밖에 안된단 뜻.
                    /// </해설>

                    using (Graphics g = Graphics.FromImage(sourceBitmapOrigin))//sourceBitmap sourceBitmap sourceBitmap
                    {
                        Pen blackPen = new Pen(Color.Black, brush_Size);
                        // Create rectangle.
                        Rectangle rect = new Rectangle(pen_startpt.X, pen_startpt.Y, 1, 1);
                        //Console.WriteLine("pen_start" + pen_startpt.X.ToString() + " " + pen_startpt.Y.ToString());
                        Brush aBrush = (Brush)Brushes.Black;
                        g.FillRectangle(aBrush, rect);

                        //pictureBox2.Image = SetAlpha((Bitmap)original_opac, trackBar1.Value);
                        RefreshPictureBoxes();

                    }
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
                    Move_srcImg_location();
                    move_startpt = move_endpt;
                    break;

                case 2: //paint mode
                    if (sourceBitmapOrigin == null)
                        return;
                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion


                    pen_endpt = new Point((int)Math.Round((mousePos.X - targetImgRect.X) / zoomScale), (int)Math.Round((mousePos.Y - targetImgRect.Y) / zoomScale));
                    ////Console.WriteLine("Move" + pen_startpt.X.ToString() + " " + pen_startpt.Y.ToString());
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
                    if (sourceBitmapOrigin == null)
                        return;
                    #region <sender 캐스팅 & 좌표지정>

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;

                    #endregion

                    //그리기 비활성화.
                    isPaint = false;

                    /////////////////////////////////////
                    ///TODO: mouse_Down-> Move -> Up까지 한 번 그린 분량의 이미지를 clone하여 stackUndo에 저장.
                    ///그리고 그리기 액션이 있을대마다 redoStack.Clear()해주기.
                    ////////////////////////////////////


                    break;
                default:
                    break;
            }
        }
        #endregion

        #region zoom control
        private void percenttextBox_TextChanged(object sender, EventArgs e)
        {
            //if (this.ignoreChanges)
            //{
            //    return;
            //}

            double percent;

            if (double.TryParse(this.percenttextBox.Text, out percent))
            {
                SetScale2(percent / 100, true, true, false);
            }
        }
        private void btn_zoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel++;
            SetScale2(Math.Pow(Constants.ratioPerLevel, zoomLevel), true, true, true); //윈도우 그림판은 첫번째 인자가 2로 잡혀있는 셈임( 25%/ 50%/ 100%/ 200%/ 400%)
        }
        private void btn_zoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel--;
            SetScale2(Math.Pow(Constants.ratioPerLevel, zoomLevel), true, true, true);
        }
        private void btn_zoomReset_Click(object sender, EventArgs e)
        {
            //최초 위치로 되돌림.
            targetImgRect.X = 0;
            targetImgRect.Y = 0;

            zoomLevel = 0;
            SetScale2(Math.Pow(Constants.ratioPerLevel, zoomLevel), true, true, true);
        }

        #endregion

        #region brush control
        private void btn_brushSizeUp_Click(object sender, EventArgs e)
        {
            if (brush_Size == Constants.Max_brush_Size)
            {
                return;
            }
            SetBrushSize(brush_Size + 1);
        }

        private void btn_brushSizeDown_Click(object sender, EventArgs e)
        {
            if (brush_Size == 1)
            {
                return;
            }
            SetBrushSize(brush_Size - 1);
        }
        #endregion

        #endregion

    }

    static class Constants
    {
        public const int Thumbnail_Width = 300;
        public const int Thumbnail_Height = 150;

        public const double ratioPerLevel = 1.5;
        public const int Max_brush_Size = 10;

        public const bool isTestmode = true;
        ///모델구동, rgb변환없이 작업 시작할 때 키고, 
        ///모델구동, rgb변환해야되거나 공식적으로 올릴땐 false
    }

}