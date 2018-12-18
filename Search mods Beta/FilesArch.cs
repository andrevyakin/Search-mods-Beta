using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SevenZip;
using SevenZipExtractor;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;

namespace Search_mods_Beta
{
    public static class FilesArch
    {
        public static List<string> Get(string path)
        {
            List<string> getAllFiles = new List<string>();

            //try
            //{
            //    switch (Path.GetExtension(path))
            //    {
            //        case ".7z":
            //        case ".zip":
            //        case ".rar":
            //            using (ArchiveFile archiveFile = new ArchiveFile(path))

            //                foreach (Entry entry in archiveFile.Entries)

            //                    getAllFiles.Add(entry.FileName);
            //            break;
            //        default:
            //            Console.WriteLine($"{path} не является поддериваемым архивом...");
            //            break;
            //    }

            //}
            //catch (Exception)
            //{
            //    Console.WriteLine($"Не удалось прочесть {path}");
            //}

            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Environment.CurrentDirectory, @"x64\", "7z.dll")
                : Path.Combine(Environment.CurrentDirectory, @"x86\", "7z.dll"));

            try
            {
                switch (Path.GetExtension(path))
                {
                    case ".7z":
                        using (var archive = SevenZipArchive.Open(path))
                            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                                getAllFiles.Add(entry.Key.Replace('/', '\\'));
                        break;

                    case ".zip":
                        using (var archive = ZipArchive.Open(path))
                            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                                getAllFiles.Add(entry.Key.Replace('/', '\\'));
                        break;

                    case ".rar":
                        using (var archive = RarArchive.Open(path))
                            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                                getAllFiles.Add(entry.Key.Replace('/', '\\'));
                        break;
                    default:
                        Console.WriteLine($"{path} не является поддериваемым архивом...");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Не удалось прочесть {path}");
            }


            return getAllFiles;                                   
        }
    }
}


