
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
            this.listPanelThumb = new System.Windows.Forms.FlowLayoutPanel();
            this.button_setscrollmode = new System.Windows.Forms.Button();
            this.button_BrushsizeUp = new System.Windows.Forms.Button();
            this.button_BrushsizeDown = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Button_ZoomIn = new System.Windows.Forms.Button();
            this.Button_ZoomOut = new System.Windows.Forms.Button();
            this.Network_operation = new System.Windows.Forms.Button();
            this.button_setPaintmode = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lable_Opacity = new System.Windows.Forms.Label();
            this.lable_ImgScale = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pBox3_CursorBoard = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button_ZoomReset = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox3_CursorBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // listPanelThumb
            // 
            this.listPanelThumb.AutoScroll = true;
            this.listPanelThumb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.listPanelThumb.Location = new System.Drawing.Point(0, 75);
            this.listPanelThumb.Name = "listPanelThumb";
            this.listPanelThumb.Size = new System.Drawing.Size(264, 772);
            this.listPanelThumb.TabIndex = 19;
            this.listPanelThumb.Paint += new System.Windows.Forms.PaintEventHandler(this.uiPanelThumbnail_Paint);
            // 
            // button_setscrollmode
            // 
            this.button_setscrollmode.Location = new System.Drawing.Point(1058, 34);
            this.button_setscrollmode.Margin = new System.Windows.Forms.Padding(2);
            this.button_setscrollmode.Name = "button_setscrollmode";
            this.button_setscrollmode.Size = new System.Drawing.Size(67, 37);
            this.button_setscrollmode.TabIndex = 28;
            this.button_setscrollmode.Text = "이동";
            this.button_setscrollmode.UseVisualStyleBackColor = true;
            this.button_setscrollmode.Click += new System.EventHandler(this.button_setscrollmode_Click);
            // 
            // button_BrushsizeUp
            // 
            this.button_BrushsizeUp.Location = new System.Drawing.Point(741, 34);
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
            this.button_BrushsizeDown.Location = new System.Drawing.Point(824, 35);
            this.button_BrushsizeDown.Margin = new System.Windows.Forms.Padding(2);
            this.button_BrushsizeDown.Name = "button_BrushsizeDown";
            this.button_BrushsizeDown.Size = new System.Drawing.Size(67, 37);
            this.button_BrushsizeDown.TabIndex = 29;
            this.button_BrushsizeDown.Text = "브러시 -";
            this.button_BrushsizeDown.UseVisualStyleBackColor = true;
            this.button_BrushsizeDown.Click += new System.EventHandler(this.button_BrushsizeDown_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Button_ZoomIn);
            this.panel1.Controls.Add(this.Button_ZoomOut);
            this.panel1.Controls.Add(this.Network_operation);
            this.panel1.Controls.Add(this.button_ZoomReset);
            this.panel1.Controls.Add(this.button_setscrollmode);
            this.panel1.Controls.Add(this.button_BrushsizeDown);
            this.panel1.Controls.Add(this.button_setPaintmode);
            this.panel1.Controls.Add(this.button_BrushsizeUp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1717, 80);
            this.panel1.TabIndex = 33;
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::Semantic.Properties.Resources.eraser;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(350, 23);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(55, 55);
            this.button6.TabIndex = 35;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::Semantic.Properties.Resources.redo2;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(241, 30);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 40);
            this.button5.TabIndex = 34;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::Semantic.Properties.Resources.iconfinder_restore_2460287;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(187, 30);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 33;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::Semantic.Properties.Resources.info;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(128, 30);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 40);
            this.button3.TabIndex = 32;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Semantic.Properties.Resources.iconfinder_Folder_Place_File_Storage_Paper_Office_1343439;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(11, 24);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 31;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Semantic.Properties.Resources.iconfinder_document_file_paper_page_07_2850900;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(597, 30);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 18;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Button_ZoomIn
            // 
            this.Button_ZoomIn.BackgroundImage = global::Semantic.Properties.Resources.zoom_in;
            this.Button_ZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Button_ZoomIn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.Button_ZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ZoomIn.Location = new System.Drawing.Point(409, 30);
            this.Button_ZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.Button_ZoomIn.Name = "Button_ZoomIn";
            this.Button_ZoomIn.Size = new System.Drawing.Size(40, 40);
            this.Button_ZoomIn.TabIndex = 25;
            this.Button_ZoomIn.UseVisualStyleBackColor = true;
            this.Button_ZoomIn.Click += new System.EventHandler(this.Button_ZoomIn_Click);
            // 
            // Button_ZoomOut
            // 
            this.Button_ZoomOut.BackgroundImage = global::Semantic.Properties.Resources.zoom_out;
            this.Button_ZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Button_ZoomOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.Button_ZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ZoomOut.Location = new System.Drawing.Point(463, 30);
            this.Button_ZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.Button_ZoomOut.Name = "Button_ZoomOut";
            this.Button_ZoomOut.Size = new System.Drawing.Size(40, 40);
            this.Button_ZoomOut.TabIndex = 25;
            this.Button_ZoomOut.UseVisualStyleBackColor = true;
            this.Button_ZoomOut.Click += new System.EventHandler(this.Button_ZoomOut_Click);
            // 
            // Network_operation
            // 
            this.Network_operation.BackgroundImage = global::Semantic.Properties.Resources.segmentation;
            this.Network_operation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Network_operation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.Network_operation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Network_operation.Location = new System.Drawing.Point(71, 25);
            this.Network_operation.Margin = new System.Windows.Forms.Padding(2);
            this.Network_operation.Name = "Network_operation";
            this.Network_operation.Size = new System.Drawing.Size(40, 40);
            this.Network_operation.TabIndex = 16;
            this.Network_operation.UseVisualStyleBackColor = true;
            this.Network_operation.Click += new System.EventHandler(this.Network_operation_Click);
            // 
            // button_setPaintmode
            // 
            this.button_setPaintmode.BackgroundImage = global::Semantic.Properties.Resources.iconfinder_17_Brush_290133;
            this.button_setPaintmode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_setPaintmode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button_setPaintmode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_setPaintmode.Location = new System.Drawing.Point(295, 30);
            this.button_setPaintmode.Margin = new System.Windows.Forms.Padding(2);
            this.button_setPaintmode.Name = "button_setPaintmode";
            this.button_setPaintmode.Size = new System.Drawing.Size(40, 40);
            this.button_setPaintmode.TabIndex = 28;
            this.button_setPaintmode.UseVisualStyleBackColor = true;
            this.button_setPaintmode.Click += new System.EventHandler(this.button_setPaintmode_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(1454, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 767);
            this.panel2.TabIndex = 34;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::Semantic.Properties.Resources.위;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Controls.Add(this.trackBar1);
            this.panel3.Controls.Add(this.lable_Opacity);
            this.panel3.Controls.Add(this.lable_ImgScale);
            this.panel3.Location = new System.Drawing.Point(15, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 248);
            this.panel3.TabIndex = 2;
            // 
            // lable_Opacity
            // 
            this.lable_Opacity.AutoSize = true;
            this.lable_Opacity.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lable_Opacity.Location = new System.Drawing.Point(63, 18);
            this.lable_Opacity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable_Opacity.Name = "lable_Opacity";
            this.lable_Opacity.Size = new System.Drawing.Size(69, 12);
            this.lable_Opacity.TabIndex = 27;
            this.lable_Opacity.Tag = 0;
            this.lable_Opacity.Text = "투명도: ? %";
            this.lable_Opacity.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_Opacity_Paint);
            // 
            // lable_ImgScale
            // 
            this.lable_ImgScale.AutoSize = true;
            this.lable_ImgScale.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lable_ImgScale.Location = new System.Drawing.Point(136, 18);
            this.lable_ImgScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable_ImgScale.Name = "lable_ImgScale";
            this.lable_ImgScale.Size = new System.Drawing.Size(57, 12);
            this.lable_ImgScale.TabIndex = 26;
            this.lable_ImgScale.Text = "배율: ? %";
            this.lable_ImgScale.Paint += new System.Windows.Forms.PaintEventHandler(this.lable_ImgScale_Paint);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::Semantic.Properties.Resources.아래;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel4.Controls.Add(this.button7);
            this.panel4.Location = new System.Drawing.Point(15, 300);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 396);
            this.panel4.TabIndex = 1;
            // 
            // pBox3_CursorBoard
            // 
            this.pBox3_CursorBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBox3_CursorBoard.BackColor = System.Drawing.Color.Gainsboro;
            this.pBox3_CursorBoard.Location = new System.Drawing.Point(295, 107);
            this.pBox3_CursorBoard.Margin = new System.Windows.Forms.Padding(2);
            this.pBox3_CursorBoard.Name = "pBox3_CursorBoard";
            this.pBox3_CursorBoard.Size = new System.Drawing.Size(1125, 690);
            this.pBox3_CursorBoard.TabIndex = 32;
            this.pBox3_CursorBoard.TabStop = false;
            this.pBox3_CursorBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pBox3_CursorBoard_Paint);
            this.pBox3_CursorBoard.MouseEnter += new System.EventHandler(this.pBox3_CursorBoard_MouseEnter);
            this.pBox3_CursorBoard.MouseLeave += new System.EventHandler(this.pBox3_CursorBoard_MouseLeave);
            this.pBox3_CursorBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBox3_CursorBoard_MouseMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.GrayText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(295, 107);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1125, 690);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(295, 107);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1125, 690);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(40, 107);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button_ZoomReset
            // 
            this.button_ZoomReset.Location = new System.Drawing.Point(526, 25);
            this.button_ZoomReset.Margin = new System.Windows.Forms.Padding(2);
            this.button_ZoomReset.Name = "button_ZoomReset";
            this.button_ZoomReset.Size = new System.Drawing.Size(50, 50);
            this.button_ZoomReset.TabIndex = 30;
            this.button_ZoomReset.Text = "zoom 100%";
            this.button_ZoomReset.UseVisualStyleBackColor = true;
            this.button_ZoomReset.Click += new System.EventHandler(this.button_ZoomReset_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Black;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(18, 45);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(81, 34);
            this.button7.TabIndex = 35;
            this.button7.Text = "브러시 +";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.ClientSize = new System.Drawing.Size(1717, 842);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pBox3_CursorBoard);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.listPanelThumb);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Main_form";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBox3_CursorBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Network_operation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel listPanelThumb;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Button_ZoomIn;
        private System.Windows.Forms.Button Button_ZoomOut;
        private System.Windows.Forms.Label lable_ImgScale;
        private System.Windows.Forms.Label lable_Opacity;
        private System.Windows.Forms.Button button_setscrollmode;
        private System.Windows.Forms.Button button_setPaintmode;
        private System.Windows.Forms.Button button_BrushsizeUp;
        private System.Windows.Forms.Button button_BrushsizeDown;
        private System.Windows.Forms.PictureBox pBox3_CursorBoard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_ZoomReset;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button7;
    }
}

