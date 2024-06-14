namespace biosignals
{
    partial class TestConnection
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
            this.check = new System.Windows.Forms.Button();
            this.portslist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.open = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.logwnd = new System.Windows.Forms.TextBox();
            this.clear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.read = new System.Windows.Forms.Button();
            this.hexbt = new System.Windows.Forms.RadioButton();
            this.asciibt = new System.Windows.Forms.RadioButton();
            this.binbt = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // check
            // 
            this.check.Location = new System.Drawing.Point(314, 10);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(75, 23);
            this.check.TabIndex = 0;
            this.check.Text = "Check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.check_Click);
            // 
            // portslist
            // 
            this.portslist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portslist.FormattingEnabled = true;
            this.portslist.Location = new System.Drawing.Point(109, 12);
            this.portslist.Name = "portslist";
            this.portslist.Size = new System.Drawing.Size(73, 21);
            this.portslist.TabIndex = 1;
            this.portslist.SelectionChangeCommitted += new System.EventHandler(this.portslist_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Port:";
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(314, 53);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 3;
            this.open.Text = "Open";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(314, 82);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 4;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // logwnd
            // 
            this.logwnd.Location = new System.Drawing.Point(15, 56);
            this.logwnd.Multiline = true;
            this.logwnd.Name = "logwnd";
            this.logwnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logwnd.Size = new System.Drawing.Size(293, 187);
            this.logwnd.TabIndex = 5;
            this.logwnd.TextChanged += new System.EventHandler(this.logwnd_TextChanged);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(314, 111);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 6;
            this.clear.Text = "Clear log";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Log window";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 309);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(181, 20);
            this.textBox2.TabIndex = 8;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(217, 306);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 9;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // read
            // 
            this.read.Location = new System.Drawing.Point(15, 264);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(75, 23);
            this.read.TabIndex = 10;
            this.read.Text = "Read";
            this.read.UseVisualStyleBackColor = true;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // hexbt
            // 
            this.hexbt.AutoSize = true;
            this.hexbt.Location = new System.Drawing.Point(315, 158);
            this.hexbt.Name = "hexbt";
            this.hexbt.Size = new System.Drawing.Size(47, 17);
            this.hexbt.TabIndex = 11;
            this.hexbt.TabStop = true;
            this.hexbt.Text = "HEX";
            this.hexbt.UseVisualStyleBackColor = true;
            // 
            // asciibt
            // 
            this.asciibt.AutoSize = true;
            this.asciibt.Location = new System.Drawing.Point(315, 204);
            this.asciibt.Name = "asciibt";
            this.asciibt.Size = new System.Drawing.Size(52, 17);
            this.asciibt.TabIndex = 12;
            this.asciibt.TabStop = true;
            this.asciibt.Text = "ASCII";
            this.asciibt.UseVisualStyleBackColor = true;
            // 
            // binbt
            // 
            this.binbt.AutoSize = true;
            this.binbt.Location = new System.Drawing.Point(314, 181);
            this.binbt.Name = "binbt";
            this.binbt.Size = new System.Drawing.Size(43, 17);
            this.binbt.TabIndex = 13;
            this.binbt.TabStop = true;
            this.binbt.Text = "BIN";
            this.binbt.UseVisualStyleBackColor = true;
            // 
            // TestConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 369);
            this.Controls.Add(this.binbt);
            this.Controls.Add(this.asciibt);
            this.Controls.Add(this.hexbt);
            this.Controls.Add(this.read);
            this.Controls.Add(this.send);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.logwnd);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portslist);
            this.Controls.Add(this.check);
            this.Name = "TestConnection";
            this.Text = "Test COM Ports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button check;
        private System.Windows.Forms.ComboBox portslist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.TextBox logwnd;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button send;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.RadioButton hexbt;
        private System.Windows.Forms.RadioButton asciibt;
        private System.Windows.Forms.RadioButton binbt;
    }
}

