namespace result
{
    partial class Result
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
            this.ResulteGroup = new System.Windows.Forms.GroupBox();
            this.ResulteDataGrid = new System.Windows.Forms.DataGridView();
            this.ShowPath = new System.Windows.Forms.TextBox();
            this.ResulteGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResulteDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ResulteGroup
            // 
            this.ResulteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResulteGroup.AutoSize = true;
            this.ResulteGroup.Controls.Add(this.ResulteDataGrid);
            this.ResulteGroup.Location = new System.Drawing.Point(3, 27);
            this.ResulteGroup.Name = "ResulteGroup";
            this.ResulteGroup.Size = new System.Drawing.Size(785, 421);
            this.ResulteGroup.TabIndex = 0;
            this.ResulteGroup.TabStop = false;
            // 
            // ResulteDataGrid
            // 
            this.ResulteDataGrid.AllowUserToAddRows = false;
            this.ResulteDataGrid.AllowUserToDeleteRows = false;
            this.ResulteDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ResulteDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ResulteDataGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ResulteDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResulteDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResulteDataGrid.Location = new System.Drawing.Point(3, 16);
            this.ResulteDataGrid.MultiSelect = false;
            this.ResulteDataGrid.Name = "ResulteDataGrid";
            this.ResulteDataGrid.ReadOnly = true;
            this.ResulteDataGrid.RowHeadersVisible = false;
            this.ResulteDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ResulteDataGrid.ShowCellToolTips = false;
            this.ResulteDataGrid.Size = new System.Drawing.Size(779, 402);
            this.ResulteDataGrid.TabIndex = 0;
            // 
            // ShowPath
            // 
            this.ShowPath.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ShowPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShowPath.Location = new System.Drawing.Point(6, 8);
            this.ShowPath.Name = "ShowPath";
            this.ShowPath.Size = new System.Drawing.Size(779, 13);
            this.ShowPath.TabIndex = 1;
            // 
            // Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ShowPath);
            this.Controls.Add(this.ResulteGroup);
            this.Name = "Result";
            this.Text = "Result";
            this.ResulteGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResulteDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ResulteGroup;
        public System.Windows.Forms.DataGridView ResulteDataGrid;
        public System.Windows.Forms.TextBox ShowPath;
    }
}