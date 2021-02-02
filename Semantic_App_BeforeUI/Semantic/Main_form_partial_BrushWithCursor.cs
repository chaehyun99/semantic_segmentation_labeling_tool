using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
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
}
