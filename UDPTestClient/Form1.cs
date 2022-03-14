using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using Dataq.Files.Wdq;

/******
 * 
 *  
 *  Properties>AssemblyInfo.cs must be changed with each build per Ioan
 *  Beta 01/28/2014
 *  [assembly: AssemblyVersion("1.1.0.9")]
 *  [assembly: AssemblyFileVersion("1.1.0.9")]
 *  Release 3/4/2014
 *  [assembly: AssemblyVersion("1.2.0.2")]
 *  [assembly: AssemblyFileVersion("1.2.0.2")]
 *
 * 
 * 
 * 
*******/



namespace UDPTestClient
 
{

    public struct DQCommand
    {
        public Int32 ID;
        public Int32 PublicKey;
        public Int32 Command;
        public Int32 Par1;
        public Int32 Par2;
        public Int32 Par3;
        public string Payload; //In C#, char occupies TWO bytes
    }

    public partial class Form1 : Form
    {
        public const int DEVICE_ADC_BUFFER_SIZE=100000;
        public const int SYNC_DEVICE_COUNT = 5;

        public const int LOCAL_PORT = 1235;
        public const int REMOTE_PORT = 1234;
        public const int CLIENT_PORT = 1427;
        public const int COMMAND_PORT = 51235;
        Boolean bGap = false;

        public const int DQCOMMAND = 0x31415926;
        public const int DQRESPONSE = 0x21712818;
        public const int DQADCDATA = 0x14142135;
        public const int DQTHUMBDATA = 0x17320508;
        public const int DQTHUMBEOF = 0x22360679;
        public const int DQTHUMBSTREAM = 0x16180339;
        public const int DQWHCHDR = 0x05772156;

        public bool bNewWindow;
        public int NewXLengh;

        public const int MYKEY = 0x6681444;

        public const int SYNCSTART = 1;
        public const int SYNC = 2;
        public const int ROUNDTRIPQUERY = 3;
        public const int ROUNDTRIPQUERYACK = 4;
        public const int SLAVEIP = 5;
        public const int SYNCSTOP = 6;
        public const int CONNECT = 10;
        public const int DISCONNECT = 11;
        public const int KEEPALIVE = 12;
        public const int SECONDCOMMAND = 13;
        public const int FINDOUTSLAVEDELAY = 15;
        public const int SETSLAVEDELAY = 16;
        public const int UDPDEBUG = 20;
        public const int USBDRIVECOMMAND = 22;

        public static int tick = 0;
        public static int device = 0;
        public static int deviceIndex = 0;
        public static Boolean connectFlg = false;
        public static Boolean singledevice;
        public static int GapCount;

        CancellationTokenSource source;
        CancellationToken token;

        int gTotalDevice=1;

        int gRunning = 0;

        
        Int64[] samplecount=new Int64[SYNC_DEVICE_COUNT];

        static UdpClient receivingUdpClient = new UdpClient(REMOTE_PORT);
        static UdpClient UdpBroadcaster = new UdpClient(CLIENT_PORT);
        static IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        static IPEndPoint remoteSender = new IPEndPoint(IPAddress.Any, 0);

        private Object thisLock = new Object();

        Int16[,] ADCDataBuffer = new Int16[SYNC_DEVICE_COUNT, DEVICE_ADC_BUFFER_SIZE];
        Int16[,] FullScreenADCData;


        int[] fillindex = new int[SYNC_DEVICE_COUNT];
        int takeindex; //Because we always take by scan, so there is no need to watch individual channel
        private readonly object listbox1;

        public Form1()
        {
            InitializeComponent();
            receivingUdpClient.EnableBroadcast = true;


        }

