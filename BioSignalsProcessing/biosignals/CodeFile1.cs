using System;

using System.Drawing;

using System.Drawing.Drawing2D;

using System.Collections;

using System.ComponentModel;

using System.Windows.Forms;

using System.Data;



namespace DoubleBuffer
{

    /// <summary>

    /// Summary description for Form1.

    /// </summary>

    public class Formtest : System.Windows.Forms.Form
    {



        private System.Windows.Forms.Timer timer1;

        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.CheckBox checkBox1;



        float _angle;

        bool _doBuffer;



        public Formtest()
        {

            //

            // Required for Windows Form Designer support

            //

            InitializeComponent();



            //

            // TODO: Add any constructor code after InitializeComponent call

            //

        }



        /// <summary>

        /// Clean up any resources being used.

        /// </summary>

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {

                if (components != null)
                {

                    components.Dispose();

                }

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

            this.timer1 = new System.Windows.Forms.Timer(this.components);

            this.checkBox1 = new System.Windows.Forms.CheckBox();

            this.SuspendLayout();

            //

            // timer1

            //

            this.timer1.Enabled = true;

            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            //

            // checkBox1

            //

            this.checkBox1.Location = new System.Drawing.Point(8, 8);

            this.checkBox1.Name = "checkBox1";

            this.checkBox1.TabIndex = 0;

            this.checkBox1.Text = "Double Buffer";

            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);

            //

            // Form1

            //

            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);

            this.ClientSize = new System.Drawing.Size(292, 273);

            this.Controls.Add(this.checkBox1);

            this.Name = "Form1";

            this.Text = "Form1";

            this.ResumeLayout(false);



        }

        #endregion



        /// <summary>

        /// The main entry point for the application.

        /// </summary>

        //[STAThread]

        //static void Main()
        //{

        //    Application.Run(new Formtest());

        //}



        private void timer1_Tick(object sender, System.EventArgs e)
        {

            _angle += 3;

            if (_angle > 359)

                _angle = 0;

            Invalidate();

        }



        private Bitmap _backBuffer;



        protected override void OnPaint(PaintEventArgs e)
        {

            if (_backBuffer == null)
            {

                _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            }



            Graphics g = null;

            if (_doBuffer)

                g = Graphics.FromImage(_backBuffer);

            else

                g = e.Graphics;



            g.Clear(Color.White);



            g.SmoothingMode = SmoothingMode.AntiAlias;



            Matrix mx = new Matrix();

            mx.Rotate(_angle, MatrixOrder.Append);

            mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);

            g.Transform = mx;

            g.FillRectangle(Brushes.Red, -100, -100, 200, 200);



            mx = new Matrix();

            mx.Rotate(-_angle, MatrixOrder.Append);

            mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);

            g.Transform = mx;

            g.FillRectangle(Brushes.Green, -75, -75, 149, 149);



            mx = new Matrix();

            mx.Rotate(_angle * 2, MatrixOrder.Append);

            mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);

            g.Transform = mx;

            g.FillRectangle(Brushes.Blue, -50, -50, 100, 100);



            if (_doBuffer)
            {

                g.Dispose();



                //Copy the back buffer to the screen



                e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0);

            }



            //base.OnPaint (e); //optional but not recommended

        }



        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

            //Don't allow the background to paint

        }



        protected override void OnSizeChanged(EventArgs e)
        {

            if (_backBuffer != null)
            {

                _backBuffer.Dispose();

                _backBuffer = null;

            }

            base.OnSizeChanged(e);

        }



        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {

            _doBuffer = this.checkBox1.Checked;

        }

    }

}