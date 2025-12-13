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
            tabPage4 = new TabPage();
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
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1108, 642);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Lọc Log";
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.DragEnter += TabPage3_DragEnter;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1108, 642);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Thu thập log trên sever";
            tabPage4.UseVisualStyleBackColor = true;
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
            tabPage2.Click += tabPage2_Click;
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
            folderBrowserDialog1.HelpRequest += folderBrowserDialog1_HelpRequest;
            // 
            // folderBrowserDialog2
            // 
            folderBrowserDialog2.HelpRequest += folderBrowserDialog2_HelpRequest;
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
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);


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
        private TabPage tabPage4;
        private FolderBrowserDialog FBD_localDesDownLoad;
        private FolderBrowserDialog FBD_winscpDLL;
        private OpenFileDialog OFD_winscpDLL_File;
        private OpenFileDialog OFD_macFilePath;
        private ContextMenuStrip contextMenuStrip1;
        private Button btnFTUcu;
        private OpenFileDialog openFileDialog1;
        private Label LB_ftu_load_status;
    }
}
