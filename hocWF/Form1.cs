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

namespace hocWF
{


    public partial class Form1 : Form
    {
        private object selectedFilePath1;
        private object selectedFilePath2;

        private DateTime lastEditTime1 = DateTime.MinValue;
        private DateTime lastEditTime2 = DateTime.MinValue;
        private const int UNDO_GROUP_DELAY = 1000;

        private StringBuilder migrationLog = new StringBuilder();

        private string selectedFolderPath = "";
        private string jsonFilePath = "";
        private string iniFilePath = "";
        private List<DiagTestItem> diagTestItems = new List<DiagTestItem>();
        private string originalIniFileContent = "";
        private string[] currentCheckedItemIds = new string[0];

        private int dragIndex = -1;

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


        private int lastHighlightedIndex = -1;
        private Rectangle dragBoxFromMouseDown;
        private object draggedItem;
        private bool isDraggedItemChecked;
        private string jsonFilePathOldFtu = "";
        private string selectedFolderPathOldFtu = "";
        private string iniFilePathOldFtu = "";

        public string LocalDownLoadLogPath = "";
        public string MacFilePath = "";
        private string WinscpFilePath = "";

        private List<string> list_path_remote_or_local = new List<string>();



        public Form1()

        {
            InitializeComponent();

            richTextBox1.VScroll += RichTextBox1_VScroll;
            richTextBox2.VScroll += RichTextBox2_VScroll;

            debounceTimer1 = new System.Windows.Forms.Timer();
            debounceTimer1.Interval = DEBOUNCE_DELAY;
            debounceTimer1.Tick += DebounceTimer1_Tick;

            debounceTimer2 = new System.Windows.Forms.Timer();
            debounceTimer2.Interval = DEBOUNCE_DELAY;
            debounceTimer2.Tick += DebounceTimer2_Tick;

            undoStack1.Push("");
            undoStack2.Push("");
            comboBox1.SelectedIndex = 0;
            checkedListBoxTests.MouseDown += CheckedListBoxTests_MouseDown;
            checkedListBoxTests.MouseMove += CheckedListBoxTests_MouseMove;
            checkedListBoxTests.MouseUp += CheckedListBoxTests_MouseUp;
            checkedListBoxTests.DragOver += CheckedListBoxTests_DragOver;
            checkedListBoxTests.DragDrop += CheckedListBoxTests_DragDrop;
            checkedListBoxTests.AllowDrop = true;

            textBoxPath.Text = @"NHẬP VÀO LINK FOLDER HOẶC ZIP PATH";
            LoadFormData("config_log_collector.json");




        }

        private void DebounceTimer1_Tick(object sender, EventArgs e)
        {
            debounceTimer1.Stop();
            SaveToUndoStack(richTextBox1, undoStack1);
        }

