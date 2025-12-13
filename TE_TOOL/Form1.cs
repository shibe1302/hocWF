using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TE_TOOL;
using TE_TOOL.Models;
using TE_TOOL.Views;
using TE_TOOL.Views._02_tab_thu_thap_log;

namespace hocWF
{


    public partial class Form1 : Form
    {
        private object selectedFilePath1;
        private object selectedFilePath2;

        private DateTime lastEditTime1 = DateTime.MinValue;
        private DateTime lastEditTime2 = DateTime.MinValue;
        private const int UNDO_GROUP_DELAY = 1000;


        private string selectedFolderPath = "";
        private string jsonFilePath = "";
        private string iniFilePath = "";

        private string originalIniFileContent = "";
        private string[] currentCheckedItemIds = new string[0];


        private bool isSyncingScroll = false;
        private bool isSyncScroll = false;

        private string currentJsonFilePath = "";
        private Stack<string> undoStack1 = new Stack<string>();
        private Stack<string> undoStack2 = new Stack<string>();

        private bool isUndoing = false;
        private bool isHighlighting = false;

        private System.Windows.Forms.Timer debounceTimer1;
        private System.Windows.Forms.Timer debounceTimer2;
        private const int DEBOUNCE_DELAY = 1000;




        public string LocalDownLoadLogPath = "";
        public string MacFilePath = "";
        private string WinscpFilePath = "";

        private List<string> list_path_remote_or_local = new List<string>();

        // Tab Views
        private LocLogView locLogView;
        private LogCollectorView logCollectorView;



        public Form1()

        {
            InitializeComponent();
            InitializeTabViews();

            richTextBox1.VScroll += RichTextBox1_VScroll;
            richTextBox2.VScroll += RichTextBox2_VScroll;

            debounceTimer1 = new System.Windows.Forms.Timer();
            debounceTimer1.Interval = DEBOUNCE_DELAY;

            debounceTimer2 = new System.Windows.Forms.Timer();
            debounceTimer2.Interval = DEBOUNCE_DELAY;
  
            undoStack1.Push("");
            undoStack2.Push("");
            comboBox1.SelectedIndex = 0;


        }


        private void InitializeTabViews()
        {
            locLogView = new LocLogView();
            {
                Dock = DockStyle.Fill;
            }
            ;
            tabPage3.Controls.Clear();
            tabPage3.Controls.Add(locLogView);

            logCollectorView = new LogCollectorView();
            {
                Dock = DockStyle.Fill;
            }
            ;
            tabPage4.Controls.Clear();
            tabPage4.Controls.Add(logCollectorView);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Supported files (*.ini;*.log;*.txt)|*.ini;*.log;*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath1 = openFileDialog1.FileName;
            }

            if (!string.IsNullOrEmpty(selectedFilePath1?.ToString()))
            {
                string content = File.ReadAllText(selectedFilePath1.ToString());

                if (checkBox1.Checked)
                {
                    content = content.Replace(" ", "");
                }

                richTextBox1.Text = content;
                SaveToUndoStack(richTextBox1, undoStack1);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn file nào!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Supported files (*.ini;*.log;*.txt)|*.ini;*.log;*.txt";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath2 = openFileDialog2.FileName;
            }

            if (!string.IsNullOrEmpty(selectedFilePath2?.ToString()))
            {
                string result;

                if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Get config from LOG")
                {
                    result = GetTestConfigSection(selectedFilePath2.ToString());
                    if (string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Hãy chuyển sang chế độ đọc file *.txt");
                    }
                }
                else
                {
                    result = File.ReadAllText(selectedFilePath2.ToString());
                }

                richTextBox2.Text = result;
                SaveToUndoStack(richTextBox2, undoStack2);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn file nào!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompareAndHighlight();
        }





