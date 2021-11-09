namespace AutoCreateLabel
{
    partial class UserInterface
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
            this.enusLabel = new System.Windows.Forms.Label();
            this.enusText = new System.Windows.Forms.TextBox();
            this.LabelId = new System.Windows.Forms.TextBox();
            this.SecondLanguage = new System.Windows.Forms.ComboBox();
            this.SecText = new System.Windows.Forms.TextBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.LabelIdLabel = new System.Windows.Forms.Label();
            this.LabelFile = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // enusLabel
            // 
            this.enusLabel.AutoSize = true;
            this.enusLabel.Location = new System.Drawing.Point(15, 95);
            this.enusLabel.Name = "enusLabel";
            this.enusLabel.Size = new System.Drawing.Size(33, 13);
            this.enusLabel.TabIndex = 0;
            this.enusLabel.Text = "en-us";
            // 
            // enusText
            // 
            this.enusText.Location = new System.Drawing.Point(16, 115);
            this.enusText.Name = "enusText";
            this.enusText.Size = new System.Drawing.Size(252, 20);
            this.enusText.TabIndex = 1;
            // 
            // LabelId
            // 
            this.LabelId.Location = new System.Drawing.Point(18, 64);
            this.LabelId.Name = "LabelId";
            this.LabelId.Size = new System.Drawing.Size(247, 20);
            this.LabelId.TabIndex = 2;
            // 
            // SecondLanguage
            // 
            this.SecondLanguage.FormattingEnabled = true;
            this.SecondLanguage.Location = new System.Drawing.Point(18, 153);
            this.SecondLanguage.Name = "SecondLanguage";
            this.SecondLanguage.Size = new System.Drawing.Size(72, 21);
            this.SecondLanguage.TabIndex = 3;
            this.SecondLanguage.Text = "zh-hans";
            // 
            // SecText
            // 
            this.SecText.Location = new System.Drawing.Point(18, 181);
            this.SecText.Name = "SecText";
            this.SecText.Size = new System.Drawing.Size(250, 20);
            this.SecText.TabIndex = 4;
            // 
            // CreateBtn
            // 
            this.CreateBtn.Location = new System.Drawing.Point(60, 215);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 6;
            this.CreateBtn.Text = "Create";
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(173, 215);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // LabelIdLabel
            // 
            this.LabelIdLabel.AutoSize = true;
            this.LabelIdLabel.Location = new System.Drawing.Point(15, 48);
            this.LabelIdLabel.Name = "LabelIdLabel";
            this.LabelIdLabel.Size = new System.Drawing.Size(42, 13);
            this.LabelIdLabel.TabIndex = 8;
            this.LabelIdLabel.Text = "LabelId";
            // 
            // LabelFile
            // 
            this.LabelFile.FormattingEnabled = true;
            this.LabelFile.Location = new System.Drawing.Point(16, 13);
            this.LabelFile.Name = "LabelFile";
            this.LabelFile.Size = new System.Drawing.Size(249, 21);
            this.LabelFile.TabIndex = 9;
            // 
            // UserInterface
            // 
            this.AcceptButton = this.CreateBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(303, 250);
            this.ControlBox = false;
            this.Controls.Add(this.LabelFile);
            this.Controls.Add(this.LabelIdLabel);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.CreateBtn);
            this.Controls.Add(this.SecText);
            this.Controls.Add(this.SecondLanguage);
            this.Controls.Add(this.LabelId);
            this.Controls.Add(this.enusText);
            this.Controls.Add(this.enusLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInterface";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Label Edit";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enusLabel;
        public System.Windows.Forms.TextBox enusText;
        public System.Windows.Forms.TextBox LabelId;
        public System.Windows.Forms.ComboBox SecondLanguage;
        public System.Windows.Forms.TextBox SecText;
        private System.Windows.Forms.Button CreateBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label LabelIdLabel;
        public System.Windows.Forms.ComboBox LabelFile;
    }
}