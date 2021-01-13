using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices; // 마우스 이동 API



/// <summary> 해결함: 기본 커서 좌표관련 문제
/// /// -현상
/// 폼의 위치에 따라 펜으로 그리는 좌표와 커서간의 정확도가 차이남. 
/// 화면 좌상단에서 멀어질수록 오차 심해짐.
/// --해결됨: 폼 창의 화면,클라이언트 대비 좌표값을 불러와야됨, 그리고 디자이너에서 scale적용되있으면 제대로 안됨.
/// </summary>


///<summary>
///일단 돌아가게 하기위해서 목표수정
///
///-확대할떄 커서기준 확대를 고집할 필요는없다
///
///-확대후 이동도 드래그스크롤 대신 스크롤바를 사용할 순 있다.
///->아무튼 보여지는 좌표가 정확한게 더 중요하다.
/// 
///</summary>

///<summary>
///현재 space 누르면 특정 좌표로 마우스 강제이동시키니까 이걸로 테스트해가면서 ㄱ.
/// 
///</summary>

namespace DrawingTool_remake_ver1
{
    public partial class Form1 : Form
    {
        #region <test: 마우스 강제이동 API>
        [DllImport("user32.dll")] static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")] static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")] static extern int SetCursorPos(int x, int y);

        #endregion


        #region <커서 좌표따는 메소드>

        /// <summary>
        /// 컨트롤(픽처박스).Location 기준으로 마우스커서의 좌표를 구합니다.
        /// </summary>
        /// <param name="pBox">좌표의 기준이 될 픽쳐박스</param>
        /// <returns>커서의 픽쳐박스에 대한 상대좌표</returns>
        private Point Get_mouse_pos_pBox(PictureBox pBox)
        {
            Point ret_Pos = new Point(0,0);
            ret_Pos.X = Control.MousePosition.X - this.PointToScreen(pBox.Location).X;
            ret_Pos.Y = Control.MousePosition.Y - this.PointToScreen(pBox.Location).Y;

            //Console.WriteLine("화면기준 마우스좌표 : (" + Control.MousePosition.X.ToString() + "," + Control.MousePosition.Y.ToString() + ")");
            //Console.WriteLine("-화면기준 박스좌표 : (" + this.PointToScreen(pBox.Location).X.ToString() + "," + this.PointToScreen(pBox.Location).Y.ToString() + ")");
            //Console.WriteLine("=박스기준 마우스좌표           : (" + ret_Pos.X.ToString() + "," + ret_Pos.Y.ToString() + ")");

            /// ret_Pos = 커서의 화면좌표 - 컨트롤의 화면좌표;
            ///         = 커서 화면기준좌표 - ( 컨트롤의 클라기준좌표 + 클라의 화면기준좌표()
            ///         = mouseposition - Form.poin2Screen(픽처박스.location  )
            ///참고: this.Top은 Point가 아닌 int값임.

            return ret_Pos;
        }

        /// <summary>
        /// 특정 Rectangle의 Location 기준으로 마우스의 좌표
        /// </summary>
        /// <param name="Rect">좌표의 기준이 될 Rectangle 객체. picturebox1을 기준으로 한 rect.location을 전제로 함.</param>
        /// <returns>마우스의 상대좌표 from Rect</returns>
        private Point Get_mouse_pos_Rect(Rectangle Rect)
        {
            Point ret_Pos = new Point(0, 0);
            ret_Pos.X = Control.MousePosition.X - (this.PointToScreen(pictureBox1.Location).X + srcImgRect.X);
            ret_Pos.Y = Control.MousePosition.Y - (this.PointToScreen(pictureBox1.Location).Y + srcImgRect.Y);

            ///mouse2rect = mouse2client - rect2client
            ///
            ///=mouse2client -  [ (client->pbox) + (pbox -> rect) ] 
            ///=mouse2client - [ this.Pt2Client(pbox.location) + srcImgRect.Location]
            ///
            ///=mouse2screen -  [ (screen->pbox) + (pbox -> rect) ] 

            return ret_Pos;
        }



        #endregion

        //마우스 휠 

        #region <이미지 배율 조정>

        //이미지 배율(일단 원본 기준 배율로 설정)

        ///TODO: 테스트한다고 대충짰지만..
        //원하는 배율을 param으로 받는 setScale() 메소드 구현해야됨.


        //private double zoom_scale = 1.0F;
        private int zoom_scale = 1;                 //좌표부터 잡고 배율 바꾸기.

        /// <summary>
        /// 키보드 1~9 누르면 그만큼 배율 설정, 0은 1씩늘리다가 제자리.
        /// </summary>
        /// <param name="e">e.Keycord로 키값읽어오면됨.</param>
        private void Toggle_zoomscale(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                    if (zoom_scale == 9)
                    {
                        zoom_scale = 1;
                        Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    }
                    else
                    {
                        zoom_scale += 1;
                        Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    }
                    break;
                case Keys.F:
                    if (zoom_scale == 9)
                    {
                        zoom_scale = 1;
                        Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    }
                    else
                    {
                        zoom_scale += 1;
                        Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    }
                    break;
                case Keys.D1:
                    zoom_scale = 1;
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;
                case Keys.D2:
                    zoom_scale = 2;
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;
                case Keys.D3:
                    zoom_scale = 3;
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;
                case Keys.D4:
                    zoom_scale = 4;
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;
                case Keys.D5:
                    zoom_scale = 5;
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;                         
                case Keys.D6:                      
                    zoom_scale = 6;                
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;                         
                case Keys.D7:                      
                    zoom_scale = 7;                
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;                         
                case Keys.D8:                      
                    zoom_scale = 8;                
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;                         
                case Keys.D9:                      
                    zoom_scale = 9;                
                    Console.WriteLine("Scale 변경: x" + Convert.ToString(zoom_scale));
                    break;
                default:
                    break;
            }
            
        }

        //참고:pictureBox 속성에서 Sizemode도 신경써야됨

        #endregion

        //그리기 펜
        private Point pen_startpt;      // 시작점
        private Point pen_endpt;

        #region 브러시 크기 조정
        private int brush_Size = 1;


        /// <summary>
        /// 브러시 사이즈 설정
        /// </summary>
        /// <param name="brush_Size"></param>
        private void setBrush_Size(int new_size)
        {
            brush_Size = new_size;
            Console.WriteLine("브러시 크기변경: " + Convert.ToString(brush_Size));
            return;
        }

        #endregion

        //픽처박스 및 들어갈 이미지
        private Bitmap drawArea;

        private Rectangle srcImgRect;

        #region <이미지 확대상태에서 이동>

        /// <summary>
        /// 스크롤가능 여부
        /// </summary>
        private bool isScroll = false;

        private Point move_startpt;
        private Point move_endpt;

        /// <summary>
        /// 이미지를 커서의 이동에 따라 이동하고 보여줍니다.
        /// </summary>
        private void Move_srcImg_location()
        {
            if (false == isScroll)
            {
                return;
            }

            srcImgRect.X += move_endpt.X - move_startpt.X;
            srcImgRect.Y += move_endpt.Y - move_startpt.Y;
            Console.WriteLine("현재 srcImgRect.X, Y   " + srcImgRect.X + "," + srcImgRect.Y);
            Console.WriteLine("현재 srcImgRect.Location" + srcImgRect.Location.ToString());
            Console.WriteLine("현재 srcImgRect.특성     " + srcImgRect.ToString());
            Console.WriteLine("==============");

            pictureBox1.Refresh();
        }
        /// <summary>
        /// 이미지의 위치를 특정 좌표로 이동 후 보여줍니다.
        /// </summary>
        /// <param name="new_location">이미지의 좌상단이 이동할 pictureBox 기준의 상대좌표.</param>
        private void Move_srcImg_location(Point new_location)
        {
            srcImgRect.Location = new_location;

            pictureBox1.Refresh();
        }


        #endregion

        #region <커서 모드 변경>
        private int cursor_mode = 2;

        /// <summary>
        /// 커서 모드를 변경합니다.
        /// </summary>
        /// <param name="num_mode">1:scroll, 2:paint</param>
        private void Chan_cursor_mode(int num_mode)
        {
            cursor_mode = num_mode;
            return;
        }
        #endregion

        #region <폼 기본 설정>
        public Form1()
        {
            ///<summary>
            ///초기설정 1.콘솔띄우기
            ///</summary>
            InitializeComponent();
            Console.WriteLine("콘솔 시작");

            ///픽처박스 들어갈 이미지: 사각좌표객체 값 설정(
            ///Top,Left와 폭, 넓이등을 픽처박스와 처음엔 같게 해줘야됨.
            ///이걸 폼 load에서 설정할지 여기서 설정할지는 다시 생각
            ///아마 여기선 상대좌표만 잡고 로딩이나 refresh될때마다 폼의 location에 따라가게 해야??
            ///

            
            


            #region <스크롤바 관련변수>
            /*
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = (이미지Rect).Width - pictureBox1.Width;


            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = (이미지Rect).Height - pictureBox1.Height;
            */
            #endregion

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("폼1: 로드 완료\n");
            Console.WriteLine("====MANUAL======\n");
            Console.WriteLine("zoom 조절:     1~9");
            Console.WriteLine("zoom 증가:      0");
            Console.WriteLine("brush 크기조정: F");

            

            ResizeRedraw = true;
            int _drawArea_w = pictureBox1.Width;
            int _drawArea_h = pictureBox1.Height; //일단 그대로 해보고 나중에 1200,900 넣어서 배율 구현
            drawArea = new Bitmap(_drawArea_w,_drawArea_h);

            //g_drawArea 사용시점: 잠깐 흰색칠하고 끝
            using (Graphics g_drawArea = Graphics.FromImage(drawArea))
            {
                g_drawArea.Clear(Color.White);
            }

            pictureBox1.Image = drawArea;
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
        /// 펜 생성/그리기/새로고침
        /// </summary>
        private void DrawShape() //Q: 나중에 인자로 brush& shape 받기?
        {
            Pen myPen = new Pen(Color.Black, brush_Size);                // myPen

            using (Graphics g = Graphics.FromImage(pictureBox1.Image)) //픽처박스에 뜨는부분만 가져와서 그리는게 아니고 전체 맵에서 뜨는걸 가져와야되나?
            {

                DrawFreeLine(myPen, g);
                pictureBox1.Refresh();
            }
        }
        #endregion



        #region <마우스 down/이동/up>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1: //scroll mode
                    isScroll = true;
                    move_startpt = Get_mouse_pos_pBox(pictureBox1);
                    break;
                case 2: //paint mode
                    #region <sender 캐스팅 & 좌표지정>
                    //sender 캐스팅해서 받기(아마 픽쳐박스)
                    PictureBox picBox_down = (PictureBox)sender;
                    //커서 상태 따라 다른 기능(미구현)
                    //커서 좌표 갱신 (setPos() 함수로 받기 지정)
                    Point mousePos_down = Get_mouse_pos_pBox(picBox_down); //컨트롤 기준의 커서위치 지정
                    #endregion

                    //펜 좌표 설정
                    pen_startpt.X = (int)((mousePos_down.X - srcImgRect.X) / zoom_scale ); //프로그램플로우 상 start pt가 처음으로 설정되는 곧.
                    pen_startpt.Y = (int)((mousePos_down.Y - srcImgRect.Y) / zoom_scale );
                    
                    ///<해설>
                    ///pen좌표*zoom배율+src의 좌표 = mosPos이니까 우변으로 넘기고 나누면 같아짐.
                    /// </해설>

                    if (e.Button == MouseButtons.Left)
                    {

                        using (Graphics g = Graphics.FromImage(pictureBox1.Image)) //픽처박스에 뜨는부분만 가져와서 그리는게 아니고 전체 맵에서 뜨는걸 가져와야되나?
                        {

                            Pen blackPen = new Pen(Color.Black, brush_Size);
                            // Create rectangle.
                            Rectangle rect = new Rectangle(pen_startpt.X, pen_startpt.Y, 1, 1);

                            // Draw rectangle to screen.
                            g.DrawRectangle(blackPen, rect);

                            ///
                            ///Brush aBrush = (Brush)Brushes.Black;
                            ///g.FillRectangle(aBrush, rect);
                            ///
                            pictureBox1.Refresh();
                        }

                    }
                    else
                    {
                        MessageBox.Show("현재 마우스 좌측 버튼만 허용.");
                    }
                    break;
                default:
                    break;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1:
                    move_endpt = Get_mouse_pos_pBox(pictureBox1);
                    Move_srcImg_location(); 
                    //isScroll = false면 그냥 반환되니까 커서좌표만 이동. true면 이미지도 같이 이동.
                    move_startpt = move_endpt;
                    break;
                case 2:
                    //펜 끝점 설정
                    pen_endpt.X = pen_startpt.X;
                    pen_endpt.Y = pen_startpt.Y;

                    #region <sender 캐스팅 & 좌표지정>
                    //sender 캐스팅해서 받기
                    PictureBox picBox_move = (PictureBox)sender;
                    ///컨트롤내 커서 좌표 갱신(기존과 다르게 외부에서 참조하는걸로)
                    Point mousePos_move = Get_mouse_pos_pBox(picBox_move); //컨트롤 기준
                    #endregion

                    if (e.Button == MouseButtons.Left)
                    {


                        //펜 위치 갱신하고 그리기
                        pen_startpt.X = (int)((mousePos_move.X - srcImgRect.X )/ zoom_scale);
                        pen_startpt.Y = (int)((mousePos_move.Y - srcImgRect.Y )/ zoom_scale);

                        ///<해설>
                        ///pen좌표*zoom배율+src의 좌표 = mosPos 이니까 우변으로 넘기고 나누면 같아짐.
                        /// </해설>

                        DrawShape();


                        ///이미지 확대된 배율에 맞게 갱신해야됨 조심할것.
                        //콘솔에서 좌표 확인?

                    }
                    else
                    {
                        //안눌렀어도 좌표는 갱신은 됨.
                    }

                    break;
                default:
                    break;
            }
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case 1:

                    isScroll = false;
                    break;
                case 2:

                    if (e.Button == MouseButtons.Left)
                    {
                        #region <sender 캐스팅 & 좌표지정>
                        //sender 형변환해서  받기
                        PictureBox picBox_up = (PictureBox)sender;
                        //커서 상태 토글
                        //커서 좌표 갱신(확대축소 고려하기)
                        Point mousePos_up = Get_mouse_pos_pBox(picBox_up);
                        #endregion

                        //펜 위치 갱신하고 그리기
                        pen_startpt.X = (int)((mousePos_up.X - srcImgRect.X) / zoom_scale);
                        pen_startpt.Y = (int)((mousePos_up.Y - srcImgRect.Y) / zoom_scale);

                        ///<해설>
                        ///pen좌표*zoom배율+src의 좌표 = mosPos 니까 우변으로 넘기고 나누면 같아짐.
                        /// </해설>


                        Console.WriteLine("L_mouseUP : (" + pen_startpt.X.ToString() + "," + pen_startpt.Y.ToString() + ")");

                    }
                    else
                    {
                        MessageBox.Show("현재 마우스 좌측 버튼만 허용.");
                    }

                    break;
                default:
                    break;
            }

        }

        #endregion


        /// <summary>
        /// 그래픽 보간 처리. paintEvent가 트리거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null) //빈 이미지면? 오류()
                {
                    //e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //선 부드럽게

                ///<코드수정>
                ///imgRect = new Rectangle(0,0, pictureBox1.Width, pictureBox1.Height);
                ///
                ///이 사각형의 좌표와 넓이값은 두가지 전제가 깔려있던 값이다.
                ///
                ///1.하나는 픽쳐박스와 소스이미지(srcImg) 각각의 W/H ratio가 같다는 조건
                ///2.그리고 소스이미지의 배율(zoom_Scale)값이 1이라는 조건.
                ///
                /// 이미지 확대/축소 기능을 넣기위해 전제 2를 제거한 코드가 다음과 같으며
                ///
                /// 전제 1은 이미지와 픽쳐박스의 ratio가 안맞아서 padding을 채우거나 위치를 새로 잡아줘야할때 생각하자.
                ///
                
                Rectangle ImgRect_for_Paint;
                ImgRect_for_Paint = new Rectangle(srcImgRect.X, srcImgRect.Y, pictureBox1.Width * zoom_scale, pictureBox1.Height * zoom_scale);


                e.Graphics.DrawImage(pictureBox1.Image, ImgRect_for_Paint);

                
                    /* //test start    
                    
                Graphics g = CreateGraphics();

                g.DrawImage(pictureBox1.Image, imgRect);
                    */ //test end

                    //결론: pictureBox1의 paint이벤트 변수 e, 혹은 e.Graphics에서 호출함-> 좌표가 e 기준.
                    //즉, DrawImage()를 호출한 그래픽이 뭔지에 따라, param으로 같은 위치를 주더라도 다르게 그려진다.

                    pictureBox1.Focus();
                }
            }

        #region <좌표 확인용. 스페이스바 누르면 특정좌표로 보내기>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.KeyCode:
                    break;
                case Keys.Modifiers:
                    break;
                case Keys.None:
                    break;
                case Keys.LButton:
                    break;
                case Keys.RButton:
                    break;
                case Keys.Cancel:
                    break;
                case Keys.MButton:
                    break;
                case Keys.XButton1:
                    break;
                case Keys.XButton2:
                    break;
                case Keys.Back:
                    break;
                case Keys.Tab:
                    break;
                case Keys.LineFeed:
                    break;
                case Keys.Clear:
                    break;
                case Keys.Return:
                    break;
                case Keys.Space:

                    Move_srcImg_location(new Point(0,0));
                    Console.WriteLine("이미지 강제이동: " + this.pictureBox1.Location.ToString());

                    srcImgRect = new Rectangle(this.PointToScreen(pictureBox1.Location).X, this.PointToScreen(pictureBox1.Location).Y, pictureBox1.Width, pictureBox1.Height);
                    Console.WriteLine("마우스커서 강제이동:"+ srcImgRect.ToString());
                    SetCursorPos(Convert.ToInt32(srcImgRect.X), Convert.ToInt32(srcImgRect.Y));
                    break;
                case Keys.End:
                    break;
                case Keys.Home:
                    break;
                case Keys.Left:
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    break;
                case Keys.Select:
                    break;
                case Keys.Print:
                    break;
                case Keys.Execute:
                    break;
                case Keys.Snapshot:
                    break;
                case Keys.Insert:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Help:
                    break;
                case Keys.D0:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D1:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D2:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D3:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D4:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D5:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D6:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D7:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D8:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.D9:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.A:
                    break;
                case Keys.B:
                    break;
                case Keys.C:
                    break;
                case Keys.D:
                    break;
                case Keys.E:
                    break;
                case Keys.F:
                    if (brush_Size == 5)
                        setBrush_Size(1);
                    else
                        setBrush_Size(brush_Size + 1);
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.I:
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.M:
                    break;
                case Keys.N:
                    break;
                case Keys.O:
                    break;
                case Keys.P:
                    break;
                case Keys.Q:
                    break;
                case Keys.R:
                    break;
                case Keys.S:
                    break;
                case Keys.T:
                    break;
                case Keys.U:
                    break;
                case Keys.V:
                    break;
                case Keys.W:
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.LWin:
                    break;
                case Keys.RWin:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.NumPad0:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad1:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad2:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad3:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad4:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad5:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad6:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad7:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad8:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.NumPad9:
                    Toggle_zoomscale(e);
                    pictureBox1.Refresh();
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Add:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Divide:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.Scroll:
                    break;
                case Keys.LShiftKey:
                    break;
                case Keys.RShiftKey:
                    break;
                case Keys.LControlKey:
                    break;
                case Keys.RControlKey:
                    break;
                case Keys.LMenu:
                    break;
                case Keys.RMenu:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Packet:
                    break;
                case Keys.Attn:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Play:
                    break;
                case Keys.Zoom:
                    break;
                case Keys.NoName:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.Shift:
                    break;
                case Keys.Control:
                    break;
                case Keys.Alt:
                    break;
                default:
                    break;
            }
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (cursor_mode)
            {
                case 1:
                    Chan_cursor_mode(2);
                    break;
                case 2:
                    Chan_cursor_mode(1);
                    break;
                default:
                    break;
            }
        }
    }
}
