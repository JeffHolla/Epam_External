using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

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
    public class LoggingStateUpdate
    {
        // Пути, нужные для работы программы
        string filesPath = @"..\..\StoringFolder\ToStore";
        string copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        // Экземпляры классов для директорий
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
        // Все файлы из директорий
        FileInfo[] files;

        public LoggingStateUpdate()
        {
            filesDirectory = new DirectoryInfo(filesPath);
            copyFilesDirectory = new DirectoryInfo(copyFilesPath);
            files = filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories);
        }

        public void StartLogging()
        {
            StateChecker()/*.Wait()*/;

            Console.WriteLine("HEY!");
            Console.ReadKey();
        }

        // Пока что не async
        public /*Task */ void StateChecker()
        {
            // Массив для хранения Тасков? Есть ли вообще смысл запускать их все одновременно?
            // Как работает ожидание всех Тасков?
            //Task[] tasksList = new Task[filesDirectory.GetFiles().Length];
            List<Task> tasksList = new List<Task>();

            // Стоит ли каждый раз пересобирать все файлы?
            // Или отдать на откуп пользователя, чтобы он мог обновлять по своему желанию
            //files = filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories);

            while (true)
            {
                // Чтобы не триггерить ядро процессора
                Task.Delay(50).Wait();
                for (int i = 0; i < files.Length; i++)
                {
                    foreach (var file in filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories))
                    {
                        if (files[i].Name == file.Name)
                        {
                            if (files[i].LastWriteTime != file.LastWriteTime)
                            {
                                // Так они сразу вызываются, что логично
                                tasksList.Add(CopyNewStateFile(files[i]));
                                
                                files[i] = file;
                            }
                        }
                    }
                }
                
                // Ждём когда все таски выполнятся
                //Task.WhenAll(tasksList).Wait(5000);
                Console.WriteLine(DateTime.Now.ToLongTimeString());


                // Блок дополнения файлов
                //Task.Delay(30000).Wait();
                //Process.Start(@"..\..\StoringFolder\ScriptsBat\AddOneLine_RecursiveDir.bat");

                //await Task.Delay(3 * 1000);
            }
        }

        // Работающий ожидатель
        public async Task WaiterSelf(int i)
        {
            await Task.Delay(i * 1000);
            Console.WriteLine($"Waited [{i}]");
        }

        // Функция копирования файлов
        // Возможно, что стоит функцию копии и метаИнфы разделить на две таски и раскидать в них словари путей и имён
        public Task /*void*/ CopyNewStateFile(FileInfo file)
        {
            Console.WriteLine($"!!! Trying to copy {file.Name} !!!");

            // Вытаскиваем имя файла без txt
            string justFileName = file.Name.Replace(".txt", "");
            string directoryPath = $@"{copyFilesDirectory.FullName}\{justFileName}";
            string fileNameWithDate = $"{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}.txt";
            
            // Интернируем строку, т.к. пользоваться ей мы будем много
            //Console.WriteLine(string.IsInterned(directoryPath) ?? "Неа");
            string.Intern(directoryPath);

            // Если путь не существует, то создаём новый
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Копируем файл в путь
            File.Copy(file.FullName, directoryPath + $"\\{fileNameWithDate}", true);

            Console.WriteLine("===================================");
            Console.WriteLine($"File \"{file.Name}\" was backuped with name -> \"{fileNameWithDate}\"!");
            Console.WriteLine("===================================");
            Console.WriteLine();

            // Создаём для него МетаИнфу и ждём окончания
            CreateMetaInformationForFile(file).Wait();

            return Task.CompletedTask;
        }

        // Функция создания метаИнфы
        // Возможно, что стоит функцию копии и метаИнфы разделить на две таски и раскидать в них словари путей и имён
        public Task CreateMetaInformationForFile(FileInfo file)
        {
            string justFileName = file.Name.Replace(".txt", "");
            string directoryPath = $@"{copyFilesDirectory.FullName}\{justFileName}";
            string fileNameWithDate = $".{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}";
            // Интернируем строку, т.к. пользоваться ей мы будем много
            string.Intern(directoryPath);

            // Создаём файл МетаИнфы и записываем 
            // - Полный путь с файлом
            // - Полный путь без файла
            // - Время изменения файла
            File.Create(directoryPath + $"\\{fileNameWithDate}").Close();
            using (StreamWriter stream = new StreamWriter(directoryPath + $"\\{fileNameWithDate}", false))
            {
                stream.WriteLine(file.FullName);
                stream.WriteLine(file.DirectoryName);
                stream.WriteLine(file.LastWriteTime);
            }

            Console.WriteLine("===================================");
            Console.WriteLine($"For file \"{file.Name}\" was created meta information -> \"{fileNameWithDate}\"!");
            Console.WriteLine("===================================");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
