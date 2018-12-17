using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_mods_Beta
{
    public static class FilesDir
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        private static List<string> getAllFiles;
        private static string head;

        public static List<string> Get(string path)
        {
            head = path;
            getAllFiles = new List<string>();
            getAllFiles = GetFilesDir(new DirectoryInfo(path));
            return getAllFiles;
        }

        private static List<string> GetFilesDir(DirectoryInfo root)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            // Сначала обрабатываю все файлы непосредственно в этой папке
            try
            {
                files = root.GetFiles("*.*");
            }
            // Это вызывается, если один из файлов требует разрешения больше
            // чем приложение предоставляет.
            catch (UnauthorizedAccessException e)
            {
                // Этот код выдает сообщение и продолжает работать.
                // Вы можете попытаться повысить Ваш уровень доступа и получить доступ к файлу.
                log.Add(e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo file in files)
                    getAllFiles.Add(file.FullName.Substring(head.Length + 1));

                // Теперь ищу все подкаталоги в этом каталоге.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                    // Рекурсивный вызов для каждого подкаталога.
                    GetFilesDir(dirInfo);
            }
            return getAllFiles;
        }
    }
}
