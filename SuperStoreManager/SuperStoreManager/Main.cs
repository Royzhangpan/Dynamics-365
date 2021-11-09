using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperStoreManager.Model;
using System.IO;
using System.Drawing;

namespace SuperStoreManager
{
    public partial class Main : Form
    {
        private string[] employeeList;

        Dictionary<string, int[]> cards;
        Dictionary<string, Bitmap> employeeCards;
        List<int> selectedNumbers = new List<int>();
        List<string> winnings = new List<string>();

        public Main()
        {
            InitializeComponent();
        }

        private void GenerateCardButton_Click(object sender, EventArgs e)
        {
            if (employeeList == null
                || employeeList.Length == 0)
            {
                MessageBox.Show("请先选择员工！", "抽奖", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cards = new Dictionary<string, int[]>();
            employeeCards = new Dictionary<string, Bitmap>();
            int imageIndex = 0;

            listView1.BeginUpdate();
            foreach (string employee in employeeList)
            {
                int[] card = GenerateCards.getCard();
                Bitmap employeeImage = GenerateCards.generateImage(card);
                cards.Add(employee, card);
                employeeCards.Add(employee, employeeImage);

                image.Images.Add(employee, employeeImage);
                ListViewItem item = new ListViewItem();
                item.Text = employee;
                item.ImageIndex = imageIndex;
                listView1.Items.Add(item);
                imageIndex++;
                System.Threading.Thread.Sleep(5);
            }
            listView1.EndUpdate();
            Export.Enabled = true;
            Start.Enabled = true;
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            FilePath.Text = openFile.FileName;

            if (result == DialogResult.OK && !string.IsNullOrEmpty(FilePath.Text))
            {
                StreamReader reader = new StreamReader(FilePath.Text, System.Text.Encoding.UTF8);
                employeeList = reader.ReadToEnd().Split(';');
                GenerateCardButton.Enabled = true;
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (employeeCards == null || employeeCards.Count == 0)
            {
                MessageBox.Show("请先生成卡片", "抽奖", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = folderBrowser.ShowDialog();
            string folderPath = folderBrowser.SelectedPath;
            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderPath))
            {
                foreach (string key in image.Images.Keys)
                {
                    Image bitmap = image.Images[key];
                    bitmap.Save(folderPath + $@"\{key}" + ".jpg");
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            startForm.selectedNumbers = selectedNumbers;
            startForm.exit = cards != null;
            image.Images.Clear();
            listView1.Items.Clear();

            Dictionary<string, int[]>[] dict = new Dictionary<string, int[]>[20];
            Dictionary<string, int[]> tmp = new Dictionary<string, int[]>();
            if (startForm.ShowDialog() == DialogResult.OK)
            {
                selectedNumbers.Add(startForm.selectedNumber);
                Count.Text = selectedNumbers.Count.ToString();
                foreach (var card in cards)
                {
                    int[] employeeNumber = card.Value;
                    int winningCount = 0;
                    string[] ix = new string[5];
                    string[] iy = new string[5];
                    string xy = "";
                    string xpy = "";
                    foreach (int selectedNum in selectedNumbers)
                    {
                        int index = Array.IndexOf(employeeNumber, selectedNum);
                        
                        if (index != -1)
                        {
                            int y = (int)(index / 5);
                            int x = index % 5;

                            ix[x] += '1';
                            if (ix[x].Length == 5)
                            {
                                winningCount = 19;
                                break;
                            }
                            iy[y] += '1';
                            if (iy[y].Length == 5)
                            {
                                winningCount = 19;
                                break;
                            }
                            if (x == y)
                            {
                                xy += '1';
                                if (xy.Length == 5)
                                {
                                    winningCount = 19;
                                    break;
                                }
                            }
                            if (x + y == 4)
                            {
                                xpy += '1';
                                if (xpy.Length == 5)
                                {
                                    winningCount = 19;
                                    break;
                                }
                            }
                            winningCount++;
                        }
                    }
                    tmp = dict[winningCount];

                    if (tmp == null)
                    {
                        tmp = new Dictionary<string, int[]>();
                    }
                    tmp.Add(card.Key, card.Value);
                    dict[winningCount] = tmp;
                }

                int imageIndex = 0;
                listView1.BeginUpdate();
                for (int i =dict.Length -1;i >=0;i--)
                {
                    tmp = dict[i];
                    if (tmp != null)
                    {
                        foreach (var card in tmp)
                        {
                            if (!image.Images.ContainsKey(card.Key))
                            {
                                if (i == 19)
                                {
                                    if (winnings.Contains(card.Key))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        winnings.Add(card.Key);
                                        image.Images.Add(card.Key, GenerateCards.generateImage(card.Value, selectedNumbers, true));
                                    }
                                }
                                else
                                {
                                    image.Images.Add(card.Key, GenerateCards.generateImage(card.Value, selectedNumbers));
                                }
                                ListViewItem item = new ListViewItem();
                                item.Text = card.Key;
                                item.ImageIndex = imageIndex;
                                listView1.Items.Add(item);
                                imageIndex++;
                            }
                        }
                    }
                }
                listView1.EndUpdate();
            }
        }
    }
}
