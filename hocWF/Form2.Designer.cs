namespace TE_TOOL
{
    partial class Form2
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
            rTB_listPath = new RichTextBox();
            BTN_savePath_RemoteOrLocal = new Button();
            SuspendLayout();
            // 
            // rTB_listPath
            // 
            rTB_listPath.Location = new Point(38, 39);
            rTB_listPath.Margin = new Padding(10);
            rTB_listPath.Name = "rTB_listPath";
            rTB_listPath.Size = new Size(716, 313);
            rTB_listPath.TabIndex = 0;
            rTB_listPath.Text = "";
            // 
            // BTN_savePath_RemoteOrLocal
            // 
            BTN_savePath_RemoteOrLocal.Location = new Point(349, 371);
            BTN_savePath_RemoteOrLocal.Name = "BTN_savePath_RemoteOrLocal";
            BTN_savePath_RemoteOrLocal.Size = new Size(94, 29);
            BTN_savePath_RemoteOrLocal.TabIndex = 1;
            BTN_savePath_RemoteOrLocal.Text = "Save";
            BTN_savePath_RemoteOrLocal.UseVisualStyleBackColor = true;
            BTN_savePath_RemoteOrLocal.Click += BTN_savePath_RemoteOrLocal_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 435);
            Controls.Add(BTN_savePath_RemoteOrLocal);
            Controls.Add(rTB_listPath);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rTB_listPath;
        private Button BTN_savePath_RemoteOrLocal;
    }
}