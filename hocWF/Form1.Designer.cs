using System.Windows.Forms;

namespace hocWF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            openFileDialog2 = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage3 = new TabPage();
            label3 = new Label();
            label2 = new Label();
            labelPath = new Label();
            buttonRunPS = new Button();
            textBoxPath = new TextBox();
            tabPage4 = new TabPage();
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
            tabPage1 = new TabPage();
            checkBox2 = new CheckBox();
            button5 = new Button();
            checkBox1 = new CheckBox();
            comboBox1 = new ComboBox();
            button4 = new Button();
            buttonToggleScroll = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            tabPage2 = new TabPage();
            LB_ftu_load_status = new Label();
            btnFTUcu = new Button();
            btnOpenFolder = new Button();
            openFileDialog3 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog2 = new FolderBrowserDialog();
            FBD_localDesDownLoad = new FolderBrowserDialog();
            FBD_winscpDLL = new FolderBrowserDialog();
            OFD_winscpDLL_File = new OpenFileDialog();
            OFD_macFilePath = new OpenFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            openFileDialog1 = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog2
            // 
            openFileDialog2.FileName = "openFileDialog2";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1116, 675);
            tabControl1.TabIndex = 11;
            // 
            // tabPage3
            // 
            tabPage3.AllowDrop = true;
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(label2);
            tabPage3.Controls.Add(labelPath);
            tabPage3.Controls.Add(buttonRunPS);
            tabPage3.Controls.Add(textBoxPath);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1108, 642);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Lọc Log";
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.DragDrop += TabPage3_DragDrop;
            tabPage3.DragEnter += TabPage3_DragEnter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkCyan;
            label3.Location = new Point(168, 154);
            label3.Name = "label3";
            label3.Size = new Size(768, 89);
            label3.TabIndex = 6;
            label3.Text = "Kéo thả file zip vào đây";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 40F);
            label2.ForeColor = Color.DeepPink;
            label2.Location = new Point(135, 98);
            label2.Name = "label2";
            label2.Size = new Size(0, 89);
            label2.TabIndex = 5;

            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.BackColor = SystemColors.ButtonHighlight;
            labelPath.Font = new Font("Segoe UI", 12F);
            labelPath.Location = new Point(162, 230);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(0, 28);
            labelPath.TabIndex = 4;
            // 
            // buttonRunPS
            // 
            buttonRunPS.BackColor = SystemColors.ButtonHighlight;
            buttonRunPS.Location = new Point(414, 350);
            buttonRunPS.Name = "buttonRunPS";
            buttonRunPS.Size = new Size(134, 42);
            buttonRunPS.TabIndex = 3;
            buttonRunPS.Text = "nhấp em đi";
            buttonRunPS.UseVisualStyleBackColor = false;
            buttonRunPS.Click += buttonRunPS_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Location = new Point(190, 286);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(708, 27);
            textBoxPath.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(CB_LocalScan);
            tabPage4.Controls.Add(BTN_macFilePath);
            tabPage4.Controls.Add(TB_MacFilePath);
            tabPage4.Controls.Add(label14);
            tabPage4.Controls.Add(CB_showPass);
            tabPage4.Controls.Add(BTN_saveFormInfo);
            tabPage4.Controls.Add(BTN_startScanLog);
            tabPage4.Controls.Add(label7);
            tabPage4.Controls.Add(BTN_winscpDll_file);
            tabPage4.Controls.Add(BTN_localFolderDownloadLog);
            tabPage4.Controls.Add(CBB_protocol);
            tabPage4.Controls.Add(TB_maxThread);
            tabPage4.Controls.Add(TB_severScan);
            tabPage4.Controls.Add(TB_password);
            tabPage4.Controls.Add(label10);
            tabPage4.Controls.Add(label11);
            tabPage4.Controls.Add(label12);
            tabPage4.Controls.Add(label13);
            tabPage4.Controls.Add(TB_winscpDLL);
            tabPage4.Controls.Add(TB_localDestinationDownload);
            tabPage4.Controls.Add(TB_portNumber);
            tabPage4.Controls.Add(TB_user);
            tabPage4.Controls.Add(TB_host);
            tabPage4.Controls.Add(label8);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label6);
            tabPage4.Controls.Add(label5);
            tabPage4.Controls.Add(label4);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1108, 642);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Thu thập log trên sever";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // CB_LocalScan
            // 
            CB_LocalScan.AutoSize = true;
            CB_LocalScan.Location = new Point(910, 287);
            CB_LocalScan.Name = "CB_LocalScan";
            CB_LocalScan.Size = new Size(99, 24);
            CB_LocalScan.TabIndex = 30;
            CB_LocalScan.Text = "Local scan";
            CB_LocalScan.UseVisualStyleBackColor = true;
            CB_LocalScan.CheckedChanged += CB_LocalScan_CheckedChanged;
            // 
            // BTN_macFilePath
            // 
            BTN_macFilePath.Location = new Point(353, 470);
            BTN_macFilePath.Name = "BTN_macFilePath";
            BTN_macFilePath.Size = new Size(94, 29);
            BTN_macFilePath.TabIndex = 29;
            BTN_macFilePath.Text = "Browser";
            BTN_macFilePath.UseVisualStyleBackColor = true;
            BTN_macFilePath.Click += BTN_macFilePath_Click;
            // 
            // TB_MacFilePath
            // 
            TB_MacFilePath.Location = new Point(90, 472);
            TB_MacFilePath.Name = "TB_MacFilePath";
            TB_MacFilePath.Size = new Size(242, 27);
            TB_MacFilePath.TabIndex = 28;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(90, 449);
            label14.Name = "label14";
            label14.Size = new Size(96, 20);
            label14.TabIndex = 27;
            label14.Text = "Mac file path";
            // 
            // CB_showPass
            // 
            CB_showPass.AutoSize = true;
            CB_showPass.Location = new Point(944, 213);
            CB_showPass.Name = "CB_showPass";
            CB_showPass.Size = new Size(65, 24);
            CB_showPass.TabIndex = 26;
            CB_showPass.Text = "show";
            CB_showPass.UseVisualStyleBackColor = true;
            CB_showPass.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // BTN_saveFormInfo
            // 
            BTN_saveFormInfo.Location = new Point(90, 538);
            BTN_saveFormInfo.Name = "BTN_saveFormInfo";
            BTN_saveFormInfo.Size = new Size(242, 29);
            BTN_saveFormInfo.TabIndex = 25;
            BTN_saveFormInfo.Text = "Save information";
            BTN_saveFormInfo.UseVisualStyleBackColor = true;
            BTN_saveFormInfo.Click += BTN_saveFormInfo_Click;
            // 
            // BTN_startScanLog
            // 
            BTN_startScanLog.Location = new Point(637, 494);
            BTN_startScanLog.Name = "BTN_startScanLog";
            BTN_startScanLog.Size = new Size(372, 73);
            BTN_startScanLog.TabIndex = 24;
            BTN_startScanLog.Text = "Bới lông tìm vết";
            BTN_startScanLog.UseVisualStyleBackColor = true;
            BTN_startScanLog.Click += BTN_startScanLog_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 35F);
            label7.ForeColor = Color.DeepPink;
            label7.Location = new Point(90, 45);
            label7.Name = "label7";
            label7.Size = new Size(468, 78);
            label7.TabIndex = 23;
            label7.Text = "LOG COLLECTOR";
            // 
            // BTN_winscpDll_file
            // 
            BTN_winscpDll_file.Location = new Point(353, 395);
            BTN_winscpDll_file.Name = "BTN_winscpDll_file";
            BTN_winscpDll_file.Size = new Size(94, 29);
            BTN_winscpDll_file.TabIndex = 22;
            BTN_winscpDll_file.Text = "Browser";
            BTN_winscpDll_file.UseVisualStyleBackColor = true;
            BTN_winscpDll_file.Click += BTN_winscpDll_file_Click;
            // 
            // BTN_localFolderDownloadLog
            // 
            BTN_localFolderDownloadLog.Location = new Point(353, 315);
            BTN_localFolderDownloadLog.Name = "BTN_localFolderDownloadLog";
            BTN_localFolderDownloadLog.Size = new Size(94, 29);
            BTN_localFolderDownloadLog.TabIndex = 21;
            BTN_localFolderDownloadLog.Text = "Browser";
            BTN_localFolderDownloadLog.UseVisualStyleBackColor = true;
            BTN_localFolderDownloadLog.Click += BTN_localFolderDownloadLog_Click;
            // 
            // CBB_protocol
            // 
            CBB_protocol.DropDownStyle = ComboBoxStyle.DropDownList;
            CBB_protocol.FormattingEnabled = true;
            CBB_protocol.Items.AddRange(new object[] { "SFTP", "SCP", "SFT" });
            CBB_protocol.Location = new Point(637, 157);
            CBB_protocol.Name = "CBB_protocol";
            CBB_protocol.Size = new Size(151, 28);
            CBB_protocol.TabIndex = 20;
            // 
            // TB_maxThread
            // 
            TB_maxThread.Location = new Point(637, 397);
            TB_maxThread.Name = "TB_maxThread";
            TB_maxThread.Size = new Size(372, 27);
            TB_maxThread.TabIndex = 18;
            TB_maxThread.KeyPress += TB_maxThread_KeyPress;
            // 
            // TB_severScan
            // 
            TB_severScan.Location = new Point(637, 317);
            TB_severScan.Name = "TB_severScan";
            TB_severScan.Size = new Size(372, 27);
            TB_severScan.TabIndex = 17;
            TB_severScan.Click += TB_severScan_Click;
            TB_severScan.TextChanged += TB_severScan_TextChanged;
            // 
            // TB_password
            // 
            TB_password.Location = new Point(637, 237);
            TB_password.Name = "TB_password";
            TB_password.Size = new Size(372, 27);
            TB_password.TabIndex = 16;
            TB_password.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(637, 374);
            label10.Name = "label10";
            label10.Size = new Size(117, 20);
            label10.TabIndex = 13;
            label10.Text = "Max thread scan";
     
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(637, 294);
            label11.Name = "label11";
            label11.Size = new Size(138, 20);
            label11.TabIndex = 12;
            label11.Text = "Remote folder scan";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(637, 213);
            label12.Name = "label12";
            label12.Size = new Size(70, 20);
            label12.TabIndex = 11;
            label12.Text = "Password";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(637, 134);
            label13.Name = "label13";
            label13.Size = new Size(65, 20);
            label13.TabIndex = 10;
            label13.Text = "Protocol";
            // 
            // TB_winscpDLL
            // 
            TB_winscpDLL.Location = new Point(90, 397);
            TB_winscpDLL.Name = "TB_winscpDLL";
            TB_winscpDLL.Size = new Size(242, 27);
            TB_winscpDLL.TabIndex = 9;
            // 
            // TB_localDestinationDownload
            // 
            TB_localDestinationDownload.Location = new Point(90, 317);
            TB_localDestinationDownload.Name = "TB_localDestinationDownload";
            TB_localDestinationDownload.Size = new Size(242, 27);
            TB_localDestinationDownload.TabIndex = 8;
            // 
            // TB_portNumber
            // 
            TB_portNumber.Location = new Point(813, 157);
            TB_portNumber.Name = "TB_portNumber";
            TB_portNumber.Size = new Size(196, 27);
            TB_portNumber.TabIndex = 7;
            TB_portNumber.KeyPress += TB_portNumber_KeyPress;
            // 
            // TB_user
            // 
            TB_user.Location = new Point(90, 237);
            TB_user.Name = "TB_user";
            TB_user.Size = new Size(357, 27);
            TB_user.TabIndex = 6;
            // 
            // TB_host
            // 
            TB_host.Location = new Point(90, 157);
            TB_host.Name = "TB_host";
            TB_host.Size = new Size(357, 27);
            TB_host.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(90, 374);
            label8.Name = "label8";
            label8.Size = new Size(107, 20);
            label8.TabIndex = 4;
            label8.Text = "WinscpDLL file";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(90, 294);
            label9.Name = "label9";
            label9.Size = new Size(193, 20);
            label9.TabIndex = 3;
            label9.Text = "Local download destination";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(813, 134);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 2;
            label6.Text = "Portnumber";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(90, 214);
            label5.Name = "label5";
            label5.Size = new Size(38, 20);
            label5.TabIndex = 1;
            label5.Text = "User";
  
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(90, 134);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 0;
            label4.Text = "Host/ip";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(buttonToggleScroll);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(richTextBox2);
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1108, 642);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tool compare";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(627, 33);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(149, 24);
            checkBox2.TabIndex = 21;
            checkBox2.Text = "Enable edit mode";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // button5
            // 
            button5.Location = new Point(244, 550);
            button5.Name = "button5";
            button5.Size = new Size(216, 30);
            button5.TabIndex = 20;
            button5.Text = "Save";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(23, 33);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(309, 24);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "So sánh file config.ini >< config lấy từ log";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Get config from LOG", "File text" });
            comboBox1.Location = new Point(863, 551);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(216, 28);
            comboBox1.TabIndex = 18;
            // 
            // button4
            // 
            button4.Location = new Point(483, 244);
            button4.Name = "button4";
            button4.Size = new Size(117, 29);
            button4.TabIndex = 17;
            button4.Text = "undo";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // buttonToggleScroll
            // 
            buttonToggleScroll.Location = new Point(483, 184);
            buttonToggleScroll.Name = "buttonToggleScroll";
            buttonToggleScroll.Size = new Size(117, 29);
            buttonToggleScroll.TabIndex = 16;
            buttonToggleScroll.Text = "đồng bộ roll";
            buttonToggleScroll.UseVisualStyleBackColor = true;
            buttonToggleScroll.Click += buttonToggleScroll_Click;
            // 
            // button3
            // 
            button3.Location = new Point(483, 134);
            button3.Name = "button3";
            button3.Size = new Size(117, 29);
            button3.TabIndex = 15;
            button3.Text = "so sánh";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(627, 550);
            button2.Name = "button2";
            button2.Size = new Size(216, 30);
            button2.TabIndex = 14;
            button2.Text = "File cũ / log của trạm";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(23, 550);
            button1.Name = "button1";
            button1.Size = new Size(216, 30);
            button1.TabIndex = 13;
            button1.Text = "File mới";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(627, 75);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(452, 444);
            richTextBox2.TabIndex = 12;
            richTextBox2.Text = "";
            richTextBox2.TextChanged += richTextBox2_TextChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(23, 75);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(437, 444);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(LB_ftu_load_status);
            tabPage2.Controls.Add(btnFTUcu);
            tabPage2.Controls.Add(btnOpenFolder);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1108, 642);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "copy test funtion";
            tabPage2.UseVisualStyleBackColor = true;

            // 
            // LB_ftu_load_status
            // 
            LB_ftu_load_status.AutoSize = true;
            LB_ftu_load_status.Location = new Point(367, 85);
            LB_ftu_load_status.Name = "LB_ftu_load_status";
            LB_ftu_load_status.Size = new Size(0, 20);
            LB_ftu_load_status.TabIndex = 7;
            // 
            // btnFTUcu
            // 
            btnFTUcu.Location = new Point(66, 81);
            btnFTUcu.Name = "btnFTUcu";
            btnFTUcu.Size = new Size(277, 29);
            btnFTUcu.TabIndex = 6;
            btnFTUcu.Text = "LOAD FTU CŨ";
            btnFTUcu.UseVisualStyleBackColor = true;
            btnFTUcu.Click += btnFTUcu_Click;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Location = new Point(66, 31);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(277, 29);
            btnOpenFolder.TabIndex = 2;
            btnOpenFolder.Text = "FTU MỚI";
            btnOpenFolder.UseVisualStyleBackColor = true;

            // 
            // openFileDialog3
            // 
            openFileDialog3.FileName = "openFileDialog3";
            // 
            // folderBrowserDialog1
            // 

            // 
            // folderBrowserDialog2
            // 

            // 
            // OFD_winscpDLL_File
            // 
            OFD_winscpDLL_File.FileName = "openFileDialog4";
            // 
            // OFD_macFilePath
            // 
            OFD_macFilePath.FileName = "OFD_macFilePath";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1140, 699);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Tool hỗ trợ ve di phai";

            tabControl1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);


        }

        private void TabPage3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                string filePath = files[0]; // lấy file đầu tiên
                textBoxPath.Text = filePath;
            }
        }

        private void TabPage3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        #endregion
        private OpenFileDialog openFileDialog2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private CheckBox checkBox2;
        private Button button5;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private Button button4;
        private Button buttonToggleScroll;
        private Button button3;
        private Button button2;
        private Button button1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private Button btnOpenFolder;
        private OpenFileDialog openFileDialog3;
        private FolderBrowserDialog folderBrowserDialog1;
        private FolderBrowserDialog folderBrowserDialog2;
        private TabPage tabPage3;
        private Label label2;
        private Label labelPath;
        private Button buttonRunPS;
        private TextBox textBoxPath;
        private Label label3;
        private TabPage tabPage4;
        private TextBox TB_host;
        private Label label8;
        private Label label9;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox TB_winscpDLL;
        private TextBox TB_localDestinationDownload;
        private TextBox TB_portNumber;
        private TextBox TB_user;
        private TextBox TB_maxThread;
        private TextBox TB_severScan;
        private TextBox TB_password;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private ComboBox CBB_protocol;
        private Button BTN_localFolderDownloadLog;
        private FolderBrowserDialog FBD_localDesDownLoad;
        private Button BTN_winscpDll_file;
        private FolderBrowserDialog FBD_winscpDLL;
        private Label label7;
        private Button BTN_startScanLog;
        private OpenFileDialog OFD_winscpDLL_File;
        private Button BTN_saveFormInfo;
        private CheckBox CB_showPass;
        private Button BTN_macFilePath;
        private TextBox TB_MacFilePath;
        private Label label14;
        private OpenFileDialog OFD_macFilePath;
        private ContextMenuStrip contextMenuStrip1;
        private CheckBox CB_LocalScan;
        private Button btnFTUcu;
        private OpenFileDialog openFileDialog1;
        private Label LB_ftu_load_status;
    }
}
