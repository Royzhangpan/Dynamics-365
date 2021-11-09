namespace SearchLabel
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
            this.value = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.FirstDG = new System.Windows.Forms.DataGridView();
            this.Exactly = new System.Windows.Forms.CheckBox();
            this.LabelId = new System.Windows.Forms.RadioButton();
            this.enus = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Language = new System.Windows.Forms.ComboBox();
            this.zhhans = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FirstDG)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(23, 28);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(255, 20);
            this.value.TabIndex = 0;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(24, 52);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(57, 23);
            this.Search.TabIndex = 1;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.button1_Click);
            // 
            // FirstDG
            // 
            this.FirstDG.AllowUserToAddRows = false;
            this.FirstDG.AllowUserToDeleteRows = false;
            this.FirstDG.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FirstDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FirstDG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstDG.Location = new System.Drawing.Point(3, 16);
            this.FirstDG.MultiSelect = false;
            this.FirstDG.Name = "FirstDG";
            this.FirstDG.ReadOnly = true;
            this.FirstDG.RowHeadersVisible = false;
            this.FirstDG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.FirstDG.Size = new System.Drawing.Size(611, 369);
            this.FirstDG.TabIndex = 2;
            this.FirstDG.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellDoubleClick);
            // 
            // Exactly
            // 
            this.Exactly.AutoSize = true;
            this.Exactly.Location = new System.Drawing.Point(134, 19);
            this.Exactly.Name = "Exactly";
            this.Exactly.Size = new System.Drawing.Size(92, 17);
            this.Exactly.TabIndex = 7;
            this.Exactly.Text = "Match exactly";
            this.Exactly.UseVisualStyleBackColor = true;
            // 
            // LabelId
            // 
            this.LabelId.AutoSize = true;
            this.LabelId.Location = new System.Drawing.Point(6, 48);
            this.LabelId.Name = "LabelId";
            this.LabelId.Size = new System.Drawing.Size(60, 17);
            this.LabelId.TabIndex = 9;
            this.LabelId.Text = "LabelId";
            this.LabelId.UseVisualStyleBackColor = true;
            // 
            // enus
            // 
            this.enus.AutoSize = true;
            this.enus.Checked = true;
            this.enus.Location = new System.Drawing.Point(72, 48);
            this.enus.Name = "enus";
            this.enus.Size = new System.Drawing.Size(51, 17);
            this.enus.TabIndex = 10;
            this.enus.TabStop = true;
            this.enus.Text = "en-us";
            this.enus.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Language);
            this.groupBox1.Controls.Add(this.zhhans);
            this.groupBox1.Controls.Add(this.LabelId);
            this.groupBox1.Controls.Add(this.enus);
            this.groupBox1.Controls.Add(this.Exactly);
            this.groupBox1.Location = new System.Drawing.Point(334, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 77);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Field";
            // 
            // Language
            // 
            this.Language.FormattingEnabled = true;
            this.Language.Location = new System.Drawing.Point(7, 20);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(121, 21);
            this.Language.TabIndex = 12;
            this.Language.SelectedValueChanged += new System.EventHandler(this.Language_SelectedValueChanged);
            // 
            // zhhans
            // 
            this.zhhans.AutoSize = true;
            this.zhhans.Location = new System.Drawing.Point(129, 48);
            this.zhhans.Name = "zhhans";
            this.zhhans.Size = new System.Drawing.Size(62, 17);
            this.zhhans.TabIndex = 11;
            this.zhhans.Text = "zh-hans";
            this.zhhans.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.FirstDG);
            this.groupBox2.Location = new System.Drawing.Point(20, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 388);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Export";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(194, 52);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Find reference";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 487);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.value);
            this.Name = "Main";
            this.Text = "Find label";
            ((System.ComponentModel.ISupportInitialize)(this.FirstDG)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.DataGridView FirstDG;
        private System.Windows.Forms.CheckBox Exactly;
        private System.Windows.Forms.RadioButton LabelId;
        private System.Windows.Forms.RadioButton enus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton zhhans;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.Button button3;
    }
}

