using System;
using System.IO;
using CCIAutoCreateSOFromPDF.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            chrysanHeader chrysan =ProcessPDF.processChrysan(new FileStream(@"C:\Users\AD\Desktop\新建文件夹\549887_Report.pdf", FileMode.Open));
            ;
            Console.WriteLine("Hello World!");
        }
    }
}
