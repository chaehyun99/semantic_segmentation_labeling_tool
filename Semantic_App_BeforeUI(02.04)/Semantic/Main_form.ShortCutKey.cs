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
            if (e.KeyCode == Keys.F) Network_route_settings();
            
            

            //이 변수은 MouseWheel관련된거라서 무시하고 작업하시면 됩니다.
            this.ctrlKeyDown = e.Control;
        }

    }
}
