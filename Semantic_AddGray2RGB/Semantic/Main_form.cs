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


namespace Semantic
{

    public partial class Main_form : Form
    {
        static class Constants
        {
            public const int Image_width = 300;
            public const int Image_height = 150;
        }

        //Image mOriginal;
        List<string> imgList = null;
        public static bool whether_to_save_ = false;
        public static FolderBrowserDialog input_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog gray_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog rgb_file_path = new FolderBrowserDialog();

        public Main_form()
        {
            this.KeyPreview = true;
            InitializeComponent();
            /*
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();

            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.CreateNoWindow = true;
            cmd.UseShellExecute = false;

            cmd.RedirectStandardOutput = true;
            cmd.RedirectStandardInput = true;
            cmd.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();

            process.StandardInput.Write(@"cd" + Environment.CurrentDirectory + Environment.NewLine);
            process.StandardInput.Write(@"C:\USERBASE\Application\Application.exe" + Environment.NewLine);
             
            //MessageBox.Show(resultValue);
            process.StandardInput.Close();
            string resultValue = process.StandardOutput.ReadToEnd();
            //MessageBox.Show(resultValue);
            process.WaitForExit();
            process.Close();
            */
            
        }
        //public static void AddEnvPath(ref string a, params string[] paths)
        public static void AddEnvPath(params string[] paths)
        {
            // PC에 설정되어 있는  환경 변수를 가져온다.
            var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
           
            // 중복 환경 변수가 없으면 list에 넣는다.
            envPaths.InsertRange(0, paths.Where(x => x.Length > 0 && !envPaths.Contains(x)).ToArray());
            // 환경 변수를 다시 설정한다.
            Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
        }

        /*public static void Wherepython()
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();

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

            process.StandardInput.Write("python" + Environment.NewLine);
            process.StandardInput.Write("import sys" + Environment.NewLine);
            process.StandardInput.Write("sys.executable" + Environment.NewLine);

            //MessageBox.Show(resultValue);
            process.StandardInput.Close();
            string dd = process.StandardOutput.ReadToEnd();
            Console.WriteLine("resultv is : "+dd);
            Console.WriteLine(" ddddd");
            process.WaitForExit();
            process.Close();

        }*/

        public static void Pythonnet_(string input_path, string output_path ,List<string> imgList)
        {
            // python 설치 경로
            var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"\anaconda\");
            //var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\\Users\\연구실\\AppData\\Local\\Programs\\Python\\Python38\\");
            // 환경 변수 설정
            //Wherepython();
            AddEnvPath(PYTHON_HOME, Path.Combine(PYTHON_HOME, @"envs\deeplab\Library\bin"));
            //Console.WriteLine("sss : "+PYTHON_HOME);
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
                //for (int index = 0; index < imgList.Count();index++) {
                    dynamic func = deeplab.Caclulating(input_path,output_path,imgList);
                    func.cal();
                //}

            }

            PythonEngine.Shutdown();
        }

        public void Network_route_settings()
        {

            Network_route_settings setting_Form = new Network_route_settings();
            //setting_Form.Passvalue = textBox1.Text;

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

            string[] files = Directory.GetFiles(input_file_path.SelectedPath);

            imgList = files.Where(x => x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();
            
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
                //UiTxt_File.Text = this.imgList[0];
                PBox_Click(pb, null);
            }


        }
        //기존 이미지 불러오기 이벤트
        /*
        private void Image_Load_Click(object sender, EventArgs e)
        {
            
            string image_file = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            var dialogResult = dialog.ShowDialog();

            if(dialogResult == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }
            else if(dialogResult == DialogResult.Cancel)
            {
                return;
            }
            
            pictureBox1.Image = Bitmap.FromFile(image_file);
            mOriginal = pictureBox1.Image;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        */
        //이미지 저장

        public static FolderBrowserDialog GetSaveFolderDialog()
        {
            FolderBrowserDialog saveFolderDig = new FolderBrowserDialog();
            saveFolderDig.RootFolder = Environment.SpecialFolder.Desktop;

            return saveFolderDig;
        }

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
                //이하 규격외 이미지 대상 테스트 전용.
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
       
        public Color Swap_G2RGB(Color co_Gray)
        {
            byte value2Swap = co_Gray.R; //R이든 G든 B든 상관x.

            Color Ret_Color = ColorTable.Entry[value2Swap];

            return Ret_Color;
        }

        public Color Swap_RGB2G(Color co_RGB)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///bool a = Color.Black == Color.FromArgb(255, 0, 0, 0);
            /// MessageBox.Show("답은? " + a); // False. 이유는 아래에
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///[참고][중요] Color.Black과  Color.FromArgb(255,0,0,0)의 ARGB값 동일, Name 다름
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
           

            if ((Ret_Color.ToArgb().Equals(co_RGB.ToArgb()))&& !(Ret_Color.ToArgb().Equals(Color.Black.ToArgb())))
            {
                MessageBox.Show("ERR: 해당 픽셀의 적절한 인덱스값을 찾지못했습니다."+Convert.ToString(co_RGB.Name));
                Ret_Color = Color.NavajoWhite;
            }

            return Ret_Color;
        }

        private void Gray2RGB_Click(Object sender, EventArgs e)
        {

            Bitmap img2Convert = pictureBox1.Image as Bitmap;
            
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
            pictureBox1.Image = img2Convert;
            //변환된 이미지를 띄울 창(pictureBox1)에 갱신해주면됨.

        }
        private void RGB2Gray_Click(object sender, EventArgs e)
        {
            Bitmap img2Convert = pictureBox1.Image as Bitmap;

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
            pictureBox1.Image = img2Convert;
            //변환된 이미지를 띄울 창(pictureBox1)에 갱신해주면됨.

        }

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

            Image img = Image.FromFile(input_file_path.SelectedPath+imgList[idx]);
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //UiTxt_File.Text = this.imgList[idx];
        }


        private void uiFp_Image_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 경로설정FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Network_route_settings();

            
        }

        private void Network_operation_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(Environment.CurrentDirectory);
            if (imgList == null)
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
            MessageBox.Show("그레이 스케일 변환 중 입니다.");
            Pythonnet_(input_file_path.SelectedPath,gray_file_path.SelectedPath,imgList);
            MessageBox.Show("그레이 스케일 변환 완료 !");
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

        }

       
    }

}
