using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GraphDisplayLib;
using GraphDisplayLib.Processing;
using System.Numerics;
using System.IO.Ports;

namespace biosignals
{  
    public partial class MainWnd : Form
    {
        private DataSource data1;
        private string filename = "16265-normalecg.txt";
        private int ns = 2048;

        public MainWnd()
        {
            InitializeComponent();
            data1 = new DataSource();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            toolStrip1.ImageList = imageList1;
            imageList1.ImageSize = new Size(32, 32);
            stopbt.Image = imageList1.Images[0];
            startbt.Image = imageList1.Images[1];
            comboBox1.Items.AddRange(new string[] { "16265-normalecg.txt", "16773-normalecg.txt", "19140-normalecg.txt", "04015-atrialfib.txt", "04936-atrialfib.txt", "07859-atrialfib.txt"});
            comboBox1.SelectedIndex = 0;
            trackBar1.Value = 11;
            ns = (int)Math.Pow(2.0, (double)trackBar1.Value);
            label3.Text = "Samples: " + ns.ToString();
            comboBox2.Items.AddRange(SerialPort.GetPortNames());
            comboBox2.SelectedIndex = 0;
            //data1.readdata(@"D:\Downloads\diploma\16265-normalecg.txt", 1);
            //data1.readdata(@"D:\Downloads\diploma\16773-normalecg.txt", 1);
            //data1.readdata(@"D:\Downloads\diploma\19140-normalecg.txt", 1);
            //data1.readdata(@"D:\Downloads\diploma\04015-atrialfib.txt", 1);
            //data1.readdata(@"D:\Downloads\diploma\04936-atrialfib.txt", 1);
            //data1.readdata(@"D:\Downloads\diploma\07859-atrialfib.txt", 1);
            
        }
        
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            data1.CloseConnection();
        }

        private void startbt_Click(object sender, EventArgs e)
        {
            data1.AddCommand(DataSource.STARTCONVERSION, 0, 0, 0);
        }

        private void stopbt_Click(object sender, EventArgs e)
        {
            data1.AddCommand(DataSource.STOPCONVERSION, 0, 0, 0);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TestConnection test = new TestConnection();
            test.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddChannel addch = new AddChannel("add");
            addch.ShowDialog();
            if (addch.DialogResult == DialogResult.OK) data1.AddCommand(DataSource.ADDCHANNEL, addch.ChannelValue, addch.HPFValue, addch.LPFValue);
        }

        private void connectbt_Click(object sender, EventArgs e)
        {
            data1.SetConnectionSettings(new ConnectionSettings(comboBox2.SelectedItem.ToString(), 115200));
            if(data1.OpenConnection()) toolStripStatusLabel1.Text = "Port " + comboBox2.SelectedItem.ToString() + " opened.";
        }

        private void disconnectbt_Click(object sender, EventArgs e)
        {
            if (data1.CloseConnection()) toolStripStatusLabel1.Text = "Port closed."; ;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            AddChannel addch = new AddChannel("remove");
            addch.ShowDialog();
            if (addch.DialogResult == DialogResult.OK) data1.AddCommand(DataSource.REMOVECHANNEL, addch.ChannelValue, addch.HPFValue, addch.LPFValue);
        }

