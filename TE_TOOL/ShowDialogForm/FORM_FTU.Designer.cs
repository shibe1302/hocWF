namespace TE_TOOL
{
    partial class FORM_FTU
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
            RTB_LOG_FUNTION = new RichTextBox();
            save_button = new Button();
            SuspendLayout();
            // 
            // RTB_LOG_FUNTION
            // 
            RTB_LOG_FUNTION.Location = new Point(34, 32);
            RTB_LOG_FUNTION.Name = "RTB_LOG_FUNTION";
            RTB_LOG_FUNTION.Size = new Size(728, 429);
            RTB_LOG_FUNTION.TabIndex = 0;
            RTB_LOG_FUNTION.Text = "";
            // 
            // save_button
            // 
            save_button.Location = new Point(351, 481);
            save_button.Name = "save_button";
            save_button.Size = new Size(94, 29);
            save_button.TabIndex = 1;
            save_button.Text = "button1";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += save_button_Click;
            // 
            // FORM_FTU
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 544);
            Controls.Add(save_button);
            Controls.Add(RTB_LOG_FUNTION);
            Name = "FORM_FTU";
            Text = "Dữ liệu từ FTU củ";
            Load += FORM_FTU_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox RTB_LOG_FUNTION;
        private Button save_button;
    }
}