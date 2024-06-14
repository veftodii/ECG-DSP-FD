using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel.Design;

namespace GraphDisplayLib
{
    /// <summary>
    /// This class is reprezenting an control that act like a plotter
    /// </summary>
    
    public partial class GraphPanel : UserControl
    {
#region Global variables

        private Bitmap GraphBMPBuffer = null;
        private Bitmap XAxisBMPBuffer = null;
        private Bitmap YAxisBMPBuffer = null;
        private Color graphbkcolor;
        private bool hold;
        private GridTypes grid;
        private List<GraphObject> gobjects;
        private Font xAxisFont, yAxisFont;

#endregion Global variables

        public GraphPanel()
        {
            InitializeComponent();
            graphbkcolor = Color.White;
            hold = false;
            gobjects = new List<GraphObject>();
            grid = GridTypes.BothGrids;
            xAxisFont = new Font("Times New Roman", 9, FontStyle.Regular);
            yAxisFont = new Font("Times New Roman", 9, FontStyle.Regular);

            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); // .NET Framework 2.0 built-in double buffer
        }

#region Properties

        [Category("Properties")]  // take this out, and you will soon have problems with serialization;
        [DefaultValue(typeof(string), "Graph 1")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get { return GraphTitle.Text; }
            set
            {
                GraphTitle.Text = value;
                this.Invalidate(true);
            }
        }

