namespace WindowsFormsApp4
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UiTxt_Folder = new System.Windows.Forms.TextBox();
            this.UiTxt_File = new System.Windows.Forms.TextBox();
            this.btn_Load = new System.Windows.Forms.Button();
            this.uiFp_Image = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.Location = new System.Drawing.Point(287, 232);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(988, 461);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(728, 738);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 54);
            this.button1.TabIndex = 1;
            this.button1.Text = "이미지 불러오기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Image_Load_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(728, 859);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(266, 54);
            this.button2.TabIndex = 2;
            this.button2.Text = "이미지 저장";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Image_Save_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1009, 738);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(266, 175);
            this.button3.TabIndex = 3;
            this.button3.Text = "컬러 변환";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.RGB);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15F);
            this.label1.Location = new System.Drawing.Point(119, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "폴더";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 15F);
            this.label3.Location = new System.Drawing.Point(59, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "파일이름";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // UiTxt_Folder
            // 
            this.UiTxt_Folder.Location = new System.Drawing.Point(262, 38);
            this.UiTxt_Folder.Name = "UiTxt_Folder";
            this.UiTxt_Folder.Size = new System.Drawing.Size(795, 28);
            this.UiTxt_Folder.TabIndex = 8;
            this.UiTxt_Folder.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // UiTxt_File
            // 
            this.UiTxt_File.Location = new System.Drawing.Point(262, 95);
            this.UiTxt_File.Name = "UiTxt_File";
            this.UiTxt_File.Size = new System.Drawing.Size(932, 28);
            this.UiTxt_File.TabIndex = 9;
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(1121, 38);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(73, 28);
            this.btn_Load.TabIndex = 10;
            this.btn_Load.Text = "찾기";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.button4_Click);
            // 
            // uiFp_Image
            // 
            this.uiFp_Image.AutoScroll = true;
            this.uiFp_Image.Location = new System.Drawing.Point(13, 201);
            this.uiFp_Image.Margin = new System.Windows.Forms.Padding(4);
            this.uiFp_Image.Name = "uiFp_Image";
            this.uiFp_Image.Size = new System.Drawing.Size(267, 712);
            this.uiFp_Image.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 925);
            this.Controls.Add(this.uiFp_Image);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.UiTxt_File);
            this.Controls.Add(this.UiTxt_Folder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UiTxt_Folder;
        private System.Windows.Forms.TextBox UiTxt_File;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.FlowLayoutPanel uiFp_Image;
    }
}

