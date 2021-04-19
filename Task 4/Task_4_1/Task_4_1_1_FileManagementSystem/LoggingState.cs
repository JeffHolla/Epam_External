using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


// Реализация основанная на сохранении имени в названии файла.
// Операция восстановления файлов хоть и проста, но не позволяет использовать себя, если вдруг пользователь решит использовать

namespace Task_4_1_1_FileManagementSystem
{
    public class LoggingState
    {
        string filesPath = @"..\..\StoringFolder\ToStore";
        string copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        DirectoryInfo filesDirectory;
        DirectoryInfo copyFilesDirectory;
        FileInfo[] files;

        public LoggingState()
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