        [Category("Properties")]
        [DefaultValue(typeof(Color), "White")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color GraphBkColor
        {
            get { return graphbkcolor; }
            set
            {
                graphbkcolor = value;
                this.Invalidate(true);
            }
        }

        [Category("Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public GridTypes Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                this.Invalidate(true);
            }
        }

        public bool Hold
        {
            set { hold = value; }
        }
        
#endregion // Properties      
        
#region Draw methods

        public void Plot(double[] X, double[] Y, LineProperties lineprop)
        {
            if (X.Length != Y.Length)
            {
                MessageBox.Show("X must have same length as Y !", "Plot Error !");
                return;
            }
            if (!hold)
                if (gobjects.Count != 0) gobjects.Clear();
            gobjects.Add(new GraphObject(X, Y));
            gobjects[gobjects.Count - 1].line.properties = lineprop;
            this.Refresh();
        }

        public void Plot(double[] Y, LineProperties lineprop)
        {
            double[] X = new double[Y.Length];
            for (int i = 0; i < X.Length; i++) X[i] = i;
                if (!hold)
                    if (gobjects.Count != 0) gobjects.Clear();
            gobjects.Add(new GraphObject(X, Y));
            gobjects[gobjects.Count - 1].line.properties = lineprop;
            this.Refresh();
        }

        public void Stem(double[] X, double[] Y, LineProperties lineprop, bool StartFromZeroOffSet = true, bool RemoveZeroPoints = true)
        {
            if (X.Length != Y.Length)
            {
                MessageBox.Show("X must have same length as Y !", "Stem Error !");
                return;
            }
            if (!hold)
                if (gobjects.Count != 0) gobjects.Clear();
            gobjects.Add(new GraphObject(X, Y, GraphTypes.STEM));
            gobjects[gobjects.Count - 1].line.properties = lineprop;
            gobjects[gobjects.Count - 1].StartFromZeroOffSet = StartFromZeroOffSet;
            gobjects[gobjects.Count - 1].RemoveZeroPoints = RemoveZeroPoints;
            this.Refresh();
        }

        public void Stem(double[] Y, LineProperties lineprop, bool StartFromZeroOffSet = true, bool RemoveZeroPoints = true)
        {
            double[] X = new double[Y.Length];
            for (int i = 0; i < X.Length; i++) X[i] = i;
            if (!hold)
                if (gobjects.Count != 0) gobjects.Clear();
            gobjects.Add(new GraphObject(X, Y, GraphTypes.STEM));
            gobjects[gobjects.Count - 1].line.properties = lineprop;
            gobjects[gobjects.Count - 1].StartFromZeroOffSet = StartFromZeroOffSet;
            gobjects[gobjects.Count - 1].RemoveZeroPoints = RemoveZeroPoints;
            this.Refresh();
        }

        public double[] Histogram(double[] Edges, double[] Y, LineProperties lineprop)
        {
            double[] counted = new double[Edges.Length-1];
            for (int i = 0; i < Edges.Length - 1; i++) counted[i] = 0.0;
            for (int i = 0; i < Edges.Length - 1; i++)
                for (int j = 0; j < Y.Length; j++)
                    if (Y[j] >= Edges[i] && Y[j] < Edges[i + 1]) counted[i] = counted[i] + 1.0;
            Bar(Edges, counted, lineprop);
            return counted;
        }

        public void Bar(double[] Edges, double[] Y, LineProperties lineprop)
        {
            // if (Edges.Length == Y.Length) center each bar on edges[i] label
            if (Edges.Length != Y.Length + 1)
            {
                MessageBox.Show("X length must be as Y+1 length !", "Bar Error !");
                return;
            }
            double[] Y1 = new double[Edges.Length];
            for (int i = 0; i < Y.Length; i++) Y1[i] = Y[i];
            Y1[Y1.Length - 1] = 0.0;
            if (!hold)
                if (gobjects.Count != 0) gobjects.Clear();
            gobjects.Add(new GraphObject(Edges, Y1, GraphTypes.BAR));
            gobjects[gobjects.Count - 1].line.properties = lineprop;
            this.Refresh();
        }

        private void SelectWndSize(out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = 0.0; maxX = 10.0; minY = 0.0; maxY = 1.0;
            if (gobjects.Count == 0) return;
            bool firsttime = true;
            double tminx, tminy, tmaxx, tmaxy;
            foreach (GraphObject gobj in gobjects)
            {
                tminx = gobj.line.XData.Min();
                tmaxx = gobj.line.XData.Max();
                tminy = gobj.line.YData.Min();
                tmaxy = gobj.line.YData.Max();
                if (firsttime)
                {
                    minX = tminx; maxX = tmaxx;
                    minY = tminy; maxY = tmaxy;
                    firsttime = !firsttime;
                    continue;
                }
                if (minX > tminx) minX = tminx;
                if (maxX < tmaxx) maxX = tmaxx;
                if (minY > tminy) minY = tminy;
                if (maxY < tmaxy) maxY = tmaxy;
            }
            if (maxX - minX == 0.0)
            {
                minX -= 1.0; maxX += 1.0;
            }
            if (maxY - minY == 0.0)
            {
                minY -= 1.0; maxY += 1.0;
            }
        }

        public void Clear()
        {
            gobjects.Clear();
            this.Refresh();
        }

        private void DrawGraph()
        {
            if (GraphBMPBuffer == null) GraphBMPBuffer = new Bitmap(DisplayBox.ClientRectangle.Width, DisplayBox.ClientRectangle.Height, PixelFormat.Format24bppRgb);
            Graphics gdc = Graphics.FromImage(GraphBMPBuffer);
            gdc.SmoothingMode = SmoothingMode.AntiAlias;
            gdc.Clear(graphbkcolor);
            if (gobjects.Count != 0)
            {
                DoublePoint drawpoint, CurrentPoint;
                double XWndMinRange, XWndMaxRange, YWndMinRange, YWndMaxRange;
                double ScaleX = 1.0, ScaleY = 1.0, TranslateX = 0.0, TranslateY = 0.0;
                bool firstpoint = true;
                DoublePoint ZeroOffSet;
                // Detect highest interval on X and Y axis
                SelectWndSize(out XWndMinRange, out XWndMaxRange, out YWndMinRange, out YWndMaxRange);
                ScaleX = (double)(DisplayBox.ClientRectangle.Width) / (XWndMaxRange - XWndMinRange);
                ScaleY = (double)(DisplayBox.ClientRectangle.Height) / (YWndMaxRange - YWndMinRange);
                TranslateX = (double)(DisplayBox.ClientRectangle.Left) - ScaleX * XWndMinRange;
                TranslateY = (double)(DisplayBox.ClientRectangle.Top) - ScaleY * YWndMinRange;
                ZeroOffSet.x = 0.0 * ScaleX + TranslateX;
                ZeroOffSet.y = (double)(DisplayBox.ClientRectangle.Top + DisplayBox.ClientRectangle.Bottom) - (0.0 * ScaleY + TranslateY);
                ZeroOffSet.x = Math.Round(ZeroOffSet.x);
                ZeroOffSet.y = Math.Round(ZeroOffSet.y);
                CurrentPoint.x = 0.0; CurrentPoint.y = 0.0;
                foreach (GraphObject gobj in gobjects)
                {
                    firstpoint = true;
                    Pen pen1 = new Pen(gobj.line.properties.Color, gobj.line.properties.Width);
                    pen1.DashStyle = gobj.line.properties.Style;
                    if (gobj.line.properties.StylePattern != null) pen1.DashPattern = gobj.line.properties.StylePattern;
                    Pen pen2 = new Pen(pen1.Color,pen1.Width);
                    pen2.DashStyle = DashStyle.Solid;
                    for (int i = 0; i < gobj.line.XData.Length; i++)
                    {
                        drawpoint.x = gobj.line.XData[i] * ScaleX + TranslateX;
                        drawpoint.y = (double)(DisplayBox.ClientRectangle.Top + DisplayBox.ClientRectangle.Bottom) - (gobj.line.YData[i] * ScaleY + TranslateY);
                        drawpoint.x = Math.Round(drawpoint.x);
                        drawpoint.y = Math.Round(drawpoint.y);
                        if (firstpoint)
                        {
                            CurrentPoint.x = drawpoint.x;
                            CurrentPoint.y = drawpoint.y;
                        }
                        switch (gobj.graphtype)
                        {
                            case GraphTypes.PLOT:
                                gdc.DrawLine(pen1, (int)CurrentPoint.x, (int)CurrentPoint.y, (int)drawpoint.x, (int)drawpoint.y);
                                break;
                            case GraphTypes.STEM:
                                if (gobj.RemoveZeroPoints && (gobj.line.YData[i] == 0.0)) break;
                                if (gobj.StartFromZeroOffSet)
                                    gdc.DrawLine(pen1, (int)drawpoint.x, (int)ZeroOffSet.y, (int)drawpoint.x, (int)drawpoint.y);
                                else
                                {
                                    if (gobj.line.YData[i] == 0.0) drawpoint.y = (double)DisplayBox.ClientRectangle.Bottom;
                                    gdc.DrawLine(pen1, (int)drawpoint.x, DisplayBox.ClientRectangle.Bottom, (int)drawpoint.x, (int)drawpoint.y);
                                }
                                gdc.DrawEllipse(pen2, (int)(drawpoint.x - 4.0), (int)(drawpoint.y - 4.0), 8, 8);
                                break;
                            case GraphTypes.BAR:
                                if (i == gobj.line.XData.Length - 1) break;
                                CurrentPoint.x = gobj.line.XData[i+1] * ScaleX + TranslateX;
                                CurrentPoint.y = ZeroOffSet.y;
                                CurrentPoint.x = Math.Round(CurrentPoint.x);
                                gdc.FillRectangle(gobj.line.properties.Fill, (int)drawpoint.x, (int)drawpoint.y, (int)(CurrentPoint.x - drawpoint.x), (int)Math.Abs(CurrentPoint.y - drawpoint.y));
                                gdc.DrawRectangle(pen1, (int)drawpoint.x, (int)drawpoint.y, (int)(CurrentPoint.x - drawpoint.x), (int)Math.Abs(CurrentPoint.y - drawpoint.y));
                                break;
                        }
                        if (firstpoint) firstpoint = !firstpoint;
                        else
                        {
                            CurrentPoint.x = drawpoint.x;
                            CurrentPoint.y = drawpoint.y;
                        }
                    }
                    pen1.Dispose();
                    pen2.Dispose();
                }
                //if (CurrentPoint.x > DisplayBox.ClientRectangle.Right)
                //{
                //    Matrix mx = new Matrix();
                //    mx.Translate(0.1F*(float)DisplayBox.ClientSize.Width, 0.0F, MatrixOrder.Append);
                //    gdc.Transform = mx;
                //    CurrentPoint.x -= 0.1F*(float)DisplayBox.ClientSize.Width;
                //   // MessageBox.Show("wnd limit");
                //}
            }
            gdc.Dispose();
        }

        private Rectangle DrawXLabels()
        {
            if (XAxisBMPBuffer == null) XAxisBMPBuffer = new Bitmap(DisplayBox.Width, 25, PixelFormat.Format24bppRgb);
            Graphics gdc = Graphics.FromImage(XAxisBMPBuffer);
            double XWndMinRange, XWndMaxRange, YWndMinRange, YWndMaxRange;
            SelectWndSize(out XWndMinRange, out XWndMaxRange, out YWndMinRange, out YWndMaxRange);
            string sxmin = String.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat," {0:0.00} ", XWndMinRange);
            string sxmax = String.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat," {0:0.00} ", XWndMaxRange);
            SizeF minbounds = gdc.MeasureString(sxmin, xAxisFont, new PointF(0, 0), new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
            SizeF maxbounds = gdc.MeasureString(sxmax, xAxisFont, new PointF(0, 0), new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
            if (maxbounds.Width < minbounds.Width) maxbounds = minbounds;
            int nlabels = (int)(Math.Floor((double)DisplayBox.Width / (double)maxbounds.Width) + 1.0);
            XAxisBMPBuffer.Dispose();
            gdc.Dispose();
            XAxisBMPBuffer = new Bitmap((int)Math.Ceiling((double)nlabels * (double)maxbounds.Width), (int)Math.Ceiling((double)maxbounds.Height + 4.0), PixelFormat.Format24bppRgb);
            gdc = Graphics.FromImage(XAxisBMPBuffer);
            gdc.Clear(this.BackColor);
            double step = (XWndMaxRange - XWndMinRange) / (nlabels - 1);
            double x = XWndMinRange;
            PointF p = new PointF();
            for (int i = 0; i < nlabels; i++)
            {
                sxmin = String.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat,"{0:0.##}", x);
                minbounds = gdc.MeasureString(sxmin, xAxisFont, new PointF(0, 0), new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
                p.X = (float)i * maxbounds.Width + ((float)(i + 1) * maxbounds.Width - (float)i * maxbounds.Width - minbounds.Width) / 2F;
                p.Y = 2;
                gdc.DrawString(sxmin, xAxisFont, Brushes.Black, p);
                //gdc.DrawRectangle(Pens.DarkRed, p.X, p.Y, minbounds.Width, minbounds.Height);
                //gdc.DrawRectangle(Pens.Plum, (float)i * maxbounds.Width, 1F, maxbounds.Width, maxbounds.Height + 4F);
                x += step;
            }
                //gdc.DrawString("XAxis Label", SystemFonts.DefaultFont, Brushes.Plum, new PointF((float) 1, (float) 5));
                //gdc.DrawString(XWndMinRange.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Black, new PointF(1F, 5F));
            //gdc.DrawString(XWndMaxRange.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Black, new PointF((float)((float)XAxisBMPBuffer.Width) - maxbounds.Width + 1F, 5F));
            gdc.Dispose();
            return new Rectangle((int)Math.Floor((double)DisplayBox.Location.X - (double)(maxbounds.Width /2F)), DisplayBox.Location.Y + DisplayBox.Height + 5, XAxisBMPBuffer.Width, XAxisBMPBuffer.Height);
        }

        private Rectangle DrawYLabels()
        {
            if (YAxisBMPBuffer == null) YAxisBMPBuffer = new Bitmap(DisplayBox.Location.X - 3, DisplayBox.Height + 10, PixelFormat.Format24bppRgb);
            Graphics gdc = Graphics.FromImage(YAxisBMPBuffer);
            double XWndMinRange, XWndMaxRange, YWndMinRange, YWndMaxRange;
            SelectWndSize(out XWndMinRange, out XWndMaxRange, out YWndMinRange, out YWndMaxRange);
            string sxmin = String.Format("{0:0.##}  ", YWndMinRange);
            string sxmax = String.Format("{0:0.##}  ", YWndMaxRange);
            SizeF minbounds = gdc.MeasureString(sxmin, yAxisFont, new PointF(0, 0), new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
            SizeF maxbounds = gdc.MeasureString(sxmax, yAxisFont, new PointF(0, 0), new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
            if (maxbounds.Width < minbounds.Width) maxbounds = minbounds;
            StringFormat szFormat = new StringFormat(StringFormatFlags.DirectionVertical);
            gdc.Clear(this.BackColor);
            //gdc.DrawString("YAxis Label", SystemFonts.DialogFont, Brushes.Red, new PointF((float) 15, (float) 10), szFormat);
            gdc.DrawString(YWndMinRange.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Black, new PointF(1F, (float)YAxisBMPBuffer.Height-15));
            gdc.DrawString(YWndMaxRange.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Black, new PointF(1F, 5F));
            szFormat.Dispose();
            gdc.Dispose();
            return new Rectangle(0, DisplayBox.Location.Y - 5, YAxisBMPBuffer.Width, YAxisBMPBuffer.Height);
        }
        
#endregion // Draw methods
        
#region Events raised

        private void DisplayBox_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph();
            e.Graphics.DrawImageUnscaled(GraphBMPBuffer, 0, 0);
        }

        private void GraphPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rx = DrawXLabels();
            Rectangle ry = DrawYLabels();
            e.Graphics.DrawImageUnscaled(YAxisBMPBuffer, ry);
            e.Graphics.DrawImageUnscaled(XAxisBMPBuffer, rx);
            //e.Graphics.DrawRectangle(Pens.Coral, ry);
            //e.Graphics.DrawRectangle(Pens.DarkMagenta, rx);
        }

        private void GraphPanel_Resize(object sender, EventArgs e)
        {
            if (GraphBMPBuffer != null)
            {
                GraphBMPBuffer.Dispose();
                GraphBMPBuffer = null;
            }
            if (XAxisBMPBuffer != null)
            {
                XAxisBMPBuffer.Dispose();
                XAxisBMPBuffer = null;
            }
            if (YAxisBMPBuffer != null)
            {
                YAxisBMPBuffer.Dispose();
                YAxisBMPBuffer = null;
            }
        }

#endregion // Events raised
    } // End GraphPanel Class

#region Graphical objects

