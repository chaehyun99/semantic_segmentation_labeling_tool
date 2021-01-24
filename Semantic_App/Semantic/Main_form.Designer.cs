
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
            this.uiPanelThumbnail = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.경로설정FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.percenttextBox = new System.Windows.Forms.TextBox();
            this.btn_zoomIn = new System.Windows.Forms.Button();
            this.btn_zoomOut = new System.Windows.Forms.Button();
            this.btn_zoomReset = new System.Windows.Forms.Button();
            this.btn_brushSizeUp = new System.Windows.Forms.Button();
            this.btn_brushSizeDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Network_operation
            // 
            this.Network_operation.Location = new System.Drawing.Point(308, 58);
            this.Network_operation.Margin = new System.Windows.Forms.Padding(2);
            this.Network_operation.Name = "Network_operation";
            this.Network_operation.Size = new System.Drawing.Size(97, 89);
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
            this.pictureBox1.Size = new System.Drawing.Size(872, 376);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(436, 58);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 89);
            this.button2.TabIndex = 18;
            this.button2.Text = "이미지 저장";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uiPanelThumbnail
            // 
            this.uiPanelThumbnail.AutoScroll = true;
            this.uiPanelThumbnail.Location = new System.Drawing.Point(25, 58);
            this.uiPanelThumbnail.Name = "uiPanelThumbnail";
            this.uiPanelThumbnail.Size = new System.Drawing.Size(267, 625);
            this.uiPanelThumbnail.TabIndex = 19;
            this.uiPanelThumbnail.Paint += new System.Windows.Forms.PaintEventHandler(this.uiPanelThumbnail_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.경로설정FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1310, 24);
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
            this.pictureBox2.Size = new System.Drawing.Size(872, 376);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(664, 102);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // percenttextBox
            // 
            this.percenttextBox.Location = new System.Drawing.Point(558, 126);
            this.percenttextBox.Name = "percenttextBox";
            this.percenttextBox.Size = new System.Drawing.Size(100, 21);
            this.percenttextBox.TabIndex = 25;
            this.percenttextBox.TextChanged += new System.EventHandler(this.percenttextBox_TextChanged);
            // 
            // btn_zoomIn
            // 
            this.btn_zoomIn.Location = new System.Drawing.Point(558, 58);
            this.btn_zoomIn.Name = "btn_zoomIn";
            this.btn_zoomIn.Size = new System.Drawing.Size(43, 23);
            this.btn_zoomIn.TabIndex = 26;
            this.btn_zoomIn.Text = "+";
            this.btn_zoomIn.UseVisualStyleBackColor = true;
            this.btn_zoomIn.Click += new System.EventHandler(this.btn_zoomIn_Click);
            // 
            // btn_zoomOut
            // 
            this.btn_zoomOut.Location = new System.Drawing.Point(656, 58);
            this.btn_zoomOut.Name = "btn_zoomOut";
            this.btn_zoomOut.Size = new System.Drawing.Size(43, 23);
            this.btn_zoomOut.TabIndex = 27;
            this.btn_zoomOut.Text = "-";
            this.btn_zoomOut.UseVisualStyleBackColor = true;
            this.btn_zoomOut.Click += new System.EventHandler(this.btn_zoomOut_Click);
            // 
            // btn_zoomReset
            // 
            this.btn_zoomReset.Location = new System.Drawing.Point(607, 58);
            this.btn_zoomReset.Name = "btn_zoomReset";
            this.btn_zoomReset.Size = new System.Drawing.Size(43, 23);
            this.btn_zoomReset.TabIndex = 28;
            this.btn_zoomReset.Text = "100%";
            this.btn_zoomReset.UseVisualStyleBackColor = true;
            this.btn_zoomReset.Click += new System.EventHandler(this.btn_zoomReset_Click);
            // 
            // btn_brushSizeUp
            // 
            this.btn_brushSizeUp.Location = new System.Drawing.Point(790, 58);
            this.btn_brushSizeUp.Name = "btn_brushSizeUp";
            this.btn_brushSizeUp.Size = new System.Drawing.Size(75, 23);
            this.btn_brushSizeUp.TabIndex = 29;
            this.btn_brushSizeUp.Text = "Brush +";
            this.btn_brushSizeUp.UseVisualStyleBackColor = true;
            this.btn_brushSizeUp.Click += new System.EventHandler(this.btn_brushSizeUp_Click);
            // 
            // btn_brushSizeDown
            // 
            this.btn_brushSizeDown.Location = new System.Drawing.Point(790, 87);
            this.btn_brushSizeDown.Name = "btn_brushSizeDown";
            this.btn_brushSizeDown.Size = new System.Drawing.Size(75, 23);
            this.btn_brushSizeDown.TabIndex = 30;
            this.btn_brushSizeDown.Text = "Brush -";
            this.btn_brushSizeDown.UseVisualStyleBackColor = true;
            this.btn_brushSizeDown.Click += new System.EventHandler(this.btn_brushSizeDown_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 700);
            this.Controls.Add(this.btn_brushSizeDown);
            this.Controls.Add(this.btn_brushSizeUp);
            this.Controls.Add(this.btn_zoomReset);
            this.Controls.Add(this.btn_zoomOut);
            this.Controls.Add(this.btn_zoomIn);
            this.Controls.Add(this.percenttextBox);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.uiPanelThumbnail);
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
        private System.Windows.Forms.FlowLayoutPanel uiPanelThumbnail;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 경로설정FToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox percenttextBox;
        private System.Windows.Forms.Button btn_zoomIn;
        private System.Windows.Forms.Button btn_zoomOut;
        private System.Windows.Forms.Button btn_zoomReset;
        private System.Windows.Forms.Button btn_brushSizeUp;
        private System.Windows.Forms.Button btn_brushSizeDown;
    }
}

