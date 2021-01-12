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

        public Color Grey2RGB(Color co)
        {
            int a = co.A;
            int r = co.R;
            int g = co.G;
            int b = co.B;


            //grey를 rgb로 어떻게 수정할 것인지 if문으로 하나하나 정하기
            a = 0;
            r = r ^ 255;
            g = g ^ 255;
            b = b ^ 255;


            co = Color.FromArgb(a, r, g, b);
            return co;
        }

        //기존 컬러 변환 버튼 이벤트
        /*
        private void RGB(Object sender, EventArgs e)
        {
            Color co;
            Color ret_co;

            Bitmap temp = mOriginal as Bitmap;
            

            for (int x = 0; x < temp.Width; x++)
            {
                for (int y = 0; y < temp.Height; y++)
                {
                    co = temp.GetPixel(x, y);

                    ret_co = Grey2RGB(co);


                    temp.SetPixel(x, y, ret_co);     // 해당 좌표 픽셀의 컬러값을 변경
                }
            }
            pictureBox1.Image = temp;

            MessageBox.Show("색상 반전 완료");
        }
        */



        /*
        private void button4_Click(object sender, EventArgs e)
        {
            
            if(input_file_path.ShowDialog() == DialogResult.OK)
            {
                UiTxt_Folder.Text = input_file_path.SelectedPath;
            }

           // this.uiFp_Image.Controls.Clear();

            //string[] files = Directory.GetFiles(input_file_path.SelectedPath);
            
            /*imgList = files.Where(x => x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();

            for (int i = 0; i< imgList.Count; i++)
            {
                Image img = Image.FromFile(imgList[i]);

                Panel pPanel = new Panel();
                pPanel.BackColor = Color.Black;
                pPanel.Size = new Size(150, 150);
                pPanel.Padding = new System.Windows.Forms.Padding(4);

                PictureBox pBox = new PictureBox();
                pBox.BackColor = Color.DimGray;
                pBox.Dock = DockStyle.Fill;
                pBox.SizeMode = PictureBoxSizeMode.Zoom;
                pBox.Image = img.GetThumbnailImage(150, 150, null, IntPtr.Zero);
                pBox.Click += PBox_Click;
                pBox.Tag = i.ToString();
                pPanel.Controls.Add(pBox);

                this.uiFp_Image.Controls.Add(pPanel);
            }

            if(imgList.Count > 0)
            {
                Panel pnl = this.uiFp_Image.Controls[0] as Panel;
                PictureBox pb = pnl.Controls[0] as PictureBox;
                UiTxt_File.Text = this.imgList[0];
                PBox_Click(pb, null);
            }
            
            
        }*/

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
    }

}
