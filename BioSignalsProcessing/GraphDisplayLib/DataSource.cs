using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace GraphDisplayLib
{

    /// <summary>
    /// Structure for connection settings with serial ports (port name, baudrate, data bits, etc.)
    /// </summary>
    public struct ConnectionSettings
    {
        public String PortName;
        public int BaudRate;
        public int DataBits;
        public Parity Parity;
        public StopBits StopBits;
        public Handshake Handshake;
        public int ReadTimeout;
        
        public ConnectionSettings(String portname)
        {
            PortName = portname;
            BaudRate = 9600;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
            Handshake = Handshake.None;
            ReadTimeout = -1;
        }

        public ConnectionSettings(String portname, int baudrate) : this(portname)
        {
            BaudRate = baudrate;
        }

        public ConnectionSettings(String portname, int baudrate, int databits, Parity parity, StopBits stopbits, Handshake handshake, int read_timeout = -1)
        {
            PortName = portname;
            BaudRate = baudrate;
            DataBits = databits;
            Parity = parity;
            StopBits = stopbits;
            Handshake = handshake;
            ReadTimeout = read_timeout;
        }
    }

    /// <summary>
    /// This class provide communication with the device
    /// </summary>
    public class DataSource
    {
        // General receive package format
        // [START | PACKID | CMDID | DATA0 | ... | DATA16 | CRC | END]

        // General transmit package format
        // [START | CMDID | DATA0 | DATA1 | DATA2 | CRC | END]

        // General channel selector format	[ CHANNEL | HPF | LPF ]

        #region Constants for pack/unpack data

        // Arrays indexes
        private const byte pk_Length = 22; // bytes
        private const byte cmd_Length = 7; // bytes
        private const byte pk_FirstDataID = 3;
        private const byte pk_LastDataID = pk_Length-3;
        private const byte cmd_FirstDataID = 2;
        private const byte cmd_LastDataID = cmd_Length-3;
        private const byte pk_pkID = 1; // Index of Rx PACKID
        private const byte pk_cmdID = 2; // Index of Rx CMDID
        private const byte cmd_cmdID = 1; // Index of Tx CMDID
        private const byte pk_CRC = pk_Length-2;
        private const byte cmd_CRC = cmd_Length-2;

        // Packet values
        private const byte STARTBYTE = 0x02;
        private const byte ENDBYTE = 0x03;

        // CMDID values
        public const byte ADDCHANNEL = 0x0A;
        public const byte REMOVECHANNEL = 0x0D;
        private const byte CMDCOMPLETE = 0x10;
        public const byte STARTCONVERSION = 0x02;
        public const byte STOPCONVERSION = 0x03;
        private const byte ERROR = 0x07;

        // Errors types (DATA0 values)
        private const byte CRC_ERROR = 0x04;
        private const byte MAXCHANNELS_ERROR = 0x06;
        private const byte UNKNOWNCMD_ERROR = 0x18;

        // Add / Remove Channel values
        public const byte ECG = 0x1A; // Channels
        public const byte EMG = 0x1B;
        public const byte ERG = 0x1C;
        public const byte HPF50mHz = 0x2A; // High Pass Filters
        public const byte LPF40Hz = 0x3A; // Low Pass Filters
        public const byte LPF100Hz = 0x3B;
        public const byte LPF10kHz = 0x3C;
        
        // Channel masks
        private const byte ECG_mask = 0x00;
        private const byte EMG_mask = 0x80;
        private const byte ERG_mask = 0xC0;
        private const byte HPF50mHz_mask = 0x00;
        private const byte LPF40Hz_mask = 0x03;
        private const byte LPF100Hz_mask = 0x01;
        private const byte LPF10kHz_mask = 0x02;

        #endregion // Constants for pack/unpack datas

        #region Global Variables

        private byte[] Packet; // For Rx
        private byte[] Command; // For Tx
        private struct cmd
        {
            public byte cmdid;
            public byte param0;
            public byte param1;
            public byte param2;
            public cmd(byte CMDID, byte PARAM0, byte PARAM1, byte PARAM2)
            {
                cmdid = CMDID;
                param0 = PARAM0;
                param1 = PARAM1;
                param2 = PARAM2;
            }
        }
        private Queue<cmd> cmdFIFO;
        private const int maxAttemptsError = 3;

        public double[] SourceX, SourceY;
        private SerialPort ExternPortSource;
        private Timer timer1;
        private bool READYTOSEND;
        private int errorCounter;

        #endregion // Global Variables

        public DataSource()
        {
            ExternPortSource = new SerialPort();
            timer1 = new Timer();
            Packet = new byte[pk_Length];
            Command = new byte[cmd_Length];
            cmdFIFO = new Queue<cmd>();
            ExternPortSource.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(ExternPortSource_DataReceived);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 100;
            timer1.Start();
            READYTOSEND = true;
            errorCounter = 0;
        }

        public void AddCommand(byte CMDID, byte PARAM0, byte PARAM1, byte PARAM2)
        {
            cmdFIFO.Enqueue(new cmd(CMDID, PARAM0, PARAM1, PARAM2));
        }

        public void SetConnectionSettings(ConnectionSettings sett)
        {
            if (ExternPortSource.IsOpen) ExternPortSource.Close();
            ExternPortSource.BaudRate = sett.BaudRate;
            ExternPortSource.DataBits = sett.DataBits;
            ExternPortSource.Parity = sett.Parity;
            ExternPortSource.StopBits = sett.StopBits;
            ExternPortSource.PortName = sett.PortName;
            ExternPortSource.ReadTimeout = sett.ReadTimeout;
            //ExternPortSource.ReceivedBytesThreshold = 1;
        }

        public bool OpenConnection()
        {
            try
            {
                if (!ExternPortSource.IsOpen) ExternPortSource.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Can't open port " + ExternPortSource.PortName + " !" + Environment.NewLine + ex.Message, "Error open connection !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            READYTOSEND = true;
            return true;
        }

        public bool CloseConnection()
        {
            if (ExternPortSource.IsOpen)
            {
                ExternPortSource.Close();
                return true;
            }
            return false;
        }

        // Don't need this function
        public void readdata(string filename, int sig)
        {
            // const string filename = @"D:\Downloads\GraphDisplay\normecg.txt";
            using (TextReader reader = File.OpenText(filename))
            {
                string line;
                double val1, val2;
                line = reader.ReadLine();
                line = reader.ReadLine();
                string[] bits;
                System.Globalization.NumberStyles st;
                System.IFormatProvider cult;
                st = System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.Number;
                cult = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                try
                {
                    SourceX = new double[16384];
                    SourceY = new double[16384];
                    int i = -1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        bits = line.Split('\t');
                        if (sig != 1 || sig != 2) sig = 1;
                        if ((double.TryParse(bits[0], st, cult, out val1)) && (double.TryParse(bits[sig], st, cult, out val2)))
                        { SourceX[++i] = val1; SourceY[i] = val2; }
                    }
                    reader.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("file err ( \" " + line + " \" )\n" + ex.Message, "func readdata()");
                }
            }
        }

        private void ExternPortSource_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (ExternPortSource.BytesToRead > 0)
            {
                for (int i = 1; i < pk_Length; i++) Packet[i - 1] = Packet[i];
                Packet[pk_Length - 1] = (byte)ExternPortSource.ReadByte();
                if (Packet[pk_Length - 1] == ENDBYTE)
                    if (Packet[0] == STARTBYTE) TranslatePacket();
            }
        }

        private void TranslatePacket()
        {
            char crc = (char)0x00;
            for (int i = pk_pkID; i <= pk_LastDataID; i++) crc += (char)Packet[i];
            byte crcb = (byte)(crc & (char)0x00FF);
            if (crcb != Packet[pk_CRC])
            {
                if (SendPacket(new cmd(DataSource.ERROR, 0, 0, 0))) READYTOSEND = false;
                else READYTOSEND = true;
            }
            else
            {
                switch (Packet[pk_cmdID])
                {
                    case CMDCOMPLETE:
                        if (cmdFIFO.Count != 0) cmdFIFO.Dequeue();
                        READYTOSEND = true;
                        break;
                    case ERROR:
                        switch (Packet[pk_FirstDataID])
                        {
                            case CRC_ERROR:
                                errorCounter++;
                                if (errorCounter >= maxAttemptsError)
                                {
                                    if (cmdFIFO.Count != 0) cmdFIFO.Dequeue();
                                    errorCounter = 0;
                                }
                                break;
                            case MAXCHANNELS_ERROR:
                                //no more channels to add
                                if (cmdFIFO.Count != 0) cmdFIFO.Dequeue();
                                break;
                            case UNKNOWNCMD_ERROR:
                                MessageBox.Show("Unknown command !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (cmdFIFO.Count != 0) cmdFIFO.Dequeue();
                                break;
                        }
                        READYTOSEND = true;
                        break;
                    case ECG: case EMG: case ERG:
                        switch (Packet[pk_FirstDataID])
                        {
                            case (ECG_mask | HPF50mHz_mask | LPF40Hz_mask):
                                ToBinaryFile("ECG_50mHz_40Hz.dat");
                                break;
                            case (ECG_mask | HPF50mHz_mask | LPF100Hz_mask):
                                ToBinaryFile("ECG_50mHz_100Hz.dat");
                                break;
                            case (ECG_mask | HPF50mHz_mask | LPF10kHz_mask):
                                ToBinaryFile("ECG_50mHz_10kHz.dat");
                                break;
                            case (EMG_mask | HPF50mHz_mask | LPF40Hz_mask):
                                ToBinaryFile("EMG_50mHz_40Hz.dat");
                                break;
                            case (EMG_mask | HPF50mHz_mask | LPF100Hz_mask):
                                ToBinaryFile("EMG_50mHz_100Hz.dat");
                                break;
                            case (EMG_mask | HPF50mHz_mask | LPF10kHz_mask):
                                ToBinaryFile("EMG_50mHz_10kHz.dat");
                                break;
                            case (ERG_mask | HPF50mHz_mask | LPF40Hz_mask):
                                ToBinaryFile("ERG_50mHz_40Hz.dat");
                                break;
                            case (ERG_mask | HPF50mHz_mask | LPF100Hz_mask):
                                ToBinaryFile("ERG_50mHz_100Hz.dat");
                                break;
                            case (ERG_mask | HPF50mHz_mask | LPF10kHz_mask):
                                ToBinaryFile("ERG_50mHz_10kHz.dat");
                                break;
                        }
                        READYTOSEND = true;
                        break;
                }
            }
        }

        private void ToBinaryFile(string file)
        {
            BinaryWriter w;
            if (!File.Exists(file)) w = new BinaryWriter(File.Open(file, FileMode.Create));
            else w = new BinaryWriter(File.Open(file, FileMode.Append));
            for (int i = pk_FirstDataID + 1; i <= pk_LastDataID; i++) w.Write(Packet[i]);
            w.Close();
        }

        private bool SendPacket(cmd command)
        {
            if (!ExternPortSource.IsOpen) return false;
            Command[0] = STARTBYTE;
            Command[cmd_cmdID] = command.cmdid;
            Command[cmd_FirstDataID] = command.param0;
            Command[cmd_FirstDataID+1] = command.param1;
            Command[cmd_FirstDataID+2] = command.param2;
            Command[cmd_Length - 1] = ENDBYTE;
            char crc = (char)0x00;
            for (int i = cmd_cmdID; i <= cmd_LastDataID; i++) crc += (char)Command[i];
            Command[cmd_CRC] = (byte)(crc & (char)0x00FF);
            ExternPortSource.Write(Command, 0, cmd_Length);
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cmdFIFO.Count == 0) return;
            if (READYTOSEND == false) return;
            if (SendPacket(cmdFIFO.Peek())) READYTOSEND = false;
            else READYTOSEND = true;
        }
    }
}
