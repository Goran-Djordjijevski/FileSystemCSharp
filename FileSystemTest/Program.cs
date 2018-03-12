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
            SaveData,
            Temp
        }

        public string configFile = "Config.txt";

        static void Main(string[] args)
        {
            var app = new Program();

            app.CreateConfig();

            app.ReadConfig();

            app.CreateDirectory();

            app.CreateFile();

            Console.ReadLine();

            app.CopySaveData();

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
                var destDirName = GetFolderByName(FolderNames.Archive);
                destDirName += Path.GetFileName(saveDataDir.TrimEnd(Path.DirectorySeparatorChar));
                destDirName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Directory.Move(saveDataDir, destDirName);
            }
        }

        public string GetFolderByName(FolderNames names)
        {
            return folders[(int)names];
        }

        public void CreateFile()
        {
            var path = GetFolderByName(FolderNames.SaveData) + "TestFile.txt";

            File.WriteAllText(path, contents: "Hello");

            var fileInfo = new FileInfo(path);
            var name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
            var extension = fileInfo.Extension;
            var size = fileInfo.Length;

            Console.WriteLine($"Created file {name} with extension {extension} with a size of {size} bytes");
        }

        public void CreateConfig()
        {
            if (!File.Exists(configFile))
            {
                File.WriteAllLines(configFile, folders);
            }
        }

        public void ReadConfig()
        {
            var lines = File.ReadAllLines(configFile);
            var total = lines.Length;

            for (int i = 0; i < total; i++)
            {
                var pathString = lines[i];
                Console.WriteLine($"Setting path - {pathString}");
            }
        }
    }
}
