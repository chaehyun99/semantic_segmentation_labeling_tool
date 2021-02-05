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

        //필요시 초깃값 바꿔도됨
        private Color brush_Color = Color.Black;
        public Color Brush_Color { get => brush_Color; set => brush_Color = value; }


        //----------------------------------------------------메서드(기능)


        //----------------------------------------------------이벤트(UI)

        //영빈이형이 만들어논 검은색 버튼 이름 =  button 7.

    }
}