    struct DoublePoint
    {
        public double x;
        public double y;
        public DoublePoint(double x1, double y1)
        {
            x = x1; y = y1;
        }
    }

    public struct LineProperties
    {
        public Color Color;
        public float Width;
        public DashStyle Style;
        public float[] StylePattern;
        public Brush Fill;
        public LineProperties(Color col) : this()
        {
            Color = col;
            Width = 1.0F;
            Style = DashStyle.Solid;
            StylePattern = null;
            Fill = Brushes.Blue;
        }
        public LineProperties(Color col, Brush fill)
        {
            Color = col;
            Width = 1.0F;
            Style = DashStyle.Solid;
            StylePattern = null;
            Fill = fill;
        }
        public LineProperties(Color col, float width, DashStyle st)
        {
            Color = col;
            Width = width;
            Style = st;
            StylePattern = null;
            Fill = Brushes.Blue;
        }
        public LineProperties(Color col, float width, DashStyle st, float[] style_pattern)
        {
            Color = col;
            Width = width;
            Style = st;
            StylePattern = style_pattern;
            Fill = Brushes.Blue;
        }
        public LineProperties(Color col, float width, DashStyle st, float[] style_pattern, Brush fill)
        {
            Color = col;
            Width = width;
            Style = st;
            StylePattern = style_pattern;
            Fill = fill;
        }
    }