        private void SaveToUndoStack(RichTextBox rtb, Stack<string> stack)
        {
            string currentText = rtb.Text;

            if (stack.Count == 0 || stack.Peek() != currentText)
            {
                stack.Push(currentText);

                if (stack.Count > 50)
                {
                    var arr = stack.ToArray();
                    stack.Clear();
                    for (int i = Math.Min(arr.Length - 1, 49); i >= 0; i--)
                    {
                        stack.Push(arr[i]);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UndoLastEdited();
        }



        private RichTextBox lastEditedRichTextBox;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lastEditedRichTextBox = richTextBox1;

            var now = DateTime.Now;
            if ((now - lastEditTime1).TotalMilliseconds > UNDO_GROUP_DELAY)
            {
                SaveToUndoStack(richTextBox1, undoStack1);
            }
            else
            {
            }
            lastEditTime1 = now;

            CompareAndHighlight();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            lastEditedRichTextBox = richTextBox2;

            var now = DateTime.Now;
            if ((now - lastEditTime2).TotalMilliseconds > UNDO_GROUP_DELAY)
            {
                SaveToUndoStack(richTextBox2, undoStack2);
            }
            lastEditTime2 = now;

            CompareAndHighlight();
        }

        private void UndoLastEdited()
        {
            if (lastEditedRichTextBox == richTextBox1)
            {
                debounceTimer1.Stop();
                SaveToUndoStack(richTextBox1, undoStack1);
                PerformUndo(richTextBox1, undoStack1);
            }
            else if (lastEditedRichTextBox == richTextBox2)
            {
                debounceTimer2.Stop();
                SaveToUndoStack(richTextBox2, undoStack2);
                PerformUndo(richTextBox2, undoStack2);
            }
            else
            {
                MessageBox.Show("Chưa có thao tác nào để Undo!");
            }
        }


        private void PerformUndo(RichTextBox rtb, Stack<string> stack)
        {
            if (stack.Count <= 1)
            {
                MessageBox.Show("Không còn gì để Undo!");
                return;
            }

            isUndoing = true;

            stack.Pop();
            string previousText = stack.Peek();
            int cursorPos = Math.Min(rtb.SelectionStart, previousText.Length);

            rtb.Text = previousText;
            rtb.SelectionStart = cursorPos;

            isUndoing = false;

            CompareAndHighlight();
        }

        private void buttonToggleScroll_Click(object sender, EventArgs e)
        {
            isSyncScroll = !isSyncScroll;
            buttonToggleScroll.Text = isSyncScroll ? "Tắt đồng bộ scroll" : "Bật đồng bộ scroll";
        }

        private void RichTextBox1_VScroll(object sender, EventArgs e)
        {
            if (isSyncScroll && !isSyncingScroll)
                SyncScroll(richTextBox1, richTextBox2);
        }

        private void RichTextBox2_VScroll(object sender, EventArgs e)
        {
            if (isSyncScroll && !isSyncingScroll)
                SyncScroll(richTextBox2, richTextBox1);
        }

        private void SyncScroll(RichTextBox source, RichTextBox target)
        {
            if (isSyncingScroll) return;
            isSyncingScroll = true;

            int nPos = GetScrollPos(source.Handle, SB_VERT);
            SendMessage(target.Handle, WM_VSCROLL, (IntPtr)(SB_THUMBPOSITION + 0x10000 * nPos), IntPtr.Zero);

            isSyncingScroll = false;
        }

        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_VSCROLL = 0x0115;
        private const int SB_VERT = 1;
        private const int SB_THUMBPOSITION = 4;
        private const int WM_SETREDRAW = 0x000B;

        private bool CanCompare()
        {
            return !string.IsNullOrEmpty(richTextBox1.Text)
                && !string.IsNullOrEmpty(richTextBox2.Text);
        }

        public string GetTestConfigSection(string filePath)
        {
            string startMark = "===Test config==================================================================";
            string endMark = "===Upload_to_taipei=============================================================";

            bool isCapture = false;
            StringBuilder result = new StringBuilder();

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.Contains(startMark))
                {
                    isCapture = true;
                    continue;
                }

                if (line.Contains(endMark))
                {
                    break;
                }

                if (isCapture)
                {

                    string cleanedLine = Regex.Replace(
                        line,
                        @"^\[[A-Z]+\s*\|\d{2}:\d{2}:\d{2}\]\s*",
                        ""
                    );

                    result.AppendLine(cleanedLine);
                }
            }
            Debug.WriteLine(result.ToString());
            return result.ToString();
        }

        int NextNonEmptyIndex(string[] lines, int start)
        {
            int k = start;
            while (k < lines.Length)
            {
                if (!string.IsNullOrWhiteSpace(lines[k]))
                    break;
                k++;
            }
            return k;
        }