        public static byte[] StrToByteArray(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (byte i = 0; i < 255; i++)
                hexindex.Add(i.ToString("X2"), i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
                while (receivingUdpClient.Available > 0)
                {
                    try
                    {

                        Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                        parse_udp(receiveBytes);

                    }


                    catch
                    {
                    }
                }
        }

        private void connect()
        {


            //numericUpDown1.Maximum = numericUpDown1.Minimum = 1;
            char[] macString = new char[12];

            connectFlg = true;
            //System.Threading.Thread.Sleep(100);

            SendQuery();

            tick = 0;
            device = 0;
            deviceIndex = 0;

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void bntNext_Click(object sender, EventArgs e)
        {
            tick = 0;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string line;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            try
            {
                //Pass the file path and file name to the StreamReader constructor
                //StreamReader sr = new StreamReader(path+"\\IP.txt");

                //Read the first line of text
                //line = sr.ReadLine();
                //List<string> names = line.Split(',').ToList<string>();

                //sync1.Text = names[0];
                //sync2.Text = names[1];
                //sync3.Text = names[2];
                //sync4.Text = names[3];
                //sync5.Text = names[4];
                //close the file
                //sr.Close();
            }
            catch { }


            connect();
            my_dqcmd.DropDownStyle = ComboBoxStyle.DropDownList;
            packetsize.DropDownStyle = ComboBoxStyle.DropDownList;
            PayLoad.Text = GetLocalIPAddress();
            DestIP.Text = PayLoad.Text.Substring(0, PayLoad.Text.LastIndexOf('.'))+".";
            DestIP.Text = PayLoad.Text.Substring(0, PayLoad.Text.LastIndexOf('.'))+".";

            listBox1.HorizontalScrollbar = true;
            listBox1.SelectionMode= SelectionMode.MultiExtended;
        }


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length < 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (ep.Length > 2)
            {
                if (!IPAddress.TryParse(string.Join(":", ep, 0, ep.Length - 1), out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            else
            {
                if (!IPAddress.TryParse(ep[0], out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            int port;
            if (!int.TryParse(ep[ep.Length - 1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        static void SendQuery()
        {

            //IPEndPoint EndPoint = new IPEndPoint(IPAddress.Broadcast, LOCAL_PORT);

            //string strRequestServers = "chuck53678354chen";
            LegacySendCommand("chuck53678354chen " + REMOTE_PORT.ToString(), "255.255.255.255:1235");

            //byte[] RequestBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strRequestServers);

            //UdpBroadcaster.EnableBroadcast = true;

            //UdpBroadcaster.Send(RequestBytes, RequestBytes.Length, EndPoint);


        }

        static void LegacySendCommand(string myCommand, string Dest = null)
        {
            byte[] data0 = Encoding.ASCII.GetBytes(myCommand);
            int i;

            byte[] udpdata = new byte[data0.Length];

            i = 0;
            Buffer.BlockCopy(data0, 0, udpdata, i, data0.Length);
            i = i + data0.Length;


            IPEndPoint EndPoint;

            if (Dest == null)
            {
                EndPoint = new IPEndPoint(IPAddress.Broadcast, COMMAND_PORT);
            }
            else
            {
                EndPoint = CreateIPEndPoint(Dest);
                //EndPoint.Port = COMMAND_PORT;
            }


            UdpBroadcaster.EnableBroadcast = true;

            UdpBroadcaster.Send(udpdata, udpdata.Length, EndPoint);


        }

        static void SendOldCommand(string myCommand, string Dest = null, int delay = 100)
        {

            byte[] data0 = Encoding.ASCII.GetBytes(myCommand);

            int i;

            byte[] udpdata = new byte[data0.Length];

            Buffer.BlockCopy(data0, 0, udpdata, 0, data0.Length);

            IPEndPoint EndPoint;

            if (Dest == null)
            {
                EndPoint = new IPEndPoint(IPAddress.Broadcast, COMMAND_PORT);
            }
            else
            {
                EndPoint = CreateIPEndPoint(Dest);
            }


            UdpBroadcaster.EnableBroadcast = true;

            UdpBroadcaster.Send(udpdata, udpdata.Length, EndPoint);

            System.Threading.Thread.Sleep(delay);

        }

        static void SendCommand (DQCommand myCommand, string Dest =null, int delay=100)
        {
 
            byte[] data0 = BitConverter.GetBytes(myCommand.ID);
            byte[] data1 = BitConverter.GetBytes(myCommand.PublicKey);
            byte[] data2 = BitConverter.GetBytes(myCommand.Command);
            byte[] data3 = BitConverter.GetBytes(myCommand.Par1);
            byte[] data4 = BitConverter.GetBytes(myCommand.Par2);
            byte[] data5 = BitConverter.GetBytes(myCommand.Par3);
            byte[] data6 = Encoding.ASCII.GetBytes(myCommand.Payload);
            int i;

            byte[] udpdata = new byte[data0.Length + data1.Length + data2.Length + data3.Length + data4.Length + data5.Length + data6.Length];

            i = 0;
            Buffer.BlockCopy(data0, 0, udpdata, i, data0.Length);
            i = i + data0.Length;
            Buffer.BlockCopy(data1, 0, udpdata, i, data1.Length);
            i = i + data1.Length;
            Buffer.BlockCopy(data2, 0, udpdata, i, data2.Length);
            i = i + data2.Length;
            Buffer.BlockCopy(data3, 0, udpdata, i, data3.Length);
            i = i + data3.Length;
            Buffer.BlockCopy(data4, 0, udpdata, i, data4.Length);
            i = i + data4.Length;
            Buffer.BlockCopy(data5, 0, udpdata, i, data5.Length);
            i = i + data5.Length;
            Buffer.BlockCopy(data6, 0, udpdata, i, data6.Length);

            IPEndPoint EndPoint;

            if (Dest == null)
            {
                EndPoint = new IPEndPoint(IPAddress.Broadcast, COMMAND_PORT);
            }
            else
            {
                EndPoint = CreateIPEndPoint(Dest);
            }

 
            UdpBroadcaster.EnableBroadcast = true;

            UdpBroadcaster.Send(udpdata, udpdata.Length, EndPoint);

            System.Threading.Thread.Sleep(delay);

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DQCommand dq;
            int number;

            dq.ID = DQCOMMAND;
            dq.Command = KEEPALIVE;
            dq.PublicKey = MYKEY;
            dq.Par1 = 0;
            dq.Par2 = 0;
            dq.Par3 = 0;
            dq.Payload = "Keep Alive";

            SendCommand(dq, DestIP.Text + sync1.Text + ":51235");

            if (gRunning == 0)
            {
                if (Int32.TryParse(sync2.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync2.Text + ":51235");
                }

                if (Int32.TryParse(sync3.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync3.Text + ":51235");
                }
                if (Int32.TryParse(sync4.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync4.Text + ":51235");
                }
                if (Int32.TryParse(sync5.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync5.Text + ":51235");
                }
            }

            gap_count.Text = "Gaps=" + GapCount.ToString();
        }

        private void sync_start_Click(object sender, EventArgs e)
        {
            int i;
            DQCommand dq;
            int number;

            dq.ID = DQCOMMAND;
            dq.Command = SYNCSTART;
            dq.PublicKey = MYKEY;
            dq.Par1 = 1;
            dq.Par2 = 2;
            dq.Par3 = 3;
            dq.Payload = DestIP.Text  + sync2.Text;

            for (i=0; i<SYNC_DEVICE_COUNT; i++)
            {
                fillindex[i] = 0;
            }
            takeindex = 0;

            singledevice = false;

            samplecount[0] = 0;
            samplecount[1] = 0;

            if (has_wifi.Checked)
            {
               
                if (Int32.TryParse(sync2.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync2.Text + ":51235", 0);
                }
                
                if (Int32.TryParse(sync3.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync3.Text + ":51235", 0);
                }

                if (Int32.TryParse(sync4.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync4.Text + ":51235", 0);
                }
                if (Int32.TryParse(sync5.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync5.Text + ":51235", 0);
                }

                SendCommand(dq, DestIP.Text  + sync1.Text + ":51235", 0);
            }
            else
            {
                SendCommand(dq);
            }

            gRunning = 1;
            timer1.Enabled = false;

            if (WindaqFile.Checked)
            {
                axWriteDataqFileII1.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Test.wdq";
                axWriteDataqFileII1.SampleRate = 60000000/Convert.ToInt32(sync_srate.Text);
                axWriteDataqFileII1.ChannelCount = Convert.ToInt16(gTotalDevice);
                axWriteDataqFileII1.Open();
                bGap = false;
            }

            source = new CancellationTokenSource();
            token = source.Token;

            var t = Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (receivingUdpClient.Available > 0)
                        {
                            Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                            parse_udp(receiveBytes);
                        }

                    }
                    catch
                    {
                    }
                    if (token.IsCancellationRequested == true) break;
                }
            }, token);

            my_query.Enabled = false;
            WindaqFile.Enabled = false;
            sync_srate.Enabled = false;
            packetsize.Enabled = false;
            has_wifi.Enabled = false;
            my_keepalive.Enabled = false;
            my_setup_bothunits.Enabled = false;
            sync_start.Enabled = false;
        }



        private void my_disconnect_Click(object sender, EventArgs e)
        {
            DQCommand dq;
            int number;

            try
            {
                source.Cancel();
            }
            catch
            {

            }

            my_query.Enabled = true;

            dq.ID = DQCOMMAND;
            dq.Command = DISCONNECT;
            dq.PublicKey = MYKEY;
            dq.Par1 = 0;
            dq.Par2 = 0;
            dq.Par3 = 0;
            dq.Payload = "disconnet";
            gRunning = 0;

            if (has_wifi.Checked)
            {
                SendCommand(dq, DestIP.Text  + sync1.Text + ":51235");
                
                if (Int32.TryParse(sync2.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync2.Text + ":51235");
                }
                
                if (Int32.TryParse(sync3.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync3.Text + ":51235");
                }
                if (Int32.TryParse(sync4.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync4.Text + ":51235");
                }
                if (Int32.TryParse(sync5.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync5.Text + ":51235");
                }
            }
            else
            {
                SendCommand(dq);
            }

            timer2.Enabled = false;

            sync1.Enabled = true;
            sync2.Enabled = true;
            sync3.Enabled = true;
            sync4.Enabled = true;
            sync5.Enabled = true;
            WindaqFile.Enabled = true;
            sync_srate.Enabled = true;
            packetsize.Enabled = true;
            has_wifi.Enabled = true;
            my_keepalive.Enabled = true;
            my_setup_bothunits.Enabled = true;
            sync_start.Enabled = true;
            WindaqFile.Checked = false;
        }

        private void sync_stop_Click(object sender, EventArgs e)
        {
            DQCommand dq;
            int number;

            try
            {
                source.Cancel();
            }
            catch
            {

            }
            

            System.Threading.Thread.Sleep(100);

            timer1.Enabled = true;
            gRunning = 0;
            my_query.Enabled = true;

            dq.ID = DQCOMMAND;
            dq.Command = SYNCSTOP;
            dq.PublicKey = MYKEY;
            dq.Par1 = 1;
            dq.Par2 = 2;
            dq.Par3 = 3;
            dq.Payload = "stop";

            if (has_wifi.Checked)
            {
                SendCommand(dq, DestIP.Text  + sync1.Text + ":51235", 0);
                
                if (Int32.TryParse(sync2.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync2.Text + ":51235", 0);
                }
                
                if (Int32.TryParse(sync3.Text, out number))
                {
                    SendCommand(dq, DestIP.Text  + sync3.Text + ":51235", 0);
                }
                if (Int32.TryParse(sync4.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync4.Text + ":51235", 0);
                }
                if (Int32.TryParse(sync5.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync5.Text + ":51235", 0);
                }
            }
            else
            {
                SendCommand(dq);
            }

            if (WindaqFile.Checked)
            {
                axWriteDataqFileII1.Close();
            }

            sync_srate.Enabled = true;
            packetsize.Enabled = true;
            has_wifi.Enabled = true;
            my_keepalive.Enabled = true;
            WindaqFile.Enabled = true;
            my_setup_bothunits.Enabled = true;
            sync_start.Enabled = true;
            WindaqFile.Checked = false;
        }

        public int parse_udp (Byte[] receiveBytes)
        {
            Int32 myID;
            Int32 myKey;
            Int32 myOrder;
            Int64 myRunningDataCount;
            Int32 myPayloadSamples;
            Int32 myNumOfChan;
            Int32 myReAligned;


            string mystring;
            short n, m;
            int i, x, y;

            
            myID = BitConverter.ToInt32(receiveBytes, 0);
            if (receiveBytes.Length > 8)
                myKey = BitConverter.ToInt32(receiveBytes, 4);
            else
                myKey = 0;
            if (receiveBytes.Length > 12)
                myReAligned = myOrder = BitConverter.ToInt32(receiveBytes, 8);
            else
                myReAligned = myOrder = 0;
            if (myOrder >= SYNC_DEVICE_COUNT) myOrder = SYNC_DEVICE_COUNT;
            if (myOrder < 0) myOrder = 0;
            m = unchecked((short)0xfffc);
            
            switch (myID)
            {
                case DQADCDATA:
                    myRunningDataCount = BitConverter.ToInt32(receiveBytes, 12);
                    myPayloadSamples = BitConverter.ToInt32(receiveBytes, 16);


                    i = Convert.ToInt32(myRunningDataCount - samplecount[myOrder]);

                    
                    if (i != 0)
                    {
                        GapCount++;
                        bGap = true;
                        /*Need to create fake data to fill the gap here!*/
                        for (i = 0; i < myRunningDataCount - samplecount[myOrder]; i++)
                        {
                            ADCDataBuffer[myOrder, fillindex[myOrder]] = 3; //event markers
                            fillindex[myOrder]++;
                            if (fillindex[myOrder] >= DEVICE_ADC_BUFFER_SIZE) fillindex[myOrder] = 0;
                        }
                        samplecount[myOrder] = myRunningDataCount ;
                    }


                    for (i = 0; i < myPayloadSamples; i++)
                    {
                        n = BitConverter.ToInt16(receiveBytes, 20 + i * 2);
                        ADCDataBuffer[myOrder, fillindex[myOrder]] = (short)(n&m);

                        fillindex[myOrder]++;
                        if (fillindex[myOrder] >= DEVICE_ADC_BUFFER_SIZE) fillindex[myOrder] = 0;
                    }
                    samplecount[myOrder] = samplecount[myOrder] + myPayloadSamples;

                    return 1;
                case DQTHUMBSTREAM:
                    myNumOfChan = BitConverter.ToInt32(receiveBytes, 12);
                    myPayloadSamples = BitConverter.ToInt32(receiveBytes, 16);

                    Int16[,] ADCData = new Int16[myNumOfChan, myPayloadSamples / myNumOfChan];

                    i = x = y = 0;
                    for (x = 0; x < myPayloadSamples / myNumOfChan ; x++)
                    {
                        for (y = 0; y <  myNumOfChan; y++) { 
                            n = BitConverter.ToInt16(receiveBytes, 20 + i * 2);
                            i = i + 1;
                            ADCData[y, x] = (short)(n & m);
                        }

                    }

                    if (myReAligned==1)
                    {
                        //Wait until we have a full screen before we plot it!
                        FullScreenADCData = new Int16[myNumOfChan, axXChart1.Xmax];

                        bNewWindow = true;
                        NewXLengh = 0;
                    }


                    if (bNewWindow)
                    {
                        x = y = 0;
                        if ((NewXLengh + myPayloadSamples / myNumOfChan) < axXChart1.Xmax)
                        {
                            for (x = 0; x < myPayloadSamples / myNumOfChan; x++)
                            {
                                for (y = 0; y < myNumOfChan; y++)
                                {
                                    FullScreenADCData[y, NewXLengh + x] = ADCData[y, x];
                                }

                            }
                            NewXLengh = NewXLengh + myPayloadSamples / myNumOfChan;
                        }
                        else
                        {
                            Int16[,] PartialScreenADCData = new Int16[myNumOfChan, NewXLengh];

                            for (x = 0; x < NewXLengh; x++)
                            {
                                for (y = 0; y < myNumOfChan; y++)
                                {
                                    PartialScreenADCData[y, x] = FullScreenADCData[y, x];
                                }

                            }

                            axXChart1
                                .Chart(PartialScreenADCData);
                            axXChart1
                                .Chart(ADCData);
                            bNewWindow = false;
                        }
                    }
                    else
                    {
                        axXChart1
                            .Chart(ADCData);
                    }

                    samplecount[0] = 0;
                    samplecount[1] = 0;
                    samplecount[2] = 0;

                    fillindex[0] = 0;
                    fillindex[1] = 0;
                    fillindex[2] = 0;
                    break;
                case DQRESPONSE:
                    myPayloadSamples = BitConverter.ToInt32(receiveBytes, 12);
                    byte[] myPayload = new byte[myPayloadSamples];
                    Array.Copy(receiveBytes, 16, myPayload,0, myPayloadSamples);
                    mystring = System.Text.Encoding.ASCII.GetString(myPayload);

                    listBox1.Items.Add(mystring);
                    return 0;
                default:
                    var vr= System.Text.Encoding.Default.GetString(receiveBytes);

                    listBox1.Items.Add(vr.ToString());
                    break;
            }
            return 0;
        }

        private void dqcmd_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void my_query_Click(object sender, EventArgs e)
        {
            DQCommand dq;

            switch (my_dqcmd.Text)
            {
                case "QUERY":
                    LegacySendCommand("chuck53678354chen "+REMOTE_PORT.ToString(), "255.255.255.255:1235");
                    break;
                case "SECONDCOMMAND":
                    dq.ID = DQCOMMAND;
                    dq.Command = SECONDCOMMAND;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    bNewWindow=true;
                    NewXLengh=0;

                    break;
                case "USBDRIVECOMMAND":
                    dq.ID = DQCOMMAND;
                    dq.Command = USBDRIVECOMMAND;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text + cmd_dest_ip.Text + ":51235");
                    bNewWindow = true;
                    NewXLengh = 0;

                    break;
                case "FINDOUTSLAVEDELAY":
                    dq.ID = DQCOMMAND;
                    dq.Command = FINDOUTSLAVEDELAY;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    break;
                case "SETSLAVEDELAY":
                    dq.ID = DQCOMMAND;
                    dq.Command = SETSLAVEDELAY;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    break;
                case "CONNECT":
                    dq.ID = DQCOMMAND;
                    dq.Command = CONNECT;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    break;
                case "DISCONNECT":
                    dq.ID = DQCOMMAND;
                    dq.Command = DISCONNECT;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    break;
                case "SLAVEIP":
                    dq.ID = DQCOMMAND;
                    dq.Command = SLAVEIP;
                    dq.PublicKey = MYKEY;
                    dq.Par1 = Convert.ToInt32(par1.Text);
                    dq.Par2 = Convert.ToInt32(par2.Text);
                    dq.Par3 = Convert.ToInt32(par3.Text);
                    dq.Payload = PayLoad.Text;
                    SendCommand(dq, DestIP.Text  + cmd_dest_ip.Text + ":51235");
                    break;
                case "ASCII":
                    SendOldCommand(PayLoad.Text, DestIP.Text + cmd_dest_ip.Text + ":51235");
                    break;
                default:
                    break;

            }

            
        }

        private void axDQChart1_ChartChanged(object sender, AxDQCHARTLib._DDQChartEvents_ChartChangedEvent e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Send2Device(DQCommand dq)
        {
            int number;

            SendCommand(dq, DestIP.Text + sync1.Text + ":51235");
            if (Int32.TryParse(sync2.Text, out number))
            {
                SendCommand(dq, DestIP.Text + sync2.Text + ":51235");
            }

            if (Int32.TryParse(sync3.Text, out number))
            {
                SendCommand(dq, DestIP.Text + sync3.Text + ":51235");
            }

            if (Int32.TryParse(sync4.Text, out number))
            {
                SendCommand(dq, DestIP.Text + sync4.Text + ":51235");
            }
            if (Int32.TryParse(sync5.Text, out number))
            {
                SendCommand(dq, DestIP.Text + sync5.Text + ":51235");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DQCommand dq;
            int number;

            /*Connet*/
            dq.ID = DQCOMMAND;
            dq.Command = CONNECT;
            dq.PublicKey = MYKEY;
            dq.Par1 = REMOTE_PORT;
            dq.Payload = GetLocalIPAddress();
            dq.Par2 = 1; //master
            dq.Par3 = 0; //device order in the group

            SendCommand(dq, DestIP.Text  + sync1.Text + ":51235");
            gTotalDevice = 1;

            dq.Par2 = 0; //salve

            if (Int32.TryParse(sync2.Text, out number))
            {
                dq.Par3++; //device order in the group
                SendCommand(dq, DestIP.Text  + sync2.Text + ":51235");
                gTotalDevice++;
            }
            if (Int32.TryParse(sync3.Text, out number))
            {
                dq.Par3 ++; //device order in the group
                SendCommand(dq, DestIP.Text  + sync3.Text + ":51235");
                gTotalDevice++;
            }

            if (Int32.TryParse(sync4.Text, out number))
            {
                dq.Par3++; //device order in the group
                SendCommand(dq, DestIP.Text + sync4.Text + ":51235");
                gTotalDevice++;
            }
            if (Int32.TryParse(sync5.Text, out number))
            {
                dq.Par3++; //device order in the group
                SendCommand(dq, DestIP.Text + sync5.Text + ":51235");
                gTotalDevice++;
            }

            sync1.Enabled = false;
            sync2.Enabled = false;
            sync3.Enabled = false;
            sync4.Enabled = false;
            sync5.Enabled = false;

            samplecount[0] = 0;
            samplecount[1] = 0;
            samplecount[2] = 0;

            /*Conneted*/


            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            switch (packetsize.Text)
            {
                case "1024":
                    dq.Payload = "ps 6";
                    break;
                case "512":
                    dq.Payload = "ps 5";
                    break;
                case "256":
                    dq.Payload = "ps 4";
                    break;
                case "128":
                    dq.Payload = "ps 3";
                    break;
                case "64":
                    dq.Payload = "ps 2";
                    break;
                case "32":
                    dq.Payload = "ps 1";
                    break;
                default:
                    dq.Payload = "ps 0";
                    break;
            }
            /*dq.Payload = "ps " + packetsize.Text;*/

            Send2Device(dq);


            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "srate "+ sync_srate.Text;

            Send2Device(dq);


            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 0 "+ActiveChannel.Text;

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 1 1";

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 2 2";

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 3 3";

            Send2Device(dq);
            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 4 4";

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 5 5";

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 6 6";

            Send2Device(dq);

            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "slist 7 7";

            Send2Device(dq);


            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "deca 4";

            Send2Device(dq);


            dq.ID = DQCOMMAND;
            dq.Command = SECONDCOMMAND;
            dq.PublicKey = MYKEY;
            dq.Payload = "dec 500";

            Send2Device(dq);



            if (has_wifi.Checked)
            {
                dq.ID = DQCOMMAND;
                dq.Command = SLAVEIP;
                dq.Par1 = gTotalDevice-1; //Number of slave device
                dq.Par2 = 0; //Index of current device
                dq.Par3 = 0; //0 = default
                dq.PublicKey = MYKEY;

                
                if (Int32.TryParse(sync2.Text, out number))
                {
                    dq.Payload = DestIP.Text  + sync2.Text;
                    SendCommand(dq, DestIP.Text  + sync1.Text + ":51235");
                    dq.Par2++;
                }

                if (Int32.TryParse(sync3.Text, out number))
                {
                    dq.Payload = DestIP.Text  + sync3.Text;
                    SendCommand(dq, DestIP.Text  + sync1.Text + ":51235");
                    dq.Par2++;
                }

                if (Int32.TryParse(sync4.Text, out number))
                {
                    dq.Payload = DestIP.Text + sync4.Text;
                    SendCommand(dq, DestIP.Text + sync1.Text + ":51235");
                    dq.Par2++;
                }
                if (Int32.TryParse(sync5.Text, out number))
                {
                    dq.Payload = DestIP.Text + sync5.Text;
                    SendCommand(dq, DestIP.Text + sync1.Text + ":51235");
                    dq.Par2++;
                }

            }
            else
            {
                dq.ID = DQCOMMAND;
                dq.Command = SLAVEIP;
                dq.Par1 = 0;
                dq.Par2 = 0;
                dq.Par3 = 0;
                dq.PublicKey = MYKEY;

                if (Int32.TryParse(sync2.Text, out number))
                {
                    SendCommand(dq, DestIP.Text + sync1.Text + ":51235");
                }
            }

            
            if (my_keepalive.Checked==true) timer2.Enabled = true;

            GapCount = 0;
            gap_count.Text = "Gaps=" + GapCount.ToString();
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //StreamWriter sw = new StreamWriter(path+"\\IP.txt");

                //Write a line of text
                //sw.WriteLine(sync1.Text + ","+ sync2.Text + "," + sync3.Text + "," + sync4.Text + "," + sync5.Text);

                //Close the file
                //sw.Close();
            }
            catch
            {
            }
            my_disconnect_Click(sender, e);
        }

        private void my_dqcmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (my_dqcmd.Text)
            {
                case "QUERY":
                    PayLoad.Text = GetLocalIPAddress();
                    par1_label.Text = "N.A";
                    par2_label.Text = "N.A";
                    par3_label.Text = "N.A";
                    payload_label.Text = "PC's IP";
                    break;
                case "SETSLAVEDELAY":
                    par1_label.Text = "Compensation";
                    par2_label.Text = "N.A";
                    par3_label.Text = "N.A";
                    payload_label.Text = "N.A";
                    break;
                case "FINDOUTSLAVEDELAY":
                    par1_label.Text = "N.A";
                    par2_label.Text = "N.A";
                    par3_label.Text = "N.A";
                    payload_label.Text = "Slave's IP";
                    break;
                case "USBDRIVECOMMAND":
                    par1_label.Text = "N.A";
                    par2_label.Text = "N.A";
                    par3_label.Text = "N.A";
                    payload_label.Text = "USB Drive Command";
                    break;
                case "SECONDCOMMAND":
                    par1_label.Text = "N.A";
                    par2_label.Text = "N.A";
                    par3_label.Text = "N.A";
                    payload_label.Text = "TiVa Command";
                    break;
                case "CONNECT":
                    PayLoad.Text = GetLocalIPAddress();
                    par1_label.Text = "PC's UDP port";
                    par2_label.Text = "Slave/Master/Alone";
                    par3_label.Text = "DeviceOrder";
                    payload_label.Text = "PC's IP";
                    break;
                case "DISCONNECT":
                    break;
                case "SLAVEIP":
                    par1_label.Text = "Number of all slaves";
                    par2_label.Text = "Index";
                    par3_label.Text = "Timing adjust";
                    payload_label.Text = "Slave's IP";
                    break;
                case "ASCII":
                    par1_label.Text = "NA";
                    par2_label.Text = "NA";
                    par3_label.Text = "NA";
                    payload_label.Text = "TiVa Commands";
                    break;

            }
        }

        private void par1_label_Click(object sender, EventArgs e)
        {

        }


        private void my_keepalive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void no_erase_CheckedChanged(object sender, EventArgs e)
        {
            if (no_erase.Checked)
            {
                axXChart1.SumWaveforms = true;
                axXChart1.Persist = 0;
                axXChart1.ScrollMode = false;
            }
            else
            {
                axXChart1.ScrollMode = true;
            }
        }

        private void sync2_TextChanged(object sender, EventArgs e)
        {

        }

        private void non_sync_start_Click(object sender, EventArgs e)
        {

        }

        private void axXChart1_ChartChanged(object sender, AxXCHARTLib._DXChartEvents_ChartChangedEvent e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            int difference1;
            int difference2;
            int difference3;
            int availablescan;
            int backstepindex;

            int i, j, k;
            my_debug.Text = samplecount[0].ToString("#,##0");
            my_debug2.Text = samplecount[1].ToString("#,##0");
            my_debug3.Text = samplecount[2].ToString("#,##0");
            my_debug4.Text = samplecount[3].ToString("#,##0");
            my_debug5.Text = samplecount[4].ToString("#,##0");

            difference1 = fillindex[0] - takeindex;
            if (difference1 < 0) difference1 = difference1 + DEVICE_ADC_BUFFER_SIZE;
            if (gTotalDevice > 1)
            {
                difference2 = fillindex[1] - takeindex;
                if (difference2 < 0) difference2 = difference2 + DEVICE_ADC_BUFFER_SIZE;
            }
            else
            {
                difference2 = 9999999;
            }
            availablescan = Math.Min(difference1, difference2);

            if (gTotalDevice > 2)
            {
                difference3 = fillindex[2] - takeindex;
                if (difference3 < 0) difference3 = difference3 + DEVICE_ADC_BUFFER_SIZE;
            }
            else
            {
                difference3 = 9999999;
            }
            availablescan = Math.Min(availablescan, difference3);
            
            if (availablescan > 0)
            {
                if (WindaqFile.Checked)
                {
                    if (bGap)
                    {
                        bGap = false;
                        axWriteDataqFileII1.InsertEventMarker(1); //EventMarker with time stamp
                    }
                    Int16[,] ADCData = new Int16[gTotalDevice, availablescan];
                    k = takeindex;
                    for (j = 0; j < availablescan; j++)
                    {
                        for (i = 0; i < gTotalDevice; i++)
                        {
                            ADCData[i, j] = ADCDataBuffer[i, k];
                        }
                        k++;
                        if (k >= DEVICE_ADC_BUFFER_SIZE) k = 0;
                    }
                    axWriteDataqFileII1.SaveData(ADCData);
                }

                if (my_scope.Checked)
                {
                    takeindex = takeindex + availablescan;
                    if (takeindex >= DEVICE_ADC_BUFFER_SIZE) takeindex = takeindex - DEVICE_ADC_BUFFER_SIZE;

                    backstepindex = takeindex;

                    Int16[,] ADCData = new Int16[gTotalDevice, axXChart1.Xmax + 1];

                    /*Skip half the width of chart first*/
                    backstepindex = backstepindex - axXChart1.Xmax / 2 - 1;
                    if (backstepindex < 0) backstepindex = backstepindex + DEVICE_ADC_BUFFER_SIZE;

                    /*Find the a high in first channel*/
                    for (j = 0; j < DEVICE_ADC_BUFFER_SIZE; j++)
                    {
                        if (ADCDataBuffer[0, backstepindex] > 20) break;
                        backstepindex--;
                        if (backstepindex < 0) backstepindex = backstepindex + DEVICE_ADC_BUFFER_SIZE;
                    }

                    /*Find the low*/
                    for (j = 0; j < DEVICE_ADC_BUFFER_SIZE; j++)
                    {
                        backstepindex--;
                        if (backstepindex < 0) backstepindex = backstepindex + DEVICE_ADC_BUFFER_SIZE;
                        if (ADCDataBuffer[0, backstepindex] < -20) break;
                    }

                    /*Skip second half the width of chart first*/
                    backstepindex = backstepindex - axXChart1.Xmax / 2;
                    if (backstepindex < 0) backstepindex = backstepindex + DEVICE_ADC_BUFFER_SIZE;

                    for (j = 0; j < axXChart1.Xmax + 1; j++)
                    {
                        for (i = 0; i < gTotalDevice; i++)
                        {
                            ADCData[i, j] = ADCDataBuffer[i, backstepindex];
                        }
                        backstepindex++;
                        if (backstepindex >= DEVICE_ADC_BUFFER_SIZE) backstepindex = 0;
                    }


                    axXChart1
                        .Chart(ADCData);
                }
                else
                {
                    Int16[,] ADCData = new Int16[gTotalDevice, availablescan];
                    for (j = 0; j < availablescan; j++)
                    {
                        for (i = 0; i < gTotalDevice; i++)
                        {
                            ADCData[i, j] = ADCDataBuffer[i, takeindex];
                        }
                        takeindex++;
                        if (takeindex >= DEVICE_ADC_BUFFER_SIZE) takeindex = 0;
                    }


                    axXChart1
                        .Chart(ADCData);
                }
            }
        }

        private void WindaqFile_CheckedChanged(object sender, EventArgs e)
        {
            if (WindaqFile.Checked == true)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Test.wdq"))
                {
                    MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Test.wdq", "File exist, please remove it manually first");
                    WindaqFile.Checked = false;
                }
            }
        }

        private void sync_srate_TextChanged(object sender, EventArgs e)
        {
            if (has_wifi.Checked)
            {
                try
                {
                    Int32 samplerate = 60000000 / Convert.ToInt32(sync_srate.Text);
                    double mypacketsize = (samplerate * 2) / 25;
                    if (mypacketsize > 1440) mypacketsize = 1440;
                    //int pp = (int)mypacketsize;
                    mypacketsize=Math.Log10(mypacketsize)/Math.Log10(2) + 1.0;
                    int pp = (int)mypacketsize;
                    pp = 1<< pp;
                    if (pp > 1024) pp = 1024;
                    packetsize.Text = pp.ToString();
                }
                catch { }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void ConnectOnly_Click(object sender, EventArgs e)
        {
            DQCommand dq;

            /*Connet*/
            dq.ID = DQCOMMAND;
            dq.Command = CONNECT;
            dq.PublicKey = MYKEY;
            dq.Par1 = REMOTE_PORT;
            dq.Payload = GetLocalIPAddress();
            dq.Par2 = 1; //master
            dq.Par3 = 0; //device order in the group

            SendCommand(dq, DestIP.Text + sync1.Text + ":51235");
            gTotalDevice = 1;

            
            sync1.Enabled = false;
            sync2.Enabled = false;
            sync3.Enabled = false;

            samplecount[0] = 0;
            samplecount[1] = 0;
            samplecount[2] = 0;

            
            if (my_keepalive.Checked == true) timer2.Enabled = true;

            GapCount = 0;
            gap_count.Text = "Gaps=" + GapCount.ToString();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 3)
            {    //Ctrl-C

                string s = "";
                foreach (object o in listBox1.SelectedItems)
                {
                    s += o.ToString() + "\r\n";
                }
                Clipboard.SetText(s);
                //Clipboard.SetText(string.Join(Environment.NewLine, listBox1.SelectedItems.OfType<string>().ToArray()));
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void axWriteDataqFileII1_ControlError(object sender, AxDATAQFILEIILib._DWriteDataqFileIIEvents_ControlErrorEvent e)
        {

        }
    }
}
