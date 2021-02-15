using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Semantic
{
    partial class UI_Main
    {

        List<string> imgList = null;

        List<Bitmap> gray_imglist = new List<Bitmap>();
        List<Bitmap> rgb_imglist = new List<Bitmap>();

        ///original_opac -> sourceBitmapRgb로 변경됨
        private Bitmap sourceBitmapRgb = null;

        private Bitmap sourceBitmapOrigin = null;

        public static bool whether_to_save_ = false;
        public static FolderBrowserDialog input_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog gray_file_path = new FolderBrowserDialog();
        public static FolderBrowserDialog rgb_file_path = new FolderBrowserDialog();

        private enum CursorMode
        {
            Scroll,
            Paint
        }

        private int brush_Size = 1;
        private CursorMode cursor_mode = CursorMode.Scroll;
        private Color brush_Color = Color.Black;


        private bool isScroll = false, isPaint = false;
        private Point move_startpt, move_endpt, pen_startpt, pen_endpt;


        private Rectangle targetImgRect = new Rectangle(0, 0, 100, 100);
        private int zoomLevel = 0;
        private double zoomScale = 1.0f;

        bool ctrlKeyDown=false;

        private ImageAttributes imageAtt = new ImageAttributes();

        private Bitmap cursorBoardBitmap = null;

        enum zoomMode
        {
            Center,
            Cursor,
            TopLeft
        }

        int current_idx = 0;

        LinkedList<Bitmap> stackUndo = new LinkedList<Bitmap>();
        LinkedList<Bitmap> stackRedo = new LinkedList<Bitmap>();

        /// <summary>
        /// 이 값으로 저장할수 있는 UNDO,REDO횟수의 최댓값 조절.
        /// </summary>
        int _maxHistory_ = 20;

    }

    static class Constants
    {
        public const int Thumbnail_Width = 243;
        public const int Thumbnail_Height = 150;

        public const double ratioPerLevel = 1.1;

        public const int Max_brush_Size = 10;

        //
        public const bool isTestmode = false;                     //원본이미지만 필요
        //
        public const bool isTest_NoLabel_mode = true;               //모델구동 생략. 레이블링해놓은 GrayScale 이미지를 경로에 둔채로 테스트.                          
                                                                    //
                                                                    //public const bool isTest_NoLabel_mode = true;
                                                                    // public const bool isTestmode = true;                       //모델구동+rgb 전부 생략. GraysCale&RGB 이미지를 경로에 둔채로 테스트.                   
                                                                    //

        ///모델구동, rgb변환없이 작업 시작할 때 키고, 
        ///모델구동, rgb변환해야되거나 공식적으로 올릴땐 false
    }

}