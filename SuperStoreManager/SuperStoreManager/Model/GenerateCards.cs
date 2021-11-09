using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace SuperStoreManager.Model
{
    class GenerateCards
    {
        public static int[] getCard()
        {
            int[] tmpCard = new int[25];
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            int getRandomNum()
            {
                int randomNum = random.Next(1, 75);
                if (Array.IndexOf(tmpCard, randomNum) != -1)
                {
                    return getRandomNum();
                }
                else
                {
                    return randomNum;
                }
            }
            for (int x = 0; x < 25; x++)
            {
                int randomNum = getRandomNum();
                tmpCard[x] = randomNum;
            }
            return tmpCard;
        }

        public static Bitmap generateImage(int[] num, List<int> selectedNum = null, bool  winning = false)
        {
            int initWidth = 210;
            int initHeight = 180;

            int x = 5;
            int y = 10;

            Bitmap image = new Bitmap(initWidth, initHeight);//初始化大小
            if (winning)
            {
                image = new Bitmap(Properties.Resources.中奖, initWidth, initHeight);
            }

            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置图片质量

            if (!winning)
            {
                g.Clear(Color.White);
            }
            for (int indexX = 0; indexX < 25; indexX++)
            {
                int indexY = indexX % 5;
                if (x > initWidth - 20)
                {
                    x = 5;
                    y += 30;
                }
                int number = num[indexX];
                Font f = new Font("Arial ", 20);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Brush b = new SolidBrush(Color.Black);
                if (selectedNum != null && selectedNum.IndexOf(number) != -1)
                {
                    b = new SolidBrush(Color.GreenYellow);
                }
                Brush r = new SolidBrush(Color.FromArgb(166, 8, 8));
                if (number < 10)
                {
                    g.DrawString(number.ToString(), f, b, x + 10, y);
                }
                else
                {
                    g.DrawString(number.ToString(), f, b, x, y);//设置位置
                }
                x += 40;
            }
          
            return image;
        }
    }
}
