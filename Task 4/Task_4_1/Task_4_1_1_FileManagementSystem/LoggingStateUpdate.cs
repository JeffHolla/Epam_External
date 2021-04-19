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
        string filesPath = @"..\..\StoringFolder\ToStore";
        string copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
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

        public /*Task */ void StateChecker()
        {
            //Task[] tasksList = new Task[filesDirectory.GetFiles().Length];
            List<Task> tasksList = new List<Task>();
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

                                //var tsk = new Task(async () => await CopyNewStateFile(files[i]));
                                //tasksList.Add(tsk);
                                //tasksList[i] = (Task.Run(() => CopyNewStateFile(files[i])));

                                //Task.Run(() => CopyNewStateFile(files[i])).Wait();

                                //Task.Run(async () => await CopyNewStateFile(files[i]));


                                files[i] = file;
                            }
                        }
                    }
                }
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                //Console.WriteLine("== Starting tasks ==");
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                //Console.WriteLine();


                //List<bool> lst = new List<bool>();
                //while (true)
                //{
                //    for (int i = 0; i < tasksList.Count; i++)
                //    {
                //        //Console.WriteLine(tasksList[i].AsyncState);
                //        //Console.WriteLine(tasksList[i].CreationOptions);
                //        Console.WriteLine(tasksList[i].Status);
                //        //Console.WriteLine(tasksList[i].Id);

                //        if (tasksList[i].IsCanceled || tasksList[i].IsFaulted)
                //        {
                //            lst.Add(false);
                //        }

                //        if (tasksList[i].IsCompleted)
                //        {
                //            lst.Add(true);
                //        }
                //    }

                //    if (!lst.Contains(false) && lst.Count(x => x == true) == tasksList.Count)
                //    {
                //        tasksList.Clear();
                //        break;
                //    }
                //}

                Task.WhenAll(tasksList).Wait(5000);
                Console.WriteLine(DateTime.Now.ToLongTimeString());


                // Блок дополнения файлов
                Task.Delay(30000).Wait();
                Process.Start(@"..\..\StoringFolder\ScriptsBat\AddOneLine.bat");

                //await Task.Delay(3 * 1000);
            }
        }

        public async Task WaiterSelf(int i, List<Task> tasks)
        {
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine(i);

            await Task.Delay(i * 1000);

            Console.WriteLine("Waited");
        }

        public Task /*void*/ CopyNewStateFile(FileInfo file)
        {
            Console.WriteLine($"!!! Trying to copy {file.Name} !!!");

            string justFileName = file.Name.Replace(".txt", "");
            string directoryPath = $@"{copyFilesDirectory.FullName}\{justFileName}";
            string fileNameWithDate = $"{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}.txt";
            //Console.WriteLine(string.IsInterned(directoryPath) ?? "Неа");
            string.Intern(directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.Copy(file.FullName, directoryPath + $"\\{fileNameWithDate}", true);

            Console.WriteLine("===================================");
            Console.WriteLine($"File \"{file.Name}\" was backuped with name -> \"{fileNameWithDate}\"!");
            Console.WriteLine("===================================");
            Console.WriteLine();

            CreateMetaInformationForFile(file).Wait();

            return Task.CompletedTask;
        }

        public Task CreateMetaInformationForFile(FileInfo file)
        {
            string justFileName = file.Name.Replace(".txt", "");
            string directoryPath = $@"{copyFilesDirectory.FullName}\{justFileName}";
            string fileNameWithDate = $".{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}";
            string.Intern(directoryPath);

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
