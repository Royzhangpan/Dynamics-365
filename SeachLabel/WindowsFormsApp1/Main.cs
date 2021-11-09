using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace SearchLabel
{
    public partial class Main : Form
    {
        DataTable   enusDtAll = new DataTable();
        DataTable   selectedDt;
        string      curLanguage;
        string      curLabelId;

        DataTable   refResult = new DataTable();
        public Main()
        {
            InitializeComponent();
            
            enusDtAll.Columns.Add("LabelId");
            enusDtAll.Columns.Add("enus");
            enusDtAll.Columns.Add("zhhans");
            Language.DataSource = Microsoft.Dynamics.Ax.Xpp.LabelHelper.GetInstalledLanguages();
            Language.Text = "zh-Hans";


            refResult.Columns.Add("Type");
            refResult.Columns.Add("Name");
            refResult.Columns.Add("Path");
        }

        private List<string> getAllLabelFiles()
        {
            var files = new DirectoryInfo(Directory.GetParent(Environment.CurrentDirectory).FullName);
            //var files = new DirectoryInfo(@"C:\AOSService\PackagesLocalDirectory");
            List<string> labelFields = new List<string>();
            foreach (var file in files.GetDirectories())
            {
                string path = file.FullName + "\\" + file.Name + "\\AxLabelFile";
                if (Directory.Exists(path))
                {
                    var folder = new DirectoryInfo(path);

                    foreach (var labelFile in folder.GetFiles("*.xml"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(labelFile.FullName);
                        string labelId = doc.SelectSingleNode("AxLabelFile/LabelFileId").InnerText;
                        if (!labelFields.Exists(t => t == labelId))
                        {
                            labelFields.Add(labelId);
                        }
                    }
                }
            }
            return labelFields;
        }

        private void loadLabels()
        {
            if (enusDtAll.Rows.Count == 0)
            {
                curLanguage = Language.Text;
                List<string> labelFields = this.getAllLabelFiles();
                foreach (string labelField in labelFields)
                {
                    var labels = Microsoft.Dynamics.Ax.Xpp.LabelHelper.GetAllLabels(labelField, new System.Globalization.CultureInfo("en-us"));

                    foreach (var label in labels)
                    {
                        DataRow dr = enusDtAll.NewRow();
                        string lableText = label.Key.ToString();
                        if (lableText.StartsWith("@", StringComparison.CurrentCulture))
                        {
                            dr["LabelId"] = lableText;
                        }
                        else
                        {
                            dr["LabelId"] = string.Format("@{0}:", labelField) + lableText;
                        }
                        dr["enus"] = label.Value.ToString();
                        dr["zhhans"] = Microsoft.Dynamics.Ax.Xpp.LabelHelper.GetLabel(labelField, lableText, new System.Globalization.CultureInfo((string)Language.SelectedItem));
                        enusDtAll.Rows.Add(dr);
                    }
                }
            }
        }

        private string valueFormat(string _value)
        {
            if (string.IsNullOrEmpty(_value))
            {
                return "";
            }
            string str2 = Regex.Replace(_value, @"[\[\+\\\|\(\)\^\*\""\]'%~#-&]", delegate (Match match)
            {
                if (match.Value == "'")
                {
                    return "''";
                }
                else
                {
                    return "[" + match.Value + "]";
                }
            });
            return str2;
        }
        private DataTable filter()
        {
            DataRow[] filter;

            if (string.IsNullOrEmpty(value.Text))
            {
                filter = enusDtAll.Select();
            }
            else
            {
                string fieldName = "";

                if (LabelId.Checked)
                {
                    fieldName = "LabelId";
                }
                else
                {
                    if (enus.Checked)
                    {
                        fieldName = "enus";
                    }
                    else if (zhhans.Checked)
                    {
                        fieldName = "zhhans";
                    }
                }
                if (Exactly.Checked)
                {
                    filter = enusDtAll.Select(string.Format("{0}='{1}'", fieldName, this.valueFormat(value.Text)));
                }
                else
                {
                    filter = enusDtAll.Select(string.Format("{0} Like '%{1}%'", fieldName, this.valueFormat(value.Text)));
                }
            }
            DataTable dtFilter = new DataTable();
            if (filter != null && filter.Length > 0)
            {
                dtFilter = filter.CopyToDataTable();
            }

            return dtFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.loadLabels();

            selectedDt = this.filter();
            FirstDG.DataSource = selectedDt;

            if (FirstDG.Columns.Count > 0)
            {
                FirstDG.Columns["enus"].HeaderText = "en-us";
                FirstDG.Columns["zhhans"].HeaderText = (string)Language.SelectedItem;
            }
            button3.Enabled = FirstDG.Rows.Count > 0;
            FirstDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FirstDG.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            FirstDG.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            value.Text = "";
            selectedDt = this.filter();
            FirstDG.DataSource = selectedDt;

            if (FirstDG.Columns.Count > 0)
            {
                FirstDG.Columns["enus"].HeaderText = "en-us";
                FirstDG.Columns["zhhans"].HeaderText = (string)Language.SelectedItem;
            }
            FirstDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FirstDG.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            FirstDG.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Label.csv";
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                string head = "";
                //拼接列头
                for (int cNum = 0; cNum < selectedDt.Columns.Count; cNum++)
                {
                    head += selectedDt.Columns[cNum].ColumnName + ",";
                }
                //csv文件写入列头
                sw.WriteLine(head);
                string data = "";
                //csv写入数据
               
                for (int i = 0; i < selectedDt.Rows.Count; i++)
                {
                    string data2 = string.Empty;
                    //拼接行数据
                    for (int cNum1 = 0; cNum1 < selectedDt.Columns.Count; cNum1++)
                    {
                        data2 = data2 + "\"" + selectedDt.Rows[i][selectedDt.Columns[cNum1].ColumnName].ToString() + "\",";
                    }
                    bool flag = data != data2;
                    if (flag)
                    {
                        sw.WriteLine(data2);
                    }
                    data = data2;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出csv失败！" + ex.Message);
                return;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                sw = null;
                fs = null;
            }
        }

        private void dataGrid_CellDoubleClick(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewCell cell = dataGridView.CurrentCell;

            switch (cell.ColumnIndex)
            {
                case 0:
                    LabelId.Checked = true;
                    break;
                case 1:
                    enus.Checked = true;
                    break;
                case 2:
                    zhhans.Checked = true;
                    break;
            }

            string cellValue = (string)cell.Value;
            if (!cellValue.Contains("%"))
            {
                Exactly.Checked = true;
            }
            else
            {
                Exactly.Checked = false;
            }

            value.Text = (string)cell.Value;
            selectedDt = this.filter();
            FirstDG.DataSource = selectedDt;

            if (FirstDG.Columns.Count > 0)
            {
                FirstDG.Columns["enus"].HeaderText = "en-us";
                FirstDG.Columns["zhhans"].HeaderText = (string)Language.SelectedItem;
            }
            FirstDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FirstDG.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            FirstDG.Refresh();
        }

        private void Language_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.Equals(curLanguage, Language.Text))
            {
                enusDtAll.Clear();
                value.Clear();
            }
            zhhans.Text = (string)Language.SelectedItem;
        }

        private void getFileList(object sender, EventArgs e)
        {
            result.Result refForm = (result.Result)sender;
            if (Equals(curLabelId, (string)FirstDG.CurrentRow.Cells["LabelId"].Value))
            {
                refForm.ResulteDataGrid.DataSource = refResult;
                refForm.ResulteDataGrid.Refresh();
                return;
            }
            refResult.Clear();
            curLabelId = (string)FirstDG.CurrentRow.Cells["LabelId"].Value;
            var files = new DirectoryInfo(Directory.GetParent(Environment.CurrentDirectory).FullName);
            //var files = new DirectoryInfo(@"C:\AOSService\PackagesLocalDirectory");
            List<string> labelFields = new List<string>();
            foreach (var file in files.GetDirectories())
            {
                string labelPath = file.FullName + "\\" + file.Name + "\\AxLabelFile";
                string path = file.FullName + "\\" + file.Name;
                var folder = new DirectoryInfo(path);

                if (!folder.Exists)
                {
                    continue;
                }
                foreach (var curPath in folder.GetDirectories())
                {
                    if (Equals(curPath.FullName, labelPath))
                    {
                        continue;
                    }
                    var curFolder = new DirectoryInfo(curPath.FullName);
                    foreach (var curFile in curFolder.GetFiles("*.xml"))
                    {
                        refForm.ShowPath.Text = curFile.FullName;
                        refForm.ShowPath.Refresh();

                        if (File.ReadAllText(curFile.FullName).Contains(curLabelId))
                        {
                            DataRow dr = refResult.NewRow();
                            dr["Type"] = curPath.Name;
                            dr["Name"] = Path.GetFileNameWithoutExtension(curFile.Name);
                            dr["Path"] = curFile.FullName;
                            refResult.Rows.Add(dr);
                        }
                    }
                }
            }
            refForm.ResulteDataGrid.DataSource = refResult;
            refForm.ResulteDataGrid.Refresh();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            result.Result refForm = new result.Result();

            refForm.Shown += new EventHandler(this.getFileList);
            refForm.ShowDialog();
        }
        
    }
}
