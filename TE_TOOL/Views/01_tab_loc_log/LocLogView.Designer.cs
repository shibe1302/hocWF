namespace TE_TOOL.Views
{
    partial class LocLogView
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
            label3 = new Label();
            labelStatus = new Label();
            buttonRunPS = new Button();
            textBoxPath = new TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkCyan;
            label3.Location = new Point(170, 142);
            label3.Name = "label3";
            label3.Size = new Size(768, 89);
            label3.TabIndex = 10;
            label3.Text = "Kéo thả file zip vào đây";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.BackColor = SystemColors.ButtonHighlight;
            labelStatus.Font = new Font("Segoe UI", 12F);
            labelStatus.Location = new Point(192, 318);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(0, 28);
            labelStatus.TabIndex = 9;
            // 
            // buttonRunPS
            // 
            buttonRunPS.BackColor = SystemColors.ButtonHighlight;
            buttonRunPS.Cursor = Cursors.Hand;
            buttonRunPS.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRunPS.Location = new Point(366, 360);
            buttonRunPS.Name = "buttonRunPS";
            buttonRunPS.Size = new Size(333, 50);
            buttonRunPS.TabIndex = 8;
            buttonRunPS.Text = "🚀 Chạy Script Lọc Log";
            buttonRunPS.UseVisualStyleBackColor = false;
            buttonRunPS.Click += buttonRunPS_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Font = new Font("Segoe UI", 10F);
            textBoxPath.Location = new Point(192, 274);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.PlaceholderText = "Nhập đường dẫn file/folder hoặc kéo thả vào đây...";
            textBoxPath.Size = new Size(708, 30);
            textBoxPath.TabIndex = 7;
            // 
            // LocLogView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(labelStatus);
            Controls.Add(buttonRunPS);
            Controls.Add(textBoxPath);
            Name = "LocLogView";
            Size = new Size(1108, 642);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label labelStatus;
        private Button buttonRunPS;
        private TextBox textBoxPath;
    }
}
