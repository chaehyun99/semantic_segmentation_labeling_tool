
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
            this.listPanelThumb = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.경로설정FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lable_ImgScale = new System.Windows.Forms.Label();
            this.lable_Opacity = new System.Windows.Forms.Label();
            this.Button_ScrollMode = new System.Windows.Forms.Button();
            this.Button_PaintMode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(440, 255);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1245, 563);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
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
            // listPanelThumb
            // 
            this.listPanelThumb.AutoScroll = true;
            this.listPanelThumb.Location = new System.Drawing.Point(36, 87);
            this.listPanelThumb.Margin = new System.Windows.Forms.Padding(4);
            this.listPanelThumb.Name = "listPanelThumb";
            this.listPanelThumb.Size = new System.Drawing.Size(381, 938);
            this.listPanelThumb.TabIndex = 19;
            this.listPanelThumb.Paint += new System.Windows.Forms.PaintEventHandler(this.uiPanelThumbnail_Paint);
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
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(440, 255);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1245, 563);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1502, 127);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(149, 69);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(827, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 50);
            this.button1.TabIndex = 25;
            this.button1.Text = "zoom +";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_ZoomIn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(827, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 50);
            this.button3.TabIndex = 25;
            this.button3.Text = "zoom -";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button_ZoomOut_Click);
            // 
            // lable_ImgScale
            // 
            this.lable_ImgScale.AutoSize = true;
            this.lable_ImgScale.Location = new System.Drawing.Point(861, 145);
            this.lable_ImgScale.Name = "lable_ImgScale";
            this.lable_ImgScale.Size = new System.Drawing.Size(87, 18);
            this.lable_ImgScale.TabIndex = 26;
            this.lable_ImgScale.Text = "배율: ? %";
            this.lable_ImgScale.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_ImgScale_Paint);
            // 
            // lable_Opacity
            // 
            this.lable_Opacity.AutoSize = true;
            this.lable_Opacity.Location = new System.Drawing.Point(1530, 171);
            this.lable_Opacity.Name = "lable_Opacity";
            this.lable_Opacity.Size = new System.Drawing.Size(105, 18);
            this.lable_Opacity.TabIndex = 27;
            this.lable_Opacity.Tag = 0;
            this.lable_Opacity.Text = "투명도: ? %";
            this.lable_Opacity.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_Opacity_Paint);
            // 
            // Button_ScrollMode
            // 
            this.Button_ScrollMode.Location = new System.Drawing.Point(1026, 91);
            this.Button_ScrollMode.Name = "Button_ScrollMode";
            this.Button_ScrollMode.Size = new System.Drawing.Size(100, 43);
            this.Button_ScrollMode.TabIndex = 28;
            this.Button_ScrollMode.Text = "이동 모드";
            this.Button_ScrollMode.UseVisualStyleBackColor = true;
            this.Button_ScrollMode.Click += new System.EventHandler(this.Button_ScrollMode_Click);
            // 
            // Button_PaintMode
            // 
            this.Button_PaintMode.Location = new System.Drawing.Point(1026, 171);
            this.Button_PaintMode.Name = "Button_PaintMode";
            this.Button_PaintMode.Size = new System.Drawing.Size(100, 43);
            this.Button_PaintMode.TabIndex = 28;
            this.Button_PaintMode.Text = "수정 모드";
            this.Button_PaintMode.UseVisualStyleBackColor = true;
            this.Button_PaintMode.Click += new System.EventHandler(this.Button_PaintMode_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1871, 1050);
            this.Controls.Add(this.Button_PaintMode);
            this.Controls.Add(this.Button_ScrollMode);
            this.Controls.Add(this.lable_Opacity);
            this.Controls.Add(this.lable_ImgScale);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.listPanelThumb);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Network_operation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel listPanelThumb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 경로설정FToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lable_ImgScale;
        private System.Windows.Forms.Label lable_Opacity;
        private System.Windows.Forms.Button Button_ScrollMode;
        private System.Windows.Forms.Button Button_PaintMode;
    }
}

