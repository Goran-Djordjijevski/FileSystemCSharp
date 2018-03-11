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
        public List<string> folders = new List<string>()
        {
            @"Workspace\",
            @"Workspace\Archive\",
            @"Workspace\Temp\"
        };

        static void Main(string[] args)
        {
            var app = new Program();

            app.CreateDirectory();

            Console.ReadLine();
        }

        public void CreateDirectory()
        {
            int total = folders.Count();

            for (int i = 0; i < total; i++)
            {
                string directoryName = folders[i];

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
}
