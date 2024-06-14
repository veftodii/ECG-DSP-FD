using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace GraphDisplayLib
{
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GraphContainer : UserControl
    {
     
        public GraphContainer()
        {
            InitializeComponent();
        }

        //private void GraphContainer_Load(object sender, EventArgs e)
        //{
        //    //this.ResizeRedraw = true;
        //}
        
}
}