        void CompareAndHighlight()
        {
            if (!CanCompare() || isHighlighting) return;
            isHighlighting = true;

            int scrollPos1 = GetScrollPos(richTextBox1.Handle, SB_VERT);
            int scrollPos2 = GetScrollPos(richTextBox2.Handle, SB_VERT);
            int selStart1 = richTextBox1.SelectionStart;
            int selLength1 = richTextBox1.SelectionLength;
            int selStart2 = richTextBox2.SelectionStart;
            int selLength2 = richTextBox2.SelectionLength;

            SendMessage(richTextBox1.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            SendMessage(richTextBox2.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            try
            {
                richTextBox1.Select(0, richTextBox1.Text.Length);
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox2.Select(0, richTextBox2.Text.Length);
                richTextBox2.SelectionBackColor = Color.White;

                string[] lines1 = richTextBox1.Lines;
                string[] lines2 = richTextBox2.Lines;

                int i = 0, j = 0;

                while (true)
                {
                    i = NextNonEmptyIndex(lines1, i);
                    j = NextNonEmptyIndex(lines2, j);

                    bool end1 = i >= lines1.Length;
                    bool end2 = j >= lines2.Length;

                    if (end1 && end2) break;

                    if (end1 ^ end2)
                    {
                        if (!end1)
                        {
                            int start1 = richTextBox1.GetFirstCharIndexFromLine(i);
                            if (start1 >= 0)
                            {
                                richTextBox1.Select(start1, lines1[i].Length);
                                richTextBox1.SelectionBackColor = Color.LightPink;
                            }
                        }
                        if (!end2)
                        {
                            int start2 = richTextBox2.GetFirstCharIndexFromLine(j);
                            if (start2 >= 0)
                            {
                                richTextBox2.Select(start2, lines2[j].Length);
                                richTextBox2.SelectionBackColor = Color.LightBlue;
                            }
                        }
                        if (!end1) i++;
                        if (!end2) j++;
                        continue;
                    }

                    string l1 = lines1[i].Trim();
                    string l2 = lines2[j].Trim();

                    if (!string.Equals(l1, l2, StringComparison.Ordinal))
                    {
                        int start1 = richTextBox1.GetFirstCharIndexFromLine(i);
                        if (start1 >= 0)
                        {
                            richTextBox1.Select(start1, lines1[i].Length);
                            richTextBox1.SelectionBackColor = Color.LightPink;
                        }

                        int start2 = richTextBox2.GetFirstCharIndexFromLine(j);
                        if (start2 >= 0)
                        {
                            richTextBox2.Select(start2, lines2[j].Length);
                            richTextBox2.SelectionBackColor = Color.LightBlue;
                        }
                    }

                    i++;
                    j++;
                }

                richTextBox1.SelectionStart = selStart1;
                richTextBox1.SelectionLength = selLength1;
                richTextBox2.SelectionStart = selStart2;
                richTextBox2.SelectionLength = selLength2;

                SendMessage(richTextBox1.Handle, WM_VSCROLL, (IntPtr)(SB_THUMBPOSITION + 0x10000 * scrollPos1), IntPtr.Zero);
                SendMessage(richTextBox2.Handle, WM_VSCROLL, (IntPtr)(SB_THUMBPOSITION + 0x10000 * scrollPos2), IntPtr.Zero);
            }
            finally
            {
                SendMessage(richTextBox1.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                SendMessage(richTextBox2.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                richTextBox1.Invalidate();
                richTextBox2.Invalidate();
                isHighlighting = false;
            }
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (selectedFilePath1 == null || string.IsNullOrEmpty(selectedFilePath1.ToString()))
            {
                MessageBox.Show("Chưa có file nào được mở để lưu!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Mày chắc chưa ? lưu là cook data cũ luôn !",
                "Xác nhận lưu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    File.WriteAllText(selectedFilePath1.ToString(), richTextBox1.Text, Encoding.UTF8);

                    MessageBox.Show("Đã lưu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                richTextBox2.ReadOnly = false;
            }
            else
            {
                richTextBox2.ReadOnly = true;
            }
        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }






        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFTUcu_Click(object sender, EventArgs e)
        {
            FORM_FTU form_ftu = new FORM_FTU();

            form_ftu.DataSaved += (content) =>
            {
                LB_ftu_load_status.Text = content;
            };

            form_ftu.Show();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnMigrateFromOldFtu_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonToggleScroll_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxFCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFTU_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }


        private void TB_severScan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

