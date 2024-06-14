using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace biosignals
{
    public partial class AddChannel : Form
    {
        private byte code;
        public byte ChannelValue
        {
            get
            {
                switch(chValues.SelectedIndex)
                {
                    case 0: code = GraphDisplayLib.DataSource.ECG; break;
                    case 1: code = GraphDisplayLib.DataSource.EMG; break;
                    case 2: code = GraphDisplayLib.DataSource.ERG; break;
                }
                return code;
            }
        }

        public byte HPFValue
        {
            get
            {
                switch (hpfValues.SelectedIndex)
                {
                    case 0: code = GraphDisplayLib.DataSource.HPF50mHz; break;
                }
                return code;
            }
        }

        public byte LPFValue
        {
            get
            {
                switch (lpfValues.SelectedIndex)
                {
                    case 0: code = GraphDisplayLib.DataSource.LPF40Hz; break;
                    case 1: code = GraphDisplayLib.DataSource.LPF100Hz; break;
                    case 2: code = GraphDisplayLib.DataSource.LPF10kHz; break;
                }
                return code;
            }
        }

        public AddChannel()
        {
            InitializeComponent();
            chValues.Items.AddRange(new object[] {"ECG (Electrocardiogram)", "EMG (Electromyogram)", "ERG (Electroretinogram)"});
            hpfValues.Items.AddRange(new object[] {"HPF 50 mHz"});
            lpfValues.Items.AddRange(new object[] {"LPF 40 Hz", "LPF 100 Hz", "LPF 10 kHz"});
        }

        public AddChannel(string ctype) : this()
        {
            if (ctype == "remove")
            {
                this.Text = "Remove channel";
                OKbt.Text = "Remove";
            }
            else
            {
                this.Text = "Add channel";
                OKbt.Text = "Add";
            }
        }

        private void AddChannel_Load(object sender, EventArgs e)
        {
            chValues.SelectedItem = chValues.Items[0];
            hpfValues.SelectedItem = hpfValues.Items[0];
            lpfValues.SelectedItem = lpfValues.Items[1];
        }

        private void OKbt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancelbt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