        private void rpeaksDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length == 0) return;
            BinaryReader r = new BinaryReader(File.OpenRead(openFileDialog1.FileName));
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            if (fi.Length == 0)
            {
                MessageBox.Show("This is an empty file !", "Read file error");
                toolStripStatusLabel1.Text = "R-peaks detection operation canceled";
                return;
            }
            int N = (int)Math.Ceiling(Math.Log(fi.Length, 2));
            N = (int)Math.Pow(2.0, (double)N);
            double[] Y = new double[N];
            int i = 0;
            try
            {
                for (i = 0; i < fi.Length; i++) Y[i] = (double)r.ReadByte();
            }
            catch (EndOfStreamException ex)
            {
                if ((long)i != fi.Length) toolStripStatusLabel1.Text = "File is corupted !";
                return;
            }
            ECG_Processing ecgh = new ECG_Processing();
            double[] Rpeak = ecgh.Detect_RPeaks(Y,6646);
            for (i = 0; i < Rpeak.Length; i++) Rpeak[i] *= Y[i];
            graphPanel1.Title = "R-peaks ( " + openFileDialog1.SafeFileName + " )";
            graphPanel1.Plot(Y, new LineProperties(Color.LimeGreen));
            graphPanel1.Hold = true;
            graphPanel1.Stem(Rpeak, new LineProperties(Color.Black,1.0F,DashStyle.Dash, new float[]{3.0F, 3.0F}));
            graphPanel1.Hold = false;
        }

        private void fFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length == 0) return;
            BinaryReader r = new BinaryReader(File.OpenRead(openFileDialog1.FileName));
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            if (fi.Length == 0)
            {
                MessageBox.Show("This is an empty file !", "Read file error");
                toolStripStatusLabel1.Text = "FFT operation canceled";
                return;
            }
            int N = (int)Math.Ceiling(Math.Log(fi.Length, 2));
            N = (int)Math.Pow(2.0, (double)N);
          //  double[] Y = new double[N];
            double[] Y = new double[ns];
            int i = 0;
            try
            {
              //  for (i = 0; i < fi.Length; i++) Y[i] = (double)r.ReadByte();
                if (ns > fi.Length) ns = (int)fi.Length;
                for (i = 0; i < ns; i++) Y[i] = (double)r.ReadByte();
            }
            catch (EndOfStreamException ex)
            {
              //  if ((long)i != fi.Length) toolStripStatusLabel1.Text = "File is corupted !";
                //return;
            }
            Complex[] fftresult = new Complex[Y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            bool a = ft.FFT(Y, fftresult, (uint) Y.Length);
            if (!a)
            {
                MessageBox.Show("Can't execute FFT !","FFT error");
                return;
            }
            graphPanel1.Title = "ECG ( " + openFileDialog1.SafeFileName + " )";
            graphPanel1.Plot(Y, new LineProperties(Color.LimeGreen));
            double[] X = new double[Y.Length];
            for (i = 0; i < Y.Length; i++)
            {
                X[i] = (double) 6646 * i / N;
                Y[i] = fftresult[i].Magnitude;
            }
            graphPanel2.Plot(X, Y, new LineProperties(Color.Blue));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            ECG_Processing ecgh = new ECG_Processing();
            double[] Rpeak = ecgh.Detect_RPeaks(data1.SourceY, 1.0/(data1.SourceX[1]-data1.SourceX[0]));
            for (int i = 0; i < Rpeak.Length; i++) Rpeak[i] *= data1.SourceY[i];
            double[] y = new double[ns];
            double[] r = new double[ns];
            for (int i = 0; i < ns; i++) y[i] = data1.SourceY[i];
            for (int i = 0; i < ns; i++) r[i] = Rpeak[i];
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            graphPanel1.Hold = true;
            graphPanel1.Stem(r, new LineProperties(Color.Black, 1.0F, DashStyle.Dash, new float[] { 3.0F, 3.0F }),false);
            graphPanel1.Hold = false;
            graphPanel2.Title = "original ECG (2048 samples)";
            graphPanel2.Plot(y, new LineProperties(Color.LimeGreen));
            double heartrate = -1.0, avgrate = 0.0;
            int a = -1, b = 0;
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] == 0) continue;
                if (a == -1)
                {
                    a = i;
                    continue;
                }
                if (heartrate == -1.0) heartrate = 60.0 / (((double)i - a) * (data1.SourceX[1] - data1.SourceX[0]));
                avgrate += ((double)i - a) * (data1.SourceX[1] - data1.SourceX[0]);
                a = i; b++;
            }
            if (b != 0)
            {
                avgrate = avgrate / (double)b;
                avgrate = 60.0 / avgrate;
                b++;
                toolStripStatusLabel1.Text = "Detected: " + b.ToString() + " R-peaks;   HeartRate: " + Math.Round(heartrate).ToString("0.#") + " bpm;   AVGHeartRate: " + avgrate.ToString("0.#") + " bpm";
                graphPanel1.Title = "R-peaks + original ECG (2048 samples)  ( HeartRate: " + Math.Round(heartrate).ToString("0.#") + " bpm;   AVGHeartRate: " + avgrate.ToString("0.#") + " bpm )";
            }
            else toolStripStatusLabel1.Text = "No peak detected !";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            double[] y = new double[ns];
            for (int i = 0; i < ns; i++) y[i] = data1.SourceY[i];
            Complex[] fftresult = new Complex[y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            bool a = ft.FFT(y, fftresult, (uint)y.Length);
            if (!a)
            {
                MessageBox.Show("Can't execute FFT !", "FFT error");
                return;
            }
            graphPanel1.Title = "original ECG (2048 samples)";
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            double[] X = new double[(int)(y.Length / 2)];
            double[] Y = new double[(int)(y.Length / 2)];
            double f1 = 1.0 / (data1.SourceX[1] - data1.SourceX[0]);
            for (int i = 0; i < (int)(y.Length / 2); i++)
            {
                X[i] = ((double)f1 * (double)i) / (double)y.Length;
                Y[i] = fftresult[i].Magnitude;
            }
            graphPanel2.Title = "FFT halfed (Hz)";
            graphPanel2.Plot(X,Y, new LineProperties(Color.Blue));
            double f = f1 / 2.0;
            toolStripStatusLabel1.Text = "Nyquist frequency:  " + f.ToString("0.##") + " Hz";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            int N = 64;
            double T = 1.0 / 128.0;
            double[] y = new double[N];
            for (int i = 0; i < N; i++) y[i] = Math.Sin(2.0 * Math.PI * 20.0 * T * ((double)i));
            graphPanel1.Title = "Sinusoidal signal, F=20 Hz, Fe=128 Hz";
            graphPanel1.Plot(y, new LineProperties(Color.Red));
            Complex[] fftresult = new Complex[y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            bool a = ft.FFT(y, fftresult, (uint)y.Length);
            if (!a)
            {
                MessageBox.Show("Can't execute FFT !", "FFT error");
                return;
            }
            double[] X = new double[(int)(y.Length / 2)];
            double[] Y = new double[(int)(y.Length / 2)];
            double f1 = 1.0 / (data1.SourceX[1] - data1.SourceX[0]);
            for (int i = 0; i < (int)(y.Length/2); i++)
            {
                X[i] = ((double)f1 * (double)i) / (double)y.Length;
                Y[i] = fftresult[i].Magnitude;
            }
            graphPanel2.Title = "FFT of function (Hz)";
            graphPanel2.Plot(X,Y, new LineProperties(Color.Blue));
            double f = 1.0 / (2.0 * T);
            toolStripStatusLabel1.Text = "Nyquist frequency:  " + f.ToString("0.##") + " Hz";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            double[] y = new double[ns];
            for (int i = 0; i < ns; i++) y[i] = data1.SourceY[i];
            Complex[] fftresult = new Complex[y.Length];
            Complex[] fftresultshift = new Complex[y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            bool a = ft.FFT(y, fftresult, (uint)y.Length);
            if (!a)
            {
                MessageBox.Show("Can't execute FFT !", "FFT error");
                return;
            }
            ft.FFTShift(fftresult, fftresultshift, (uint)y.Length);
            graphPanel1.Title = "original ECG (2048 samples)";
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            double[] X = new double[y.Length];
            double[] Y = new double[y.Length];
            double f1 = 1.0 / (data1.SourceX[1] - data1.SourceX[0]);
            double f = f1 / 2.0;
            //for (int i = 0; i < y.Length; i++)
            //{
            //    X[i] = ((double)f1 * (double)i) / (double)y.Length;
            //    Y[i] = fftresultshift[i].Magnitude;
            //}
            //graphPanel1.Plot(X, Y, new LineProperties(Color.Blue));
            int j = 0;
            int half = (int) y.Length / 2;
            for (int i = -half; i < half; i++)
            {
                X[j] = (double)f1 * (double)i / (double)y.Length;
                Y[j] = fftresultshift[j].Magnitude;
                j++;
            }
            graphPanel2.Title = "FFT shifted for zero base frequency (Hz)";
            graphPanel2.Plot(X, Y, new LineProperties(Color.Blue));
            toolStripStatusLabel1.Text = "Nyquist frequency:  " + f.ToString("0.##") + " Hz";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            double[] y = new double[ns];
            for (int i = 0; i < ns; i++) y[i] = data1.SourceY[i];
            Complex[] fftresult = new Complex[y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            bool a = ft.FFT(y, fftresult, (uint)y.Length);
            if (!a)
            {
                MessageBox.Show("Can't execute FFT !", "FFT error");
                return;
            }
            graphPanel1.Title = "original ECG (2048 samples)";
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            double[] X = new double[y.Length];
            double[] Y = new double[y.Length];
            double f1 = 1.0 / (data1.SourceX[1] - data1.SourceX[0]);
            for (int i = 0; i < y.Length; i++)
            {
                X[i] = ((double)f1 * (double)i) / (double)y.Length;
                Y[i] = fftresult[i].Magnitude;
            }
            graphPanel2.Title = "FFT (Hz)";
            graphPanel2.Plot(X, Y, new LineProperties(Color.Blue));
            double f = f1 / 2.0;
            toolStripStatusLabel1.Text = "Nyquist frequency:  " + f.ToString("0.##") + " Hz";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            ECG_Processing ecgh = new ECG_Processing();
            double[] Rpeak = ecgh.Detect_RPeaks(data1.SourceY, 1.0/(data1.SourceX[1]-data1.SourceX[0]));
            for (int i = 0; i < Rpeak.Length; i++) Rpeak[i] *= data1.SourceY[i];
            //int N = data1.SourceY.Length;
            int N = ns;
            double[] y = new double[N];
            double[] r = new double[N];
            for (int i = 0; i < N; i++) y[i] = data1.SourceY[i];
            for (int i = 0; i < N; i++) r[i] = Rpeak[i];
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            graphPanel1.Title = "Original ECG (2048 samples)";
            int a = -1;
            List<double> times = new List<double>();
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] == 0) continue;
                if (a == -1)
                {
                    a = i;
                    continue;
                }
                times.Add(((double)(i - a)) * (data1.SourceX[1] - data1.SourceX[0]));
                a = i;
            }
            toolStripStatusLabel1.Text = "Detected: " + (times.Count+1).ToString() + " R-peaks";
            if (times.Count == 0)
            {
                MessageBox.Show("No R-R interval detected !", "Histogram error");
                return;
            }
            double x1 = Math.Floor(times.Min() * 100.0) / 100.0;
            double x2 = Math.Ceiling(times.Max() * 100.0) / 100.0;
            List<double> X1 = new List<double>();
            while (x1 <= x2+0.01)
            {
                X1.Add(x1);
                x1 += 0.01; // step
            }
            double[] c = graphPanel2.Histogram(X1.ToArray(), times.ToArray(), new LineProperties(Color.White, 1F, DashStyle.Solid, null, Brushes.Indigo));
            graphPanel2.Title = "Histogram of R-peaks period";
            x1 = c.Where<double>(dv => dv != 0.0).Min();
            x2 = c.Max();
            X1.Clear();
            while (x1 <= x2+1)
            {
                X1.Add(x1);
                x1 += 1.0; // step
            }
            c = graphPanel3.Histogram(X1.ToArray(), c.ToArray(), new LineProperties(Color.White, 1F, DashStyle.Solid, null, Brushes.Blue));
            graphPanel3.Title = "Histogram from above histogram";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            graphPanel3.Clear();
            data1.readdata(filename, 1);
            ECG_Processing ecgh = new ECG_Processing();
            double[] Rpeak = ecgh.Detect_RPeaks(data1.SourceY, 1.0 / (data1.SourceX[1] - data1.SourceX[0]));
            for (int i = 0; i < Rpeak.Length; i++) Rpeak[i] *= data1.SourceY[i];
            //int N = data1.SourceY.Length;
            int N = ns;
            double[] y = new double[N];
            double[] r = new double[N];
            for (int i = 0; i < N; i++) y[i] = data1.SourceY[i];
            for (int i = 0; i < N; i++) r[i] = Rpeak[i];
            graphPanel1.Plot(y, new LineProperties(Color.LimeGreen));
            graphPanel1.Title = "Original ECG (2048 samples)";
            Complex[] fftresult = new Complex[y.Length];
            FastFourierTransform ft = new FastFourierTransform();
            int a = -1, b = 0; int n;
            double[] t;
            Random rd = new Random();
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] == 0) continue;
                if (a == -1)
                {
                    a = i;
                    continue;
                }
                n = (int)Math.Ceiling(Math.Log(i - a + 1, 2));
                n = (int)Math.Pow(2.0, (double)n);
                t = new double[n];
                for (int j = 0; j < i - a + 1; j++) t[j] = y[j + a];
                if (!ft.FFT(t, fftresult, (uint)t.Length))
                {
                    MessageBox.Show("Can't execute FFT !", "FFT error");
                    return;
                }
                double[] X = new double[(int)(t.Length / 2)];
                double[] Y = new double[(int)(t.Length / 2)];
                double f1 = 1.0 / (data1.SourceX[1] - data1.SourceX[0]);
                for (int k = 0; k < (int)(t.Length / 2); k++)
                {
                    X[k] = ((double)f1 * (double)k) / (double)t.Length;
                    Y[k] = fftresult[k].Magnitude;
                }
                graphPanel2.Title = "FFT halfed for each segment (Hz)";
                graphPanel2.Plot(X, Y, new LineProperties(Color.FromArgb(rd.Next(0, 255), rd.Next(0, 255), rd.Next(0, 255))));
                graphPanel2.Hold = true;
                a = i; b++;
            }
            toolStripStatusLabel1.Text = "Detected: " + (b + 1).ToString() + " R-peaks";
            if (b == 0)
            {
                MessageBox.Show("No R-R interval detected !", "FFT on R-R interval");
                return;
            }
            graphPanel2.Hold = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ns = (int)Math.Pow(2.0, (double)trackBar1.Value);
            label3.Text = "Samples: " + ns.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filename = comboBox1.SelectedItem.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length == 0) return;
            BinaryReader r = new BinaryReader(File.OpenRead(openFileDialog1.FileName));
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            if (fi.Length == 0)
            {
                MessageBox.Show("This is an empty file !", "Read file error");
                toolStripStatusLabel1.Text = "R-peaks detection operation canceled";
                return;
            }
            int N = (int)Math.Ceiling(Math.Log(fi.Length, 2));
            N = (int)Math.Pow(2.0, (double)N);
            double[] Y = new double[ns];
            int i = 0;
            try
            {
                if(ns>fi.Length) ns = (int)fi.Length;
                for (i = 0; i < ns; i++) Y[i] = (double)r.ReadByte();
            }
            catch (EndOfStreamException ex)
            {
               // if ((long)i != fi.Length) toolStripStatusLabel1.Text = "File is corupted !";
               // return;
            }
            
            graphPanel1.Title = openFileDialog1.SafeFileName;
            graphPanel1.Plot(Y, new LineProperties(Color.Blue));
       }
   }
}

// To file
//string filename = @"D:\Downloads\diploma\BioSignalsProcessing\biosignals\Rpeaks.txt";
//using (StreamWriter w = new StreamWriter(filename))
//{
//    for (int i=0;i<Rpeaks.Length;i++)
//        w.WriteLine("{0,10} {1,10} {2,10}", data1.Source[i].x.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat), data1.Source[i].y.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat), Rpeaks[i].ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
//    w.Close();
//}