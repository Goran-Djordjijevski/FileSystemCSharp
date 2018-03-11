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
            @"Workspace\Temp\",
            @"Workspace\Temp\SaveData"
        };

        public enum FolderNames
        {
            Workspace,
            Archive,
            Temp,
            SaveData
        }

        static void Main(string[] args)
        {
            var app = new Program();

            app.CreateDirectory();

            app.CopySaveData();

            Console.ReadLine();

            app.DeleteTemp();
        }

        public void CreateDirectory()
        {
            int total = folders.Count();

            for (int i = 0; i < total; i++)
            {
                string directoryName = GetFolderByName((FolderNames)i);

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

        public void DeleteTemp()
        {
            var tempDirectory = GetFolderByName(FolderNames.Temp);

            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(path: tempDirectory, recursive: true);
            }
        }

        public void CopySaveData()
        {
            var saveDataDir = GetFolderByName(FolderNames.SaveData);

            if (Directory.Exists(saveDataDir))
            {
                var destDirName = GetFolderByName(FolderNames.Archive) + "SaveData_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Directory.Move(saveDataDir, destDirName);
            }
        }

        public string GetFolderByName(FolderNames names)
        {
            return folders[(int)names];
        }
    }
}
