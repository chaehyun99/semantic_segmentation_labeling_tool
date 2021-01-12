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

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        Image mOriginal;

        public Form1()
        {
            InitializeComponent();

            
        }

        //이미지 로드
        private void Image_Load_Click(object sender, EventArgs e)
        {
            //창 띄워서 파일 불러오기
            //pictureBox1.Load(@"C:\Users\KHS\Desktop\현수\증명사진_권현수.jpg");
            
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

        //이미지 저장

        public static FolderBrowserDialog GetSaveFolderDialog()
        {
            FolderBrowserDialog saveFolderDig = new FolderBrowserDialog();
            saveFolderDig.RootFolder = Environment.SpecialFolder.Desktop;

            return saveFolderDig;
        }

        private void Image_Save_Click(Object sender, EventArgs e)
        {
            using(FolderBrowserDialog folderDiag = GetSaveFolderDialog())
            {
                if(folderDiag.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string dir = folderDiag.SelectedPath;

                    Image image = pictureBox1.Image;

                    string fileName = "test.jpg".ToString();

                    if(image != null)
                    {
                        string imageSavePath = string.Format(@"{0}\{1}", dir, fileName);
                        image.Save(imageSavePath);
                    }

                    MessageBox.Show("저장 완료");

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        List<string> imgList = null;

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                UiTxt_Folder.Text = fbd.SelectedPath;
            }

            this.uiFp_Image.Controls.Clear();

            string[] files = Directory.GetFiles(fbd.SelectedPath);
            imgList = files.Where(x => x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();

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
            
            
        }

        public void PBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.uiFp_Image.Controls.Count; i++)
            {
                if(this.uiFp_Image.Controls[i] is Panel)
                {
                    Panel pnl = this.uiFp_Image.Controls[i] as Panel;
                    pnl.BackColor = Color.Black;
                }
            }

            PictureBox pb = sender as PictureBox;
            pb.Parent.BackColor = Color.Red;

            int idx = Convert.ToInt32(pb.Tag.ToString());

            Image img = Image.FromFile(imgList[idx]);
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            UiTxt_File.Text = this.imgList[idx];
        }

        private void uiFp_Image_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
