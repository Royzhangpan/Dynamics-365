using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D365O_Addin_CreateMethod
{

    public class methodList
    {
        public List<string> classMethod { set; get; }
        public List<string> tableMethod { set; get; }
        public List<parmMethodContract> parmMethod { set; get; }
    }
    public partial class UserInterface : Form
    {
        public List<parmMethodContract> parmMethodList = new List<parmMethodContract>();

        public methodList methods { set; get; }
        public Boolean closeOk { set; get; }
        public UserInterface(AddinDesignerEventArgs e)
        {
            InitializeComponent();

            if (e.SelectedElement is IClassItem)
            {
                TableMethod.Enabled = false;
            }

            if (e.SelectedElement is ITable)
            {
                classMethodGroup.Enabled = false;
                parmMethod.Enabled = false;
            }
            closeOk = false;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            parmMethodContract contract = new parmMethodContract();
            contract.edtName = EDT.Text;
            contract.pack = packParm.Checked;
            if (string.IsNullOrEmpty(EDT.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(MethodName.Text))
            {
                contract.methodName = contract.edtName.Replace(" ", "");
            }
            else
            {
                contract.methodName = MethodName.Text.Replace(" ", "");
            }

            if (!string.IsNullOrEmpty(DataMember.Text))
            {
                contract.dataMember = DataMember.Text;
            }
            parmMethodList.Add(contract);
            textBox2.Text += "public " + contract.edtName + " parm" + contract.methodName + "(" + EDT.Text + " _" + 
                            contract.methodName.First().ToString().ToLower() + contract.methodName.Substring(1) + ")\r\n";
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ok_Click(object sender, EventArgs e)
        {
            methods = new methodList();
            List<string> classMethod = new List<string>();
            if (construct.Checked)
            {
                classMethod.Add("construct");
            }

            if (main.Checked)
            {
                classMethod.Add("main");
            }

            if (pack.Checked)
            {
                classMethod.Add("pack");
            }

            if (run.Checked)
            {
                classMethod.Add("run");
            }

            methods.classMethod = classMethod;
            
            List<string> tableMethod = new List<string>();
            if (find.Checked)
            {
                tableMethod.Add("find");
            }

            if (Exists.Checked)
            {
                tableMethod.Add("exist");
            }

            methods.tableMethod = tableMethod;
            methods.parmMethod = parmMethodList;
            closeOk = true;
            this.Close();
        }

        private void methodName_Modified(object sender, EventArgs e)
        {
            if (parmMethod.Text.Contains(" "))
            {
                MessageBox.Show("方法名不能带空格。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
