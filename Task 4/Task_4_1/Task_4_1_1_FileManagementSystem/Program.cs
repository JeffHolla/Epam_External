using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
    class Program
    {
        static void Main(string[] args)
        {
            LoggingState loggingState = new LoggingState();

            loggingState.StartLogging();


            //string copyFilesPath = @"..\..\ToStore\filesCopy";
            //string filesPath = @"..\..\ToStore";

            //DirectoryInfo directory = new DirectoryInfo(filesPath);

            //var tmp = directory.GetFiles();
            //string destPath = $@"{copyFilesPath}\{}";
            //string sourcePath = $"{filesPath}\\test.txt";
            //File.Copy(sourcePath, destPath);

            Console.ReadKey();
        }
    }

    public class LoggingState
    {
        string filesPath = @"..\..\ToStore";
        string copyFilesPath = @"..\..\ToStore\filesCopy";
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
        FileInfo[] files;

        public LoggingState()
        {
            filesDirectory = new DirectoryInfo(filesPath);
            copyFilesDirectory = new DirectoryInfo(copyFilesPath);
            files = filesDirectory.GetFiles();
        }

        public void StartLogging()
        {
            StateChecker()/*.Wait()*/;

            Console.WriteLine("HEY!");
            Console.ReadKey();
        }

        public /*Task */ void StateChecker()
        {
            Task[] tasksList = new Task[filesDirectory.GetFiles().Length];

            while (true)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    foreach (var file in filesDirectory.GetFiles())
                    {
                        if (files[i].Name == file.Name)
                        {
                            if (files[i].LastWriteTime != file.LastWriteTime)
                            {
                                // Так они сразу вызываются, что логично
                                //tasksList.Add(CopyNewStateFile(files[i]));

                                //var tsk = new Task(async () => await CopyNewStateFile(files[i]));
                                //tasksList[i] = (Task.Run(() => CopyNewStateFile(files[i])));

                                Task.Run(() => CopyNewStateFile(files[i])).Wait();

                                files[i] = file;
                            }
                        }
                    }
                }
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                //Console.WriteLine("== Starting tasks ==");
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                //Console.WriteLine();

                
                //for (int i = 0; i < tasksList.Count; i++)
                //{
                //    //Console.WriteLine();
                //    //Console.WriteLine(tasksList[i].AsyncState);
                //    //Console.WriteLine(tasksList[i].CreationOptions);
                //    //Console.WriteLine(tasksList[i].Status);
                //    //Console.WriteLine(tasksList[i].Id);
                //    //Console.WriteLine();

                //    tasksList[i].Start();
                //}

                //Console.WriteLine(status);

                StartAsync(ref tasksList);

                //WaiterSelf(1, tasksList).Wait();

                //WaiterSelf(3).Wait();

                //await Task.Delay(3 * 1000);
            }
        }
        public void StartAsync(ref Task[] tasks)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] is null)
                    return;
            }
            try
            {
                while (true)
                {
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        Console.WriteLine(tasks[i].AsyncState);
                        Console.WriteLine(tasks[i].CreationOptions);
                        Console.WriteLine(tasks[i].Status);
                        Console.WriteLine(tasks[i].Id);
                        Console.WriteLine();
                    }
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

            Stopwatch.StartNew();

            File.Copy(file.FullName, directoryPath + $"\\{fileNameWithDate}", true);

            Console.WriteLine(Stopwatch.Frequency);

            Console.WriteLine("===================================");
            Console.WriteLine($"File \"{file.Name}\" was backuped with name -> \"{fileNameWithDate}\"!");
            Console.WriteLine("===================================");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }

    //while (true)
    //{
    //    Console.WriteLine("=============================");
    //    files.AsParallel().ForAll(x => x.Refresh());
    //    foreach (var file in files)
    //    {
    //        Console.WriteLine($"{file} -> {file.Length}");
    //    }

    //    //Console.ReadKey();
    //}
}
