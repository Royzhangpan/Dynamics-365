using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var metadataProviderFactory = new Microsoft.Dynamics.AX.Metadata.Storage.MetadataProviderFactory();
            var createDiskProvider =  metadataProviderFactory.CreateDiskProvider(@"C:\AOSService\PackagesLocalDirectory");

            foreach (var labelfile in createDiskProvider.Views.ListObjectsForModel("HSO"))
            {
                Console.WriteLine(labelfile);
            }
            Console.ReadKey();
        }
    }
}
