using System;
using System.IO;
using System.Linq;

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
    public class RestoreBackupedFiles
    {
        string filesPath = @"..\..\StoringFolder\ToStore";
        string copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
        DirectoryInfo[] dirs;

        public RestoreBackupedFiles()
        {
            filesDirectory = new DirectoryInfo(filesPath);
            copyFilesDirectory = new DirectoryInfo(copyFilesPath);
            dirs = copyFilesDirectory.GetDirectories("*", SearchOption.AllDirectories);
        }

        public void StartRestoring(DateTime userDateTime)
        {
            Console.WriteLine("Запущено восстановление файлов");

            // Проходим по каждой директории, т.к. каждая директория - каждый файл
            foreach (var dir in dirs)
            {
                // Выцепляем все файлы
                var allFiles = dir.GetFiles();

                // Если файлов нет, то идём к следующей директории
                if (allFiles.Length == 0)
                {
                    continue;
                }

                // Разбираем файлы на метаФайлы и обычные файлы
                var metaFiles = allFiles.Where(x => x.Name[0] == '.').ToList();
                var realFiles = allFiles.Where(x => x.Name[0] != '.').ToList();

                // Идём с конца списка реальных файлов, чтобы удобнее попадать в время и даты, если не существует
                // равной даты
                for (int i = realFiles.Count - 1; i >= 0; i--)
                {
                    // Выбираем полное имя без расширения
                    string fullFileName = realFiles[i].Name.Replace(".txt", "");

                    // Выцепляем нужную нам дату со временем
                    string dateWithTime = fullFileName.
                        Substring(fullFileName.LastIndexOf("_") + 1,
                        fullFileName.Length - fullFileName.LastIndexOf("_") - 1)
                        .Replace("!", ":");

                    // Преобразуем их в Дату
                    DateTime fileDateTime;
                    if(!DateTime.TryParse(dateWithTime, out fileDateTime))
                    {
                        // Если не получилось преобразовать, то пропускаем файл
                        continue;
                    }

                    // Если пользовательская дата и время меньше, становится равна, или больше, чем у файла, то
                    // заменяем файл на его копию из резерва
                    if(fileDateTime <= userDateTime)
                    {
                        // Выбираем из списка метаФайлов нужный нам по дате
                        var metaFileForCurrentFile = metaFiles.Where(
                            file => fullFileName.Insert(0, ".") == file.Name)
                            .First();

                        FileRestore(realFiles[i], metaFileForCurrentFile);

                        break;
                    }
                }
            }

            Console.WriteLine("Восстановление файлов завершено!");
        }

        // Восстанавливаем файл и директорию
        public void FileRestore(FileInfo fileToRestore, FileInfo metaInformation)
        {
            string filePathToRestore = "";
            string dirPathToRestore = "";
            // Выцепляем из метаИнформации нужную директорию для восстановления
            // и полный путь файла в директории
            using(StreamReader reader = new StreamReader(metaInformation.FullName))
            {
                filePathToRestore = reader.ReadLine();
                dirPathToRestore = reader.ReadLine();
            }

            // Восстанавливаем директорию, если её нет и копируем файл из резерва в основную папку 
            Directory.CreateDirectory(dirPathToRestore);
            File.Copy(fileToRestore.FullName, filePathToRestore, true);
        }
    }
}
