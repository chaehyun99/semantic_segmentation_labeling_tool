using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void button_RunModel_Click(object sender, EventArgs e)
        {

        }

        private void button_Info_Click(object sender, EventArgs e)
        {

        }

        private void button_Undo_Click(object sender, EventArgs e)
        {

        }

        private void button_Redo_Click(object sender, EventArgs e)
        {

        }

        private void button_PaintMode_Click(object sender, EventArgs e)
        {

        }

        private void button_ScrollMode_Click(object sender, EventArgs e)
        {

        }

        private void button_ZoomIn_Click(object sender, EventArgs e)
        {

        }

        private void button_ZoomReset_Click(object sender, EventArgs e)
        {

        }

        private void button_ZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void flowPanel_ColorSelect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Class0_Click(object sender, EventArgs e)
        {

        }

        private void tablePanel_Color_20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer_MenuBar_Left_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}


