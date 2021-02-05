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
        #region Field
        private enum ColorName
        {
            Background,     //0
            Aeroplane,      //1
            Bicycle,        //2
            Bird,           //3
            Boat,           //4
            Bottle,         //5
            Bus,            //6
            Car,            //7
            Cat,            //8
            Chair,          //9
            Cow,            //10
            DiningTable,    //11
            Dog,            //12
            Horse,          //13
            Motorbike,      //14
            Person,         //15
            PottedPlant,    //16
            Sheep,          //17
            Sofa,           //18
            Train,          //19
            TV_Monitor      //20
        }
        Button[] button_setColor = new Button[21];
        #endregion

        //----------------------------------------------------메서드(기능)
        #region method
        private ColorName RGB2ColorName(Color toFind)
        {
            for (int i = 0; i < ColorTable.Entry_Length; i++)
                if (ColorTable.Entry[i].ToArgb().Equals(toFind.ToArgb()))
                    return (ColorName)i;

            return (ColorName)ColorTable.Entry_Length;
        }

        private void SetBrushColor(ColorName colorName)
        {
            brush_Color = ColorTable.Entry[(int)colorName];
        }
        #endregion

        //----------------------------------------------------이벤트(UI)
        #region Event
        //브러쉬 색생 버튼 초기화. Main_form_Load에서 1회 호출.
        private void InitBtnBrushColor()
        {
            button_setColor[0] = button_setColor0;
            button_setColor[1] = button_setColor1;
            button_setColor[2] = button_setColor2;
            button_setColor[3] = button_setColor3;
            button_setColor[4] = button_setColor4;
            button_setColor[5] = button_setColor5;
            button_setColor[6] = button_setColor6;
            button_setColor[7] = button_setColor7;
            button_setColor[8] = button_setColor8;
            button_setColor[9] = button_setColor9;
            button_setColor[10] = button_setColor10;
            button_setColor[11] = button_setColor11;
            button_setColor[12] = button_setColor12;
            button_setColor[13] = button_setColor13;
            button_setColor[14] = button_setColor14;
            button_setColor[15] = button_setColor15;
            button_setColor[16] = button_setColor16;
            button_setColor[17] = button_setColor17;
            button_setColor[18] = button_setColor18;
            button_setColor[19] = button_setColor19;
            button_setColor[20] = button_setColor20;

            int i = 0;
            foreach (var btn in button_setColor)
            {
                //btn.bar = 1;
                btn.BackColor = ColorTable.Entry[i];
                btn.Text = ((ColorName)i).ToString();
                btn.Click += btn_SetColor_Click;
                btn.MouseEnter += btn_SetColor_MouseEnter;
                btn.MouseLeave += btn_SetColor_MouseLeave;
                i++;
            }
        }

        //브러쉬 색상 선택. 버튼의 BackColor를 따라감.
        private void btn_SetColor_Click(object sender, EventArgs e)
        {
            Color btnColor = ((Button)sender).BackColor;
            SetBrushColor(RGB2ColorName(btnColor));
        }

        private void btn_SetColor_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btn_SetColor_MouseLeave(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
