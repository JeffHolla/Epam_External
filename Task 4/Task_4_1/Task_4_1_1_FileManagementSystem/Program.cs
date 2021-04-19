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

            //loggingState.StartLogging();

            DateTime dateTime = DateTime.Parse("19.04.2021 19:59:27");

            //Console.WriteLine(dateTime);

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

        public void StartRestoring(DateTime userDateTime)
        {
            foreach (var dir in dirs)
            {
                var allFiles = dir.GetFiles();

                if (allFiles.Length == 0)
                {
                    continue;
                }

                var metaFiles = allFiles.Where(x => x.Name[0] == '.').ToList();
                var realFiles = allFiles.Where(x => x.Name[0] != '.').ToList();

                for (int i = realFiles.Count - 1; i >= 0; i--)
                {
                    string fullFileName = realFiles[i].Name.Replace(".txt", "");

                    string str1 = $"{fullFileName.Substring(0, fullFileName.LastIndexOf("_"))}";

                    //Console.WriteLine(fullFileName.LastIndexOf(" ") + " " + fullFileName.Length);

                    string dateWithTime = fullFileName.
                        Substring(fullFileName.LastIndexOf("_") + 1, fullFileName.Length - 1 - fullFileName.LastIndexOf("_")).
                        Replace("!", ":");

                    DateTime fileDateTime = DateTime.Parse(dateWithTime);

                    if(fileDateTime <= userDateTime)
                    {
                        var metaFileForCurrentFile = metaFiles.Where(file => fullFileName.Insert(0, ".") == file.Name).First();
                        FileRestore(realFiles[i], metaFileForCurrentFile);

                        break;
                    }
                }
            }
        }

        public void FileRestore(FileInfo fileToRestore, FileInfo metaInformation)
        {
            string filePathToRestore = "";
            string dirPathToRestore = "";
            using(StreamReader reader = new StreamReader(metaInformation.FullName))
            {
                filePathToRestore = reader.ReadLine();
                dirPathToRestore = reader.ReadLine();
            }

            Directory.CreateDirectory(dirPathToRestore);
            File.Copy(fileToRestore.FullName, filePathToRestore, true);

        }
    }
}
