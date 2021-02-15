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

    public partial class UI_Main : Form
    {
        public UI_Main()
        {
            InitializeComponent();

            //창 최대화시 작업표시줄 가리지 않게 조정..
            Rectangle bounds = Screen.FromHandle(this.Handle).WorkingArea;
            int x_offset = SystemInformation.HorizontalResizeBorderThickness + SystemInformation.FixedFrameBorderSize.Width;
            int y_offset = SystemInformation.VerticalResizeBorderThickness + SystemInformation.FixedFrameBorderSize.Height;

            bounds.X -= x_offset;
            bounds.Width += (x_offset * 2);
            bounds.Height += y_offset;

            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.MaximizedBounds = bounds;

            // 창 크기 최대화 처리
            this.WindowState = FormWindowState.Maximized;            
        }

        private void button_Path_Click(object sender, EventArgs e)
        {
            Network_route_settings();
        }

        private void button_RunModel_Click(object sender, EventArgs e)
        {
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

            if (Constants.isTest_NoLabel_mode == true)
            {

                MessageBox.Show("테스트모드-RGB Swap only로 실행합니다. 모델 구동만 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {

                    //new Bitmap(경로)로 바꿀예정
                    gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));
                    Console.WriteLine("그레이경로: " + gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png");
                    rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));

                    //아예 rgb변환도 생략.
                    //rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image

                    /*테스트용 RGB 이미지 생성할때만 풀면됨
                    rgb_imglist[0].Save(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");
                    MessageBox.Show("rgb테스트용 이미지 저장 완료");
                    */
                }
                RefreshAllPictureBox();
                return;
            }
            else if (Constants.isTestmode == true)
            {
                MessageBox.Show("테스트모드로 실행합니다. 모델 구동, GrayToRGB변환을 생략하고 수정, 저장단계로 넘어갑니다.");
                //대체할 코드
                for (int index = 0; index < imgList.Count(); index++)
                {

                    //new Bitmap(경로)로 바꿀예정
                    gray_imglist.Add(new Bitmap(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png"));
                    rgb_imglist.Add(new Bitmap(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png"));
                    Console.WriteLine("그레이경로: " + gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png");
                    Console.WriteLine("RGB 경로: " + rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");

                    //아예 rgb변환도 생략.
                    //rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));  /// gray -> rgb image

                    /*테스트용 RGB 이미지 생성할때만 풀면됨
                    rgb_imglist[0].Save(rgb_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_rgb_img.png");
                    MessageBox.Show("rgb테스트용 이미지 저장 완료");
                    */
                }


                RefreshAllPictureBox();
                return;
            }

            //테스트 아닐때

            MessageBox.Show("그레이 스케일 변환 중 입니다.");
            Pythonnet_(input_file_path.SelectedPath, gray_file_path.SelectedPath, imgList);
            MessageBox.Show("그레이 스케일 변환 완료 !");

            //GrayScale 이미지 변수에 저장
            //Gray2RGB 이미지 변수에 저장



            for (int index = 0; index < imgList.Count(); index++)
            {
                using (FileStream fs = new FileStream(gray_file_path.SelectedPath + imgList[index].Remove(imgList[index].Count() - 4, 4) + "_gray_img.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Bitmap img = new Bitmap((Bitmap)Image.FromStream(fs));
                    gray_imglist.Add(img);
                    rgb_imglist.Add(Gray2RGB_Click(gray_imglist[index]));
                    fs.Close();
                }
            }


            RefreshAllPictureBox();

            return;
        }
                
        private void button_Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("이미지 불러오기: F \n딥러닝 레이블링 적용: Q  \n브러쉬: B \n지우개: E \n이미지 확대: Scroll up \n이미지 축소: Scroll down \n되돌리기 : Ctrl + Z  \n앞돌리기: Ctrl + Y \n프로젝트 저장: Ctrl + S");
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            //되돌리기가 수행가능한 상태인지 확인.
            if (stackUndo.Count <= 1) //UNDO의 Last를 항상 화면에 띄우는 이미지와 같게할 것이므로 최소 1스택.
            {
                return;
            }

            //1.최신 작업내역을 꺼내어 stackRedo에 저장  
            stackRedo.AddLast(new Bitmap(stackUndo.Last.Value));
            stackUndo.RemoveLast();

            if (stackRedo.Count > _maxHistory_)
            {
                stackRedo.RemoveFirst();
            }

            //이미지 꺼내서 화면 갱신
            this.sourceBitmapRgb = new Bitmap(stackUndo.Last.Value);

            picBox_Rgb.Refresh();

        }

        private void button_Redo_Click(object sender, EventArgs e)
        {
            //되돌리기가 수행가능한 상태인지 확인.
            if (stackRedo.Count <= 0)
            {
                return;
            }

            //1.최신 Undo내역을 꺼내어 stackUndo에 저장   
            stackUndo.AddLast(new Bitmap(stackRedo.Last.Value));
            stackRedo.RemoveLast();

            if (stackUndo.Count > _maxHistory_)
            {
                stackUndo.RemoveFirst();
            }

            //이미지 꺼내서 화면 갱신
            this.sourceBitmapRgb = new Bitmap(stackUndo.Last.Value);
            picBox_Rgb.Refresh();

        }

        private void button_PaintMode_Click(object sender, EventArgs e)
        {
            Console.WriteLine("커서모드" + Convert.ToString(cursor_mode));
            cursor_mode = CursorMode.Paint;
        }

        private void button_ScrollMode_Click(object sender, EventArgs e)
        {
            Console.WriteLine("커서모드" + Convert.ToString(cursor_mode));
            cursor_mode = CursorMode.Scroll;
        }

        private void button_ZoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel++;
            SetScale(Math.Pow(Constants.ratioPerLevel, zoomLevel)); //윈도우 그림판은 첫번째 인자가 2로 잡혀있는 셈( 25%/ 50%/ 100%/ 200%/ 400%)

        }

        private void button_ZoomReset_Click(object sender, EventArgs e)
        {
            //최초 위치로 되돌림.
            targetImgRect.X = 0;
            targetImgRect.Y = 0;

            zoomLevel = 0;
            SetScale(Math.Pow(1.5, zoomLevel));
        }

        private void button_ZoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel--;
            SetScale(Math.Pow(Constants.ratioPerLevel, zoomLevel));
        }

        private void UI_Main_Load(object sender, EventArgs e)
        {
            InitBtnBrushColor();
            // TODO: picBox 3종류간의 이벤트 호출을 최상단의 하나에 통합.
            // 방법1. 최상단에서 같은종류의 이벤트 호출시 나머지를 호출.
            // 방법2. 현재 상태처럼 대리자 리스트 관리를 통해서 호출.

            // 제일 앞 컨트롤에 뒤에 덮인 컨트롤의 이벤트 연결.
            this.picBox_Cursor.MouseDown += picBox_Rgb_MouseDown;
            this.picBox_Cursor.MouseUp += picBox_Rgb_MouseUp;

            // 호출순서 변경(이렇게 하니까 버그는 해결했는데 제대로 된 방법인지 고찰필요.)
            this.picBox_Cursor.MouseMove -= picBox_Cursor_MouseMove;
            this.picBox_Cursor.MouseMove += picBox_Rgb_MouseMove;
            this.picBox_Cursor.MouseMove += picBox_Cursor_MouseMove;

            this.picBox_Cursor.MouseWheel += picBox_Origin_MouseWheel;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {

            if (null == rgb_imglist || 0 == rgb_imglist.Count)
            {
                MessageBox.Show("저장 할 이미지가 없습니다 !! ");
                return;
            }

            if (sourceBitmapRgb == null)
            {
                MessageBox.Show("수정 된 이미지가 없습니다 !! ");
                return;
            }

            if (gray_file_path.SelectedPath == string.Empty)
            {
                MessageBox.Show("그레이 스케일 저장 경로가 없습니다.");
                Network_route_settings();
                return;
            }

            // index 넣어서 저장
            //rgb_imglist[index] = sourceBitmapRgb.;
            MessageBox.Show(current_idx.ToString());
            rgb_imglist[current_idx] = new Bitmap(sourceBitmapRgb);

            for (int index = 0; index < rgb_imglist.Count(); index++)
            {
                gray_imglist[index] = RGB2Gray_Click(rgb_imglist[index]);
                gray_imglist[index].Save(gray_file_path.SelectedPath + imgList[index].Remove(imgList[0].Count() - 4, 4) + "_gray_img.png");
            }
        }

        private void picBox_Rgb_MouseUp(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case CursorMode.Scroll: //scroll mode

                    isScroll = false;
                    break;

                case CursorMode.Paint: //paint mode

                    if (sourceBitmapRgb == null)
                    {
                        return;
                    }

                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 
                    Point mousePos = e.Location;


                    //그리기 비활성화.
                    isPaint = false;

                    Input_Action(sourceBitmapRgb);

                    break;
                default:
                    break;
            }
        }

        private void Scale_toolStripStatusLabel_Paint(object sender, PaintEventArgs e)
        {
            Scale_toolStripStatusLabel.Text =
                "Scale:"
                + Convert.ToString(Math.Round(zoomScale * 100))
                + "%"
                ;
        }

        private void picBox_Rgb_MouseDown(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case CursorMode.Scroll: //scroll mode
                    isScroll = true;
                    move_startpt = e.Location;
                    break;

                case CursorMode.Paint: //paint mode

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


                    /// pen좌표*zoom배율+src의 좌표 = mosPos이니까 우변으로 넘기고 나누면 동일
                    /// = 2배로 보고있을땐 10cm 이동해도 5cm 이동한 셈밖에 안됨


                    #region 클릭만 했을때도 점(원) 그려짐.
                    using (Pen myPen = new Pen(brush_Color, 1))
                    using (Graphics g = Graphics.FromImage(sourceBitmapRgb))
                    {
                        Brush aBrush = new SolidBrush(brush_Color);

                        /* //Create Proper Circle */
                        Rectangle rectDot = new Rectangle(pen_startpt.X - (brush_Size + 2) / 2, pen_startpt.Y - (brush_Size + 2) / 2, brush_Size + 1, brush_Size + 1);

                        if (brush_Size == 1)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size, brush_Size);
                            g.FillRectangle(aBrush, rectDot);
                        }
                        else if (brush_Size == 2)
                        {
                            rectDot = new Rectangle(pen_startpt.X - (brush_Size) / 2, pen_startpt.Y - (brush_Size) / 2, brush_Size - 1, brush_Size - 1);
                            g.DrawRectangle(myPen, rectDot);
                        }
                        else
                        {
                            g.FillEllipse(aBrush, rectDot);
                        }

                        RefreshAllPictureBox();
                    }
                    #endregion
                    break;
                default:
                    break;
            }

        }

        private void picBox_Rgb_MouseMove(object sender, MouseEventArgs e)
        {
            switch (cursor_mode)
            {
                case CursorMode.Scroll: //scroll mode
                    move_endpt = e.Location;
                    Move_targetRect_location();
                    move_startpt = move_endpt;
                    break;

                case CursorMode.Paint: //paint mode
                    if (sourceBitmapRgb == null)
                    {
                        return;
                    }
                    #region <sender 캐스팅 & 좌표지정>
                    PictureBox picBox = (PictureBox)sender;

                    //커서 좌표 갱신 

                    Point mousePos = e.Location;

                    #endregion

                    //이미지의 확대,이동을 역산: 소스비트맵의 해당 좌표
                    pen_endpt = new Point((int)Math.Round((mousePos.X - targetImgRect.X) / zoomScale), (int)Math.Round((mousePos.Y - targetImgRect.Y) / zoomScale));

                    DrawShape();
                    pen_startpt = pen_endpt;

                    break;
                default:
                    break;
            }

        }

        private void picBox_Cursor_Paint(object sender, PaintEventArgs e)
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
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
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

        private void picBox_Cursor_MouseEnter(object sender, EventArgs e)
        {
            //화면에 띄울 커서의 종류, 픽쳐박스에서 따라다닐 브러시의 표시유무(isOnpicBox3).
            //둘 다 isPaint, isScroll, cursor_mode에 따라 변경.

            isOnPicBox3 = true;
        }

        private void picBox_Cursor_MouseLeave(object sender, EventArgs e)
        {
            isOnPicBox3 = false;


            PictureBox picBox = (PictureBox)sender;
            if ((CursorMode.Paint != cursor_mode) || (null == cursorBoardBitmap))
            {
                return;
            }

            using (Graphics g = Graphics.FromImage(cursorBoardBitmap))
            {
                g.Clear(Color.Transparent);
            }
            picBox.Refresh();

        }

        private void picBox_Cursor_MouseMove(object sender, MouseEventArgs e)
        {
            //picBox3.MouseMove 의 delegate로 picBox2_MouseMove를 줘도 되고,
            //아예 여기서 picBox2_MouseMove를 발생시켜도 됨.

            if ((CursorMode.Paint != cursor_mode) || (null == cursorBoardBitmap))
            {
                return;
            }
            DrawCursor(picBox_Cursor, e);
        }

        //마우스휠 이벤트는 디자이너에서 생성되지 않음.
        public void picBox_Origin_MouseWheel(object sender, MouseEventArgs e)
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

            RefreshAllPictureBox();
            //TODO:Scale_toolStripStatusLabel 갱신여부 확인하기. (zoomScale 변수와, 해당레이블의 Paint 이벤트 참조.).
        }

        private void colorSlider_Opacity_Scroll(object sender, ScrollEventArgs e)
        {
            if (null == sourceBitmapRgb)
            {
                return;
            }
            SetAlpha(decimal.ToInt32(colorSlider_Opacity.Value));

            RefreshAllPictureBox();
        }

        private void colorSlider_BrushSize_Scroll(object sender, ScrollEventArgs e)
        {
            SetBrushSize(decimal.ToInt32(colorSlider_BrushSize.Value));
        }

        private void picBox_Origin_Paint(object sender, PaintEventArgs e)
        {
            //ORIGIN 이미지가 없을때.
            if (null == imgList || 0 == imgList.Count || null == sourceBitmapOrigin)
            {
                return;
            }

            // 보간 방식 지정. 축소시엔 부드럽게, 확대했을땐 픽셀 뚜렷하게.
            if (zoomScale < 1)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            }
            else
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }

            //타겟영역 갱신.
            targetImgRect.Width = (int)Math.Round(sourceBitmapOrigin.Width * zoomScale);
            targetImgRect.Height = (int)Math.Round(sourceBitmapOrigin.Height * zoomScale);

            //---------------------------------------------------------------------------------
            ///이미지가 픽쳐박스 탈출하는 것 방지.
            //---------------------------------------------------------------------------------

            //적용하는 순서에 따라 zoomOut시에 어느 모서리로 붙을지 결정.
            //중앙에 띄우고싶으면..?

            if (targetImgRect.X > 0) targetImgRect.X = 0;
            if (targetImgRect.Y > 0) targetImgRect.Y = 0;
            if (targetImgRect.X + targetImgRect.Width < picBox_Origin.Width) targetImgRect.X = picBox_Origin.Width - targetImgRect.Width;
            if (targetImgRect.Y + targetImgRect.Height < picBox_Origin.Height) targetImgRect.Y = picBox_Origin.Height - targetImgRect.Height;

            //---------------------------------------------------------------------------------

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

        private void UI_Main_KeyDown(object sender, KeyEventArgs e)
        {
            /*
              되돌리기 : Ctrl + Z
              앞돌리기 : Ctrl + Y
            브러쉬 : B
            지우개 : E
            이미지 확대 : Scroll up
            이미지 축소 : Scroll down
            이미지 불러오기 : F
            딥러닝 레이블링 적용 : Q
            프로젝트 저장 : Ctrl + S
            정보창: I
            */
            if (e.KeyCode == Keys.F) button_Path_Click(null,null);
            if (e.KeyCode == Keys.Q) button_RunModel_Click(null, null);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.Z)) button_Undo_Click(null, null);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.Y)) button_Redo_Click(null, null);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.S)) button_Save_Click(null, null);
            if (e.KeyCode == Keys.B) button_PaintMode_Click(null, null);
            if (e.KeyCode == Keys.E); // button6_Click(sender, e); -> brush 색깔 0이 검정색 = 지우개.
            if (e.KeyCode == Keys.I) button_Info_Click(null, null);

            this.ctrlKeyDown = e.Control;
        }

        private void UI_Main_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}