    enum GraphTypes { PLOT, STEM, BAR };

    public enum GridTypes { None, XGrid, YGrid, BothGrids};

    class GraphLine
    {
        public double[] XData, YData;
        public LineProperties properties;
        public GraphLine(double[] X, double[] Y)
        {
            if (X.Length == Y.Length)
            {
                XData = new double[X.Length];
                YData = new double[Y.Length];
                X.CopyTo(XData, 0);
                Y.CopyTo(YData, 0);
            }
            properties = new LineProperties(Color.Blue);
        }

    } // End GraphLine Class

    class GraphObject
    {
        public GraphTypes graphtype;
        public GraphLine line;
        private string name;
        public bool StartFromZeroOffSet, RemoveZeroPoints;
        public GraphObject(double[] X, double[] Y, GraphTypes grf_type = GraphTypes.PLOT)
        {
            graphtype = grf_type;
            line = new GraphLine(X, Y);
            name = "Line1";
            StartFromZeroOffSet = true;
            RemoveZeroPoints = true;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length == 0) name = "";
                else name = value; 
            }
        }
    } // End GraphObject Class

#endregion Graphical objects
}

//draw metods (code for delete)
//private void DrawBackground(Graphics g, float CurWidth, float CurHeight, float CurOFFX, float CurOFFY)
//{
//    Rectangle rbgn = new Rectangle((int)CurOFFX, (int)CurOFFY, (int)CurWidth, (int)CurHeight);

