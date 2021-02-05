using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Semantic
{
    public partial class Network_route_settings : Form
    {
        FolderBrowserDialog input_file_path = Main_form.input_file_path;
        FolderBrowserDialog input_file_path_ = new FolderBrowserDialog();

        FolderBrowserDialog gray_file_path = Main_form.gray_file_path;
        FolderBrowserDialog gray_file_path_ = new FolderBrowserDialog();

        FolderBrowserDialog rgb_file_path = Main_form.rgb_file_path;
        FolderBrowserDialog rgb_file_path_ = new FolderBrowserDialog();


        public Network_route_settings()
        {
            InitializeComponent();
            Input_image_path.Text = Main_form.input_file_path.SelectedPath;
            Gray_scale_path.Text = Main_form.gray_file_path.SelectedPath;
            Rgb_path.Text = Main_form.rgb_file_path.SelectedPath;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (input_file_path_.ShowDialog() == DialogResult.OK)
            {
                Input_image_path.Text = input_file_path_.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gray_file_path_.ShowDialog() == DialogResult.OK)
            {
                Gray_scale_path.Text = gray_file_path_.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rgb_file_path_.ShowDialog() == DialogResult.OK)
            {
                Rgb_path.Text = rgb_file_path_.SelectedPath;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Input_image_path.Text = null;
            Close();
        }

        private void Network_route_settings_Load(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            /*if (input_file_path_.SelectedPath == string.Empty)
            {
                MessageBox.Show("저장할 입력 경로가 없습니다 !!");
                return;
            }*/
            Main_form.whether_to_save_ = true; 
            input_file_path.SelectedPath = input_file_path_.SelectedPath;
            input_file_path.SelectedPath = Input_image_path.Text;

            gray_file_path.SelectedPath = gray_file_path_.SelectedPath;
            gray_file_path.SelectedPath = Gray_scale_path.Text;

            rgb_file_path.SelectedPath = rgb_file_path_.SelectedPath;
            rgb_file_path.SelectedPath = Rgb_path.Text;

            Close();
        }

        private void Input_image_path_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
