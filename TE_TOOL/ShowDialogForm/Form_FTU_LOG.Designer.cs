namespace TE_TOOL
{
    partial class Form_FTU_LOG
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
            richTextBox1 = new RichTextBox();
            BTN_save = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(43, 40);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(702, 320);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // BTN_save
            // 
            BTN_save.Location = new Point(347, 386);
            BTN_save.Name = "BTN_save";
            BTN_save.Size = new Size(94, 29);
            BTN_save.TabIndex = 1;
            BTN_save.Text = "Save";
            BTN_save.UseVisualStyleBackColor = true;
            // 
            // Form_FTU_LOG
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTN_save);
            Controls.Add(richTextBox1);
            Name = "Form_FTU_LOG";
            Text = "Form_FTU_LOG";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button BTN_save;
    }
}