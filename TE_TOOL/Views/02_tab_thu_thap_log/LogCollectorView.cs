using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TE_TOOL.CONFIG;
using TE_TOOL.Models;

namespace TE_TOOL.Views._02_tab_thu_thap_log
{
    public partial class LogCollectorView : UserControl
    {
        public string MacFilePath = "";
        public string LocalDownLoadLogPath = "";
        private List<string> list_path_remote_or_local = new List<string>();
        private string WinscpFilePath = "";



        public LogCollectorView()
        {
            InitializeComponent();
            LoadFormData(CONFIG_CONSTANT.CONFIG_LOG_COLLECTOR);
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
                    RemoteFolderScan = TB_severScan.Text.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList(),
                    MaxThreadScan = TB_maxThread.Text,
                    MacFilePath = TB_MacFilePath.Text,
                    ScanLocalMode = (CB_LocalScan.Checked)

                };

                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);


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
                TB_severScan.Text = string.Join(";", config.RemoteFolderScan);
                TB_maxThread.Text = config.MaxThreadScan;
                TB_MacFilePath.Text = config.MacFilePath;
                CB_LocalScan.Checked = config.ScanLocalMode;
                List<string> cleaned = config.RemoteFolderScan
                .Select(p => p.EndsWith("\\") ? p.Substring(0, p.Length - 1) : p)
                .ToList();
                this.list_path_remote_or_local = cleaned;

            }
        }
        private void BTN_saveFormInfo_Click(object sender, EventArgs e)
        {
            SaveFormData(CONFIG_CONSTANT.CONFIG_LOG_COLLECTOR);
            // Thông báo thành công
            MessageBox.Show("Đã lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Debug.WriteLine("BTN_startScanLog_Click invoked");

            // Nếu list rỗng nhưng textbox có giá trị thì populate tạm từ TB_severScan
            if ((list_path_remote_or_local == null || list_path_remote_or_local.Count == 0)
                && !string.IsNullOrWhiteSpace(TB_severScan.Text))
            {
                list_path_remote_or_local = TB_severScan.Text
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim().EndsWith("\\") ? p.Trim().TrimEnd('\\') : p.Trim())
                    .ToList();
            }

            // Nếu vẫn rỗng -> thông báo (để kiểm tra button có được trigger hay không)
            if (list_path_remote_or_local == null || list_path_remote_or_local.Count == 0)
            {
                MessageBox.Show("Chưa có đường dẫn nguồn (Remote/Local). Vui lòng cấu hình 'Remote folder scan' trước khi chạy.", "Thiếu cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var item in list_path_remote_or_local)
            {


                if (CB_LocalScan.Checked)
                {
                    // Scan local mode
                    string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_collection_ps1", "scan-local.ps1");

                    if (!File.Exists(scriptPath))
                    {
                        MessageBox.Show("Không tìm thấy script scan-local.ps1!", "Lỗi",
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
                        RedirectStandardInput = false,
                        WorkingDirectory = Path.Combine(scriptPath, "log_collection_ps1")
                    };

                    SaveFormData(CONFIG_CONSTANT.CONFIG_LOG_COLLECTOR);
                    Process.Start(psi);
                }
                else
                {
                    string appDir = AppDomain.CurrentDomain.BaseDirectory;
                    string scriptPath = Path.Combine(appDir, "log_collection_ps1", "hash-set.ps1");

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
                        string remoteFolder = item;
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
                            CreateNoWindow = false,
                            WorkingDirectory = Path.Combine(scriptPath, "log_collection_ps1")
                        };
                        SaveFormData(CONFIG_CONSTANT.CONFIG_LOG_COLLECTOR);
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
            List<string> currentPaths = TB_severScan.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();


            Form2 f2 = new Form2(currentPaths);
            f2.PathsSaved += F2_PathsSaved;
            f2.ShowDialog();
        }
        private void F2_PathsSaved(List<string> paths)
        {
            var cleaned = paths
            .Select(p => p.EndsWith("\\") ? p.Substring(0, p.Length - 1) : p)
            .ToList();
            this.list_path_remote_or_local = cleaned;
            TB_severScan.Text = string.Join(";", paths);
            Debug.WriteLine(TB_severScan.Text);
        }

        private void FBD_localDesDownLoad_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
