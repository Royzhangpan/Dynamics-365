using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using SuperStoreManager.Model;

namespace SuperStoreManager
{
    public partial class StartForm : Form
    {
        public List<int> selectedNumbers { get; set; }
        public int selectedNumber { get; set; }
        public bool exit { get; set; }
        bool isEnd = false;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        public StartForm()
        {
            InitializeComponent();
        }
        
        private void StartForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (selectedNumbers.Count > 0)
            {
                foreach (int t in selectedNumbers)
                {
                    Numbers.Text = t.ToString() + $"\n{Numbers.Text}";
                }
            }
            Numbers.Enabled = true;
            player.Stream = SuperStoreManager.Properties.Resources.抽奖音乐;
            player.Load();
            player.PlayLooping();
        }
        private void StartForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isEnd)
            {
                timer1.Stop();
                Numbers.Text = Number.Text + $"\n{Numbers.Text}";
                isEnd = true;
                player.Stop();
            }
            else
            {
                timer1.Start();
                player.PlayLooping();
                isEnd = false;
            }
            selectedNumber = int.Parse(Number.Text);
            if (exit)
            {
                Numbers.Refresh();
                player.Stop();
                Thread.Sleep(1000);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            int getRandomNum(List<int> selectedNumbers)
            {
                int randomNum = random.Next(1, 75);
                if (selectedNumbers.IndexOf(randomNum) != -1) 
                {
                    return getRandomNum(selectedNumbers);
                }
                else
                {
                    return randomNum;
                }
            }
            Number.Text = getRandomNum(selectedNumbers).ToString();

            Number.Refresh();
        }
    }
}
