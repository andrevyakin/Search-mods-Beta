using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_mods_Beta
{
    public class Mod
    {
        public static string PathSkyrim {private get; set; }
        public static string PathNexus { private get; set; }
        public static string PathResult { private get; set; }

        private static List<string> skyrim;
        private static Dictionary<string, List<string>> nexus;
        private static int countNexus = 0;
        private static Dictionary<string, List<string>> result;

        public static void InitializeSkyrim()
        {
            skyrim = FilesDir.Get(PathSkyrim);
        }

        public static void InitializeNexus()
        {
            List<string> allFileNexus = FilesDir.Get(PathNexus);

            nexus = new Dictionary<string, List<string>>();

            foreach (var item in allFileNexus)
            {
                List<string> currentArch = FilesArch.Get(Path.Combine(PathNexus, item));

                nexus.Add(item.Remove(item.LastIndexOf('.')), currentArch);

                countNexus += currentArch.Count;
            }            
        }

        public static void InitializeResult()
        {
            result = new Dictionary<string, List<string>>();
           
            foreach (var fileSkyrim in skyrim)
            {
                bool flag = false;
                
                foreach (var modNexus in nexus)
                {
                    List<string> temp = new List<string>();
                   
                    foreach (var fileNexus in modNexus.Value)
                    {
                        if (fileSkyrim.Length > fileNexus.Length) continue;
                        
                        if (string.Compare(fileSkyrim, fileNexus.Substring(fileNexus.Length - fileSkyrim.Length), StringComparison.OrdinalIgnoreCase) == 0)
                        {

                            if (temp.Contains(fileSkyrim)) continue;
                            temp.Add(fileSkyrim);

                            if (!result.ContainsKey(modNexus.Key))
                                result.Add(modNexus.Key, new List<string> { fileSkyrim });
                            else
                                result[modNexus.Key].Add(fileSkyrim);

                            flag = true;
                        }
                    }
                }
                if (!flag)
                    if (!result.ContainsKey("Not found"))
                        result.Add("Not found", new List<string> { fileSkyrim });
                    else
                        result["Not found"].Add(fileSkyrim);
            }

            Console.WriteLine($"Файлов в сборке: {skyrim.Count}");
            Console.WriteLine($"Файлов в исходниках: {countNexus}");
            Console.WriteLine($"Не найдено: {result["Not found"].Count}");
        }

        public static void Create(string postfix = null)
        {
            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Environment.CurrentDirectory, @"x64\", "7z.dll")
                : Path.Combine(Environment.CurrentDirectory, @"x86\", "7z.dll"));

            foreach (var mod in result)
            {
                //if(mod.Key != "Not found") continue;

                if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(PathResult, mod.Key)) ?? throw new InvalidOperationException()))
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(PathResult, mod.Key)) ?? throw new InvalidOperationException());
                try
                {
                    using (FileStream fs = new FileStream(string.Concat(PathResult, '\\', mod.Key, postfix ?? string.Empty, ".7z"), FileMode.Create))
                    {
                        SevenZipCompressor szc = new SevenZipCompressor
                        {
                            CompressionMethod = CompressionMethod.Lzma2,
                            CompressionLevel = CompressionLevel.Normal,
                            CompressionMode = CompressionMode.Create,
                            DirectoryStructure = true,
                            PreserveDirectoryRoot = false,
                            ArchiveFormat = OutArchiveFormat.SevenZip
                        };

                        Dictionary<string, string> temp = new Dictionary<string, string>();

                        foreach (var item in mod.Value)
                            temp.Add(item, Path.Combine(PathSkyrim, item));

                        if (fs != null)
                            szc.CompressFileDictionary(temp, fs);
                    }
                }
                catch (Exception e)
                {
                    StreamWriter file = new StreamWriter(Path.Combine(PathResult, "Ошибки при архивировании.txt"), true);
                    file.WriteLine(e);
                    file.Close();
                }
                
            }
        }

        public static void Display(bool flag = false)
        {
            int count = 0;
            foreach (var mod in !flag ? result : nexus)
            {
                Console.WriteLine(string.Format($"\nИмя мода: {mod.Key}\nФайлы в моде:\n") + new string('-', 40));

                foreach (var file in mod.Value)
                {
                    Console.WriteLine(file);
                    if (++count % 10 == 0)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                            break;
                        Console.WriteLine(string.Format($"\nИмя мода: {mod.Key}\nФайлы в моде:\n") + new string('-', 40));
                    }
                }
            }
        }
    }
}
