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


        /////////////// Undo,Redo 작동방식//////
        /// 
        ///1.1. 제일 최근에 수정한 이미지는 항상 stackUndo에 저장되있고 pictureBox2에도 떠있다.
        ///
        ///2.1. Undo()를 호출하면 stackUndo의 last를 복제해서 stackRedo에 삽입한다.
        ///2.2. 그 다음 stackUndo의 새로운 last을 (pictureBox2)에 띄운다.
        ///
        ///etc. '이미지를 띄운다'고 함은 sourceBitmap(혹은 Original_opac)를 바꿔준 뒤 
        ///     pictureBox2.Refresh()를 통해 pictureBox_Paint 이벤트를 발생시키는 것.
        /// 
        ///3.1. 마우스 이벤트(그리기)를 통해 stackUndo에 스냅샷이 추가 될때, redoStack은 비운다.(저장할 이유가 없다)
        ///
        ///4.1. Redo()를 호출하면 stackRedo의 Last를 복제해서 stackUndo에 삽입한다.
        ///4.2. 그 다음 stackUndo의 새로운 Last를 pictureBox2에 띄운다..
        ///
        ///5.1. 다른썸네일을 클릭하는 순간(이미지를 새로 불러올때)마다 두 저장소를 비워준 뒤 stackUndo에 원본이미지를 복제해서 삽입한다. 
        ///////////////////
        ///


        //----------------------------------------------------필드(변수)

        LinkedList<Bitmap> stackUndo = new LinkedList<Bitmap>();
        LinkedList<Bitmap> stackRedo = new LinkedList<Bitmap>();

        /// <summary>
        /// 이 값으로 저장할수 있는 UNDO,REDO횟수의 최댓값 조절.
        /// </summary>
        int _maxHistory_ = 20;               

        //----------------------------------------------------메서드(기능)
        
        /// <summary>
        /// 이미지가 수정될 때마다 stackUndo에 복제본 삽입.
        /// </summary>
        /// <param name="src4Input"></param>
        private void Input_Action(Bitmap src4Input)
        {
            if (null == src4Input)
            {
                return;
            }

            //1.새로운 수정내용이 생겼으므로 기존의 redo스택을 날린다.
            stackRedo.Clear();

            //2.입력받은 액션(src4Input)을 stackUndo에 저장.
            stackUndo.AddLast(new Bitmap(src4Input));

            //ETC.모든 AddLast이후, 저장개수 검사 후 지정한 최댓값을 넘으면 오래된 순으로 제거.
            if (stackUndo.Count > _maxHistory_)
            {
                stackUndo.RemoveFirst();
            }
        }

        private void ClearStack()
        {
            stackUndo.Clear();
            stackRedo.Clear();
        }

        //----------------------------------------------------이벤트(UI)

        private void UNDO(object sender, EventArgs e)
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

            pictureBox2.Refresh();

        }

        private void REDO(object sender, EventArgs e)
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
            pictureBox2.Refresh();

        }

        private void Main_form_Load(object sender, EventArgs e)
        {

            //Undo,Redo 버튼 이벤트 연결
            this.button4.Click += UNDO;
            this.button5.Click += REDO;

            this.ctrlKeyDown = false;

            //제일 앞 컨트롤에 뒤에 덮인 컨트롤의 이벤트 연결.
            this.pBox3_CursorBoard.MouseDown += pictureBox2_MouseDown;
            this.pBox3_CursorBoard.MouseUp += pictureBox2_MouseUp;

            //호출순서 변경(정석은 아닌듯)
            this.pBox3_CursorBoard.MouseMove -= pBox3_CursorBoard_MouseMove;
            this.pBox3_CursorBoard.MouseMove += pictureBox2_MouseMove;
            this.pBox3_CursorBoard.MouseMove += pBox3_CursorBoard_MouseMove;

            this.pBox3_CursorBoard.MouseWheel += pictureBox1_MouseWheel;

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
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

        public void PBoxThumbnail_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.listPanelThumb.Controls.Count; i++)
            {
                if (this.listPanelThumb.Controls[i] is Panel)
                {
                    Panel pnl = this.listPanelThumb.Controls[i] as Panel;
                    pnl.BackColor = Color.Black;
                }
            }

            PictureBox pb = sender as PictureBox;
            pb.Parent.BackColor = Color.Red;

            int idx = Convert.ToInt32(pb.Tag.ToString());
            sourceBitmapOrigin = new Bitmap(input_file_path.SelectedPath + imgList[idx]);

            //픽쳐박스2에 띄워질 비트맵 변경. (+ 커서가 그려질 비트맵 크기조절)
            if (null == rgb_imglist || 0 == rgb_imglist.Count())
            {
                Console.WriteLine("rgb_imglist.Count: " + Convert.ToString(rgb_imglist.Count));
                return;
            }
            else
            {
                sourceBitmapRgb = new Bitmap(rgb_imglist[idx]);

                //커서가 그려질 보드의 비트맵 갱신(크기)
                cursorBoardBitmap = new Bitmap(sourceBitmapRgb.Width, sourceBitmapRgb.Height);
                cursorBoardBitmap.MakeTransparent();

                SetAlpha(decimal.ToInt32(colorSlider1.Value));

                // 픽쳐박스에 올라온 이미지가 바뀔때마다 
                    // 1. 기존 스택을 비우고,
                ClearStack();

                    // 2.UndoStack에 +1을 시도.(rgb변환이 되었든 안되었든)
                Input_Action(sourceBitmapRgb);
                
                RefreshAllPictureBox();

            }

        }
    }
}
