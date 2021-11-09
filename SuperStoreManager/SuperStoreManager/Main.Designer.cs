namespace SuperStoreManager
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.FilePathlabel = new System.Windows.Forms.Label();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.GenerateCardButton = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.SelectFile = new System.Windows.Forms.Button();
            this.Export = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.Start = new System.Windows.Forms.Button();
            this.Count = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilePathlabel
            // 
            this.FilePathlabel.AutoSize = true;
            this.FilePathlabel.BackColor = System.Drawing.Color.Transparent;
            this.FilePathlabel.Location = new System.Drawing.Point(34, 13);
            this.FilePathlabel.Name = "FilePathlabel";
            this.FilePathlabel.Size = new System.Drawing.Size(55, 13);
            this.FilePathlabel.TabIndex = 2;
            this.FilePathlabel.Text = "文件路径";
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(34, 30);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(258, 20);
            this.FilePath.TabIndex = 3;
            // 
            // GenerateCardButton
            // 
            this.GenerateCardButton.BackColor = System.Drawing.Color.Transparent;
            this.GenerateCardButton.Enabled = false;
            this.GenerateCardButton.Location = new System.Drawing.Point(34, 56);
            this.GenerateCardButton.Name = "GenerateCardButton";
            this.GenerateCardButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateCardButton.TabIndex = 1;
            this.GenerateCardButton.Text = "生成卡片";
            this.GenerateCardButton.UseVisualStyleBackColor = false;
            this.GenerateCardButton.Click += new System.EventHandler(this.GenerateCardButton_Click);
            // 
            // image
            // 
            this.image.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.image.ImageSize = new System.Drawing.Size(210, 180);
            this.image.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.LargeImageList = this.image;
            this.listView1.Location = new System.Drawing.Point(3, 16);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(527, 247);
            this.listView1.SmallImageList = this.image;
            this.listView1.StateImageList = this.image;
            this.listView1.TabIndex = 5;
            this.listView1.TileSize = new System.Drawing.Size(100, 184);
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 266);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // openFile
            // 
            this.openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // SelectFile
            // 
            this.SelectFile.BackColor = System.Drawing.Color.Transparent;
            this.SelectFile.Location = new System.Drawing.Point(299, 26);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(75, 23);
            this.SelectFile.TabIndex = 0;
            this.SelectFile.Text = "选择文件";
            this.SelectFile.UseVisualStyleBackColor = false;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // Export
            // 
            this.Export.Enabled = false;
            this.Export.Location = new System.Drawing.Point(116, 55);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(75, 23);
            this.Export.TabIndex = 2;
            this.Export.Text = "导出";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(207, 55);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 3;
            this.Start.Text = "开始抽奖";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Count
            // 
            this.Count.AutoSize = true;
            this.Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Count.Location = new System.Drawing.Point(417, 3);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(0, 76);
            this.Count.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(551, 358);
            this.Controls.Add(this.Count);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GenerateCardButton);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.FilePathlabel);
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "抽奖";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FilePathlabel;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button GenerateCardButton;
        private System.Windows.Forms.ImageList image;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label Count;
    }
}

