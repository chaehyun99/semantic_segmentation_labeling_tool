using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TestProject
{
    /// <summary>
    /// 메인 폼
    /// </summary>
    public partial class MainForm : Form
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region Field

        /// <summary>
        /// 픽쳐박스위에서 소스 이미지가 실제로 표현될 영역.
        /// </summary>
        Rectangle targetImgRect = new Rectangle(0,0,1,1);

        /// <summary>
        /// 마우스의 이벤트를 지정하는 값.
        /// (이동모드 : 1)         
        /// (그리기모드 : 2)         
        /// </summary>
        private int cursor_mode = 2;

        /// <summary>
        /// 말그대로 펜?브러시?의 굵기, 여러가지 방식으로 지정할 수 있다.
        /// </summary>
        private int brush_Size = 1;

        /// <summary>
        /// 비트맵 수정: 시작점
        /// </summary>
        private Point pen_startpt;

        /// <summary>
        /// 비트맵 수정: 끝점
        /// </summary>
        private Point pen_endpt;

        /// <summary>
        /// 그래픽 그리기가 가능 여부
        /// </summary>
        private bool isPaint = false;

        /// <summary>
        /// 소스 비트맵
        /// </summary>
        private Bitmap sourceBitmap = null;

        /// <summary>
        /// 변경 무시 여부
        /// </summary>
        private bool ignoreChanges = false;

        /// <summary>
        /// 이미지 이동을 위한 벡터의 시작점
        /// </summary>
        private Point move_startpt;

        /// <summary>
        /// 이미지 이동을 위한 벡터의 끝점
        /// </summary>
        private Point move_endpt;

        /// <summary>
        /// 스크롤가능 여부
        /// </summary>
        private bool isScroll = false;

        /// <summary>
        /// 이미지 배율
        /// </summary>
        private double zoomScale;
        //private double zoomPercent;

        /// <summary>
        /// 화면에 표시할 소스 이미지의 영역
        /// </summary>
        private Point[] srcImagePointArray;

        /// <summary>
        /// 브러시의 컬러
        /// </summary>
        private Color brush_Color = Color.Black;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - MainForm()

        /// <summary>
        /// 생성자
        /// </summary>
        public MainForm()
        {
            InitializeComponent();


            #region 이벤트를 설정한다. -merge할때, openMenuItem, saveAsMenuItem 필요없음.


            this.openMenuItem.Click         += openMenuItem_Click;
            this.saveAsMenuItem.Click       += saveAsMenuItem_Click;
            this.widthTextBox.TextChanged   += widthTextBox_TextChanged;
            this.heightTextBox.TextChanged  += heightTextBox_TextChanged;
            this.percentTextBox.TextChanged += percentTextBox_TextChanged;



            #endregion
        }



        #endregion



        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Private
        //////////////////////////////////////////////////////////////////////////////// Event

        #region <마우스 이벤트>

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1: //scroll mode
                    isScroll = true;
                    move_startpt = e.Location;
                    break;

                case 2: //paint mode

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;

                    //그리기 활성화
                    isPaint = true;

                    //펜 시작점 갱신.
                    pen_startpt.X = (int)((mousePos.X - targetImgRect.X) / zoomScale); 
                    pen_startpt.Y = (int)((mousePos.Y - targetImgRect.Y) / zoomScale);

                    ///<해설>
                    ///pen좌표*zoom배율+targetRect좌표 = mosPos이니까 우변으로 넘기고 나누면 같아짐.
                    ///= 2배로 보고있을땐 10cm 이동해도 5cm 이동한 셈밖에 안된단 뜻.
                    ///</해설>


                    #region 클릭만 했을때도 점(원) 그려짐.
                    using (Pen myPen = new Pen(brush_Color, 1))
                    using (Graphics g = Graphics.FromImage(sourceBitmap))
                    {
                        Brush aBrush = new SolidBrush(brush_Color); //TODO: Yellow 대신에 brush_Color로.

                        /* //Create Circle */
                        Rectangle rectDot = new Rectangle(pen_startpt.X - brush_Size / 2, pen_startpt.Y - brush_Size / 2, brush_Size, brush_Size);

                        g.DrawEllipse(myPen, rectDot);
                        g.FillEllipse(aBrush, rectDot);

                        //Create rectangle.
                       // g.FillRectangle(Brushes.Black, rect_dot);


                        //g.DrawRectangle(aBrush, )

                        pictureBox2.Refresh();
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

                    #region <sender 캐스팅 & 좌표지정>

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;

                    #endregion

                    //그리기 비활성화.
                    isPaint = false;

                    /////////////////////////////////////
                    ///TODO: mouse_Down-> Move -> Up까지 한 번 그린 분량의 이미지를 clone하여 stack에 저장.                    
                    ////////////////////////////////////
                    ///
                    //<UNDO_STACK_NAME>.add((Image)this.sourceBitmap.Clone());

                    break;
                default:
                    break;
            }
        }
        #endregion


        #region <Group: 브러시 크기 조정>

        /// <summary>
        /// 브러시의 크기를 사전에 설정된 값 중에서 지정합니다.
        /// </summary>
        /// <param name="new_size">변경될 브러시 사이즈</param>
        private void SetBrush_Size(int new_size)
        {
            brush_Size = new_size;
            return;
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

            targetImgRect.Offset(move_endpt.X - move_startpt.X, move_endpt.Y - move_startpt.Y);

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
            //펜선이 꺾일때 끊기지 않고 그려지게 함. 기본설정은 LineCap.Flat임.
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

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

            Pen myPen = new Pen(brush_Color, brush_Size);                // myPen

            using (Graphics g = Graphics.FromImage(sourceBitmap)) //픽처박스에 뜨는부분만 가져와서 그리는게 아니고 전체 맵에서 뜨는걸 가져와야되나?
            {

                DrawFreeLine(myPen, g);
                pictureBox2.Refresh();
            }
        }
        #endregion


        #region 파일 열기 메뉴 항목 클릭시 처리하기 - openMenuItem_Click(sender, e)

        /// <summary>
        /// 파일 열기 메뉴 항목 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if(this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.sourceBitmap = LoadBitmap(this.openFileDialog.FileName);


                    targetImgRect.Location = new Point(0,0);
                    SetScale(1/Math.Max((double)sourceBitmap.Width/pictureBox2.Width, (double)sourceBitmap.Height/pictureBox2.Height), true, true, true);

                    this.saveAsMenuItem.Enabled = true;

                    this.widthTextBox.Enabled   = true;
                    this.heightTextBox.Enabled  = true;
                    this.percentTextBox.Enabled = true;
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        #endregion
        #region 파일 저장하기 메뉴 항목 클릭시 처리하기 - saveAsMenuItem_Click(sender, e)

        /// <summary>
        /// 파일 저장하기 메뉴 항목 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            if(this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveImage(this.sourceBitmap, this.saveFileDialog.FileName);
            }
        }

        #endregion
        #region 너비 텍스트 박스 텍스트 변경시 처리하기 - widthTextBox_TextChanged(sender, e)

        /// <summary>
        /// 너비 텍스트 박스 텍스트 변경시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void widthTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.ignoreChanges)
            {
                return;
            }

            double width;

            if(double.TryParse(this.widthTextBox.Text, out width))
            {
                SetScale(width / this.sourceBitmap.Width, false, true, true);
            }
        }
        #endregion


        #region 높이 텍스트 박스 텍스트 변경시 처리하기 - heightTextBox_TextChanged(sender, e)

        /// <summary>
        /// 높이 텍스트 박스 텍스트 변경시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void heightTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.ignoreChanges)
            {
                return;
            }

            double height;

            if(double.TryParse(this.heightTextBox.Text, out height))
            {
                SetScale(height / this.sourceBitmap.Height, true, false, true);
            }
        }

        #endregion
        #region 비율 텍스트 박스 텍스트 변경시 처리하기 - percentTextBox_TextChanged(sender, e)

        /// <summary>
        /// 비율 텍스트 박스 텍스트 변경시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void percentTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.ignoreChanges)
            {
                return;
            }

            double percent;

            if(double.TryParse(this.percentTextBox.Text, out percent))
            {
                SetScale(percent / 100, true, true, false);
            }
        }

        #endregion

        // ///////////////////////////////////////////////////////////////////////////////////// Function

        #region 소스이미지가 픽쳐박스에 필러박스나 레터박스를 만들도록 하는 Rectangle 계산

        /// <summary>
        /// 화면비를 고정한 채로 픽쳐박스에 딱 맞게 들어가는 정사각형 객체를 계산해줍니다.
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="ImgRect"></param>
        /// <returns></returns>
        public static Rectangle FitAspectRatio(PictureBox picBox, Bitmap srcImg)
        {
            ////////////////////////////////Base on this feature
            ///https://stackoverflow.com/questions/6565703/math-algorithm-fit-image-to-screen-retain-aspect-ratio
            ////////////////////////////////


            Rectangle ret_Rectangle = new Rectangle();

            int W = srcImg.Width;
            int H = srcImg.Height;
            int w = picBox.Width;
            int h = picBox.Height;

            Double ratio = Math.Max(W/w, H/h);

            ret_Rectangle.X = (int) (picBox.Location.X + w * (1 - ratio) / 2);
            ret_Rectangle.Y = (int) (picBox.Location.Y + h * (1 - ratio) / 2);
            ret_Rectangle.Width = (int)Math.Round(w * ratio);
            ret_Rectangle.Height = (int) Math.Round(h * ratio);

            return ret_Rectangle;
        }

        private static Rectangle FitAspectRatio(PictureBox picBox, Rectangle ImgRect)
        {
            Rectangle ret_Rectangle = new Rectangle();

            int W = ImgRect.Width;
            int H = ImgRect.Height;
            int w = picBox.Width;
            int h = picBox.Height;

            Double ratio = Math.Max(W / w, H / h);

            ret_Rectangle.X = (int)(picBox.Location.X + w * (1 - ratio) / 2);
            ret_Rectangle.Y = (int)(picBox.Location.Y + h * (1 - ratio) / 2);
            ret_Rectangle.Width = (int)Math.Round(w * ratio);
            ret_Rectangle.Height = (int)Math.Round(h * ratio);

            return ret_Rectangle;
        }

        
        #endregion

        #region 비트맵 로드하기 - LoadBitmap(filePath)

        /// <summary>
        /// 비트맵 로드하기
        /// </summary>
        /// <param name="filePath">파일 경로</param>
        /// <returns>비트맵</returns>
        private Bitmap LoadBitmap(string filePath)
        {
            using(Bitmap bitmap = new Bitmap(filePath))
            {
                return (Bitmap)bitmap.Clone();
            }
        }

        #endregion
        #region 스케일 설정하기 - SetScale(scale, showWidth, showHeight, showPercent)

        /// <summary>
        /// 스케일 설정하기
        /// </summary>
        /// <param name="scale">스케일</param>
        /// <param name="showWidth">너비 표시 여부</param>
        /// <param name="showHeight">높이 표시 여부</param>
        /// <param name="showPercent">비율 표시 여부</param>
        private void SetScale(double scale, bool showWidth , bool showHeight, bool showPercent)
        {
            //MessageBox.Show(" 최적 ratio: " + scale.ToString());
            int width  = (int)(this.sourceBitmap.Width  * scale);
            int height = (int)(this.sourceBitmap.Height * scale);


            zoomScale = scale;
            targetImgRect.Width = width;
            targetImgRect.Height = height;


            if((width < 1) || (height < 1)) // 값이 너무 작아서 1x1보다 작아진 경우
            {
                return;
            }

            Bitmap targetBitmap = new Bitmap(width, height); 

            using(Graphics targetGraphics = Graphics.FromImage(targetBitmap))
            {
                                    
                //targetGraphics.DrawImage(this.sourceBitmap, targetImgRect, sourceRectangle, GraphicsUnit.Pixel);
                targetGraphics.DrawImage(this.sourceBitmap, targetImgRect);
                pictureBox2.Refresh();




                //TODO: 소스이미지 갱신하고 픽쳐박스 띄우는 부분 함수로 뺄 수 있는지?

                ///<해결된문제>: 확대 상태에서, 배율을 변경하고 커서로 이동(스크롤)하면 
                ///setScale()이 호출된 순간의 이미지 잔상이 남아있음.
                ///특히, 잔상이 남는 영역이 특정 영역만큼 제한되어있으므로 확인해 볼것.
                ///
                /// =>setScale 내의 drawImage 파라미터 수정.       
                /// </해결된문제>


            }


            //this.pictureBox2.Image = targetBitmap;

            this.ignoreChanges = true;

            if(showWidth)
            {
                this.widthTextBox.Text = width.ToString("0");
            }

            if(showHeight)
            {
                this.heightTextBox.Text = height.ToString("0");
            }

            if(showPercent)
            {
                int percent = (int)(scale * 100);

                this.percentTextBox.Text = percent.ToString("0");
            }

            this.ignoreChanges = false;
        }

        #endregion
        #region 이미지 저장하기 - SaveImage(image, filePath)

        /// <summary>
        /// 이미지 저장하기
        /// </summary>
        /// <param name="image">이미지</param>
        /// <param name="filePath">파일 경로</param>
        private void SaveImage(Image image, string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch(fileExtension.ToLower())
            {
                case ".bmp"  : image.Save(filePath, ImageFormat.Bmp ); break;
                case ".exif" : image.Save(filePath, ImageFormat.Exif); break;
                case ".gif"  : image.Save(filePath, ImageFormat.Gif ); break;
                case ".jpg"  :
                case ".jpeg" : image.Save(filePath, ImageFormat.Jpeg); break;
                case ".png"  : image.Save(filePath, ImageFormat.Png ); break;
                case ".tif"  :
                case ".tiff" : image.Save(filePath, ImageFormat.Tiff); break;

                default :

                    throw new NotSupportedException("알 수 없는 파일 확장자 입니다 : " + fileExtension);
            }
        }


        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            Bitmap init_srcImg = new Bitmap(300,300);

            //원하는 초기 이미지 아래에 설정

            //
            this.sourceBitmap = init_srcImg;

            SetScale(1, true, true, true);
        }

        /// <summary>
        /// 컨트롤에 그리기가 발생하면,
        /// 소스이미지의 위치,크기를 재설정하여 그립니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle rect = new Rectangle(0, 0, (int)Math.Round(this.sourceBitmap.Width * zoomScale), (int)Math.Round(this.sourceBitmap.Height * zoomScale));

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
            e.Graphics.DrawImage(this.sourceBitmap, targetImgRect);



                /// 
            /// TODO: mouse_down..... isPaint=False; 뒤에 스냅샷 삽입하는 라인 작성.
            /// 
            /// <되돌리기_스택_이름>.add(this.SourceBitmap.clone());
            /// 


            /////////////// Undo,Redo 작동방식? //////
            /// 
            ///1.1. 최신 작업내역에 해당하는 스냅샷Image는 stack_Undo의 Top에 있다.
            ///
            ///2.1. Undo를 누르면 stack_Undo의 Top을 꺼내서 stack_Redo에 삽입한다.
            ///2.2. 그 다음 stack_Undo의 새로운 Top을 (pictureBox2)에 띄운다.
            ///
            ///3.1. 마우스 이벤트(그리기)를 통해 stack_Undo에 스냅샷이 추가 될때, stack_Redo는 클리어된다.(저장할 이유가 없다)
            ///
            ///4.1. Redo를 누르면 stack_Redo의 Top을 꺼내서 stack_Undo에 삽입한다.
            ///4.2. 그 다음 stack_Undo의 새로운 Top을 (pictureBox2)에 띄운다.
            ///
            ///5.1. 다른썸네일을 클릭하는 순간, rgb_imglist.Add(stack_Undo.Top);
            ///5.2. stack_redo&undo -> clear().
            /////////////////
            ///











        }


        #region 브러시 크기 조절버튼

        /// <summary>
        /// 브러시의 크기를 1늘립니다. 최대 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (brush_Size == 10)
                return;

            SetBrush_Size(brush_Size + 1);
        }
        /// <summary>
        /// 브러시의 크기를 줄입니다. 최소 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (brush_Size == 1)
                return;

            SetBrush_Size(brush_Size -1);
        }

        #endregion

        /// <summary>
        /// 스크롤(이동)모드로 커서 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            cursor_mode = 1;
        }

        #region 수정 - 색상지정 버튼

        /// <summary>
        /// 검은색 펜 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            cursor_mode = 2;

            //Black
            brush_Color = Color.Black;
        }
        /// <summary>
        /// 빨간색 펜 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            cursor_mode = 2;

            //Red
            brush_Color = Color.Red;
        }
        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            #region 
            #endregion
            FitAspectRatio(pictureBox2, targetImgRect);
            targetImgRect.Location = new Point(0,0);


            SetScale(1 / Math.Max((double)sourceBitmap.Width / pictureBox2.Width, (double)sourceBitmap.Height / pictureBox2.Height), true, true, true);

        }
    }
}