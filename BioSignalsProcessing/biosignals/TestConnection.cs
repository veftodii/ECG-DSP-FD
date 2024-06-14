using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace biosignals
{
    public partial class TestConnection : Form
    {
      public TestConnection()
        {
            InitializeComponent();
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.PortName = "COM1";
            serialPort1.ReadTimeout = 500;
            serialPort1.ReceivedBytesThreshold = 1;
        }

        private void check_Click(object sender, EventArgs e)
        {
            portslist.Items.Clear();
            portslist.Items.Add("<None>");
            portslist.Items.AddRange(SerialPort.GetPortNames());
            portslist.SelectedItem = portslist.Items[0];
            logwnd.Text = "Available ports: \r\n"+Environment.NewLine;
            for (int i = 1; i < portslist.Items.Count; i++)
                logwnd.AppendText((String)portslist.Items[i] + Environment.NewLine);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            open.Enabled = false;
            close.Enabled = false;
            logwnd.Enabled = false;
            clear.Enabled = false;
            send.Enabled = false;
            textBox2.Enabled = false;
            read.Enabled = false;
            asciibt.Checked = true;
        }

        private void FormDesign()
        {
            if (portslist.SelectedItem.Equals(portslist.Items[0]))
            {
                open.Enabled = false;
                close.Enabled = false;
                logwnd.Enabled = false;
                read.Enabled = false;
                send.Enabled = false;
                textBox2.Enabled = false;
                
            }
            else
            {
                if (serialPort1.IsOpen && serialPort1.PortName == (String)portslist.SelectedItem)
                {
                    open.Enabled = false;
                    close.Enabled = true;
                    logwnd.Enabled = true;
                    read.Enabled = true;
                    send.Enabled = true;
                    textBox2.Enabled = true;
                }
                else
                {
                    //if (serialPort1.IsOpen) open.Enabled = false;
                    //else open.Enabled = true;
                    open.Enabled = true;
                    close.Enabled = false;
                    logwnd.Enabled = false;
                    read.Enabled = false;
                    send.Enabled = false;
                    textBox2.Enabled = false;
                }
            }
        }

        private void portslist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FormDesign();   
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            serialPort1.PortName = portslist.SelectedItem.ToString();
            try
            {
                if (!serialPort1.IsOpen) serialPort1.Open();
                logwnd.AppendText("\r\nPort "+serialPort1.PortName+" is open" + Environment.NewLine);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error open");
            }
            FormDesign();
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            logwnd.AppendText("\r\nPort " + serialPort1.PortName + " is closed" + Environment.NewLine);
            FormDesign();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            logwnd.Clear();
        }

        private void logwnd_TextChanged(object sender, EventArgs e)
        {
            if (logwnd.Text.Length == 0) clear.Enabled = false;
            else clear.Enabled = true;
        }

        private void datacomevent(string header, byte[] data)
        {
            string tmp = String.Empty;
            if (hexbt.Checked)
            {
                for (int i = 0; i < data.Length; i++)
                    tmp += String.Format(" 0x{0:X2}", data[i]);
                logwnd.AppendText(header + tmp);
            }
            else if (binbt.Checked)
            {
                for (int i = 0; i < data.Length; i++)
                    tmp += " " + Convert.ToString(data[i], 2).PadLeft(8,'0');
                logwnd.AppendText(header + tmp);
            }
            else if (asciibt.Checked)
            {
                for (int i = 0; i < data.Length; i++)
                    tmp += Convert.ToChar(data[i]).ToString();
                logwnd.AppendText(header + tmp);
            }
        }

        private void read_Click(object sender, EventArgs e)
        {
            byte[] s = new byte[serialPort1.BytesToRead];
            serialPort1.Read(s, 0, serialPort1.BytesToRead);
            if (serialPort1.IsOpen) datacomevent("\r\nread " + (k++).ToString() + " >> ", s);
        }

        private void send_Click(object sender, EventArgs e)
        {
            char[] c = textBox2.Text.ToCharArray();
            if (serialPort1.IsOpen) serialPort1.Write(c, 0, c.Length);
        }
        
        int k = 0;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           byte[] s = new byte[serialPort1.BytesToRead];
           serialPort1.Read(s, 0, serialPort1.BytesToRead);
           logwnd.Invoke(new MethodInvoker(delegate { datacomevent("\r\nread event " + (k++).ToString() + " >> ", s); }));
        }
            
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(serialPort1.IsOpen) serialPort1.Close();
        }
    }
}
