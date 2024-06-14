namespace biosignals
{
    partial class MainWnd
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.connectbt = new System.Windows.Forms.ToolStripButton();
            this.disconnectbt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startbt = new System.Windows.Forms.ToolStripButton();
            this.stopbt = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rpeaksDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iFFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.graphContainer1 = new GraphDisplayLib.GraphContainer();
            this.graphPanel1 = new GraphDisplayLib.GraphPanel();
            this.graphPanel2 = new GraphDisplayLib.GraphPanel();
            this.graphPanel3 = new GraphDisplayLib.GraphPanel();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.graphContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 453);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(604, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(102, 369);
            this.panel1.TabIndex = 11;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(13, 307);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "from_file";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Port:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(13, 27);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(70, 21);
            this.comboBox2.TabIndex = 19;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(13, 203);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 5;
            this.button7.Text = "fft_segmented";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(14, 244);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 4;
            this.button6.Text = "hist_test";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 336);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "fft_func_test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 116);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "fft_test";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 174);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "fft_shift_test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "fft_half_test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "rpeak_test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectbt,
            this.disconnectbt,
            this.toolStripSeparator1,
            this.startbt,
            this.stopbt,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(706, 52);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // connectbt
            // 
            this.connectbt.Image = ((System.Drawing.Image)(resources.GetObject("connectbt.Image")));
            this.connectbt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectbt.Name = "connectbt";
            this.connectbt.Size = new System.Drawing.Size(51, 49);
            this.connectbt.Text = "Connect";
            this.connectbt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.connectbt.Click += new System.EventHandler(this.connectbt_Click);
            // 
            // disconnectbt
            // 
            this.disconnectbt.Image = ((System.Drawing.Image)(resources.GetObject("disconnectbt.Image")));
            this.disconnectbt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.disconnectbt.Name = "disconnectbt";
            this.disconnectbt.Size = new System.Drawing.Size(63, 49);
            this.disconnectbt.Text = "Disconnect";
            this.disconnectbt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.disconnectbt.Click += new System.EventHandler(this.disconnectbt_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // startbt
            // 
            this.startbt.Image = ((System.Drawing.Image)(resources.GetObject("startbt.Image")));
            this.startbt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startbt.Name = "startbt";
            this.startbt.Size = new System.Drawing.Size(36, 49);
            this.startbt.Text = "Start";
            this.startbt.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.startbt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.startbt.ToolTipText = "Start";
            this.startbt.Click += new System.EventHandler(this.startbt_Click);
            // 
            // stopbt
            // 
            this.stopbt.ImageTransparentColor = System.Drawing.Color.White;
            this.stopbt.Name = "stopbt";
            this.stopbt.Size = new System.Drawing.Size(33, 49);
            this.stopbt.Text = "Stop";
            this.stopbt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.stopbt.Click += new System.EventHandler(this.stopbt_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(36, 49);
            this.toolStripButton3.Text = "Add";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(50, 49);
            this.toolStripButton4.Text = "Remove";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processingToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(706, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // processingToolStripMenuItem
            // 
            this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rpeaksDetectionToolStripMenuItem,
            this.fFTToolStripMenuItem,
            this.iFFTToolStripMenuItem});
            this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
            this.processingToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.processingToolStripMenuItem.Text = "Processing";
            // 
            // rpeaksDetectionToolStripMenuItem
            // 
            this.rpeaksDetectionToolStripMenuItem.Name = "rpeaksDetectionToolStripMenuItem";
            this.rpeaksDetectionToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.rpeaksDetectionToolStripMenuItem.Text = "R-peaks Detection";
            this.rpeaksDetectionToolStripMenuItem.Click += new System.EventHandler(this.rpeaksDetectionToolStripMenuItem_Click);
            // 
            // fFTToolStripMenuItem
            // 
            this.fFTToolStripMenuItem.Name = "fFTToolStripMenuItem";
            this.fFTToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.fFTToolStripMenuItem.Text = "FFT (Fast Fourier Transform)";
            this.fFTToolStripMenuItem.Click += new System.EventHandler(this.fFTToolStripMenuItem_Click);
            // 
            // iFFTToolStripMenuItem
            // 
            this.iFFTToolStripMenuItem.Name = "iFFTToolStripMenuItem";
            this.iFFTToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.iFFTToolStripMenuItem.Text = "IFFT (Inverse FFT)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 20);
            this.toolStripMenuItem1.Text = "Test connection";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "dat";
            this.openFileDialog1.Filter = "Binary file (*.dat)|*.dat|All files|*.*";
            this.openFileDialog1.Title = "Select a recorded binary file";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "stop.PNG");
            this.imageList1.Images.SetKeyName(1, "start.bmp");
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(323, 37);
            this.trackBar1.Maximum = 21;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar1.Size = new System.Drawing.Size(253, 33);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.Value = 11;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(582, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(582, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "File:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(323, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Number of samples ( 2^N,  N=[0; 16] ) :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(579, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Samples: ";
            // 
            // graphContainer1
            // 
            this.graphContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphContainer1.AutoScroll = true;
            this.graphContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.graphContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphContainer1.Controls.Add(this.graphPanel1);
            this.graphContainer1.Controls.Add(this.graphPanel2);
            this.graphContainer1.Controls.Add(this.graphPanel3);
            this.graphContainer1.Location = new System.Drawing.Point(0, 78);
            this.graphContainer1.Margin = new System.Windows.Forms.Padding(5);
            this.graphContainer1.MinimumSize = new System.Drawing.Size(160, 160);
            this.graphContainer1.Name = "graphContainer1";
            this.graphContainer1.Padding = new System.Windows.Forms.Padding(3);
            this.graphContainer1.Size = new System.Drawing.Size(596, 370);
            this.graphContainer1.TabIndex = 1;
            // 
            // graphPanel1
            // 
            this.graphPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.graphPanel1.Location = new System.Drawing.Point(3, 3);
            this.graphPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.graphPanel1.MinimumSize = new System.Drawing.Size(150, 150);
            this.graphPanel1.Name = "graphPanel1";
            this.graphPanel1.Size = new System.Drawing.Size(522, 180);
            this.graphPanel1.TabIndex = 2;
            // 
            // graphPanel2
            // 
            this.graphPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel2.Location = new System.Drawing.Point(3, 185);
            this.graphPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.graphPanel2.MinimumSize = new System.Drawing.Size(150, 150);
            this.graphPanel2.Name = "graphPanel2";
            this.graphPanel2.Size = new System.Drawing.Size(522, 180);
            this.graphPanel2.TabIndex = 5;
            this.graphPanel2.Title = "Graph 2";
            // 
            // graphPanel3
            // 
            this.graphPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel3.Location = new System.Drawing.Point(3, 367);
            this.graphPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.graphPanel3.MinimumSize = new System.Drawing.Size(300, 50);
            this.graphPanel3.Name = "graphPanel3";
            this.graphPanel3.Size = new System.Drawing.Size(522, 180);
            this.graphPanel3.TabIndex = 6;
            this.graphPanel3.Title = "Graph 3";
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(706, 475);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.graphContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 280);
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biosignals ( Test Mode )";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.graphContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphDisplayLib.GraphContainer graphContainer1;
        private GraphDisplayLib.GraphPanel graphPanel1;
        private GraphDisplayLib.GraphPanel graphPanel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton startbt;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton stopbt;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton connectbt;
        private System.Windows.Forms.ToolStripButton disconnectbt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem processingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rpeaksDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iFFTToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private GraphDisplayLib.GraphPanel graphPanel3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button8;




    }
}