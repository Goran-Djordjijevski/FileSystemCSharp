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
            app.DeleteTemp();

            Console.ReadLine();
        }

        public void CreateDirectory()
        {
            var total = folders.Count;

            for (int i = 0; i < total; i++)
            {
                var directoryName = GetFolderByName((FolderNames)i);
                if (Directory.Exists(path: directoryName))
                {
                    Console.WriteLine($"Dir {directoryName} exists");
                }
                else
                {
                    Directory.CreateDirectory(path: directoryName);
                    Console.WriteLine($"Create dir {directoryName}");
                }
            }
        }

        public void DeleteTemp()
        {
            var tempDir = GetFolderByName(FolderNames.Temp);

            if (Directory.Exists(path: tempDir))
            {
                Directory.Delete(path: tempDir, recursive: true);
            }
        }

        public void CopySaveData()
        {
            var saveDataDir = GetFolderByName(FolderNames.SaveData);

            if (Directory.Exists(saveDataDir))
            {
                var destDirName = GetFolderByName(FolderNames.Archive) + "SaveData_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Directory.Move(sourceDirName: saveDataDir, destDirName: destDirName);
            }
        }

        public string GetFolderByName(FolderNames name)
        {
            return folders[(int)name];
        }
    }
}
