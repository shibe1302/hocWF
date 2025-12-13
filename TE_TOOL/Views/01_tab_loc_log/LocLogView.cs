using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TE_TOOL.Services;

namespace TE_TOOL.Views
{
    public partial class LocLogView : UserControl
    {
        private readonly LocLogService _service;
        public LocLogView()
        {
            InitializeComponent();
            _service = new LocLogService();
            SetupDragDrop();
        }

        private void SetupDragDrop()
        {
            this.AllowDrop = true;
            this.DragEnter += OnDragEnter;
            this.DragDrop += OnDragDrop;
        }
        private void OnDragEnter(object sender, DragEventArgs e)
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

        /// <summary>
        /// Xử lý khi thả file
        /// </summary>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length > 0)
                {
                    string filePath = files[0];
                    textBoxPath.Text = filePath;

                    // Hiển thị tên file
                    string displayName = _service.GetDisplayName(filePath);
                    labelStatus.Text = $"Đã chọn: {displayName}";
                    labelStatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi đọc file: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý khi click nút Run
        /// </summary>
        private void buttonRunPS_Click(object sender, EventArgs e)
        {
            string filePath = textBoxPath.Text.Trim();

            // Validate input
            if (!_service.ValidateFilePath(filePath))
            {
                ShowWarning("Vui lòng nhập đường dẫn hợp lệ!\n\nBạn có thể:\n- Nhập đường dẫn vào ô text\n- Kéo thả file/folder vào đây");
                return;
            }

            // Kiểm tra script
            if (!_service.CheckScriptExists())
            {
                ShowError($"Không tìm thấy PowerShell script!\n\nĐường dẫn: {_service.GetScriptPath()}");
                return;
            }

            // Chạy script
            try
            {
                buttonRunPS.Enabled = false;
                labelStatus.Text = "Đang chạy script...";
                labelStatus.ForeColor = Color.Blue;

                _service.RunFilterScript(filePath);

                labelStatus.Text = "Script đã được chạy! Kiểm tra cửa sổ PowerShell.";
                labelStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi chạy script:\n{ex.Message}");
                labelStatus.Text = "Lỗi!";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonRunPS.Enabled = true;
            }
        }

        /// <summary>
        /// Hiển thị thông báo cảnh báo
        /// </summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Hiển thị thông báo lỗi
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hiển thị thông báo thông tin
        /// </summary>
        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Clear form
        /// </summary>
        public void ClearForm()
        {
            textBoxPath.Clear();
            labelStatus.Text = string.Empty;
        }
    }
}
