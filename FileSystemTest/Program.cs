using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Program();

            app.CreateDirectory();

            Console.ReadLine();
        }

        public void CreateDirectory()
        {
            string directoryName = "TestFolder";

            if (Directory.Exists(directoryName))
            {
                Console.WriteLine($"Directory {directoryName} exists");
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                Console.WriteLine($"Create directory {directoryName}");
            }
        }
    }
}
