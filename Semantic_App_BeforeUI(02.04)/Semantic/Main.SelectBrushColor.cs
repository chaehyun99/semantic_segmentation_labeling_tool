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

    public partial class UI_Main : Form
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
        public static class ColorTable
        {

            //c#에 맞게 사용. 색상표는 r언어나 matlab에서 쓰는것 중에 적당히
            public static int Entry_Length = 40;

            public static Color[] Entry =
            {
                Color.FromArgb(0, 0, 0),            // 0=background    
                Color.FromArgb(128, 0, 0),          // 1=aeroplane     
                Color.FromArgb(0, 128, 0),          // 2=bicycle       
                Color.FromArgb(128, 128, 0),        // 3=bird          
                Color.FromArgb(0, 0, 128),          // 4=boat          
                Color.FromArgb(128, 0, 128),        // 5=bottle        
                Color.FromArgb(0, 128, 128),        // 6=bus           
                Color.FromArgb(128, 128, 128),      // 7=car           
                Color.FromArgb(0, 255, 255),      // 8=cat           
                Color.FromArgb(192, 0, 0),          // 9=chair         
                Color.FromArgb(64, 128, 0),         // 10=cow          
                Color.FromArgb(192, 128, 0),        // 11=dining table 
                Color.FromArgb(64, 0, 128),         // 12=dog          
                Color.FromArgb(192, 0, 128),        // 13=horse        
                Color.FromArgb(64, 128, 128),       // 14=motorbike    
                Color.FromArgb(192, 128, 128),      // 15=person       
                Color.FromArgb(0, 64, 0),           // 16=potted plant 
                Color.FromArgb(128, 64, 0),         // 17=sheep        
                Color.FromArgb(0, 192, 0),          // 18=sofa         
                Color.FromArgb(128, 192, 0),        // 19=train        
                Color.FromArgb(0, 64, 128),          // 20=tv/monitor
                
                //규격외 파일 테스트용 더미 컬러
                Color.FromArgb(0,0, 64),            // 0=background    
                Color.FromArgb(128, 0, 64),          // 1=aeroplane     
                Color.FromArgb(0, 128, 64),          // 2=bicycle       
                Color.FromArgb(128, 128, 64),        // 3=bird          
                Color.FromArgb(0, 192, 128),          // 4=boat          
                Color.FromArgb(128, 64, 128),        // 5=bottle        
                Color.FromArgb(0, 128, 128),        // 6=bus           
                Color.FromArgb(128, 0, 128),      // 7=car           
                Color.FromArgb(255, 255, 255),      // 8=cat           
                Color.FromArgb(192, 0, 0),          // 9=chair         
                Color.FromArgb(64, 128, 0),         // 10=cow          
                Color.FromArgb(192, 128, 0),        // 11=dining table 
                Color.FromArgb(64, 0, 128),         // 12=dog          
                Color.FromArgb(192, 0, 128),        // 13=horse        
                Color.FromArgb(64, 128, 128),       // 14=motorbike    
                Color.FromArgb(192, 128, 128),      // 15=person       
                Color.FromArgb(0, 64, 0),           // 16=potted plant 
                Color.FromArgb(128, 64, 0),         // 17=sheep        
                Color.FromArgb(0, 192, 0),          // 18=sofa         
                Color.FromArgb(128, 192, 0),        // 19=train        
                Color.FromArgb(0, 64, 128),

            };
            /*
            public static Color[] Entry_byKnownName =
             {
                //20번까지는 python 모델에서 사용하는 클래스 순서 그대로 사용.
                Color.Black,       // 0=background
                Color.LightPink,        // 1=aeroplane
                Color.DarkGreen,       //2=bicycle
                Color.LightBlue,        // 3=bird
                Color.LawnGreen,        // 4=boat
                Color.Lavender,         // 5=bottle
                Color.Khaki,            // 6=bus
                Color.Ivory,            // 7=car
                Color.IndianRed,        // 8=cat
                Color.HotPink,          // 9=chair
                Color.Lime,             // 10=cow
                Color.LightCoral,        // 11=dining table
                Color.LightSalmon,      // 12=dog
                Color.SlateBlue,        // 13=horse
                Color.OrangeRed,        // 14=motorbike
                Color.Yellow,           // 15=person
                Color.Navy,             // 16=potted plant
                Color.Wheat,            // 17=sheep
                Color.MediumTurquoise,  // 18=sofa
                Color.Magenta,          // 19=train
                Color.Gray,             // 20=tv/monitor
                ///이하 규격외 이미지 대상 테스트 전용.
                ///ColorTable의 인덱스 관련된 오류가 있을때 주석 풀고 테스트.
                Color.Tan,
                Color.Aqua,
                Color.DarkCyan,
                Color.DarkKhaki,
                Color.LemonChiffon,
                Color.DeepPink,
                Color.Pink,
                Color.LightCoral,
                Color.AntiqueWhite,
                Color.AliceBlue, //30        
                Color.DarkCyan,
                Color.BlueViolet,
                Color.Chartreuse,
                Color.DarkOliveGreen,
                Color.Cornsilk,
                Color.DarkOrange,
                Color.Gainsboro,
                Color.Blue,
                Color.Bisque,
                Color.DarkGoldenrod//40
             };
             */
        }
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
            int i = 0;
            foreach (TableLayoutPanel item in this.flowPanel_ColorSelect.Controls)
            {
                //버튼 좌측 색상패널
                item.GetControlFromPosition(0, 0).BackColor = ColorTable.Entry[i];
                
                //버튼 우측 레이블(클래스명)
                item.GetControlFromPosition(1, 0).Text = ((ColorName)i).ToString();

                //버튼 우측 레이블(색상)
                item.GetControlFromPosition(1, 0).BackColor = Color.FromArgb(100, item.GetControlFromPosition(0, 0).BackColor);

                //item.GetControlFromPosition(0, 0).Click += btn_SetColor_Click;
                item.GetControlFromPosition(1, 0).Click += btn_SetColor_Click;
                item.GetControlFromPosition(1, 0).MouseEnter += btn_SetColor_MouseEnter;
                item.GetControlFromPosition(1, 0).MouseLeave += btn_SetColor_MouseLeave;
                //item.BackColor = ColorTable.Entry[i];
                i++;
            }

            //int i = 0;
            //foreach (var btn in button_setColor)
            //{
            //    //btn.bar = 1;
            //    btn.BackColor = ColorTable.Entry[i];
            //    btn.Text = ((ColorName)i).ToString();
            //    btn.Click += btn_SetColor_Click;
            //    btn.MouseEnter += btn_SetColor_MouseEnter;
            //    btn.MouseLeave += btn_SetColor_MouseLeave;
            //    i++;
            //}
        }

        //브러쉬 색상 선택. 버튼의 BackColor를 따라감.
        private void btn_SetColor_Click(object sender, EventArgs e)
        {
            Color btnColor = Color.FromArgb(255, ((Control)sender).BackColor);
            //MessageBox.Show(btnColor.ToString());
            SetBrushColor(RGB2ColorName(btnColor));
        }

        private void btn_SetColor_MouseEnter(object sender, EventArgs e)
        {
            Color btnColor = Color.FromArgb(200, ((Control)sender).BackColor);
            ((Control)sender).BackColor = btnColor;
        }

        private void btn_SetColor_MouseLeave(object sender, EventArgs e)
        {
            Color btnColor = Color.FromArgb(100, ((Control)sender).BackColor);
            ((Control)sender).BackColor = btnColor;
        }
        #endregion

    }
}
