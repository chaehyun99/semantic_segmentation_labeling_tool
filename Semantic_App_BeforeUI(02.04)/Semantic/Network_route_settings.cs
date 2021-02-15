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
    public partial class Network_route_settings : Form
    {
        FolderBrowserDialog input_file_path = UI_Main.input_file_path;
        FolderBrowserDialog input_file_path_ = new FolderBrowserDialog();

        FolderBrowserDialog gray_file_path = UI_Main.gray_file_path;
        FolderBrowserDialog gray_file_path_ = new FolderBrowserDialog();

        FolderBrowserDialog rgb_file_path = UI_Main.rgb_file_path;
        FolderBrowserDialog rgb_file_path_ = new FolderBrowserDialog();

        public Network_route_settings()
        {
            InitializeComponent();
            Input_image_path.Text = UI_Main.input_file_path.SelectedPath;
            Gray_scale_path.Text = UI_Main.gray_file_path.SelectedPath;
            Rgb_path.Text = UI_Main.rgb_file_path.SelectedPath;
        }

        private void button_Findpath_Input_Click(object sender, EventArgs e)
        {
            if (input_file_path_.ShowDialog() == DialogResult.OK)
            {
                Input_image_path.Text = input_file_path_.SelectedPath;
            }
        }

        private void button_Findpath_Gray_Click(object sender, EventArgs e)
        {
            if (gray_file_path_.ShowDialog() == DialogResult.OK)
            {
                Gray_scale_path.Text = gray_file_path_.SelectedPath;
            }
        }

        private void button_Findpath_Rgb_Click(object sender, EventArgs e)
        {
            if (rgb_file_path_.ShowDialog() == DialogResult.OK)
            {
                Rgb_path.Text = rgb_file_path_.SelectedPath;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Input_image_path.Text = null;
            Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            {
                /*if (input_file_path_.SelectedPath == string.Empty)
                {
                    MessageBox.Show("저장할 입력 경로가 없습니다 !!");
                    return;
                }*/
                UI_Main.whether_to_save_ = true;
                input_file_path.SelectedPath = input_file_path_.SelectedPath;
                input_file_path.SelectedPath = Input_image_path.Text;

                gray_file_path.SelectedPath = gray_file_path_.SelectedPath;
                gray_file_path.SelectedPath = Gray_scale_path.Text;

                rgb_file_path.SelectedPath = rgb_file_path_.SelectedPath;
                rgb_file_path.SelectedPath = Rgb_path.Text;

                Close();
            }
        }
    }
}
