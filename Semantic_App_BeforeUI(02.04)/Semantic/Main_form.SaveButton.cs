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

        //이건 어디까지 됬다가 안된건지 몰라서 그대로 긁어왔습니다.


        public static FolderBrowserDialog GetSaveFolderDialog()
        {
            FolderBrowserDialog saveFolderDig = new FolderBrowserDialog();
            saveFolderDig.RootFolder = Environment.SpecialFolder.Desktop;

            return saveFolderDig;
        }

        private void Image_Save()
        {
            if (null == rgb_imglist || 0 == rgb_imglist.Count)
            {
                MessageBox.Show("저장 할 이미지가 없습니다 !! ");
                return;
            }

            if (sourceBitmapRgb == null)
            {
                MessageBox.Show("수정 된 이미지가 없습니다 !! ");
                return;
            }

            if (gray_file_path.SelectedPath == string.Empty)
            {
                MessageBox.Show("그레이 스케일 저장 경로가 없습니다.");
                Network_route_settings();
                return;
            }

            // index 넣어서 저장
            //rgb_imglist[index] = sourceBitmapRgb.;
            MessageBox.Show(current_idx.ToString());
            rgb_imglist[current_idx] = new Bitmap(sourceBitmapRgb);

            for (int index = 0; index < rgb_imglist.Count(); index++)
            {
                gray_imglist[index] = RGB2Gray_Click(rgb_imglist[index]);
                gray_imglist[index].Save(gray_file_path.SelectedPath + imgList[index].Remove(imgList[0].Count() - 4, 4) + "_1gray_img.png");
            }
        }
    }
}