        private void DebounceTimer2_Tick(object sender, EventArgs e)
        {
            debounceTimer2.Stop();
            SaveToUndoStack(richTextBox2, undoStack2);
        }
        private void ProcessOldFtuItems(bool migrateAllFromJson = false)
        {
            try
            {
                migrationLog.Clear();
                migrationLog.AppendLine("=== BÁO CÁO MIGRATE TỪ FTU CŨ ===");
                migrationLog.AppendLine($"Thời gian: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                migrationLog.AppendLine($"Chế độ: {(migrateAllFromJson ? "TẤT CẢ items từ JSON" : "Chỉ items từ INI")}");
                migrationLog.AppendLine();

                if (!File.Exists(jsonFilePathOldFtu))
                {
                    MessageBox.Show("Không tìm thấy file JSON của FTU cũ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string oldJsonContent = File.ReadAllText(jsonFilePathOldFtu);
                DiagTestConfig oldConfig = JsonSerializer.Deserialize<DiagTestConfig>(oldJsonContent);

                if (oldConfig == null || oldConfig.DiagTestItems == null)
                {
                    MessageBox.Show("File JSON cũ không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<DiagTestItem> filteredOldItems = new List<DiagTestItem>();

                if (migrateAllFromJson)
                {
                    filteredOldItems = oldConfig.DiagTestItems;
                    migrationLog.AppendLine($"Tổng số items trong JSON cũ: {filteredOldItems.Count}");
                }
                else
                {
                    if (!File.Exists(iniFilePathOldFtu))
                    {
                        MessageBox.Show("Không tìm thấy file INI của FTU cũ!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string oldIniContent = File.ReadAllText(iniFilePathOldFtu);
                    string[] oldCheckedIds = ExtractItemsId(oldIniContent);

                    if (oldCheckedIds.Length == 0)
                    {
                        MessageBox.Show("File INI cũ không có item nào được chọn!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    HashSet<string> oldCheckedIdSet = new HashSet<string>(oldCheckedIds);

                    foreach (var item in oldConfig.DiagTestItems)
                    {
                        if (oldCheckedIdSet.Contains(item.ID.ToString()))
                        {
                            filteredOldItems.Add(item);
                        }
                    }

                    migrationLog.AppendLine($"Tổng số items trong INI cũ: {oldCheckedIds.Length}");
                    migrationLog.AppendLine($"Danh sách ID từ INI: {string.Join(", ", oldCheckedIds)}");
                }

                migrationLog.AppendLine($"Items sẽ migrate (theo thứ tự JSON cũ): {filteredOldItems.Count}");
                foreach (var item in filteredOldItems)
                {
                    migrationLog.AppendLine($"  - ID: {item.ID}, Name: {item.Name}");
                }
                migrationLog.AppendLine();

                if (diagTestItems == null || diagTestItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng load JSON mới trước khi migrate!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Dictionary<int, int> newJsonIdToIndex = new Dictionary<int, int>();
                for (int i = 0; i < diagTestItems.Count; i++)
                {
                    newJsonIdToIndex[diagTestItems[i].ID] = i;
                }

                List<MigrateItem> itemsToMigrate = new List<MigrateItem>();
                List<DiagTestItem> itemsNotFound = new List<DiagTestItem>();

                foreach (var oldItem in filteredOldItems)
                {
                    if (newJsonIdToIndex.ContainsKey(oldItem.ID))
                    {
                        itemsToMigrate.Add(new MigrateItem
                        {
                            OldItem = oldItem,
                            NewIndex = newJsonIdToIndex[oldItem.ID]
                        });
                    }
                    else
                    {
                        itemsNotFound.Add(oldItem);
                    }
                }

                migrationLog.AppendLine("=== KẾT QUẢ KIỂM TRA ===");
                migrationLog.AppendLine($"✓ Tìm thấy trong FTU mới: {itemsToMigrate.Count} items");
                foreach (var item in itemsToMigrate)
                {
                    migrationLog.AppendLine($"  ✓ ID: {item.OldItem.ID}, Name: {item.OldItem.Name}");
                }
                migrationLog.AppendLine();

                if (itemsNotFound.Count > 0)
                {
                    migrationLog.AppendLine($"✗ KHÔNG tìm thấy trong FTU mới: {itemsNotFound.Count} items");
                    foreach (var item in itemsNotFound)
                    {
                        migrationLog.AppendLine($"  ✗ ID: {item.ID}, Name: {item.Name}");
                    }
                    migrationLog.AppendLine();
                }

                if (itemsToMigrate.Count > 0)
                {
                    MigrateItemsToTop(itemsToMigrate);
                    migrationLog.AppendLine("=== ĐÃ HOÀN TẤT MIGRATE ===");
                    migrationLog.AppendLine($"Đã xếp {itemsToMigrate.Count} items lên đầu danh sách và đánh dấu checked.");
                }

                ShowMigrationLog();
                UpdateTextBoxFromCheckedItems();

                string summaryMessage = $"Migrate hoàn tất!\n\n" +
                                        $"✓ Thành công: {itemsToMigrate.Count} items\n" +
                                        $"✗ Không tìm thấy: {itemsNotFound.Count} items\n\n" +
                                        $"Xem chi tiết trong log.";

                MessageBox.Show(summaryMessage, "Kết quả Migrate",
                    MessageBoxButtons.OK,
                    itemsNotFound.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý migrate: {ex.Message}\n\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MigrateItemsToTop(List<MigrateItem> itemsToMigrate)
        {
            if (itemsToMigrate == null || itemsToMigrate.Count == 0)
                return;

            checkedListBoxTests.BeginUpdate();

            for (int i = 0; i < checkedListBoxTests.Items.Count; i++)
            {
                checkedListBoxTests.SetItemChecked(i, false);
            }

            List<DiagTestItem> newDiagTestItems = new List<DiagTestItem>();
            HashSet<int> migratedIds = new HashSet<int>();

            foreach (var migrateItem in itemsToMigrate)
            {
                newDiagTestItems.Add(migrateItem.OldItem);
                migratedIds.Add(migrateItem.OldItem.ID);
            }

            foreach (var item in diagTestItems)
            {
                if (!migratedIds.Contains(item.ID))
                {
                    newDiagTestItems.Add(item);
                }
            }

            diagTestItems.Clear();
            diagTestItems.AddRange(newDiagTestItems);

            checkedListBoxTests.Items.Clear();
            foreach (var item in diagTestItems)
            {
                string displayText = $"{item.ID} - {item.Name}";
                checkedListBoxTests.Items.Add(displayText);
            }

            for (int i = 0; i < itemsToMigrate.Count; i++)
            {
                checkedListBoxTests.SetItemChecked(i, true);
            }

            checkedListBoxTests.EndUpdate();

            if (checkedListBoxTests.Items.Count > 0)
            {
                checkedListBoxTests.TopIndex = 0;
            }
        }
        private Process ftuExeProcess;

        private void OpenFtuExeFile()
        {
            try
            {
                if (string.IsNullOrEmpty(selectedFolderPath))
                {
                    MessageBox.Show("Không tìm thấy đường dẫn FTU!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] exeFiles = Directory.GetFiles(selectedFolderPath, "FTU_*.exe", SearchOption.AllDirectories);

                if (exeFiles.Length == 0)
                {
                    MessageBox.Show($"Không tìm thấy file FTU_*.exe trong folder:\n{selectedFolderPath}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string exeFilePath = exeFiles[0];
                if (exeFiles.Length > 1)
                {
                    var rootExe = exeFiles.FirstOrDefault(f =>
                        Path.GetDirectoryName(f) == selectedFolderPath);

                    if (!string.IsNullOrEmpty(rootExe))
                    {
                        exeFilePath = rootExe;
                    }

                    MessageBox.Show($"Tìm thấy {exeFiles.Length} file FTU_*.exe.\nSẽ chạy: {exeFilePath.Replace(selectedFolderPath, ".")}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Debug.WriteLine($"Running FTU exe: {exeFilePath}");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = exeFilePath,
                    Arguments = "-p=UniFiDream --mvc",
                    WorkingDirectory = Path.GetDirectoryName(exeFilePath),
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                ftuExeProcess = Process.Start(startInfo);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chạy FTU_*.exe:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void KillFtuExe()
        {
            try
            {
                if (ftuExeProcess != null && !ftuExeProcess.HasExited)
                {
                    ftuExeProcess.Kill();
                    ftuExeProcess.Dispose();
                    ftuExeProcess = null;
                    MessageBox.Show("Đã kill FTU exe thành công!");
                }
                else
                {
                    MessageBox.Show("Không có FTU exe nào đang chạy.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kill FTU exe: {ex.Message}");
            }
        }




        private class ItemMoveInfo
        {
            public DiagTestItem Item { get; set; }
            public int CurrentIndex { get; set; }
            public int TargetIndex { get; set; }
        }
        private void ShowMigrationLog()
        {
            Form logForm = new Form();
            logForm.Text = "Migration Log";
            logForm.Size = new Size(800, 600);
            logForm.StartPosition = FormStartPosition.CenterParent;

            RichTextBox logTextBox = new RichTextBox();
            logTextBox.Dock = DockStyle.Fill;
            logTextBox.Font = new Font("Consolas", 10);
            logTextBox.ReadOnly = true;
            logTextBox.Text = migrationLog.ToString();

            HighlightLogText(logTextBox);

            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Dock = DockStyle.Bottom;
            btnClose.Height = 40;
            btnClose.Click += (s, e) => logForm.Close();

            Button btnSaveLog = new Button();
            btnSaveLog.Text = "Lưu Log";
            btnSaveLog.Dock = DockStyle.Bottom;
            btnSaveLog.Height = 40;
            btnSaveLog.Click += (s, e) => SaveLogToFile();

            logForm.Controls.Add(logTextBox);
            logForm.Controls.Add(btnSaveLog);
            logForm.Controls.Add(btnClose);

            logForm.ShowDialog();
        }

        private void HighlightLogText(RichTextBox rtb)
        {
            string text = rtb.Text;

            int index = 0;
            while ((index = text.IndexOf("✓", index)) != -1)
            {
                rtb.Select(index, 1);
                rtb.SelectionColor = Color.Green;
                index++;
            }

            index = 0;
            while ((index = text.IndexOf("✗", index)) != -1)
            {
                rtb.Select(index, 1);
                rtb.SelectionColor = Color.Red;
                index++;
            }

            string[] headers = { "===", "KẾT QUẢ", "BÁO CÁO", "HOÀN TẤT" };
            foreach (string header in headers)
            {
                index = 0;
                while ((index = text.IndexOf(header, index)) != -1)
                {
                    int lineStart = text.LastIndexOf('\n', index) + 1;
                    int lineEnd = text.IndexOf('\n', index);
                    if (lineEnd == -1) lineEnd = text.Length;

                    rtb.Select(lineStart, lineEnd - lineStart);
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                    rtb.SelectionColor = Color.DarkBlue;
                    index = lineEnd;
                }
            }

            rtb.Select(0, 0);
        }

        private void SaveLogToFile()
        {
            try
            {
                string logFileName = $"migration_log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string logPath = Path.Combine(
                    string.IsNullOrEmpty(selectedFolderPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : selectedFolderPath,
                    logFileName
                );

                File.WriteAllText(logPath, migrationLog.ToString(), Encoding.UTF8);

                MessageBox.Show($"Đã lưu log tại:\n{logPath}", "Lưu thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu log: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class MigrateItem
        {
            public DiagTestItem OldItem { get; set; }
            public int NewIndex { get; set; }
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

        private void button5_Click(object sender, EventArgs e)
        {
            debounceTimer2.Stop();
            SaveToUndoStack(richTextBox2, undoStack2);
            PerformUndo(richTextBox2, undoStack2);
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void buttonRunPS_Click(object sender, EventArgs e)
        {
            string filePath = textBoxPath.Text.Trim();


            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Vui lòng nhập đủ Path, FTU, FCD", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string mainPs1 = Path.Combine(appDir,"loc-log-ps1", "main.ps1");

            if (!File.Exists(mainPs1))
            {
                MessageBox.Show($"Không tìm thấy main.ps1 tại:\n{mainPs1}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoExit -ExecutionPolicy Bypass -File \"{mainPs1}\" \"{filePath}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                Process.Start(psi);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chạy script:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        public string[] ExtractItemsId(string input)
        {
            string pattern = @"\[ITEMS\]\r?\n\s*id\s*=\s*(?<numbers>[\d,\s]+)";
            Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string numbersString = match.Groups["numbers"].Value;

                string[] numbersArray = numbersString
    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .Where(s => !string.IsNullOrEmpty(s))
    .ToArray();

                return numbersArray;
            }
            else
            {
                return Array.Empty<string>();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn folder chứa file JSON và INI";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                    Debug.WriteLine("Selected folder: " + selectedFolderPath);

                    if (FindRequiredFiles())
                    {
                        if (LoadJsonFile())
                        {
                            LoadIniFileAndDisplay();


                        }
                    }
                }
            }
        }
        private bool FindRequiredFiles()
        {
            try
            {
                string[] jsonFiles = Directory.GetFiles(selectedFolderPath, "*_reorder.json", SearchOption.AllDirectories);

                if (jsonFiles.Length == 0)
                {
                    MessageBox.Show("Không tìm thấy file *_reorder.json trong folder và các subfolder!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (jsonFiles.Length > 1)
                {
                    string fileList = string.Join("\n", jsonFiles.Select(f => f.Replace(selectedFolderPath, ".")));
                    MessageBox.Show($"Tìm thấy {jsonFiles.Length} file *_reorder.json:\n{fileList}\n\nSẽ sử dụng file: {jsonFiles[0].Replace(selectedFolderPath, ".")}",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                jsonFilePath = jsonFiles[0];
                Debug.WriteLine("Found JSON: " + jsonFilePath);

                string[] iniFiles = Directory.GetFiles(selectedFolderPath, "selected_items.ini", SearchOption.AllDirectories);

                if (iniFiles.Length == 0)
                {
                    string jsonDirectory = Path.GetDirectoryName(jsonFilePath);
                    iniFilePath = Path.Combine(jsonDirectory, "selected_items.ini");

                    MessageBox.Show($"Không tìm thấy selected_items.ini.\nSẽ tạo file mới tại:\n{iniFilePath.Replace(selectedFolderPath, ".")}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (iniFiles.Length > 1)
                    {
                        string jsonDirectory = Path.GetDirectoryName(jsonFilePath);
                        string sameDirectoryIni = iniFiles.FirstOrDefault(f => Path.GetDirectoryName(f) == jsonDirectory);

                        if (!string.IsNullOrEmpty(sameDirectoryIni))
                        {
                            iniFilePath = sameDirectoryIni;
                            MessageBox.Show($"Tìm thấy {iniFiles.Length} file selected_items.ini.\nSử dụng file cùng folder với JSON:\n{iniFilePath.Replace(selectedFolderPath, ".")}",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            iniFilePath = iniFiles[0];
                            string fileList = string.Join("\n", iniFiles.Select(f => f.Replace(selectedFolderPath, ".")));
                            MessageBox.Show($"Tìm thấy {iniFiles.Length} file selected_items.ini:\n{fileList}\n\nSử dụng file: {iniFilePath.Replace(selectedFolderPath, ".")}",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        iniFilePath = iniFiles[0];
                        Debug.WriteLine("Found INI: " + iniFilePath);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm files: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool FindJsonOldFTU()
        {
            try
            {

                if (string.IsNullOrEmpty(selectedFolderPathOldFtu))
                {
                    MessageBox.Show("Chưa chọn folder!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                string[] jsonFiles = Directory.GetFiles(selectedFolderPathOldFtu, "*_reorder.json", SearchOption.AllDirectories);

                if (jsonFiles.Length == 0)
                {
                    MessageBox.Show("Không tìm thấy file *_reorder.json trong folder và các subfolder!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (jsonFiles.Length > 1)
                {

                    string fileList = string.Join("\n", jsonFiles.Select(f => f.Replace(selectedFolderPathOldFtu, ".")));
                    MessageBox.Show($"Tìm thấy {jsonFiles.Length} file *_reorder.json:\n{fileList}\n\nSẽ sử dụng file: {jsonFiles[0].Replace(selectedFolderPathOldFtu, ".")}",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                jsonFilePathOldFtu = jsonFiles[0];
                Debug.WriteLine("Found JSON: " + jsonFilePathOldFtu);

                string[] iniFiles = Directory.GetFiles(selectedFolderPathOldFtu, "selected_items.ini", SearchOption.AllDirectories);

                if (iniFiles.Length == 0)
                {
                    MessageBox.Show($"Không tìm thấy selected_items.ini",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (iniFiles.Length > 1)
                    {
                        string jsonDirectory = Path.GetDirectoryName(jsonFilePathOldFtu);
                        string sameDirectoryIni = iniFiles.FirstOrDefault(f => Path.GetDirectoryName(f) == jsonDirectory);

                        if (!string.IsNullOrEmpty(sameDirectoryIni))
                        {
                            iniFilePathOldFtu = sameDirectoryIni;
                            MessageBox.Show($"Tìm thấy {iniFiles.Length} file selected_items.ini.\nSử dụng file cùng folder với JSON:\n{iniFilePathOldFtu.Replace(selectedFolderPathOldFtu, ".")}",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            iniFilePathOldFtu = iniFiles[0];
                            string fileList = string.Join("\n", iniFiles.Select(f => f.Replace(selectedFolderPathOldFtu, ".")));
                            MessageBox.Show($"Tìm thấy {iniFiles.Length} file selected_items.ini:\n{fileList}\n\nSử dụng file: {iniFilePathOldFtu.Replace(selectedFolderPathOldFtu, ".")}",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        iniFilePathOldFtu = iniFiles[0];
                        Debug.WriteLine("Found INI: " + iniFilePathOldFtu);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm files: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool LoadJsonFile()
        {
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                DiagTestConfig config = JsonSerializer.Deserialize<DiagTestConfig>(jsonContent);

                if (config == null || config.DiagTestItems == null || config.DiagTestItems.Count == 0)
                {
                    MessageBox.Show("File JSON không hợp lệ hoặc không có dữ liệu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                diagTestItems = config.DiagTestItems;
                Debug.WriteLine($"Loaded {diagTestItems.Count} items from JSON");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file JSON: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void LoadIniFileAndDisplay()
        {
            try
            {
                if (File.Exists(iniFilePath))
                {
                    originalIniFileContent = File.ReadAllText(iniFilePath);
                    currentCheckedItemIds = ExtractItemsId(originalIniFileContent);

                    if (currentCheckedItemIds.Length > 0)
                    {
                        string resultMessage = string.Join(", ", currentCheckedItemIds);
                        Debug.WriteLine("Checked IDs from INI: " + resultMessage);
                    }
                    else
                    {
                        Debug.WriteLine("No checked items found in INI");
                    }
                }
                else
                {
                    originalIniFileContent = "[ITEMS]\nid = ";
                    currentCheckedItemIds = new string[0];
                }

                PopulateCheckedListBoxFromIni();

                MessageBox.Show($"Đã load thành công!\n\nJSON: {Path.GetFileName(jsonFilePath)}\nINI: {Path.GetFileName(iniFilePath)}\n\nTổng items: {diagTestItems.Count}\nItems được check: {currentCheckedItemIds.Length}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file INI: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateCheckedListBoxFromIni()
        {
            checkedListBoxTests.Items.Clear();

            HashSet<string> checkedIdSet = new HashSet<string>(currentCheckedItemIds);

            foreach (var item in diagTestItems)
            {
                string displayText = $"{item.ID} - {item.Name}";

                bool isChecked = checkedIdSet.Contains(item.ID.ToString());

                checkedListBoxTests.Items.Add(displayText, isChecked);
            }

            Debug.WriteLine($"Populated {checkedListBoxTests.Items.Count} items to CheckedListBox");
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public static string ReplaceItemsId(string fileContent, string newIdList)
        {

            string pattern = @"(\[ITEMS\]\r?\n\s*id\s*=\s*)[\d,\s]*";
            string replacement = $"${{1}}{newIdList}";
            return Regex.Replace(fileContent, pattern, replacement);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(iniFilePath))
            {
                MessageBox.Show("Vui lòng chọn folder trước khi lưu!", "Lỗi Lưu File",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newIdList = textBox1.Text.Trim();
            newIdList = NormalizeIdList(newIdList);

            try
            {
                if (string.IsNullOrEmpty(originalIniFileContent))
                {
                    originalIniFileContent = "[ITEMS]\nid = ";
                }

                string finalContent = ReplaceItemsId(originalIniFileContent, newIdList);
                finalContent = finalContent.Replace("\uFEFF", string.Empty);
                DialogResult openFtuResult = MessageBox.Show(
"Ngài có chắc sẽ làm điều này không ?",
"Save",
MessageBoxButtons.YesNo,
MessageBoxIcon.Question
);
                File.WriteAllText(iniFilePath, finalContent, new UTF8Encoding(false));

                originalIniFileContent = finalContent;
                currentCheckedItemIds = ExtractItemsId(finalContent);

                if (!string.IsNullOrEmpty(jsonFilePath))
                {
                    SaveJsonWithNewOrder();
                }

                MessageBox.Show($"Đã lưu thành công!\n\nINI: {Path.GetFileName(iniFilePath)}\nJSON: {Path.GetFileName(jsonFilePath)}\n\nItems được check: {currentCheckedItemIds.Length}",
                    "Lưu Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult openFtuResult1 = MessageBox.Show(
"Bạn có muốn mở FTU để kiểm tra lại không?",
"Mở FTU",
MessageBoxButtons.YesNo,
MessageBoxIcon.Question
);

                if (openFtuResult1 == DialogResult.Yes)
                {
                    OpenFtuExeFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi ghi file: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveJsonWithNewOrder()
        {
            try
            {

                var newConfig = new DiagTestConfig
                {
                    DiagTestItems = new List<DiagTestItem>(diagTestItems)
                };


                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string jsonContent = JsonSerializer.Serialize(newConfig, options);
                jsonContent = jsonContent.Replace("\uFEFF", string.Empty);


                File.WriteAllText(jsonFilePath, jsonContent, new UTF8Encoding(false));

                Debug.WriteLine($"Saved JSON with {diagTestItems.Count} items to: {jsonFilePath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu JSON: {ex.Message}", ex);
            }
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTests.Items.Count; i++)
            {
                checkedListBoxTests.SetItemChecked(i, true);
            }
            UpdateTextBoxFromCheckedItems();
        }

        private void btnDisSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTests.Items.Count; i++)
            {
                checkedListBoxTests.SetItemChecked(i, false);
            }
            UpdateTextBoxFromCheckedItems();
        }
        private void CheckedListBoxTests_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() => UpdateTextBoxFromCheckedItems()));
        }
        private void UpdateTextBoxFromCheckedItems()
        {
            List<string> checkedIds = new List<string>();

            for (int i = 0; i < checkedListBoxTests.Items.Count; i++)
            {
                if (checkedListBoxTests.GetItemChecked(i))
                {
                    checkedIds.Add(diagTestItems[i].ID.ToString());
                }
            }

            textBox1.Text = string.Join(", ", checkedIds);
        }

        private string NormalizeIdList(string idList)
        {
            if (string.IsNullOrWhiteSpace(idList))
                return string.Empty;

            var ids = idList.Split(',')
                            .Select(id => id.Trim())
                            .Where(id => !string.IsNullOrEmpty(id));

            return string.Join(",", ids);
        }


        private void CheckedListBoxTests_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkedListBoxTests.Items.Count == 0)
                return;

            dragIndex = checkedListBoxTests.IndexFromPoint(e.X, e.Y);

            if (dragIndex != ListBox.NoMatches)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(
                    new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)),
                    dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }
        }
        private void CheckedListBoxTests_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Point point = checkedListBoxTests.PointToClient(new Point(e.X, e.Y));
            int currentIndex = checkedListBoxTests.IndexFromPoint(point);

            if (currentIndex != ListBox.NoMatches && currentIndex != dragIndex)
            {
                if (lastHighlightedIndex != currentIndex)
                {
                    checkedListBoxTests.SelectedIndex = currentIndex;
                    lastHighlightedIndex = currentIndex;
                }
            }
        }

        private void CheckedListBoxTests_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty &&
    !dragBoxFromMouseDown.Contains(e.X, e.Y) &&
    dragIndex != -1)
                {
                    draggedItem = checkedListBoxTests.Items[dragIndex];
                    isDraggedItemChecked = checkedListBoxTests.GetItemChecked(dragIndex);

                    checkedListBoxTests.SelectedIndex = dragIndex;

                    checkedListBoxTests.DoDragDrop(draggedItem, DragDropEffects.Move);

                    dragBoxFromMouseDown = Rectangle.Empty;
                }
            }
        }
        private void CheckedListBoxTests_MouseUp(object sender, MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;
        }
        private void CheckedListBoxTests_DragDrop(object sender, DragEventArgs e)
        {
            Point point = checkedListBoxTests.PointToClient(new Point(e.X, e.Y));
            int dropIndex = checkedListBoxTests.IndexFromPoint(point);

            if (dropIndex == ListBox.NoMatches)
                dropIndex = checkedListBoxTests.Items.Count - 1;

            if (dragIndex != -1 && dropIndex != -1 && dragIndex != dropIndex)
            {
                checkedListBoxTests.BeginUpdate();

                var tempItem = diagTestItems[dragIndex];
                diagTestItems.RemoveAt(dragIndex);
                diagTestItems.Insert(dropIndex, tempItem);

                checkedListBoxTests.Items.RemoveAt(dragIndex);
                checkedListBoxTests.Items.Insert(dropIndex, draggedItem);
                checkedListBoxTests.SetItemChecked(dropIndex, isDraggedItemChecked);

                checkedListBoxTests.SelectedIndex = dropIndex;

                checkedListBoxTests.EndUpdate();

                UpdateTextBoxFromCheckedItems();
            }

            dragIndex = -1;
            lastHighlightedIndex = -1;
            dragBoxFromMouseDown = Rectangle.Empty;
        }
        public class DiagTestItem
        {
            public string File { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
            public int Check { get; set; }
            public int EN { get; set; }
        }

        public class DiagTestConfig
        {
            public List<DiagTestItem> DiagTestItems { get; set; }
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
            folderBrowserDialog1.Description = "Chọn folder chứa file JSON và INI";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFolderPathOldFtu = folderBrowserDialog1.SelectedPath; Debug.WriteLine("Selected folder: " + selectedFolderPathOldFtu);

                if (FindJsonOldFTU())
                {
                    textBox2.Text = selectedFolderPathOldFtu;
                }
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnMigrateFromOldFtu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jsonFilePath) || diagTestItems == null || diagTestItems.Count == 0)
            {
                MessageBox.Show("Vui lòng load FTU mới trước (button6)!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(jsonFilePathOldFtu) || string.IsNullOrEmpty(iniFilePathOldFtu))
            {
                MessageBox.Show("Vui lòng chọn FTU cũ trước (btnFTUcu)!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
    "Bạn có chắc muốn migrate items từ FTU cũ?\n\n" +
    $"FTU cũ: {Path.GetFileName(jsonFilePathOldFtu)}\n" +
    $"FTU mới: {Path.GetFileName(jsonFilePath)}",
    "Xác nhận Migrate",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question
);

            if (result == DialogResult.Yes)
            {
                ProcessOldFtuItems();
            }
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
        public void SaveFormData(string filePath)
        {
            // Kiểm tra PortNumber
            if (!int.TryParse(TB_portNumber.Text, out int port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Port number phải nằm trong khoảng 1–65535!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra MaxThread
            if (!int.TryParse(TB_maxThread.Text, out int maxThread) || maxThread < 1 || maxThread > 1000)
            {
                MessageBox.Show("Max thread scan phải nằm trong khoảng 1–1000!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var config = new LogCollectorConfig
                {
                    Host = TB_host.Text,
                    User = TB_user.Text,
                    Password = TB_password.Text,
                    Protocol = CBB_protocol.SelectedItem?.ToString(),
                    PortNumber = TB_portNumber.Text,
                    LocalDownloadDestination = TB_localDestinationDownload.Text,
                    WinscpDLL = TB_winscpDLL.Text,
                    RemoteFolderScan = TB_severScan.Text,
                    MaxThreadScan = TB_maxThread.Text,
                    MacFilePath = TB_MacFilePath.Text,
                    ScanLocalMode = (CB_LocalScan.Checked)

                };

                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);

                // Thông báo thành công
                MessageBox.Show("Đã lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Thông báo thất bại
                MessageBox.Show($"Lưu cấu hình thất bại!\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadFormData(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<LogCollectorConfig>(json);

            if (config != null)
            {
                TB_host.Text = config.Host;
                TB_user.Text = config.User;
                TB_password.Text = config.Password;
                CBB_protocol.SelectedItem = config.Protocol;
                TB_portNumber.Text = config.PortNumber;
                TB_localDestinationDownload.Text = config.LocalDownloadDestination;
                TB_winscpDLL.Text = config.WinscpDLL;
                TB_severScan.Text = config.RemoteFolderScan;
                TB_maxThread.Text = config.MaxThreadScan;
                TB_MacFilePath.Text = config.MacFilePath;
                CB_LocalScan.Checked = config.ScanLocalMode;
            }
        }

        private void BTN_saveFormInfo_Click(object sender, EventArgs e)
        {
            SaveFormData("config_log_collector.json");
        }

        private void BTN_localFolderDownloadLog_Click(object sender, EventArgs e)
        {

            if (FBD_localDesDownLoad == null)
                FBD_localDesDownLoad = new FolderBrowserDialog();


            DialogResult result = FBD_localDesDownLoad.ShowDialog();


            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(FBD_localDesDownLoad.SelectedPath))
            {

                TB_localDestinationDownload.Text = FBD_localDesDownLoad.SelectedPath;


                LocalDownLoadLogPath = FBD_localDesDownLoad.SelectedPath;
                Debug.WriteLine(LocalDownLoadLogPath);
            }
        }

        private void BTN_winscpDll_file_Click(object sender, EventArgs e)
        {
            if (OFD_winscpDLL_File == null)
                OFD_winscpDLL_File = new OpenFileDialog();

            OFD_winscpDLL_File.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";
            OFD_winscpDLL_File.Title = "Chọn WinscpDLL file";

            DialogResult result = OFD_winscpDLL_File.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OFD_winscpDLL_File.FileName))
            {
                TB_winscpDLL.Text = OFD_winscpDLL_File.FileName;

                WinscpFilePath = OFD_winscpDLL_File.FileName;
                Debug.WriteLine(WinscpFilePath);
            }
        }
        private void TB_portNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím số và phím điều khiển (Backspace, Delete...)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }

        private void TB_maxThread_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_showPass.Checked)
            {
                // Hiện mật khẩu
                TB_password.UseSystemPasswordChar = false;
            }
            else
            {
                // Ẩn mật khẩu
                TB_password.UseSystemPasswordChar = true;
            }
        }

        private void BTN_startScanLog_Click(object sender, EventArgs e)
        {
            foreach (var item in list_path_remote_or_local)
            {
                if (CB_LocalScan.Checked)
                {
                    // Scan local mode
                    string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_collection_ps1", "scan-local-new-algorithm.ps1");

                    if (!File.Exists(scriptPath))
                    {
                        MessageBox.Show("Không tìm thấy script scan-local-new-algorithm.ps1!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Các tham số truyền vào
                    string sourceFolder = item;
                    string destinationFolder = TB_localDestinationDownload.Text;
                    string macFilePath = TB_MacFilePath.Text;
                    int maxScanThreads = int.TryParse(TB_maxThread.Text, out int tmp) ? tmp : 1;

                    string arguments = $"-NoExit -ExecutionPolicy Bypass -File \"{scriptPath}\" " +
                                       $"-SourceFolder \"{sourceFolder}\" " +
                                       $"-DestinationFolder \"{destinationFolder}\" " +
                                       $"-MacFilePath \"{macFilePath}\" " +
                                       $"-MaxScanThreads {maxScanThreads}";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = arguments,
                        UseShellExecute = true,
                        CreateNoWindow = false,
                        RedirectStandardError = false,
                        RedirectStandardInput = false
                    };

                    SaveFormData("config_log_collector.json");
                    Process.Start(psi);
                }
                else
                {
                    string appDir = AppDomain.CurrentDomain.BaseDirectory;
                    string scriptPath = Path.Combine(appDir, "log_collection_ps1", "hash-set-new-algorithm.ps1");

                    if (!File.Exists(scriptPath))
                    {
                        MessageBox.Show($"Không tìm thấy hash-set.ps1 tại:\n{scriptPath}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        // Lấy dữ liệu từ form
                        string scpHost = TB_host.Text;
                        string port = TB_portNumber.Text;
                        string scpUser = TB_user.Text;
                        string scpPassword = TB_password.Text;
                        string protocol = CBB_protocol.SelectedItem?.ToString() ?? "";
                        string remoteFolder = TB_severScan.Text;
                        string localDestination = TB_localDestinationDownload.Text;
                        string winscpDllPath = TB_winscpDLL.Text;
                        string macFilePath = TB_MacFilePath.Text;
                        int maxScanThreads = int.TryParse(TB_maxThread.Text, out int tmp) ? tmp : 1;

                        // Chuẩn bị tham số truyền cho PowerShell
                        string arguments =
                            $"-NoExit -ExecutionPolicy Bypass -File \"{scriptPath}\" " +
                            $"-ScpHost \"{scpHost}\" " +
                            $"-Port \"{port}\" " +
                            $"-ScpUser \"{scpUser}\" " +
                            $"-ScpPassword \"{scpPassword}\" " +
                            $"-Protocol \"{protocol}\" " +
                            $"-RemoteFolder \"{remoteFolder}\" " +
                            $"-LocalDestination \"{localDestination}\" " +
                            $"-winscpDllPath \"{winscpDllPath}\" " +
                            $"-MacFilePath \"{macFilePath}\" " +
                            $"-MaxScanThreads {maxScanThreads}";

                        var psi = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = arguments,
                            UseShellExecute = true,
                            CreateNoWindow = false
                        };
                        SaveFormData("config_log_collector.json");
                        Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể chạy script!\nChi tiết: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }



        }

        private void BTN_macFilePath_Click(object sender, EventArgs e)
        {
            OFD_macFilePath.Title = "Chọn file MAC";
            OFD_macFilePath.Filter = "Tất cả các file (*.*)|*.*";

            // Nếu người dùng chọn OK
            if (OFD_macFilePath.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn
                string selectedPath = OFD_macFilePath.FileName;

                // Gán vào TextBox
                TB_MacFilePath.Text = selectedPath;

                // Lưu vào property trong class
                MacFilePath = selectedPath;
                Debug.WriteLine(MacFilePath);
            }
        }

        private void CB_LocalScan_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_LocalScan.Checked)
            {
                label11.Text = "Local folder scan";
            }
            else
            {
                label11.Text = "Remote folder scan";
            }
        }

        private void TB_severScan_Click(object sender, EventArgs e)
        {
            List<string> currentPaths = TB_severScan.Text
        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(p => p.Trim())
        .ToList();

            Form2 f2 = new Form2(currentPaths);
            f2.PathsSaved += F2_PathsSaved;
            f2.ShowDialog();
        }
        private void F2_PathsSaved(List<string> paths)
        {
            this.list_path_remote_or_local = paths;
            TB_severScan.Text = string.Join(";", paths);
            Debug.WriteLine(TB_severScan.Text);
        }

        private void TB_severScan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

