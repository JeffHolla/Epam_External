using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

/*
 * Дана папка, которая является хранилищем файлов.
 * 
 * Для всех текстовых файлов (*.txt), находящихся в этой папке или вложенных подпапках,
 * реализовать сохранение истории изменений с возможностью отката состояния к любому моменту.
 * 
 * Принцип работы программы:
 * 1. При запуске программа спрашивает пользователя, какой из режимов он хочет включить:
 * наблюдения или отката изменений. Как вариант, можно использовать ключи командной
 * строки.
 * 2. При выборе режима наблюдения все происходящие с текстовыми файлами изменения
 * логируются до момента закрытия программы. Как вариант, можно создавать на диске в
 * отдельной папке копии файлов по состоянию на момент изменения.
 * 3. При выборе режима отката изменений пользователь вводит дату и время, на которые
 * должен быть осуществлён откат, после чего все текстовые файлы в папке должны принять
 * вид, соответствующий указанному времени.
 * 
 * Возможностью изменения файлов в момент, когда программа не находится в режиме
 * отслеживания изменений, пренебречь.
    */

namespace Task_4_1_1_FileManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggingStateUpdate loggingState = new LoggingStateUpdate();

            loggingState.StartLogging();

            DateTime dateTime = DateTime.Parse("19.04.2021 3:23:34");

            Console.WriteLine(dateTime);

            RestoreBackupedFiles restoreBackupedFiles = new RestoreBackupedFiles();

            restoreBackupedFiles.StartRestoring(dateTime);

            Console.ReadKey();
        }
    }

    public class RestoreBackupedFiles
    {
        string filesPath = @"..\..\StoringFolder\ToStore";
        string copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
        //FileInfo[] files;
        DirectoryInfo[] dirs;

        public RestoreBackupedFiles()
        {
            filesDirectory = new DirectoryInfo(filesPath);
            copyFilesDirectory = new DirectoryInfo(copyFilesPath);
            dirs = copyFilesDirectory.GetDirectories();
        }

        public void StartRestoring(DateTime dateTime)
        {
            StringBuilder dateBuilder = new StringBuilder();

            foreach (var dir in dirs)
            {
                var allFiles = dir.GetFiles();

                if (allFiles.Length == 0)
                {
                    continue;
                }

                var metaFiles = allFiles.Where(x => x.Name[0] == '.').ToList();
                var realFiles = allFiles.Where(x => x.Name[0] != '.').ToList();

                string fileName = realFiles[0].Name.Substring(0, realFiles[0].Name.LastIndexOf("_"));

                Dictionary<FileInfo, string> filePathPair = new Dictionary<FileInfo, string>();
                //var dict = metaFiles.ToLookup(x => x, y => y.OpenText().ReadToEnd().Split()[1]).ToDictionary(x => x, y => y);

                // Я максимально пытался избегать потоков и чтения из файлов. Это достаточно долго, если я не ошибаюсь)
                // Но, похоже, без мета информации достаточно сложно что-то сделать
                // А Byte[] расшифровки не особо лучше
                for (int i = 0; i < realFiles.Count; i++)
                {
                    string fullFileName = realFiles[i].Name.Replace(".txt", "");

                    string str1 = $"{fullFileName.Substring(0, fullFileName.LastIndexOf("_"))}";

                    Console.WriteLine(fullFileName.LastIndexOf(" ") + " " + fullFileName.Length);

                    string dateWithTime = fullFileName.Substring(fullFileName.LastIndexOf("_") + 1, fullFileName.Length - 1 - fullFileName.LastIndexOf("_"));

                    string time = $"{fullFileName.Substring(fullFileName.LastIndexOf(" ") + 1, fullFileName.Length - 1 - fullFileName.LastIndexOf(" "))}";

                    dateBuilder.Append(time);

                    dateBuilder.Insert(time.Length - 2, ":");
                    dateBuilder.Insert(time.Length - 4, ":");


                }
            }
        }
    }
}
