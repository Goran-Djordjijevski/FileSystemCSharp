using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

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

        public string configFile
        {
            get
            {
                return GetUserDataFolder() + "Config.txt";
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();

            app.CreateConfig();

            app.ReadConfig();

            app.CreateDirectory();

            app.CreateFile();

            Console.ReadLine();

            app.ArchiveConfig();

            app.SaveImages();

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
            return GetUserDataFolder() + folders[(int)names];
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

        public void ArchiveConfig()
        {
            var configPath = configFile;
            var configName = Path.GetFileName(configPath);
            var tempPath = GetFolderByName(FolderNames.Temp) + configName;
            var newPath = GetFolderByName(FolderNames.SaveData) + configName;

            File.Copy(configPath, tempPath);

            var lines = File.ReadAllLines(tempPath);
            var configString = String.Join(Environment.NewLine, lines);
            var workspaceDirectoryName = Path.GetDirectoryName(GetFolderByName(FolderNames.Workspace));
            var newWorkspaceDirName = workspaceDirectoryName + DateTime.Now.ToString("yyyyMMddHHmmss");

            configString = configString.Replace(workspaceDirectoryName, newWorkspaceDirName);

            File.WriteAllText(tempPath, configString);

            File.Move(tempPath, newPath);
        }

        public void SaveImages()
        {
            var imageFileName = GetFolderByName(FolderNames.SaveData) + "image.jpeg";

            var bitMap = new Bitmap(128, 128, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(bitMap);
            g.Clear(Color.Magenta);

            bitMap.Save(imageFileName, ImageFormat.Jpeg);
        }

        public string GetUserDataFolder()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            directory += @"\FileSystemTest\";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
