using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace TestProject
{
    /// <summary>
    /// 메인 폼
    /// </summary>
    public partial class MainForm : Form
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 인쇄 이미지
        /// </summary>
        private Image printImage;

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

            #region 이벤트를 설정한다.

            this.formButton.Click        += formButton_Click;
            this.clientAreaButton.Click  += clientAreaButton_Click;
            this.groupBoxButton.Click    += groupBoxButton_Click;
            this.tabPage1Button.Click    += tabPage1Button_Click;
            this.tabPage2Button.Click    += tabPage2Button_Click;
            this.printDocument.PrintPage += printDocument_PrintPage;

            #endregion
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Private
        //////////////////////////////////////////////////////////////////////////////// Event

        #region 폼 버튼 클릭시 처리하기 - formButton_Click(sender, e)

        /// <summary>
        /// 폼 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void formButton_Click(object sender, EventArgs e)
        {
            using(Bitmap bitmap = GetBitmap(this))
            {
                PrintImage(bitmap);
            }
        }

        #endregion
        #region 클라이언트 영역 버튼 클릭시 처리하기 - clientAreaButton_Click(sender, e)

        /// <summary>
        /// 클라이언트 영역 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void clientAreaButton_Click(object sender, EventArgs e)
        {
            using(Bitmap bitmap = GetClientAreaBitmap(this))
            {
                PrintImage(bitmap);
            }
        }

        #endregion
        #region 그룹 박스 버튼 클릭시 처리하기 - groupBoxButton_Click(sender, e)

        /// <summary>
        /// 그룹 박스 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void groupBoxButton_Click(object sender, EventArgs e)
        {
            using(Bitmap bitmap = GetBitmap(groupBox1))
            {
                PrintImage(bitmap);
            }
        }

        #endregion
        #region 탭 페이지 1 버튼 클릭시 처리하기 - tabPage1Button_Click(sender, e)

        /// <summary>
        /// 탭 페이지 1 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void tabPage1Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.tabControl1.SelectedIndex;

            this.tabControl1.SelectedIndex = 0;

            using(Bitmap bitmap = GetBitmap(this.tabPage1))
            {
                PrintImage(bitmap);
            }

            this.tabControl1.SelectedIndex = selectedIndex;
        }

        #endregion
        #region 탭 페이지 2 버튼 클릭시 처리하기 - tabPage2Button_Click(sender, e)

        /// <summary>
        /// 탭 페이지 2 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void tabPage2Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;

            this.tabControl1.SelectedIndex = 1;

            using(Bitmap bitmap = GetBitmap(this.tabPage2))
            {
                PrintImage(bitmap);
            }

            this.tabControl1.SelectedIndex = selectedIndex;
        }

        #endregion
        #region 인쇄 문서 페이지 인쇄시 처리하기 - printDocument_PrintPage(sender, e)

        /// <summary>
        /// 인쇄 문서 페이지 인쇄시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int xCenter = e.MarginBounds.X + e.MarginBounds.Width  / 2;
            int yCenter = e.MarginBounds.Y + e.MarginBounds.Height / 2;

            Rectangle rectangle = new Rectangle
            (
                xCenter - printImage.Width  / 2,
                yCenter - printImage.Height / 2,
                printImage.Width,
                printImage.Height
            );

            e.Graphics.InterpolationMode = InterpolationMode.High;

            e.Graphics.DrawImage(printImage, rectangle);
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////// Function

        #region 비트맵 구하기 - GetBitmap(control)

        /// <summary>
        /// 비트맵 구하기
        /// </summary>
        /// <param name="control">컨트롤</param>
        /// <returns>비트맵</returns>
        private Bitmap GetBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);

            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));

            return bitmap;
        }

        #endregion
        #region 클라이언트 영역 비트맵 구하기 - GetClientAreaBitmap(form)

        /// <summary>
        /// 클라이언트 영역 비트맵 구하기
        /// </summary>
        /// <param name="form">폼</param>
        /// <returns>클라이언트 영역 비트맵</returns>
        private Bitmap GetClientAreaBitmap(Form form)
        {
            using(Bitmap formBitmap = GetBitmap(form))
            {
                Point originPoint = form.PointToScreen(new Point(0, 0));

                int dx = originPoint.X - form.Left;
                int dy = originPoint.Y - form.Top;

                int width  = form.ClientSize.Width;
                int height = form.ClientSize.Height;

                Bitmap bitmap = new Bitmap(width, height);

                using(Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawImage
                    (
                        formBitmap,
                        0,
                        0,
                        new Rectangle(dx, dy, width, height),
                        GraphicsUnit.Pixel
                    );
                }

                return bitmap;
            }
        }

        #endregion
        #region 이미지 인쇄하기 - PrintImage(image)

        /// <summary>
        /// 이미지 인쇄하기
        /// </summary>
        /// <param name="image">이미지</param>
        private void PrintImage(Image image)
        {
            this.printImage = image;

            this.printPreviewDialog.ShowDialog();
        }

        #endregion
    }
}