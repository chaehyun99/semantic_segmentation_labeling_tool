
namespace Semantic
{
    partial class Main_form
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
            this.Network_operation = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.uiFp_Image = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.경로설정FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Network_operation
            // 
            this.Network_operation.Location = new System.Drawing.Point(440, 87);
            this.Network_operation.Name = "Network_operation";
            this.Network_operation.Size = new System.Drawing.Size(139, 134);
            this.Network_operation.TabIndex = 16;
            this.Network_operation.Text = "시멘틱 구동";
            this.Network_operation.UseVisualStyleBackColor = true;
            this.Network_operation.Click += new System.EventHandler(this.Network_operation_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.Location = new System.Drawing.Point(440, 255);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1246, 564);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(623, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 134);
            this.button2.TabIndex = 18;
            this.button2.Text = "이미지 저장";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uiFp_Image
            // 
            this.uiFp_Image.AutoScroll = true;
            this.uiFp_Image.Location = new System.Drawing.Point(36, 87);
            this.uiFp_Image.Margin = new System.Windows.Forms.Padding(4);
            this.uiFp_Image.Name = "uiFp_Image";
            this.uiFp_Image.Size = new System.Drawing.Size(381, 938);
            this.uiFp_Image.TabIndex = 19;
            this.uiFp_Image.Paint += new System.Windows.Forms.PaintEventHandler(this.uiFp_Image_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.경로설정FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1871, 33);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 경로설정FToolStripMenuItem
            // 
            this.경로설정FToolStripMenuItem.Name = "경로설정FToolStripMenuItem";
            this.경로설정FToolStripMenuItem.Size = new System.Drawing.Size(125, 29);
            this.경로설정FToolStripMenuItem.Text = "경로 설정(F)";
            this.경로설정FToolStripMenuItem.Click += new System.EventHandler(this.경로설정FToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(808, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 134);
            this.button1.TabIndex = 21;
            this.button1.Text = "Gray2RGB\r\n(한 장)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Gray2RGB_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(980, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 134);
            this.button3.TabIndex = 22;
            this.button3.Text = "RGB2Gray\r\n(한 장)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.RGB2Gray_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1871, 1050);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uiFp_Image);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Network_operation);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main_form";
            this.Text = "Main_form";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Network_operation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel uiFp_Image;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 경로설정FToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}

