using SevenZipExtractor;
using System;
using System.Collections.Generic;

namespace Search_mods_Beta
{
    public static class FilesArch
    {
        public static List<string> Get(string path)
        {
            List<string> getAllFiles = new List<string>();

            try
            {
                using (ArchiveFile archiveFile = new ArchiveFile(path))

                    foreach (Entry entry in archiveFile.Entries)

                        getAllFiles.Add(entry.FileName);
            }
            catch (Exception)
            {
                //Console.WriteLine($"Не удалось прочесть {path}");
            }
                   

            return getAllFiles;                                   
        }
    }
}


