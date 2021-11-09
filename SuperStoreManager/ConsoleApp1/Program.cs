using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    class Program
    {
        static int[,] getCard()
        {
            int[,] card = new int[5, 5];
            int[] tmpCard = new int[25];
            Random random = new Random();

            int getRandomNum()
            {
                int randomNum = random.Next(1, 66);
                int index = Array.IndexOf(tmpCard, randomNum);
                if (index == -1)
                {
                    return randomNum;
                }
                else
                {
                    return getRandomNum();
                }
            }
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    int randomNum = getRandomNum();
                    tmpCard[x + y] = randomNum;
                    card[x, y] = randomNum;
                }
            }
            return card;
        }

        static Bitmap generateImage(int[,] num)
        {
            int initWidth = 210;
            int initHeight = 180;

            int x = 5;
            int y = 10;
            Bitmap image = new Bitmap(initWidth, initHeight);//初始化大小
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置图片质量
            g.Clear(Color.White);
            for (int indexX = 0; indexX < 5; indexX++)
            {
                for (int indexY = 0; indexY < 5; indexY++)
                {
                    if (x > initWidth - 20)
                    {
                        x = 5;
                        y += 30;
                    }
                    Font f = new Font("Arial ", 20);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                    Brush b = new SolidBrush(Color.Black);
                    Brush r = new SolidBrush(Color.FromArgb(166, 8, 8));
                    int number = num[indexX, indexY];
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
            }
            return image;
        }
        static void Main(string[] args)
        {
            generateImage(getCard()).Save(@"C:\Users\Administrator\Desktop\" + "1.jpg", ImageFormat.Jpeg);
        }
    }
}
