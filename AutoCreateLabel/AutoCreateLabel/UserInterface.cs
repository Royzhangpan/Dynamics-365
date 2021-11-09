using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Dynamics.AX.Metadata.Core;
namespace AutoCreateLabel
{
    public partial class UserInterface : Form
    {
        public bool closeOk { set; get; }
        public UserInterface()
        {
            InitializeComponent();
            this.closeOk = false;
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.LabelId.Text))
            {
                 MessageBox.Show("The LabelId cannot empty.",string.Empty,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.closeOk = true;
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