//    if (BgndColorTop != BgndColorBot)
//    {
//        using (LinearGradientBrush lb1 = new LinearGradientBrush(new Point((int)0, (int)0),
//                                                                 new Point((int)0, (int)(CurHeight)),
//                                                                 BgndColorTop,
//                                                                 BgndColorBot))
//        {
//            g.FillRectangle(lb1, rbgn);
//        }
//    }
//    else
//    {
//        using (SolidBrush sb1 = new SolidBrush(BgndColorTop))
//        {
//            g.FillRectangle(sb1, rbgn);
//        }
//    }
//}

//private void DrawGrid(Graphics g, DataSource source, float CurrOffX, float CurOffY)
//{
//    using (Pen minorGridPen = new Pen(CurMinGridClor))
//    {
//        minorGridPen.DashPattern = MinorGridPattern;
//        minorGridPen.DashStyle = MinorGridDashStyle;


//        using (Pen p2 = new Pen(CurGridColor))
//        {
//            p2.DashPattern = MajorGridPattern;
//            p2.DashStyle = MajorGridDashStyle;
//            g.DrawLine(minorGridPen, new Point((int)(x + CurrOffX - 0.5f), (int)(CurOffY)),
//                                     new Point((int)(x + CurrOffX - 0.5f), (int)(CurOffY + source.CurGraphHeight)));
//        }
//    }
//}

