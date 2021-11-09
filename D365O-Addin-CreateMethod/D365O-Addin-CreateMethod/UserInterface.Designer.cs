namespace D365O_Addin_CreateMethod
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
            this.classMethodGroup = new System.Windows.Forms.GroupBox();
            this.pack = new System.Windows.Forms.CheckBox();
            this.run = new System.Windows.Forms.CheckBox();
            this.main = new System.Windows.Forms.CheckBox();
            this.construct = new System.Windows.Forms.CheckBox();
            this.TableMethod = new System.Windows.Forms.GroupBox();
            this.Exists = new System.Windows.Forms.CheckBox();
            this.find = new System.Windows.Forms.CheckBox();
            this.parmMethod = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.DataMember = new System.Windows.Forms.TextBox();
            this.MethodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EDT = new System.Windows.Forms.ComboBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.packParm = new System.Windows.Forms.CheckBox();
            this.packParmLabel = new System.Windows.Forms.Label();
            this.classMethodGroup.SuspendLayout();
            this.TableMethod.SuspendLayout();
            this.parmMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // classMethodGroup
            // 
            this.classMethodGroup.Controls.Add(this.pack);
            this.classMethodGroup.Controls.Add(this.run);
            this.classMethodGroup.Controls.Add(this.main);
            this.classMethodGroup.Controls.Add(this.construct);
            this.classMethodGroup.Location = new System.Drawing.Point(12, 12);
            this.classMethodGroup.Name = "classMethodGroup";
            this.classMethodGroup.Size = new System.Drawing.Size(154, 130);
            this.classMethodGroup.TabIndex = 1;
            this.classMethodGroup.TabStop = false;
            this.classMethodGroup.Text = "Class method";
            // 
            // pack
            // 
            this.pack.AutoSize = true;
            this.pack.Location = new System.Drawing.Point(7, 92);
            this.pack.Name = "pack";
            this.pack.Size = new System.Drawing.Size(110, 17);
            this.pack.TabIndex = 3;
            this.pack.Text = "pack and unpack";
            this.pack.UseVisualStyleBackColor = true;
            // 
            // run
            // 
            this.run.AutoSize = true;
            this.run.Location = new System.Drawing.Point(7, 68);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(41, 17);
            this.run.TabIndex = 2;
            this.run.Text = "run";
            this.run.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            this.main.AutoSize = true;
            this.main.Location = new System.Drawing.Point(7, 44);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(48, 17);
            this.main.TabIndex = 1;
            this.main.Text = "main";
            this.main.UseVisualStyleBackColor = true;
            // 
            // construct
            // 
            this.construct.AutoSize = true;
            this.construct.Location = new System.Drawing.Point(7, 20);
            this.construct.Name = "construct";
            this.construct.Size = new System.Drawing.Size(70, 17);
            this.construct.TabIndex = 0;
            this.construct.Text = "construct";
            this.construct.UseVisualStyleBackColor = true;
            // 
            // TableMethod
            // 
            this.TableMethod.Controls.Add(this.Exists);
            this.TableMethod.Controls.Add(this.find);
            this.TableMethod.Location = new System.Drawing.Point(207, 12);
            this.TableMethod.Name = "TableMethod";
            this.TableMethod.Size = new System.Drawing.Size(138, 130);
            this.TableMethod.TabIndex = 2;
            this.TableMethod.TabStop = false;
            this.TableMethod.Text = "Table method";
            // 
            // Exists
            // 
            this.Exists.AutoSize = true;
            this.Exists.Location = new System.Drawing.Point(7, 44);
            this.Exists.Name = "Exists";
            this.Exists.Size = new System.Drawing.Size(47, 17);
            this.Exists.TabIndex = 1;
            this.Exists.Text = "exist";
            this.Exists.UseVisualStyleBackColor = true;
            // 
            // find
            // 
            this.find.AutoSize = true;
            this.find.Location = new System.Drawing.Point(7, 20);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(43, 17);
            this.find.TabIndex = 0;
            this.find.Text = "find";
            this.find.UseVisualStyleBackColor = true;
            // 
            // parmMethod
            // 
            this.parmMethod.Controls.Add(this.textBox2);
            this.parmMethod.Controls.Add(this.Add);
            this.parmMethod.Controls.Add(this.DataMember);
            this.parmMethod.Controls.Add(this.MethodName);
            this.parmMethod.Controls.Add(this.label2);
            this.parmMethod.Controls.Add(this.label1);
            this.parmMethod.Controls.Add(this.label3);
            this.parmMethod.Controls.Add(this.EDT);
            this.parmMethod.Controls.Add(this.packParm);
            this.parmMethod.Controls.Add(this.packParmLabel);
            this.parmMethod.Location = new System.Drawing.Point(12, 156);
            this.parmMethod.Name = "parmMethod";
            this.parmMethod.Size = new System.Drawing.Size(333, 282);
            this.parmMethod.TabIndex = 3;
            this.parmMethod.TabStop = false;
            this.parmMethod.Text = "Parm method";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1, 119);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(307, 157);
            this.textBox2.TabIndex = 6;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(220, 83);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(65, 23);
            this.Add.TabIndex = 5;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // DataMember
            // 
            this.DataMember.AutoSize = true;
            this.DataMember.Location = new System.Drawing.Point(202, 38);
            this.DataMember.Name = "DataMember";
            this.DataMember.Size = new System.Drawing.Size(87, 17);
            this.DataMember.TabIndex = 4;
            this.DataMember.Text = "";

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 19);
            this.label3.Name = "label2";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Data member";
            // 
            // MethodName
            // 
            this.MethodName.Location = new System.Drawing.Point(7, 83);
            this.MethodName.Name = "MethodName";
            this.MethodName.Size = new System.Drawing.Size(180, 20);
            this.MethodName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Method name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data type";
            // 
            // EDT
            // 
            this.EDT.FormattingEnabled = true;
            this.EDT.Location = new System.Drawing.Point(7, 38);
            this.EDT.Name = "EDT";
            this.EDT.Size = new System.Drawing.Size(180, 21);
            this.EDT.TabIndex = 0;
            // 
            // packMethod
            // 
            this.packParm.AutoSize = true;
            this.packParm.Location = new System.Drawing.Point(202, 83);
            this.packParm.Name = "parmMethod";
            this.packParm.Size = new System.Drawing.Size(70, 17);
            this.packParm.TabIndex = 7;
            this.packParm.Text = "pack";
            this.packParm.Checked = true;
            this.packParm.UseVisualStyleBackColor = true;
            // 
            // packParmLabel
            // 
            this.packParmLabel.AutoSize = true;
            this.packParmLabel.Location = new System.Drawing.Point(195, 66);
            this.packParmLabel.Name = "packParmLabel";
            this.packParmLabel.Size = new System.Drawing.Size(72, 13);
            this.packParmLabel.TabIndex = 8;
            this.packParmLabel.Text = "Pack";
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(71, 453);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 4;
            this.ok.Text = "ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(162, 453);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 489);
            this.ControlBox = false;
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.parmMethod);
            this.Controls.Add(this.TableMethod);
            this.Controls.Add(this.classMethodGroup);
            this.Name = "UserInterface";
            this.Text = "";
            this.classMethodGroup.ResumeLayout(false);
            this.classMethodGroup.PerformLayout();
            this.TableMethod.ResumeLayout(false);
            this.TableMethod.PerformLayout();
            this.parmMethod.ResumeLayout(false);
            this.parmMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox classMethodGroup;
        private System.Windows.Forms.CheckBox construct;
        private System.Windows.Forms.CheckBox main;
        private System.Windows.Forms.CheckBox run;
        private System.Windows.Forms.CheckBox pack;
        private System.Windows.Forms.GroupBox TableMethod;
        private System.Windows.Forms.CheckBox Exists;
        private System.Windows.Forms.CheckBox find;
        private System.Windows.Forms.GroupBox parmMethod;
        public System.Windows.Forms.ComboBox EDT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MethodName;
        private System.Windows.Forms.TextBox DataMember;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox packParm;
        private System.Windows.Forms.Label packParmLabel;
    }
}