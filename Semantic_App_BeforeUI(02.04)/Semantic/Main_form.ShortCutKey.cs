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
        //----------------------------------------------------필드(변수)


        //----------------------------------------------------메서드(기능)


        //----------------------------------------------------이벤트(UI)

        //가급적 이미 있는 함수 호출만 해주세요.

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
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

            if (e.KeyCode == Keys.D3)
            {
                SetBrushColor(ColorName.Car); 
            }
            if (e.KeyCode == Keys.D1)
            {
                SetBrushColor(ColorName.Bicycle);
            }
            if (e.KeyCode == Keys.D2)
            {
                SetBrushColor(ColorName.Person);
            }

            if (e.KeyCode == Keys.F) button1_Click(sender, e);
            if (e.KeyCode == Keys.Q) Network_operation_Click(sender, e);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.Z)) UNDO(sender, e);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.Y)) REDO(sender, e);
            if (this.ctrlKeyDown && (e.KeyCode == Keys.S)) button2_Click_1(sender, e);
            if (e.KeyCode == Keys.B) button_setPaintmode_Click(sender, e);
            if (e.KeyCode == Keys.E)// button6_Click(sender, e);->brush색깔 0으로 해주면됨.
            if (e.KeyCode == Keys.I) button3_Click(sender, e);

            
            //이 변수은 MouseWheel관련된거라서 무시하고 작업하시면 됩니다.
            this.ctrlKeyDown = e.Control;
        }

    }
}