//private List<int> DrawGraphCurve(Graphics g, DataSource source, float offset_x, float offset_y)
//{
//    using (Pen p = new Pen(source.GraphColor))
//    {
//        if (ps.Count > 0)
//        {
//            g.DrawLines(p, ps.ToArray());
//        }
//    }
//}

//private void DrawGraphCaption(Graphics g, DataSource source, List<int> marker_pos, float offset_x, float offset_y)
//{
//    using (Brush b = new SolidBrush(source.GraphColor))
//    {
//        using (Pen pen = new Pen(b))
//        {
//            pen.DashPattern = new float[] { 2, 2 };

//            g.DrawString(source.Name, legendFont, b, new PointF(offset_x + 12, offset_y + 2));

//        }
//    }
//}

//private void DrawXLabels(Graphics g, DataSource source, List<int> marker_pos, float offset_x, float offset_y)
//{
//    Color XLabColor = source.GraphColor;
//    using (Brush b = new SolidBrush(XLabColor))
//    {
//        using (Pen pen = new Pen(b))
//        {
//            pen.DashPattern = new float[] { 2, 2 };
//            if (MoveMinorGrid == false)
//            {
//                g.DrawLine(pen, x, GraphCaptionLineHeight + offset_y + source.CurGraphHeight - 14, x, GraphCaptionLineHeight + offset_y + source.CurGraphHeight);
//                g.DrawString(value, legendFont, b, new PointF((int)(0.5f + x + offset_x + 4), GraphCaptionLineHeight + offset_y + source.CurGraphHeight - 14));
//            }
//            else
//            {
//                SizeF dim = g.MeasureString(value, legendFont);
//                g.DrawString(value, legendFont, b, new PointF((int)(0.5f + x + offset_x + 4 - dim.Width / 2), GraphCaptionLineHeight + offset_y + source.CurGraphHeight - 14));

//            }
//        }
//    }
//}