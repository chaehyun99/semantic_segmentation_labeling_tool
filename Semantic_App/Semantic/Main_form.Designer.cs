
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
            this.Button_ZoomIn = new System.Windows.Forms.Button();
            this.Button_ZoomOut = new System.Windows.Forms.Button();
            this.lable_ImgScale = new System.Windows.Forms.Label();
            this.lable_Opacity = new System.Windows.Forms.Label();
            this.button_setscrollmode = new System.Windows.Forms.Button();
            this.button_setPaintmode = new System.Windows.Forms.Button();
            this.button_BrushsizeUp = new System.Windows.Forms.Button();
            this.button_BrushsizeDown = new System.Windows.Forms.Button();
            this.button_ZoomReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Network_operation
            // 
            this.Network_operation.Location = new System.Drawing.Point(324, 74);
            this.Network_operation.Margin = new System.Windows.Forms.Padding(2);
            this.Network_operation.Name = "Network_operation";
            this.Network_operation.Size = new System.Drawing.Size(90, 38);
            this.Network_operation.TabIndex = 16;
            this.Network_operation.Text = "시멘틱 구동";
            this.Network_operation.UseVisualStyleBackColor = true;
            this.Network_operation.Click += new System.EventHandler(this.Network_operation_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(308, 170);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(703, 340);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 75);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 38);
            this.button2.TabIndex = 18;
            this.button2.Text = "이미지 저장";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listPanelThumb
            // 
            this.listPanelThumb.AutoScroll = true;
            this.listPanelThumb.Location = new System.Drawing.Point(25, 58);
            this.listPanelThumb.Name = "listPanelThumb";
            this.listPanelThumb.Size = new System.Drawing.Size(267, 625);
            this.listPanelThumb.TabIndex = 19;
            this.listPanelThumb.Paint += new System.Windows.Forms.PaintEventHandler(this.uiPanelThumbnail_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.경로설정FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1025, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 경로설정FToolStripMenuItem
            // 
            this.경로설정FToolStripMenuItem.Name = "경로설정FToolStripMenuItem";
            this.경로설정FToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.경로설정FToolStripMenuItem.Text = "경로 설정(F)";
            this.경로설정FToolStripMenuItem.Click += new System.EventHandler(this.경로설정FToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(308, 170);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(703, 340);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(926, 91);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Button_ZoomIn
            // 
            this.Button_ZoomIn.Location = new System.Drawing.Point(614, 91);
            this.Button_ZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.Button_ZoomIn.Name = "Button_ZoomIn";
            this.Button_ZoomIn.Size = new System.Drawing.Size(53, 40);
            this.Button_ZoomIn.TabIndex = 25;
            this.Button_ZoomIn.Text = "zoom +";
            this.Button_ZoomIn.UseVisualStyleBackColor = true;
            this.Button_ZoomIn.Click += new System.EventHandler(this.Button_ZoomIn_Click);
            // 
            // Button_ZoomOut
            // 
            this.Button_ZoomOut.Location = new System.Drawing.Point(557, 91);
            this.Button_ZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.Button_ZoomOut.Name = "Button_ZoomOut";
            this.Button_ZoomOut.Size = new System.Drawing.Size(53, 40);
            this.Button_ZoomOut.TabIndex = 25;
            this.Button_ZoomOut.Text = "zoom -";
            this.Button_ZoomOut.UseVisualStyleBackColor = true;
            this.Button_ZoomOut.Click += new System.EventHandler(this.Button_ZoomOut_Click);
            // 
            // lable_ImgScale
            // 
            this.lable_ImgScale.AutoSize = true;
            this.lable_ImgScale.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lable_ImgScale.Location = new System.Drawing.Point(610, 64);
            this.lable_ImgScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable_ImgScale.Name = "lable_ImgScale";
            this.lable_ImgScale.Size = new System.Drawing.Size(57, 12);
            this.lable_ImgScale.TabIndex = 26;
            this.lable_ImgScale.Text = "배율: ? %";
            this.lable_ImgScale.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_ImgScale_Paint);
            // 
            // lable_Opacity
            // 
            this.lable_Opacity.AutoSize = true;
            this.lable_Opacity.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lable_Opacity.Location = new System.Drawing.Point(942, 64);
            this.lable_Opacity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable_Opacity.Name = "lable_Opacity";
            this.lable_Opacity.Size = new System.Drawing.Size(69, 12);
            this.lable_Opacity.TabIndex = 27;
            this.lable_Opacity.Tag = 0;
            this.lable_Opacity.Text = "투명도: ? %";
            this.lable_Opacity.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_Opacity_Paint);
            // 
            // button_setscrollmode
            // 
            this.button_setscrollmode.Location = new System.Drawing.Point(752, 58);
            this.button_setscrollmode.Margin = new System.Windows.Forms.Padding(2);
            this.button_setscrollmode.Name = "button_setscrollmode";
            this.button_setscrollmode.Size = new System.Drawing.Size(67, 37);
            this.button_setscrollmode.TabIndex = 28;
            this.button_setscrollmode.Text = "이동";
            this.button_setscrollmode.UseVisualStyleBackColor = true;
            this.button_setscrollmode.Click += new System.EventHandler(this.button_setscrollmode_Click);
            // 
            // button_setPaintmode
            // 
            this.button_setPaintmode.Location = new System.Drawing.Point(752, 99);
            this.button_setPaintmode.Margin = new System.Windows.Forms.Padding(2);
            this.button_setPaintmode.Name = "button_setPaintmode";
            this.button_setPaintmode.Size = new System.Drawing.Size(67, 37);
            this.button_setPaintmode.TabIndex = 28;
            this.button_setPaintmode.Text = "그리기";
            this.button_setPaintmode.UseVisualStyleBackColor = true;
            this.button_setPaintmode.Click += new System.EventHandler(this.button_setPaintmode_Click);
            // 
            // button_BrushsizeUp
            // 
            this.button_BrushsizeUp.Location = new System.Drawing.Point(838, 100);
            this.button_BrushsizeUp.Margin = new System.Windows.Forms.Padding(2);
            this.button_BrushsizeUp.Name = "button_BrushsizeUp";
            this.button_BrushsizeUp.Size = new System.Drawing.Size(67, 37);
            this.button_BrushsizeUp.TabIndex = 28;
            this.button_BrushsizeUp.Text = "브러시 +";
            this.button_BrushsizeUp.UseVisualStyleBackColor = true;
            this.button_BrushsizeUp.Click += new System.EventHandler(this.button_BrushsizeUp_Click);
            // 
            // button_BrushsizeDown
            // 
            this.button_BrushsizeDown.Location = new System.Drawing.Point(838, 59);
            this.button_BrushsizeDown.Margin = new System.Windows.Forms.Padding(2);
            this.button_BrushsizeDown.Name = "button_BrushsizeDown";
            this.button_BrushsizeDown.Size = new System.Drawing.Size(67, 37);
            this.button_BrushsizeDown.TabIndex = 29;
            this.button_BrushsizeDown.Text = "브러시 -";
            this.button_BrushsizeDown.UseVisualStyleBackColor = true;
            this.button_BrushsizeDown.Click += new System.EventHandler(this.button_BrushsizeDown_Click);
            // 
            // button_ZoomReset
            // 
            this.button_ZoomReset.Location = new System.Drawing.Point(671, 91);
            this.button_ZoomReset.Margin = new System.Windows.Forms.Padding(2);
            this.button_ZoomReset.Name = "button_ZoomReset";
            this.button_ZoomReset.Size = new System.Drawing.Size(53, 40);
            this.button_ZoomReset.TabIndex = 30;
            this.button_ZoomReset.Text = "zoom 100%";
            this.button_ZoomReset.UseVisualStyleBackColor = true;
            this.button_ZoomReset.Click += new System.EventHandler(this.button_ZoomReset_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 520);
            this.Controls.Add(this.button_ZoomReset);
            this.Controls.Add(this.button_BrushsizeDown);
            this.Controls.Add(this.button_setPaintmode);
            this.Controls.Add(this.button_BrushsizeUp);
            this.Controls.Add(this.button_setscrollmode);
            this.Controls.Add(this.lable_Opacity);
            this.Controls.Add(this.lable_ImgScale);
            this.Controls.Add(this.Button_ZoomOut);
            this.Controls.Add(this.Button_ZoomIn);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.listPanelThumb);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Network_operation);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.Button Button_ZoomIn;
        private System.Windows.Forms.Button Button_ZoomOut;
        private System.Windows.Forms.Label lable_ImgScale;
        private System.Windows.Forms.Label lable_Opacity;
        private System.Windows.Forms.Button button_setscrollmode;
        private System.Windows.Forms.Button button_setPaintmode;
        private System.Windows.Forms.Button button_BrushsizeUp;
        private System.Windows.Forms.Button button_BrushsizeDown;
        private System.Windows.Forms.Button button_ZoomReset;
    }
}

