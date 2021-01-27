using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TestProject
{
    public partial class Form1 : Form
    {
        Bitmap sourceBitmap = null;
        Bitmap cursorBoardBitmap = null;
        Color brush_Color = Color.Black;
        int brush_Size = 3;
        bool isPaint = false;
        private bool isCursor;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sourceBitmap = new Bitmap(this.Width, this.Height);
            cursorBoardBitmap = new Bitmap(this.Width, this.Height);

        }

        private void DrawCursorsOnForm(Cursor cursor)
        {
            // If the form's cursor is not the Hand cursor and the 
            // Current cursor is the Default, Draw the specified 
            // cursor on the form in normal size and twice normal size.
            if (this.Cursor != Cursors.Hand &
              Cursor.Current == Cursors.Default)
            {
                // Draw the cursor stretched.
                Graphics graphics = this.CreateGraphics();
                Rectangle rectangle = new Rectangle(
                  new Point(Cursor.Position.X, Cursor.Position.Y), new Size(cursor.Size.Width * 2,
                  cursor.Size.Height * 2));
                cursor.DrawStretched(graphics, rectangle);

                // Draw the cursor in normal size.
                rectangle.Location = new Point(
                rectangle.Width + rectangle.Location.X,
                  rectangle.Height + rectangle.Location.Y);
                rectangle.Size = cursor.Size;
                cursor.Draw(graphics, rectangle);

            }
        }

        private void drawDot(Bitmap targetBitmap,bool isSwitch, MouseEventArgs e)
        {
            if (isSwitch == false)
            {
                return;
            }
            /*
            Image image = Bitmap.FromFile("pen.png");
            Bitmap bitmap = new Bitmap(image);
            Graphics graphics = Graphics.FromImage(bitmap);
            IntPtr handle = bitmap.GetHicon();
            Cursor penCursor = new Cursor(handle);
            */
            /////////////////////////////////////////////////////////////////
            using (Pen myPen = new Pen(brush_Color, 1))
            using (Graphics g = Graphics.FromImage(targetBitmap))
            {
                Brush aBrush = new SolidBrush(brush_Color);
                
                /* //Create Proper Circle */
                Rectangle rectDot = new Rectangle(e.Location.X, e.Location.Y, brush_Size + 1, brush_Size + 1);

                if (brush_Size == 1)
                {
                    rectDot = new Rectangle(e.Location.X - (brush_Size) / 2, e.Location.Y - (brush_Size) / 2, brush_Size, brush_Size);
                    g.FillRectangle(aBrush, rectDot);
                }
                else if (brush_Size == 2)
                {
                    rectDot = new Rectangle(e.Location.X - (brush_Size) / 2, e.Location.Y - (brush_Size) / 2, brush_Size - 1, brush_Size - 1);
                    g.DrawRectangle(myPen, rectDot);
                }
                else
                {
                    g.Clear(Color.Transparent);
                    g.FillEllipse(aBrush, rectDot);
                }
                this.Refresh();
                pictureBox1.Refresh();
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //DrawCursorsOnForm(this.Cursor);
            isPaint = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isPaint = true;
            drawDot(sourceBitmap, isPaint ,e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //DrawCursorsOnForm(this.Cursor);
            drawDot(sourceBitmap, isPaint,e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle targetImgRect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(this.sourceBitmap, targetImgRect);

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Rectangle targetImgRect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(this.cursorBoardBitmap, targetImgRect);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            isCursor = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            isCursor = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            drawDot(cursorBoardBitmap, isCursor, e);

        }

    }
}
