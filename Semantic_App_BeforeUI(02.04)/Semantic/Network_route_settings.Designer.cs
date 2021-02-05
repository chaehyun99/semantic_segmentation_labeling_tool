
namespace Semantic
{
    partial class Network_route_settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Input_image_path = new System.Windows.Forms.TextBox();
            this.Gray_scale_path = new System.Windows.Forms.TextBox();
            this.Rgb_path = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Input_image_path
            // 
            this.Input_image_path.Location = new System.Drawing.Point(222, 73);
            this.Input_image_path.Margin = new System.Windows.Forms.Padding(2);
            this.Input_image_path.Name = "Input_image_path";
            this.Input_image_path.Size = new System.Drawing.Size(414, 21);
            this.Input_image_path.TabIndex = 10;
            // 
            // Gray_scale_path
            // 
            this.Gray_scale_path.Location = new System.Drawing.Point(222, 177);
            this.Gray_scale_path.Margin = new System.Windows.Forms.Padding(2);
            this.Gray_scale_path.Name = "Gray_scale_path";
            this.Gray_scale_path.Size = new System.Drawing.Size(414, 21);
            this.Gray_scale_path.TabIndex = 11;
            // 
            // Rgb_path
            // 
            this.Rgb_path.Location = new System.Drawing.Point(222, 284);
            this.Rgb_path.Margin = new System.Windows.Forms.Padding(2);
            this.Rgb_path.Name = "Rgb_path";
            this.Rgb_path.Size = new System.Drawing.Size(414, 21);
            this.Rgb_path.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(673, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "찾아보기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(673, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "찾아보기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(673, 287);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "찾아보기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15F);
            this.label1.Location = new System.Drawing.Point(4, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "입력 이미지 경로";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15F);
            this.label2.Location = new System.Drawing.Point(4, 178);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Grayscal 저장 경로";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 15F);
            this.label3.Location = new System.Drawing.Point(4, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Rgb 이미지 저장 경로";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(244, 360);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 42);
            this.button4.TabIndex = 19;
            this.button4.Text = "취소";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(479, 360);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(105, 42);
            this.Save.TabIndex = 20;
            this.Save.Text = "저장";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Network_route_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Rgb_path);
            this.Controls.Add(this.Gray_scale_path);
            this.Controls.Add(this.Input_image_path);
            this.Name = "Network_route_settings";
            this.ShowInTaskbar = false;
            this.Text = "Network_route_settings";
            this.Load += new System.EventHandler(this.Network_route_settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input_image_path;
        private System.Windows.Forms.TextBox Gray_scale_path;
        private System.Windows.Forms.TextBox Rgb_path;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Save;
    }
}