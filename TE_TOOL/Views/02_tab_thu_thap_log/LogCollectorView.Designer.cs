namespace TE_TOOL.Views._02_tab_thu_thap_log
{
    partial class LogCollectorView
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
            CB_LocalScan = new CheckBox();
            BTN_macFilePath = new Button();
            TB_MacFilePath = new TextBox();
            label14 = new Label();
            CB_showPass = new CheckBox();
            BTN_saveFormInfo = new Button();
            BTN_startScanLog = new Button();
            label7 = new Label();
            BTN_winscpDll_file = new Button();
            BTN_localFolderDownloadLog = new Button();
            CBB_protocol = new ComboBox();
            TB_maxThread = new TextBox();
            TB_severScan = new TextBox();
            TB_password = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            TB_winscpDLL = new TextBox();
            TB_localDestinationDownload = new TextBox();
            TB_portNumber = new TextBox();
            TB_user = new TextBox();
            TB_host = new TextBox();
            label8 = new Label();
            label9 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            OFD_macFilePath = new OpenFileDialog();
            FBD_localDesDownLoad = new FolderBrowserDialog();
            OFD_winscpDLL_File = new OpenFileDialog();
            SuspendLayout();
            // 
            // CB_LocalScan
            // 
            CB_LocalScan.AutoSize = true;
            CB_LocalScan.Location = new Point(915, 283);
            CB_LocalScan.Name = "CB_LocalScan";
            CB_LocalScan.Size = new Size(99, 24);
            CB_LocalScan.TabIndex = 86;
            CB_LocalScan.Text = "Local scan";
            CB_LocalScan.UseVisualStyleBackColor = true;
            // 
            // BTN_macFilePath
            // 
            BTN_macFilePath.Location = new Point(358, 466);
            BTN_macFilePath.Name = "BTN_macFilePath";
            BTN_macFilePath.Size = new Size(94, 29);
            BTN_macFilePath.TabIndex = 85;
            BTN_macFilePath.Text = "Browser";
            BTN_macFilePath.UseVisualStyleBackColor = true;
            // 
            // TB_MacFilePath
            // 
            TB_MacFilePath.Location = new Point(95, 468);
            TB_MacFilePath.Name = "TB_MacFilePath";
            TB_MacFilePath.Size = new Size(242, 27);
            TB_MacFilePath.TabIndex = 84;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(95, 445);
            label14.Name = "label14";
            label14.Size = new Size(96, 20);
            label14.TabIndex = 83;
            label14.Text = "Mac file path";
            // 
            // CB_showPass
            // 
            CB_showPass.AutoSize = true;
            CB_showPass.Location = new Point(949, 209);
            CB_showPass.Name = "CB_showPass";
            CB_showPass.Size = new Size(65, 24);
            CB_showPass.TabIndex = 82;
            CB_showPass.Text = "show";
            CB_showPass.UseVisualStyleBackColor = true;
            // 
            // BTN_saveFormInfo
            // 
            BTN_saveFormInfo.Location = new Point(95, 534);
            BTN_saveFormInfo.Name = "BTN_saveFormInfo";
            BTN_saveFormInfo.Size = new Size(242, 29);
            BTN_saveFormInfo.TabIndex = 81;
            BTN_saveFormInfo.Text = "Save information";
            BTN_saveFormInfo.UseVisualStyleBackColor = true;
            BTN_saveFormInfo.Click += BTN_saveFormInfo_Click;
            // 
            // BTN_startScanLog
            // 
            BTN_startScanLog.Location = new Point(642, 490);
            BTN_startScanLog.Name = "BTN_startScanLog";
            BTN_startScanLog.Size = new Size(372, 73);
            BTN_startScanLog.TabIndex = 80;
            BTN_startScanLog.Text = "Bới lông tìm vết";
            BTN_startScanLog.UseVisualStyleBackColor = true;
            BTN_startScanLog.Click += BTN_startScanLog_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 35F);
            label7.ForeColor = Color.DeepPink;
            label7.Location = new Point(95, 41);
            label7.Name = "label7";
            label7.Size = new Size(468, 78);
            label7.TabIndex = 79;
            label7.Text = "LOG COLLECTOR";
            // 
            // BTN_winscpDll_file
            // 
            BTN_winscpDll_file.Location = new Point(358, 391);
            BTN_winscpDll_file.Name = "BTN_winscpDll_file";
            BTN_winscpDll_file.Size = new Size(94, 29);
            BTN_winscpDll_file.TabIndex = 78;
            BTN_winscpDll_file.Text = "Browser";
            BTN_winscpDll_file.UseVisualStyleBackColor = true;
            // 
            // BTN_localFolderDownloadLog
            // 
            BTN_localFolderDownloadLog.Location = new Point(358, 311);
            BTN_localFolderDownloadLog.Name = "BTN_localFolderDownloadLog";
            BTN_localFolderDownloadLog.Size = new Size(94, 29);
            BTN_localFolderDownloadLog.TabIndex = 77;
            BTN_localFolderDownloadLog.Text = "Browser";
            BTN_localFolderDownloadLog.UseVisualStyleBackColor = true;
            // 
            // CBB_protocol
            // 
            CBB_protocol.DropDownStyle = ComboBoxStyle.DropDownList;
            CBB_protocol.FormattingEnabled = true;
            CBB_protocol.Items.AddRange(new object[] { "SFTP", "SCP", "SFT" });
            CBB_protocol.Location = new Point(642, 153);
            CBB_protocol.Name = "CBB_protocol";
            CBB_protocol.Size = new Size(151, 28);
            CBB_protocol.TabIndex = 76;
            // 
            // TB_maxThread
            // 
            TB_maxThread.Location = new Point(642, 393);
            TB_maxThread.Name = "TB_maxThread";
            TB_maxThread.Size = new Size(372, 27);
            TB_maxThread.TabIndex = 75;
            // 
            // TB_severScan
            // 
            TB_severScan.Location = new Point(642, 313);
            TB_severScan.Name = "TB_severScan";
            TB_severScan.Size = new Size(372, 27);
            TB_severScan.TabIndex = 74;
            // 
            // TB_password
            // 
            TB_password.Location = new Point(642, 233);
            TB_password.Name = "TB_password";
            TB_password.Size = new Size(372, 27);
            TB_password.TabIndex = 73;
            TB_password.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(642, 370);
            label10.Name = "label10";
            label10.Size = new Size(117, 20);
            label10.TabIndex = 72;
            label10.Text = "Max thread scan";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(642, 290);
            label11.Name = "label11";
            label11.Size = new Size(138, 20);
            label11.TabIndex = 71;
            label11.Text = "Remote folder scan";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(642, 209);
            label12.Name = "label12";
            label12.Size = new Size(70, 20);
            label12.TabIndex = 70;
            label12.Text = "Password";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(642, 130);
            label13.Name = "label13";
            label13.Size = new Size(65, 20);
            label13.TabIndex = 69;
            label13.Text = "Protocol";
            // 
            // TB_winscpDLL
            // 
            TB_winscpDLL.Location = new Point(95, 393);
            TB_winscpDLL.Name = "TB_winscpDLL";
            TB_winscpDLL.Size = new Size(242, 27);
            TB_winscpDLL.TabIndex = 68;
            // 
            // TB_localDestinationDownload
            // 
            TB_localDestinationDownload.Location = new Point(95, 313);
            TB_localDestinationDownload.Name = "TB_localDestinationDownload";
            TB_localDestinationDownload.Size = new Size(242, 27);
            TB_localDestinationDownload.TabIndex = 67;
            // 
            // TB_portNumber
            // 
            TB_portNumber.Location = new Point(818, 153);
            TB_portNumber.Name = "TB_portNumber";
            TB_portNumber.Size = new Size(196, 27);
            TB_portNumber.TabIndex = 66;
            // 
            // TB_user
            // 
            TB_user.Location = new Point(95, 233);
            TB_user.Name = "TB_user";
            TB_user.Size = new Size(357, 27);
            TB_user.TabIndex = 65;
            // 
            // TB_host
            // 
            TB_host.Location = new Point(95, 153);
            TB_host.Name = "TB_host";
            TB_host.Size = new Size(357, 27);
            TB_host.TabIndex = 64;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(95, 370);
            label8.Name = "label8";
            label8.Size = new Size(107, 20);
            label8.TabIndex = 63;
            label8.Text = "WinscpDLL file";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(95, 290);
            label9.Name = "label9";
            label9.Size = new Size(193, 20);
            label9.TabIndex = 62;
            label9.Text = "Local download destination";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(818, 130);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 61;
            label6.Text = "Portnumber";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(95, 210);
            label5.Name = "label5";
            label5.Size = new Size(38, 20);
            label5.TabIndex = 60;
            label5.Text = "User";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(95, 130);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 59;
            label4.Text = "Host/ip";
            // 
            // OFD_macFilePath
            // 
            OFD_macFilePath.FileName = "OFD_macFilePath";
            // 
            // FBD_localDesDownLoad
            // 
            FBD_localDesDownLoad.HelpRequest += FBD_localDesDownLoad_HelpRequest;
            // 
            // OFD_winscpDLL_File
            // 
            OFD_winscpDLL_File.FileName = "OFD_winscpDLL_File";
            // 
            // LogCollectorView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CB_LocalScan);
            Controls.Add(BTN_macFilePath);
            Controls.Add(TB_MacFilePath);
            Controls.Add(label14);
            Controls.Add(CB_showPass);
            Controls.Add(BTN_saveFormInfo);
            Controls.Add(BTN_startScanLog);
            Controls.Add(label7);
            Controls.Add(BTN_winscpDll_file);
            Controls.Add(BTN_localFolderDownloadLog);
            Controls.Add(CBB_protocol);
            Controls.Add(TB_maxThread);
            Controls.Add(TB_severScan);
            Controls.Add(TB_password);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(label13);
            Controls.Add(TB_winscpDLL);
            Controls.Add(TB_localDestinationDownload);
            Controls.Add(TB_portNumber);
            Controls.Add(TB_user);
            Controls.Add(TB_host);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Name = "LogCollectorView";
            Size = new Size(1108, 642);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CB_LocalScan;
        private Button BTN_macFilePath;
        private TextBox TB_MacFilePath;
        private Label label14;
        private CheckBox CB_showPass;
        private Button BTN_saveFormInfo;
        private Button BTN_startScanLog;
        private Label label7;
        private Button BTN_winscpDll_file;
        private Button BTN_localFolderDownloadLog;
        private ComboBox CBB_protocol;
        private TextBox TB_maxThread;
        private TextBox TB_severScan;
        private TextBox TB_password;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private TextBox TB_winscpDLL;
        private TextBox TB_localDestinationDownload;
        private TextBox TB_portNumber;
        private TextBox TB_user;
        private TextBox TB_host;
        private Label label8;
        private Label label9;
        private Label label6;
        private Label label5;
        private Label label4;
        private OpenFileDialog OFD_macFilePath;
        private FolderBrowserDialog FBD_localDesDownLoad;
        private OpenFileDialog OFD_winscpDLL_File;
    }
}
