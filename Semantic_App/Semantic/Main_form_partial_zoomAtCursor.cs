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
        ///////////////////////////////////^속성////////////

        enum zoomMode
        {
            Center,
            Cursor,
            TopLeft
        }



        ///////////////////////////////////////////////////////// v 메소드

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

            RefreshAllPictureBox();
            lable_ImgScale.Refresh();
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
                +"]"
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

        #region <이미지 스크롤 by 마우스 드래그>
        /// <summary>
        /// 타겟이미지의 위치 변화 벡터 = 커서의 위치 변화벡터.
        /// 이동할때마다 이미지를 갱신합니다.
        /// </summary>
        private void Move_targetRect_location()
        {
            if (false == isScroll)
            {
                return;
            }

            targetImgRect.X += move_endpt.X - move_startpt.X;
            targetImgRect.Y += move_endpt.Y - move_startpt.Y;

            RefreshAllPictureBox();
        }
        #endregion


        private void SetTargetRectByZoomAtCenter(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - pictureBox1.Width / 2) * Constants.ratioPerLevel) + pictureBox1.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - pictureBox1.Height / 2) * Constants.ratioPerLevel) + pictureBox1.Height / 2;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - pictureBox1.Width / 2) / Constants.ratioPerLevel) + pictureBox1.Width / 2;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - pictureBox1.Height / 2) / Constants.ratioPerLevel) + pictureBox1.Height / 2;
            }

            Console.WriteLine("휠.줌.중앙_targetImgRect: " + Convert.ToString(targetImgRect));
        }

        private void SetTargetRectByZoomAtCursor(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - e.X) * Constants.ratioPerLevel) + e.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - e.Y) * Constants.ratioPerLevel) + e.Y;
            }
            else
            {
                targetImgRect.X = (int)Math.Round((targetImgRect.X - e.X) / Constants.ratioPerLevel) + e.X;
                targetImgRect.Y = (int)Math.Round((targetImgRect.Y - e.Y) / Constants.ratioPerLevel) + e.Y;
            }

            Console.WriteLine("휠.줌.커서_targetImgRect: " + Convert.ToString(targetImgRect));
        }



        ////////////////////////////^메소드 ///////////////////v 이벤트/////////


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
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
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

        //굳이 안바꿔도 되나 추후 이것 수정하는 경우를 생각해서..
        //이 함수 호출하는 메서드들은 어짜피 직접 수정해야될거같아서 안가져옴.
        private void RefreshAllPictureBox()
        {
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pBox3_CursorBoard.Refresh();
        }
    }
}
