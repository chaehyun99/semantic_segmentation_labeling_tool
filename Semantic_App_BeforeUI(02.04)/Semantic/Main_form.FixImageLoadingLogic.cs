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

// TODO: 오류나는 지점 찾아서 로직 수정.
// 문제상황: 
//              이미지 로딩이후, 시맨틱 구동(+RGB변환) 이후
//          1. 썸네일을 클릭하지 않으면 투명도가 적용되지않은 상태거나
//          2. 썸네일을 클릭해도 곧바로 갱신안되고 다른 움직임을 취해야 바뀜.

//전체 코드 보면서
//refresh()호출되는 부분 최대한 확인해주세요.

// 가장 유력한 로직 오류지점:  PBoxThumbnail_Click()
// Undo/Redo 작업 끝났으니까 수정해도 됨.
// 




namespace Semantic
{
    public partial class Main_form : Form
    {

        //----------------------------------------------------필드(변수)
       
        //----------------------------------------------------메서드(기능)       
        
        //----------------------------------------------------이벤트(UI)
    }
}
