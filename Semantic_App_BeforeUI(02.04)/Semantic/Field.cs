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

        bool ctrlKeyDown;

        private ImageAttributes imageAtt = new ImageAttributes();
    }
}