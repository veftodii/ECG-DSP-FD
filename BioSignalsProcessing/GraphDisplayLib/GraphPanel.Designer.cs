namespace GraphDisplayLib
{
    partial class GraphPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GraphTitle = new System.Windows.Forms.Label();
            this.DisplayBox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // GraphTitle
            // 
            this.GraphTitle.AutoSize = true;
            this.GraphTitle.Location = new System.Drawing.Point(46, 0);
            this.GraphTitle.Margin = new System.Windows.Forms.Padding(0);
            this.GraphTitle.Name = "GraphTitle";
            this.GraphTitle.Size = new System.Drawing.Size(45, 13);
            this.GraphTitle.TabIndex = 3;
            this.GraphTitle.Text = "Graph 1";
            // 
            // DisplayBox
            // 
            this.DisplayBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayBox.BackColor = System.Drawing.Color.White;
            this.DisplayBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DisplayBox.Location = new System.Drawing.Point(46, 16);
            this.DisplayBox.Name = "DisplayBox";
            this.DisplayBox.Size = new System.Drawing.Size(593, 171);
            this.DisplayBox.TabIndex = 4;
            this.DisplayBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayBox_Paint);
            // 
            // GraphPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DisplayBox);
            this.Controls.Add(this.GraphTitle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(150, 150);
            this.Name = "GraphPanel";
            this.Size = new System.Drawing.Size(669, 219);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPanel_Paint);
            this.Resize += new System.EventHandler(this.GraphPanel_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GraphTitle;
        private System.Windows.Forms.Panel DisplayBox;
    }
}
