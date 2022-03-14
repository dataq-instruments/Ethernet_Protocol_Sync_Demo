namespace UDPTestClient
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.my_disconnect = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.sync_start = new System.Windows.Forms.Button();
            this.sync_stop = new System.Windows.Forms.Button();
            this.my_query = new System.Windows.Forms.Button();
            this.my_debug = new System.Windows.Forms.Label();
            this.my_setup_bothunits = new System.Windows.Forms.Button();
            this.my_debug2 = new System.Windows.Forms.Label();
            this.sync1 = new System.Windows.Forms.TextBox();
            this.sync2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sync_srate = new System.Windows.Forms.TextBox();
            this.my_dqcmd = new System.Windows.Forms.ComboBox();
            this.par1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.par2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.par3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PayLoad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmd_dest_ip = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.par1_label = new System.Windows.Forms.Label();
            this.par2_label = new System.Windows.Forms.Label();
            this.par3_label = new System.Windows.Forms.Label();
            this.my_keepalive = new System.Windows.Forms.CheckBox();
            this.my_scope = new System.Windows.Forms.CheckBox();
            this.no_erase = new System.Windows.Forms.CheckBox();
            this.gap_count = new System.Windows.Forms.Label();
            this.packetsize = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.has_wifi = new System.Windows.Forms.CheckBox();
            this.my_debug3 = new System.Windows.Forms.Label();
            this.sync3 = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.axWriteDataqFileII1 = new AxDATAQFILEIILib.AxWriteDataqFileII();
            this.WindaqFile = new System.Windows.Forms.CheckBox();
            this.DestIP = new System.Windows.Forms.TextBox();
            this.payload_label = new System.Windows.Forms.Label();
            this.ConnectOnly = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ActiveChannel = new System.Windows.Forms.TextBox();
            this.sync4 = new System.Windows.Forms.TextBox();
            this.sync5 = new System.Windows.Forms.TextBox();
            this.my_debug4 = new System.Windows.Forms.Label();
            this.my_debug5 = new System.Windows.Forms.Label();
            this.axXChart1 = new AxXCHARTLib.AxXChart();
            ((System.ComponentModel.ISupportInitialize)(this.axWriteDataqFileII1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axXChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(501, 342);
            this.listBox1.TabIndex = 12;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            this.listBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox1_KeyPress);
            // 
            // my_disconnect
            // 
            this.my_disconnect.Location = new System.Drawing.Point(1154, 9);
            this.my_disconnect.Name = "my_disconnect";
            this.my_disconnect.Size = new System.Drawing.Size(70, 48);
            this.my_disconnect.TabIndex = 37;
            this.my_disconnect.Text = "Disconnect";
            this.my_disconnect.UseVisualStyleBackColor = true;
            this.my_disconnect.Click += new System.EventHandler(this.my_disconnect_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // sync_start
            // 
            this.sync_start.Location = new System.Drawing.Point(1042, 9);
            this.sync_start.Name = "sync_start";
            this.sync_start.Size = new System.Drawing.Size(45, 48);
            this.sync_start.TabIndex = 41;
            this.sync_start.Text = "SyncStart";
            this.sync_start.UseVisualStyleBackColor = true;
            this.sync_start.Click += new System.EventHandler(this.sync_start_Click);
            // 
            // sync_stop
            // 
            this.sync_stop.Location = new System.Drawing.Point(1093, 8);
            this.sync_stop.Name = "sync_stop";
            this.sync_stop.Size = new System.Drawing.Size(55, 49);
            this.sync_stop.TabIndex = 46;
            this.sync_stop.Text = "Sync Stop";
            this.sync_stop.UseVisualStyleBackColor = true;
            this.sync_stop.Click += new System.EventHandler(this.sync_stop_Click);
            // 
            // my_query
            // 
            this.my_query.Location = new System.Drawing.Point(13, 488);
            this.my_query.Name = "my_query";
            this.my_query.Size = new System.Drawing.Size(100, 23);
            this.my_query.TabIndex = 48;
            this.my_query.Text = "Send Command";
            this.my_query.UseVisualStyleBackColor = true;
            this.my_query.Click += new System.EventHandler(this.my_query_Click);
            // 
            // my_debug
            // 
            this.my_debug.AutoSize = true;
            this.my_debug.Location = new System.Drawing.Point(357, 379);
            this.my_debug.Name = "my_debug";
            this.my_debug.Size = new System.Drawing.Size(35, 13);
            this.my_debug.TabIndex = 49;
            this.my_debug.Text = "status";
            // 
            // my_setup_bothunits
            // 
            this.my_setup_bothunits.Location = new System.Drawing.Point(587, 7);
            this.my_setup_bothunits.Name = "my_setup_bothunits";
            this.my_setup_bothunits.Size = new System.Drawing.Size(62, 51);
            this.my_setup_bothunits.TabIndex = 51;
            this.my_setup_bothunits.Text = "Connect Setup";
            this.my_setup_bothunits.UseVisualStyleBackColor = true;
            this.my_setup_bothunits.Click += new System.EventHandler(this.button1_Click);
            // 
            // my_debug2
            // 
            this.my_debug2.AutoSize = true;
            this.my_debug2.Location = new System.Drawing.Point(357, 392);
            this.my_debug2.Name = "my_debug2";
            this.my_debug2.Size = new System.Drawing.Size(35, 13);
            this.my_debug2.TabIndex = 52;
            this.my_debug2.Text = "status";
            // 
            // sync1
            // 
            this.sync1.ForeColor = System.Drawing.Color.Crimson;
            this.sync1.Location = new System.Drawing.Point(655, 10);
            this.sync1.Name = "sync1";
            this.sync1.Size = new System.Drawing.Size(25, 20);
            this.sync1.TabIndex = 54;
            this.sync1.Text = "195";
            // 
            // sync2
            // 
            this.sync2.Location = new System.Drawing.Point(686, 11);
            this.sync2.Name = "sync2";
            this.sync2.Size = new System.Drawing.Size(25, 20);
            this.sync2.TabIndex = 55;
            this.sync2.Text = "93";
            this.sync2.TextChanged += new System.EventHandler(this.sync2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(828, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "@ srate";
            // 
            // sync_srate
            // 
            this.sync_srate.Location = new System.Drawing.Point(878, 8);
            this.sync_srate.Name = "sync_srate";
            this.sync_srate.Size = new System.Drawing.Size(47, 20);
            this.sync_srate.TabIndex = 57;
            this.sync_srate.Text = "1000";
            this.sync_srate.TextChanged += new System.EventHandler(this.sync_srate_TextChanged);
            // 
            // my_dqcmd
            // 
            this.my_dqcmd.FormattingEnabled = true;
            this.my_dqcmd.Items.AddRange(new object[] {
            "QUERY",
            "SECONDCOMMAND",
            "SETSLAVEDELAY",
            "FINDOUTSLAVEDELAY",
            "CONNECT",
            "DISCONNECT",
            "SLAVEIP",
            "USBDRIVECOMMAND",
            "ASCII"});
            this.my_dqcmd.Location = new System.Drawing.Point(51, 363);
            this.my_dqcmd.Name = "my_dqcmd";
            this.my_dqcmd.Size = new System.Drawing.Size(185, 21);
            this.my_dqcmd.TabIndex = 58;
            this.my_dqcmd.Text = "QUERY";
            this.my_dqcmd.SelectedIndexChanged += new System.EventHandler(this.my_dqcmd_SelectedIndexChanged);
            // 
            // par1
            // 
            this.par1.Location = new System.Drawing.Point(51, 390);
            this.par1.Name = "par1";
            this.par1.Size = new System.Drawing.Size(83, 20);
            this.par1.TabIndex = 61;
            this.par1.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Par1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 423);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Par2";
            // 
            // par2
            // 
            this.par2.Location = new System.Drawing.Point(51, 416);
            this.par2.Name = "par2";
            this.par2.Size = new System.Drawing.Size(83, 20);
            this.par2.TabIndex = 63;
            this.par2.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 445);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Par3";
            // 
            // par3
            // 
            this.par3.Location = new System.Drawing.Point(51, 442);
            this.par3.Name = "par3";
            this.par3.Size = new System.Drawing.Size(83, 20);
            this.par3.TabIndex = 65;
            this.par3.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "PayLoad";
            // 
            // PayLoad
            // 
            this.PayLoad.Location = new System.Drawing.Point(76, 462);
            this.PayLoad.Name = "PayLoad";
            this.PayLoad.Size = new System.Drawing.Size(160, 20);
            this.PayLoad.TabIndex = 68;
            this.PayLoad.Text = "slist 0 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 493);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = ">>192.168.0.";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cmd_dest_ip
            // 
            this.cmd_dest_ip.Location = new System.Drawing.Point(234, 493);
            this.cmd_dest_ip.Name = "cmd_dest_ip";
            this.cmd_dest_ip.Size = new System.Drawing.Size(25, 20);
            this.cmd_dest_ip.TabIndex = 70;
            this.cmd_dest_ip.Text = "195";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 366);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 71;
            this.label9.Text = "Cmd";
            // 
            // par1_label
            // 
            this.par1_label.AutoSize = true;
            this.par1_label.Location = new System.Drawing.Point(150, 393);
            this.par1_label.Name = "par1_label";
            this.par1_label.Size = new System.Drawing.Size(28, 13);
            this.par1_label.TabIndex = 72;
            this.par1_label.Text = "N.A.";
            this.par1_label.Click += new System.EventHandler(this.par1_label_Click);
            // 
            // par2_label
            // 
            this.par2_label.AutoSize = true;
            this.par2_label.Location = new System.Drawing.Point(150, 416);
            this.par2_label.Name = "par2_label";
            this.par2_label.Size = new System.Drawing.Size(28, 13);
            this.par2_label.TabIndex = 73;
            this.par2_label.Text = "N.A.";
            // 
            // par3_label
            // 
            this.par3_label.AutoSize = true;
            this.par3_label.Location = new System.Drawing.Point(150, 444);
            this.par3_label.Name = "par3_label";
            this.par3_label.Size = new System.Drawing.Size(28, 13);
            this.par3_label.TabIndex = 74;
            this.par3_label.Text = "N.A.";
            // 
            // my_keepalive
            // 
            this.my_keepalive.AutoSize = true;
            this.my_keepalive.Checked = true;
            this.my_keepalive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.my_keepalive.Location = new System.Drawing.Point(763, 43);
            this.my_keepalive.Name = "my_keepalive";
            this.my_keepalive.Size = new System.Drawing.Size(74, 17);
            this.my_keepalive.TabIndex = 76;
            this.my_keepalive.Text = "KeepAlive";
            this.my_keepalive.UseVisualStyleBackColor = true;
            this.my_keepalive.CheckedChanged += new System.EventHandler(this.my_keepalive_CheckedChanged);
            // 
            // my_scope
            // 
            this.my_scope.AutoCheck = false;
            this.my_scope.AutoSize = true;
            this.my_scope.Location = new System.Drawing.Point(1246, 45);
            this.my_scope.Name = "my_scope";
            this.my_scope.Size = new System.Drawing.Size(59, 17);
            this.my_scope.TabIndex = 78;
            this.my_scope.Text = "Trigger";
            this.my_scope.UseVisualStyleBackColor = true;
            // 
            // no_erase
            // 
            this.no_erase.AutoSize = true;
            this.no_erase.Location = new System.Drawing.Point(1246, 7);
            this.no_erase.Name = "no_erase";
            this.no_erase.Size = new System.Drawing.Size(96, 17);
            this.no_erase.TabIndex = 80;
            this.no_erase.Text = "SumWaveform";
            this.no_erase.UseVisualStyleBackColor = true;
            this.no_erase.CheckedChanged += new System.EventHandler(this.no_erase_CheckedChanged);
            // 
            // gap_count
            // 
            this.gap_count.AutoSize = true;
            this.gap_count.Location = new System.Drawing.Point(357, 366);
            this.gap_count.Name = "gap_count";
            this.gap_count.Size = new System.Drawing.Size(39, 13);
            this.gap_count.TabIndex = 81;
            this.gap_count.Text = "Gap=0";
            // 
            // packetsize
            // 
            this.packetsize.FormattingEnabled = true;
            this.packetsize.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.packetsize.Location = new System.Drawing.Point(686, 41);
            this.packetsize.Name = "packetsize";
            this.packetsize.Size = new System.Drawing.Size(53, 21);
            this.packetsize.TabIndex = 82;
            this.packetsize.Text = "16";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(659, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 83;
            this.label10.Text = "PS";
            // 
            // has_wifi
            // 
            this.has_wifi.AutoSize = true;
            this.has_wifi.Checked = true;
            this.has_wifi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.has_wifi.Location = new System.Drawing.Point(843, 43);
            this.has_wifi.Name = "has_wifi";
            this.has_wifi.Size = new System.Drawing.Size(70, 17);
            this.has_wifi.TabIndex = 84;
            this.has_wifi.Text = "P2PSync";
            this.has_wifi.UseVisualStyleBackColor = true;
            // 
            // my_debug3
            // 
            this.my_debug3.AutoSize = true;
            this.my_debug3.Location = new System.Drawing.Point(357, 405);
            this.my_debug3.Name = "my_debug3";
            this.my_debug3.Size = new System.Drawing.Size(35, 13);
            this.my_debug3.TabIndex = 85;
            this.my_debug3.Text = "status";
            // 
            // sync3
            // 
            this.sync3.Location = new System.Drawing.Point(717, 11);
            this.sync3.Name = "sync3";
            this.sync3.Size = new System.Drawing.Size(25, 20);
            this.sync3.TabIndex = 86;
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 30;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // axWriteDataqFileII1
            // 
            this.axWriteDataqFileII1.Enabled = true;
            this.axWriteDataqFileII1.Location = new System.Drawing.Point(413, 366);
            this.axWriteDataqFileII1.Name = "axWriteDataqFileII1";
            this.axWriteDataqFileII1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWriteDataqFileII1.OcxState")));
            this.axWriteDataqFileII1.Size = new System.Drawing.Size(100, 50);
            this.axWriteDataqFileII1.TabIndex = 87;
            this.axWriteDataqFileII1.ControlError += new AxDATAQFILEIILib._DWriteDataqFileIIEvents_ControlErrorEventHandler(this.axWriteDataqFileII1_ControlError);
            // 
            // WindaqFile
            // 
            this.WindaqFile.AutoSize = true;
            this.WindaqFile.Location = new System.Drawing.Point(1246, 27);
            this.WindaqFile.Name = "WindaqFile";
            this.WindaqFile.Size = new System.Drawing.Size(42, 17);
            this.WindaqFile.TabIndex = 88;
            this.WindaqFile.Text = "File";
            this.WindaqFile.UseVisualStyleBackColor = true;
            this.WindaqFile.CheckedChanged += new System.EventHandler(this.WindaqFile_CheckedChanged);
            // 
            // DestIP
            // 
            this.DestIP.Location = new System.Drawing.Point(128, 493);
            this.DestIP.Name = "DestIP";
            this.DestIP.Size = new System.Drawing.Size(100, 20);
            this.DestIP.TabIndex = 89;
            // 
            // payload_label
            // 
            this.payload_label.AutoSize = true;
            this.payload_label.Location = new System.Drawing.Point(258, 465);
            this.payload_label.Name = "payload_label";
            this.payload_label.Size = new System.Drawing.Size(35, 13);
            this.payload_label.TabIndex = 90;
            this.payload_label.Text = "label2";
            // 
            // ConnectOnly
            // 
            this.ConnectOnly.Location = new System.Drawing.Point(529, 8);
            this.ConnectOnly.Name = "ConnectOnly";
            this.ConnectOnly.Size = new System.Drawing.Size(57, 50);
            this.ConnectOnly.TabIndex = 91;
            this.ConnectOnly.Text = "Connect 1st";
            this.ConnectOnly.UseVisualStyleBackColor = true;
            this.ConnectOnly.Click += new System.EventHandler(this.ConnectOnly_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(931, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 92;
            this.label2.Text = "Channel#";
            // 
            // ActiveChannel
            // 
            this.ActiveChannel.Location = new System.Drawing.Point(990, 9);
            this.ActiveChannel.Name = "ActiveChannel";
            this.ActiveChannel.Size = new System.Drawing.Size(26, 20);
            this.ActiveChannel.TabIndex = 93;
            this.ActiveChannel.Text = "0";
            // 
            // sync4
            // 
            this.sync4.Location = new System.Drawing.Point(748, 11);
            this.sync4.Name = "sync4";
            this.sync4.Size = new System.Drawing.Size(25, 20);
            this.sync4.TabIndex = 94;
            // 
            // sync5
            // 
            this.sync5.Location = new System.Drawing.Point(779, 11);
            this.sync5.Name = "sync5";
            this.sync5.Size = new System.Drawing.Size(25, 20);
            this.sync5.TabIndex = 95;
            // 
            // my_debug4
            // 
            this.my_debug4.AutoSize = true;
            this.my_debug4.Location = new System.Drawing.Point(357, 420);
            this.my_debug4.Name = "my_debug4";
            this.my_debug4.Size = new System.Drawing.Size(35, 13);
            this.my_debug4.TabIndex = 96;
            this.my_debug4.Text = "status";
            // 
            // my_debug5
            // 
            this.my_debug5.AutoSize = true;
            this.my_debug5.Location = new System.Drawing.Point(357, 433);
            this.my_debug5.Name = "my_debug5";
            this.my_debug5.Size = new System.Drawing.Size(35, 13);
            this.my_debug5.TabIndex = 97;
            this.my_debug5.Text = "status";
            // 
            // axXChart1
            // 
            this.axXChart1.Enabled = true;
            this.axXChart1.Location = new System.Drawing.Point(529, 64);
            this.axXChart1.Name = "axXChart1";
            this.axXChart1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axXChart1.OcxState")));
            this.axXChart1.Size = new System.Drawing.Size(849, 442);
            this.axXChart1.TabIndex = 79;
            this.axXChart1.ChartChanged += new AxXCHARTLib._DXChartEvents_ChartChangedEventHandler(this.axXChart1_ChartChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 524);
            this.Controls.Add(this.my_debug5);
            this.Controls.Add(this.my_debug4);
            this.Controls.Add(this.sync5);
            this.Controls.Add(this.sync4);
            this.Controls.Add(this.ActiveChannel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConnectOnly);
            this.Controls.Add(this.payload_label);
            this.Controls.Add(this.DestIP);
            this.Controls.Add(this.WindaqFile);
            this.Controls.Add(this.axWriteDataqFileII1);
            this.Controls.Add(this.sync_start);
            this.Controls.Add(this.sync3);
            this.Controls.Add(this.my_debug3);
            this.Controls.Add(this.has_wifi);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.packetsize);
            this.Controls.Add(this.gap_count);
            this.Controls.Add(this.no_erase);
            this.Controls.Add(this.axXChart1);
            this.Controls.Add(this.my_scope);
            this.Controls.Add(this.my_keepalive);
            this.Controls.Add(this.par3_label);
            this.Controls.Add(this.par2_label);
            this.Controls.Add(this.par1_label);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmd_dest_ip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PayLoad);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.par3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.par2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.par1);
            this.Controls.Add(this.my_dqcmd);
            this.Controls.Add(this.sync_srate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sync2);
            this.Controls.Add(this.sync1);
            this.Controls.Add(this.my_debug2);
            this.Controls.Add(this.my_setup_bothunits);
            this.Controls.Add(this.my_debug);
            this.Controls.Add(this.my_query);
            this.Controls.Add(this.sync_stop);
            this.Controls.Add(this.my_disconnect);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UDP Sync Example";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.axWriteDataqFileII1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axXChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button my_disconnect;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button sync_start;
        private System.Windows.Forms.Button sync_stop;
        private System.Windows.Forms.Button my_query;
        private System.Windows.Forms.Label my_debug;
        private System.Windows.Forms.Button my_setup_bothunits;
        private System.Windows.Forms.Label my_debug2;
        private System.Windows.Forms.TextBox sync1;
        private System.Windows.Forms.TextBox sync2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sync_srate;
        private System.Windows.Forms.ComboBox my_dqcmd;
        private System.Windows.Forms.TextBox par1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox par2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox par3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PayLoad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cmd_dest_ip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label par1_label;
        private System.Windows.Forms.Label par2_label;
        private System.Windows.Forms.Label par3_label;
        private System.Windows.Forms.CheckBox my_keepalive;
        private System.Windows.Forms.CheckBox my_scope;
        private AxXCHARTLib.AxXChart axXChart1;
        private System.Windows.Forms.CheckBox no_erase;
        private System.Windows.Forms.Label gap_count;
        private System.Windows.Forms.ComboBox packetsize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox has_wifi;
        private System.Windows.Forms.Label my_debug3;
        private System.Windows.Forms.TextBox sync3;
        private System.Windows.Forms.Timer timer3;
        private AxDATAQFILEIILib.AxWriteDataqFileII axWriteDataqFileII1;
        private System.Windows.Forms.CheckBox WindaqFile;
        private System.Windows.Forms.TextBox DestIP;
        private System.Windows.Forms.Label payload_label;
        private System.Windows.Forms.Button ConnectOnly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ActiveChannel;
        private System.Windows.Forms.TextBox sync4;
        private System.Windows.Forms.TextBox sync5;
        private System.Windows.Forms.Label my_debug4;
        private System.Windows.Forms.Label my_debug5;
    }
}

