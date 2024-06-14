namespace biosignals
{
    partial class AddChannel
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
            this.chValues = new System.Windows.Forms.ComboBox();
            this.chLabel = new System.Windows.Forms.Label();
            this.hpfValues = new System.Windows.Forms.ComboBox();
            this.lpfValues = new System.Windows.Forms.ComboBox();
            this.lpfLabel = new System.Windows.Forms.Label();
            this.hpfLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OKbt = new System.Windows.Forms.Button();
            this.cancelbt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chValues
            // 
            this.chValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chValues.FormattingEnabled = true;
            this.chValues.Location = new System.Drawing.Point(15, 54);
            this.chValues.Name = "chValues";
            this.chValues.Size = new System.Drawing.Size(127, 21);
            this.chValues.TabIndex = 0;
            // 
            // chLabel
            // 
            this.chLabel.AutoSize = true;
            this.chLabel.Location = new System.Drawing.Point(12, 38);
            this.chLabel.Name = "chLabel";
            this.chLabel.Size = new System.Drawing.Size(49, 13);
            this.chLabel.TabIndex = 1;
            this.chLabel.Text = "Channel:";
            // 
            // hpfValues
            // 
            this.hpfValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hpfValues.FormattingEnabled = true;
            this.hpfValues.Location = new System.Drawing.Point(166, 54);
            this.hpfValues.Name = "hpfValues";
            this.hpfValues.Size = new System.Drawing.Size(127, 21);
            this.hpfValues.TabIndex = 2;
            // 
            // lpfValues
            // 
            this.lpfValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lpfValues.FormattingEnabled = true;
            this.lpfValues.Location = new System.Drawing.Point(321, 54);
            this.lpfValues.Name = "lpfValues";
            this.lpfValues.Size = new System.Drawing.Size(127, 21);
            this.lpfValues.TabIndex = 3;
            // 
            // lpfLabel
            // 
            this.lpfLabel.AutoSize = true;
            this.lpfLabel.Location = new System.Drawing.Point(318, 38);
            this.lpfLabel.Name = "lpfLabel";
            this.lpfLabel.Size = new System.Drawing.Size(81, 13);
            this.lpfLabel.TabIndex = 4;
            this.lpfLabel.Text = "Low Pass Filter:";
            // 
            // hpfLabel
            // 
            this.hpfLabel.AutoSize = true;
            this.hpfLabel.Location = new System.Drawing.Point(163, 38);
            this.hpfLabel.Name = "hpfLabel";
            this.hpfLabel.Size = new System.Drawing.Size(83, 13);
            this.hpfLabel.TabIndex = 5;
            this.hpfLabel.Text = "High Pass Filter:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Choose a channel from the following list:";
            // 
            // OKbt
            // 
            this.OKbt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbt.Location = new System.Drawing.Point(278, 137);
            this.OKbt.Name = "OKbt";
            this.OKbt.Size = new System.Drawing.Size(75, 23);
            this.OKbt.TabIndex = 7;
            this.OKbt.Text = "Add";
            this.OKbt.UseVisualStyleBackColor = true;
            this.OKbt.Click += new System.EventHandler(this.OKbt_Click);
            // 
            // cancelbt
            // 
            this.cancelbt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbt.Location = new System.Drawing.Point(373, 137);
            this.cancelbt.Name = "cancelbt";
            this.cancelbt.Size = new System.Drawing.Size(75, 23);
            this.cancelbt.TabIndex = 8;
            this.cancelbt.Text = "Cancel";
            this.cancelbt.UseVisualStyleBackColor = true;
            this.cancelbt.Click += new System.EventHandler(this.cancelbt_Click);
            // 
            // AddChannel
            // 
            this.AcceptButton = this.OKbt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbt;
            this.ClientSize = new System.Drawing.Size(465, 172);
            this.Controls.Add(this.cancelbt);
            this.Controls.Add(this.OKbt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hpfLabel);
            this.Controls.Add(this.lpfLabel);
            this.Controls.Add(this.lpfValues);
            this.Controls.Add(this.hpfValues);
            this.Controls.Add(this.chLabel);
            this.Controls.Add(this.chValues);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddChannel";
            this.ShowInTaskbar = false;
            this.Text = "Add channel";
            this.Load += new System.EventHandler(this.AddChannel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox chValues;
        private System.Windows.Forms.Label chLabel;
        private System.Windows.Forms.ComboBox hpfValues;
        private System.Windows.Forms.ComboBox lpfValues;
        private System.Windows.Forms.Label lpfLabel;
        private System.Windows.Forms.Label hpfLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKbt;
        private System.Windows.Forms.Button cancelbt;
    }
}