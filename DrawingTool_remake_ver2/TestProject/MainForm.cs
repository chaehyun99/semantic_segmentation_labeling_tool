using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

/*
 * TODO: 커서의 모양을 변경해주는 설정이 필요함.
 * 
 * 출처: https://kjun.kr/596 [kjun.kr (kjcoder.tistory.com)]
 * 
 * 기본적인 커서 변경은 Cursor 클래스로 제공됨.
 * 
 * 어떤 이벤트,메서드에서 변경하는가?
 * -> pictureBox2_MouseEnter, pictureBox2_MouseLeave, SetBrush_Size
 * 
 * 1.이동모드 2. 수정모드
 * 
 * 3.수정모드에서 브러시 크기별로 이미지 저장해둔 다음, cursor로 불러오기.(그 방법은 다음과 같다)
Image image = Bitmap.FromFile("pen.png");
Bitmap bitmap = new Bitmap(image);
Graphics graphics = Graphics.FromImage(bitmap);
IntPtr handle = bitmap.GetHicon();
Cursor penCursor = new Cursor(handle);
    */

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

        #region <마우스 이벤트>

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
                    ///-> 2배로 확대했으니 10cm 이동해도 원본에선 5cm 이동한 셈.
                    ///</해설>


                    #region 클릭만 했을때도 점(원) 그려짐.
                    using (Pen myPen = new Pen(brush_Color, 1))
                    using (Graphics g = Graphics.FromImage(sourceBitmap))
                    {
                        Brush aBrush = new SolidBrush(brush_Color); 

                        /* //Create Proper Circle */
                        Rectangle rectDot = new Rectangle(pen_startpt.X - (brush_Size+2) / 2, pen_startpt.Y - (brush_Size+2) / 2, brush_Size+1, brush_Size+1);

                        if(brush_Size==1)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size, brush_Size);
                            g.FillRectangle(aBrush, rectDot);
                        }
                        else if(brush_Size == 2)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size-1, brush_Size-1);
                            g.DrawRectangle(myPen, rectDot);
                        }
                        else
                        {
                            g.FillEllipse(aBrush, rectDot);
                        }


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
            //좌표를 호출한 컨트롤 기준으로(e.location) 잡아놓았음.
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
            //만약 커서 표시해줄거면 여기서 갱신해줘야됨.

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

            /*TODO: pictureBox와 targetImgRect간의 Width, Height 대소관계에 따라 limit 걸어주기.
             * 
             * 원래 1-19일자 작업예정인데 중복코드 정리하느라 개념만 잡아놓고 하루 미룸.
             * 
                 * pictureBox2 -> pBox
                 * targetImgRect -> tRect
                 * (tRect.Width = srcImgBitmap.Width * zoomScale)         
            
                -고려할 속성&경우의 수

                1. 가로 스크롤
                    1-a. pBox.Width>=tRect.Width
	                    이미 가로축에서 이미지가 모두 표현되었기에 스크롤 불필요.

                    1-b pBox.Width < tRect.Width
	                    좌우로 스크롤이 가능하지만 픽쳐박스가 tRect의 영역 밖으로 나가면 안됨.
	                    ( tRect.Left <= pBox.Left ) & ( pBox.Right <= tRect.Right )

                2.세로 스크롤
                    2-a. pBox.Height >= tRect.Height
	                    1-a와 마찬가지로 스크롤 불필요d

                    2-b. pBox.Height  <  tRect.Height
	                    상하 스크롤 가능,  1-b와 제한조건 비슷.
	                    ( tRect.Top <= pBox.Top ) & ( pBox.Bottom <= tRect.Bottom )      
            
                기존 구현방식:
                pBox2.mouse_move에서 cursorMode=1&isScroll=True 일때,
                Move_srcImg_location()->pBox2.refresh()
                =>Move_srcImg_location내부에서 체크하도록 수정.
              */
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

        #region 이미지 파일 입출력
        #region 파일 열기 메뉴 항목 클릭시 처리하기 - openMenuItem_Click(sender, e)

        /// <summary>
        /// 파일 열기 메뉴 항목 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.sourceBitmap = LoadBitmap(this.openFileDialog.FileName);

                    //이미지 불러올때마다 좌표 잡기.
                    targetImgRect.Location = new Point(0, 0);

                    SetImageScale(GetScale_fitImg2PicBox(pictureBox2, sourceBitmap), true, true, true);

                    this.saveAsMenuItem.Enabled = true;

                    this.widthTextBox.Enabled = true;
                    this.heightTextBox.Enabled = true;
                    this.percentTextBox.Enabled = true;
                }
                catch (Exception exception)
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
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveImage(this.sourceBitmap, this.saveFileDialog.FileName);
            }
        }

        #endregion
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
                SetImageScale(width / this.sourceBitmap.Width, false, true, true);
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
                SetImageScale(height / this.sourceBitmap.Height, true, false, true);
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
                SetImageScale(percent / 100, true, true, false);
            }
        }

        #endregion

        // ///////////////////////////////////////////////////////////////////////////////////// Function

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
        public void Fit_Img2picBox(PictureBox picBox, Bitmap srcImg, bool isAlignCenter)
        {
            /*
             * 1.picBox와 srcbitmapImage의 속성값으로 Ratio구하기.
             * 2.해당 ratio를 적용시킨 TargetImg설정.
             * 3.zoomScale 값 변경
             * 4.픽쳐박스 갱신
             */

            int W_origin = srcImg.Width;
            int H_origin = srcImg.Height;
            int W_screen = picBox.Width;
            int H_screen = picBox.Height;

            Double ratio = GetScale_fitImg2PicBox(picBox, sourceBitmap);

            int W_new = (int)Math.Round(ratio * W_origin);
            int H_new = (int)Math.Round(ratio * H_origin);

            //이미지 정렬위치
            if (true == isAlignCenter)
            {

                targetImgRect.X = (int)((W_screen - W_new) / 2);
                targetImgRect.Y = (int)((H_screen - H_new) / 2);

            }
            else
            {

                targetImgRect.X = 0;
                targetImgRect.Y = 0;

            }

            //이미지 표시 크기
            targetImgRect.Width = W_new;
            targetImgRect.Height = H_new;

            //배율 저장하고 화면갱신
            zoomScale = ratio;
            pictureBox2.Refresh();

            // 텍스트박스(넓이/높이/비율) 갱신
            this.ignoreChanges = true;

            this.widthTextBox.Text = W_new.ToString("0");
            this.heightTextBox.Text = H_new.ToString("0");
            int percent = (int)(ratio * 100);
            this.percentTextBox.Text = percent.ToString("0");
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

        /// <summary>
        /// pictureBox와 Bitmap 객체의 넓이비 또는 높이비 중 큰쪽을 반환합니다.
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="srcImg"></param>
        /// <returns></returns>
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
        private void SetImageScale(double scale, bool showWidth, bool showHeight, bool showPercent)
        {
            //1.
            int width = (int)(this.sourceBitmap.Width * scale);
            int height = (int)(this.sourceBitmap.Height * scale);

            if ((width < 1) || (height < 1)) // 값이 너무 작아서 1x1보다 작아진 경우
            {
                return;
            }
            //2.
            zoomScale = scale;
            //3.
            targetImgRect.Width = width;
            targetImgRect.Height = height;
            //4.
            pictureBox2.Refresh();
            //
            this.ignoreChanges = true;

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

        #region 인터페이스 (버튼/화면 입출력)

        private void MainForm_Load(object sender, EventArgs e)
        {
            Bitmap init_srcImg = new Bitmap(300, 300);

            //원하는 초기 이미지 아래에 설정

            //
            this.sourceBitmap = init_srcImg;

            SetImageScale(1, true, true, true);
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
        }

        #region 브러시 크기 조절 - 현재 인터페이스: 버튼

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
        /// 커서를 화면 스크롤(이동)모드로 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            cursor_mode = 1;
        }

        #region 그리기:펜 색상변경 - 현재 인터페이스: 버튼

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

        /// <summary>
        /// 이미지를 현재 픽쳐박스 크기에 맞게 되돌리는 기능.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            #region 
            #endregion
            //3번째 인자를 True로 지정하면 중앙 정렬.
            Fit_Img2picBox(pictureBox2, sourceBitmap, true);            

            ////??
            //SetScale(GetRatio_picBox2Rect(pictureBox2, targetImgRect);
            //SetScale(GetRatio_picBox2Rect(pictureBox2,targetImgRect), true, true, true);

        }
        #endregion
    }
}