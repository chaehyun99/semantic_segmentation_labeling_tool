
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
            this.components = new System.ComponentModel.Container();
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
            this.button_ZoomReset = new System.Windows.Forms.Button();
            this.button_setPaintmode = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lable_Opacity = new System.Windows.Forms.Label();
            this.lable_ImgScale = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_setColor0 = new System.Windows.Forms.Button();
            this.listPanelThumb = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pBox3_CursorBoard = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button_setColor1 = new System.Windows.Forms.Button();
            this.button_setColor3 = new System.Windows.Forms.Button();
            this.button_setColor2 = new System.Windows.Forms.Button();
            this.button_setColor7 = new System.Windows.Forms.Button();
            this.button_setColor6 = new System.Windows.Forms.Button();
            this.button_setColor5 = new System.Windows.Forms.Button();
            this.button_setColor4 = new System.Windows.Forms.Button();
            this.button_setColor15 = new System.Windows.Forms.Button();
            this.button_setColor14 = new System.Windows.Forms.Button();
            this.button_setColor13 = new System.Windows.Forms.Button();
            this.button_setColor12 = new System.Windows.Forms.Button();
            this.button_setColor11 = new System.Windows.Forms.Button();
            this.button_setColor10 = new System.Windows.Forms.Button();
            this.button_setColor9 = new System.Windows.Forms.Button();
            this.button_setColor8 = new System.Windows.Forms.Button();
            this.button_setColor19 = new System.Windows.Forms.Button();
            this.button_setColor18 = new System.Windows.Forms.Button();
            this.button_setColor17 = new System.Windows.Forms.Button();
            this.button_setColor16 = new System.Windows.Forms.Button();
            this.button_setColor20 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox3_CursorBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_setscrollmode
            // 
            this.button_setscrollmode.Location = new System.Drawing.Point(1058, 34);
            this.button_setscrollmode.Margin = new System.Windows.Forms.Padding(0);
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
            this.button_BrushsizeUp.Margin = new System.Windows.Forms.Padding(7);
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
            this.button_BrushsizeDown.Margin = new System.Windows.Forms.Padding(7);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1347, 84);
            this.panel1.TabIndex = 33;
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::Semantic.Properties.Resources.eraser;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(350, 23);
            this.button6.Margin = new System.Windows.Forms.Padding(7);
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
            this.button5.Margin = new System.Windows.Forms.Padding(7);
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
            this.button4.Margin = new System.Windows.Forms.Padding(7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 33;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::Semantic.Properties.Resources.info;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(128, 30);
            this.button3.Margin = new System.Windows.Forms.Padding(7);
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
            this.button1.Margin = new System.Windows.Forms.Padding(7);
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
            this.button2.Margin = new System.Windows.Forms.Padding(7);
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
            this.Button_ZoomIn.Margin = new System.Windows.Forms.Padding(7);
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
            this.Button_ZoomOut.Margin = new System.Windows.Forms.Padding(7);
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
            this.Network_operation.Margin = new System.Windows.Forms.Padding(7);
            this.Network_operation.Name = "Network_operation";
            this.Network_operation.Size = new System.Drawing.Size(40, 40);
            this.Network_operation.TabIndex = 16;
            this.Network_operation.UseVisualStyleBackColor = true;
            this.Network_operation.Click += new System.EventHandler(this.Network_operation_Click);
            // 
            // button_ZoomReset
            // 
            this.button_ZoomReset.Location = new System.Drawing.Point(526, 25);
            this.button_ZoomReset.Margin = new System.Windows.Forms.Padding(7);
            this.button_ZoomReset.Name = "button_ZoomReset";
            this.button_ZoomReset.Size = new System.Drawing.Size(50, 50);
            this.button_ZoomReset.TabIndex = 30;
            this.button_ZoomReset.Text = "zoom 100%";
            this.button_ZoomReset.UseVisualStyleBackColor = true;
            this.button_ZoomReset.Click += new System.EventHandler(this.button_ZoomReset_Click);
            // 
            // button_setPaintmode
            // 
            this.button_setPaintmode.BackgroundImage = global::Semantic.Properties.Resources.iconfinder_17_Brush_290133;
            this.button_setPaintmode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_setPaintmode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.button_setPaintmode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_setPaintmode.Location = new System.Drawing.Point(295, 30);
            this.button_setPaintmode.Margin = new System.Windows.Forms.Padding(7);
            this.button_setPaintmode.Name = "button_setPaintmode";
            this.button_setPaintmode.Size = new System.Drawing.Size(40, 40);
            this.button_setPaintmode.TabIndex = 28;
            this.button_setPaintmode.UseVisualStyleBackColor = true;
            this.button_setPaintmode.Click += new System.EventHandler(this.button_setPaintmode_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.listPanelThumb);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.pBox3_CursorBoard);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 84);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1347, 616);
            this.panel5.TabIndex = 34;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1084, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 616);
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
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 248);
            this.panel3.TabIndex = 2;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(40, 107);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lable_Opacity
            // 
            this.lable_Opacity.AutoSize = true;
            this.lable_Opacity.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lable_Opacity.Location = new System.Drawing.Point(63, 18);
            this.lable_Opacity.Margin = new System.Windows.Forms.Padding(0);
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
            this.lable_ImgScale.Margin = new System.Windows.Forms.Padding(0);
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
            this.panel4.Controls.Add(this.button_setColor20);
            this.panel4.Controls.Add(this.button_setColor19);
            this.panel4.Controls.Add(this.button_setColor18);
            this.panel4.Controls.Add(this.button_setColor17);
            this.panel4.Controls.Add(this.button_setColor16);
            this.panel4.Controls.Add(this.button_setColor15);
            this.panel4.Controls.Add(this.button_setColor14);
            this.panel4.Controls.Add(this.button_setColor13);
            this.panel4.Controls.Add(this.button_setColor12);
            this.panel4.Controls.Add(this.button_setColor11);
            this.panel4.Controls.Add(this.button_setColor10);
            this.panel4.Controls.Add(this.button_setColor9);
            this.panel4.Controls.Add(this.button_setColor8);
            this.panel4.Controls.Add(this.button_setColor7);
            this.panel4.Controls.Add(this.button_setColor6);
            this.panel4.Controls.Add(this.button_setColor5);
            this.panel4.Controls.Add(this.button_setColor4);
            this.panel4.Controls.Add(this.button_setColor3);
            this.panel4.Controls.Add(this.button_setColor2);
            this.panel4.Controls.Add(this.button_setColor1);
            this.panel4.Controls.Add(this.button_setColor0);
            this.panel4.Location = new System.Drawing.Point(15, 300);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 396);
            this.panel4.TabIndex = 1;
            // 
            // button_setColor0
            // 
            this.button_setColor0.BackColor = System.Drawing.Color.Black;
            this.button_setColor0.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor0.FlatAppearance.BorderSize = 0;
            this.button_setColor0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor0.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor0.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor0.Location = new System.Drawing.Point(22, 32);
            this.button_setColor0.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor0.Name = "button_setColor0";
            this.button_setColor0.Size = new System.Drawing.Size(94, 20);
            this.button_setColor0.TabIndex = 35;
            this.button_setColor0.UseVisualStyleBackColor = false;
            // 
            // listPanelThumb
            // 
            this.listPanelThumb.AutoScroll = true;
            this.listPanelThumb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.listPanelThumb.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPanelThumb.Location = new System.Drawing.Point(0, 0);
            this.listPanelThumb.Margin = new System.Windows.Forms.Padding(0);
            this.listPanelThumb.Name = "listPanelThumb";
            this.listPanelThumb.Size = new System.Drawing.Size(264, 616);
            this.listPanelThumb.TabIndex = 19;
            this.listPanelThumb.Paint += new System.Windows.Forms.PaintEventHandler(this.uiPanelThumbnail_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.GrayText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(271, 7);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(806, 597);
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
            this.pictureBox1.Location = new System.Drawing.Point(271, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(806, 597);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // pBox3_CursorBoard
            // 
            this.pBox3_CursorBoard.BackColor = System.Drawing.Color.Gainsboro;
            this.pBox3_CursorBoard.Location = new System.Drawing.Point(271, 7);
            this.pBox3_CursorBoard.Margin = new System.Windows.Forms.Padding(7);
            this.pBox3_CursorBoard.Name = "pBox3_CursorBoard";
            this.pBox3_CursorBoard.Size = new System.Drawing.Size(806, 597);
            this.pBox3_CursorBoard.TabIndex = 32;
            this.pBox3_CursorBoard.TabStop = false;
            this.pBox3_CursorBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pBox3_CursorBoard_Paint);
            this.pBox3_CursorBoard.MouseEnter += new System.EventHandler(this.pBox3_CursorBoard_MouseEnter);
            this.pBox3_CursorBoard.MouseLeave += new System.EventHandler(this.pBox3_CursorBoard_MouseLeave);
            this.pBox3_CursorBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBox3_CursorBoard_MouseMove);
            // 
            // button_setColor1
            // 
            this.button_setColor1.BackColor = System.Drawing.Color.Black;
            this.button_setColor1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor1.FlatAppearance.BorderSize = 0;
            this.button_setColor1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor1.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor1.Location = new System.Drawing.Point(119, 32);
            this.button_setColor1.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor1.Name = "button_setColor1";
            this.button_setColor1.Size = new System.Drawing.Size(94, 20);
            this.button_setColor1.TabIndex = 36;
            this.button_setColor1.UseVisualStyleBackColor = false;
            // 
            // button_setColor3
            // 
            this.button_setColor3.BackColor = System.Drawing.Color.Black;
            this.button_setColor3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor3.FlatAppearance.BorderSize = 0;
            this.button_setColor3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor3.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor3.Location = new System.Drawing.Point(119, 55);
            this.button_setColor3.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor3.Name = "button_setColor3";
            this.button_setColor3.Size = new System.Drawing.Size(94, 20);
            this.button_setColor3.TabIndex = 38;
            this.button_setColor3.UseVisualStyleBackColor = false;
            // 
            // button_setColor2
            // 
            this.button_setColor2.BackColor = System.Drawing.Color.Black;
            this.button_setColor2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor2.FlatAppearance.BorderSize = 0;
            this.button_setColor2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor2.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor2.Location = new System.Drawing.Point(22, 55);
            this.button_setColor2.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor2.Name = "button_setColor2";
            this.button_setColor2.Size = new System.Drawing.Size(94, 20);
            this.button_setColor2.TabIndex = 37;
            this.button_setColor2.UseVisualStyleBackColor = false;
            // 
            // button_setColor7
            // 
            this.button_setColor7.BackColor = System.Drawing.Color.Black;
            this.button_setColor7.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor7.FlatAppearance.BorderSize = 0;
            this.button_setColor7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor7.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor7.Location = new System.Drawing.Point(119, 101);
            this.button_setColor7.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor7.Name = "button_setColor7";
            this.button_setColor7.Size = new System.Drawing.Size(94, 20);
            this.button_setColor7.TabIndex = 42;
            this.button_setColor7.UseVisualStyleBackColor = false;
            // 
            // button_setColor6
            // 
            this.button_setColor6.BackColor = System.Drawing.Color.Black;
            this.button_setColor6.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor6.FlatAppearance.BorderSize = 0;
            this.button_setColor6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor6.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor6.Location = new System.Drawing.Point(22, 101);
            this.button_setColor6.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor6.Name = "button_setColor6";
            this.button_setColor6.Size = new System.Drawing.Size(94, 20);
            this.button_setColor6.TabIndex = 41;
            this.button_setColor6.UseVisualStyleBackColor = false;
            // 
            // button_setColor5
            // 
            this.button_setColor5.BackColor = System.Drawing.Color.Black;
            this.button_setColor5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor5.FlatAppearance.BorderSize = 0;
            this.button_setColor5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor5.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor5.Location = new System.Drawing.Point(119, 78);
            this.button_setColor5.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor5.Name = "button_setColor5";
            this.button_setColor5.Size = new System.Drawing.Size(94, 20);
            this.button_setColor5.TabIndex = 40;
            this.button_setColor5.UseVisualStyleBackColor = false;
            // 
            // button_setColor4
            // 
            this.button_setColor4.BackColor = System.Drawing.Color.Black;
            this.button_setColor4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor4.FlatAppearance.BorderSize = 0;
            this.button_setColor4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor4.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor4.Location = new System.Drawing.Point(22, 78);
            this.button_setColor4.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor4.Name = "button_setColor4";
            this.button_setColor4.Size = new System.Drawing.Size(94, 20);
            this.button_setColor4.TabIndex = 39;
            this.button_setColor4.UseVisualStyleBackColor = false;
            // 
            // button_setColor15
            // 
            this.button_setColor15.BackColor = System.Drawing.Color.Black;
            this.button_setColor15.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor15.FlatAppearance.BorderSize = 0;
            this.button_setColor15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor15.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor15.Location = new System.Drawing.Point(119, 193);
            this.button_setColor15.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor15.Name = "button_setColor15";
            this.button_setColor15.Size = new System.Drawing.Size(94, 20);
            this.button_setColor15.TabIndex = 50;
            this.button_setColor15.UseVisualStyleBackColor = false;
            // 
            // button_setColor14
            // 
            this.button_setColor14.BackColor = System.Drawing.Color.Black;
            this.button_setColor14.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor14.FlatAppearance.BorderSize = 0;
            this.button_setColor14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor14.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor14.Location = new System.Drawing.Point(22, 193);
            this.button_setColor14.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor14.Name = "button_setColor14";
            this.button_setColor14.Size = new System.Drawing.Size(94, 20);
            this.button_setColor14.TabIndex = 49;
            this.button_setColor14.UseVisualStyleBackColor = false;
            // 
            // button_setColor13
            // 
            this.button_setColor13.BackColor = System.Drawing.Color.Black;
            this.button_setColor13.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor13.FlatAppearance.BorderSize = 0;
            this.button_setColor13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor13.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor13.Location = new System.Drawing.Point(119, 170);
            this.button_setColor13.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor13.Name = "button_setColor13";
            this.button_setColor13.Size = new System.Drawing.Size(94, 20);
            this.button_setColor13.TabIndex = 48;
            this.button_setColor13.UseVisualStyleBackColor = false;
            // 
            // button_setColor12
            // 
            this.button_setColor12.BackColor = System.Drawing.Color.Black;
            this.button_setColor12.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor12.FlatAppearance.BorderSize = 0;
            this.button_setColor12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor12.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor12.Location = new System.Drawing.Point(22, 170);
            this.button_setColor12.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor12.Name = "button_setColor12";
            this.button_setColor12.Size = new System.Drawing.Size(94, 20);
            this.button_setColor12.TabIndex = 47;
            this.button_setColor12.UseVisualStyleBackColor = false;
            // 
            // button_setColor11
            // 
            this.button_setColor11.BackColor = System.Drawing.Color.Black;
            this.button_setColor11.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor11.FlatAppearance.BorderSize = 0;
            this.button_setColor11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor11.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor11.Location = new System.Drawing.Point(119, 147);
            this.button_setColor11.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor11.Name = "button_setColor11";
            this.button_setColor11.Size = new System.Drawing.Size(94, 20);
            this.button_setColor11.TabIndex = 46;
            this.button_setColor11.UseVisualStyleBackColor = false;
            // 
            // button_setColor10
            // 
            this.button_setColor10.BackColor = System.Drawing.Color.Black;
            this.button_setColor10.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor10.FlatAppearance.BorderSize = 0;
            this.button_setColor10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor10.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor10.Location = new System.Drawing.Point(22, 147);
            this.button_setColor10.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor10.Name = "button_setColor10";
            this.button_setColor10.Size = new System.Drawing.Size(94, 20);
            this.button_setColor10.TabIndex = 45;
            this.button_setColor10.UseVisualStyleBackColor = false;
            // 
            // button_setColor9
            // 
            this.button_setColor9.BackColor = System.Drawing.Color.Black;
            this.button_setColor9.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor9.FlatAppearance.BorderSize = 0;
            this.button_setColor9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor9.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor9.Location = new System.Drawing.Point(119, 124);
            this.button_setColor9.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor9.Name = "button_setColor9";
            this.button_setColor9.Size = new System.Drawing.Size(94, 20);
            this.button_setColor9.TabIndex = 44;
            this.button_setColor9.UseVisualStyleBackColor = false;
            // 
            // button_setColor8
            // 
            this.button_setColor8.BackColor = System.Drawing.Color.Black;
            this.button_setColor8.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor8.FlatAppearance.BorderSize = 0;
            this.button_setColor8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor8.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor8.Location = new System.Drawing.Point(22, 124);
            this.button_setColor8.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor8.Name = "button_setColor8";
            this.button_setColor8.Size = new System.Drawing.Size(94, 20);
            this.button_setColor8.TabIndex = 43;
            this.button_setColor8.UseVisualStyleBackColor = false;
            // 
            // button_setColor19
            // 
            this.button_setColor19.BackColor = System.Drawing.Color.Black;
            this.button_setColor19.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor19.FlatAppearance.BorderSize = 0;
            this.button_setColor19.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor19.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor19.Location = new System.Drawing.Point(119, 239);
            this.button_setColor19.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor19.Name = "button_setColor19";
            this.button_setColor19.Size = new System.Drawing.Size(94, 20);
            this.button_setColor19.TabIndex = 54;
            this.button_setColor19.UseVisualStyleBackColor = false;
            // 
            // button_setColor18
            // 
            this.button_setColor18.BackColor = System.Drawing.Color.Black;
            this.button_setColor18.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor18.FlatAppearance.BorderSize = 0;
            this.button_setColor18.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor18.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor18.Location = new System.Drawing.Point(22, 239);
            this.button_setColor18.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor18.Name = "button_setColor18";
            this.button_setColor18.Size = new System.Drawing.Size(94, 20);
            this.button_setColor18.TabIndex = 53;
            this.button_setColor18.UseVisualStyleBackColor = false;
            // 
            // button_setColor17
            // 
            this.button_setColor17.BackColor = System.Drawing.Color.Black;
            this.button_setColor17.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor17.FlatAppearance.BorderSize = 0;
            this.button_setColor17.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor17.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor17.Location = new System.Drawing.Point(119, 216);
            this.button_setColor17.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor17.Name = "button_setColor17";
            this.button_setColor17.Size = new System.Drawing.Size(94, 20);
            this.button_setColor17.TabIndex = 52;
            this.button_setColor17.UseVisualStyleBackColor = false;
            // 
            // button_setColor16
            // 
            this.button_setColor16.BackColor = System.Drawing.Color.Black;
            this.button_setColor16.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor16.FlatAppearance.BorderSize = 0;
            this.button_setColor16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor16.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor16.Location = new System.Drawing.Point(22, 216);
            this.button_setColor16.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor16.Name = "button_setColor16";
            this.button_setColor16.Size = new System.Drawing.Size(94, 20);
            this.button_setColor16.TabIndex = 51;
            this.button_setColor16.UseVisualStyleBackColor = false;
            // 
            // button_setColor20
            // 
            this.button_setColor20.BackColor = System.Drawing.Color.Black;
            this.button_setColor20.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_setColor20.FlatAppearance.BorderSize = 0;
            this.button_setColor20.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button_setColor20.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setColor20.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setColor20.ForeColor = System.Drawing.Color.Silver;
            this.button_setColor20.Location = new System.Drawing.Point(22, 262);
            this.button_setColor20.Margin = new System.Windows.Forms.Padding(0);
            this.button_setColor20.Name = "button_setColor20";
            this.button_setColor20.Size = new System.Drawing.Size(94, 20);
            this.button_setColor20.TabIndex = 55;
            this.button_setColor20.UseVisualStyleBackColor = false;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.ClientSize = new System.Drawing.Size(1347, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Main_form";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox3_CursorBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Network_operation;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Button_ZoomIn;
        private System.Windows.Forms.Button Button_ZoomOut;
        private System.Windows.Forms.Button button_setscrollmode;
        private System.Windows.Forms.Button button_setPaintmode;
        private System.Windows.Forms.Button button_BrushsizeUp;
        private System.Windows.Forms.Button button_BrushsizeDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button_ZoomReset;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lable_Opacity;
        private System.Windows.Forms.Label lable_ImgScale;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_setColor0;
        private System.Windows.Forms.FlowLayoutPanel listPanelThumb;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pBox3_CursorBoard;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button_setColor20;
        private System.Windows.Forms.Button button_setColor19;
        private System.Windows.Forms.Button button_setColor18;
        private System.Windows.Forms.Button button_setColor17;
        private System.Windows.Forms.Button button_setColor16;
        private System.Windows.Forms.Button button_setColor15;
        private System.Windows.Forms.Button button_setColor14;
        private System.Windows.Forms.Button button_setColor13;
        private System.Windows.Forms.Button button_setColor12;
        private System.Windows.Forms.Button button_setColor11;
        private System.Windows.Forms.Button button_setColor10;
        private System.Windows.Forms.Button button_setColor9;
        private System.Windows.Forms.Button button_setColor8;
        private System.Windows.Forms.Button button_setColor7;
        private System.Windows.Forms.Button button_setColor6;
        private System.Windows.Forms.Button button_setColor5;
        private System.Windows.Forms.Button button_setColor4;
        private System.Windows.Forms.Button button_setColor3;
        private System.Windows.Forms.Button button_setColor2;
        private System.Windows.Forms.Button button_setColor1;
    }
}

