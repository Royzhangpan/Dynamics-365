namespace SuperStoreManager
{
    partial class StartForm
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
            this.Number = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Numbers = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Number
            // 
            this.Number.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Number.BackColor = System.Drawing.Color.Transparent;
            this.Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 400F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Number.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Number.Location = new System.Drawing.Point(12, 74);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(946, 536);
            this.Number.TabIndex = 0;
            this.Number.Text = "1";
            this.Number.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Numbers
            // 
            this.Numbers.Dock = System.Windows.Forms.DockStyle.Left;
            this.Numbers.Enabled = false;
            this.Numbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Numbers.ForeColor = System.Drawing.Color.Red;
            this.Numbers.Location = new System.Drawing.Point(0, 0);
            this.Numbers.Multiline = true;
            this.Numbers.Name = "Numbers";
            this.Numbers.ReadOnly = true;
            this.Numbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Numbers.Size = new System.Drawing.Size(94, 619);
            this.Numbers.TabIndex = 1;
            this.Numbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SuperStoreManager.Properties.Resources.a60e57f871e82fa0bd3c12dcb7069128;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(972, 619);
            this.Controls.Add(this.Numbers);
            this.Controls.Add(this.Number);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.StartForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Number;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox Numbers;
    }
}