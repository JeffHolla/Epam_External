using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
        List<Thread> myThreads = new List<Thread>();
        // Пути, нужные для работы программы
        string _filesPath = @"..\..\StoringFolder\ToStore";
        string _copyFilesPath = @"..\..\StoringFolder\SystemInformation\filesCopy";
        // Экземпляры классов для директорий
        DirectoryInfo _filesDirectory;
        DirectoryInfo _copyFilesDirectory;
        // Все файлы из директорий
        FileInfo[] _files;

        public LoggingStateUpdate()
        {
            _filesDirectory = new DirectoryInfo(_filesPath);
            _copyFilesDirectory = new DirectoryInfo(_copyFilesPath);
            _files = _filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories);
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
                //Task.Delay(1000).Wait();
                Thread.Sleep(1000);
                for (int i = 0; i < _files.Length; i++)
                {
                    foreach (var file in _filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories))
                    {
                        if (_files[i].Name == file.Name && _files[i].FullName == file.FullName)
                        {
                            if (_files[i].LastWriteTime != file.LastWriteTime)
                            {
                                // Так они сразу вызываются, что логично
                                // TODO: Переделать под потоки
                                //tasksList.Add(StartCreatingBackup(_files[i]));

                                Thread t = new Thread(new ParameterizedThreadStart(StartCreatingBackup));
                                t.Name = "StartCreatingBackup -> " + DateTime.Now.ToShortTimeString();
                                t.Start(_files[i]);
                                myThreads.Add(t);

                                //StartCreatingBackup(_files[i]);

                                _files[i] = file;
                            }
                        }
                    }
                }

                //Console.WriteLine(DateTime.Now.ToLongTimeString());


                // Блок дополнения файлов
                //Task.Delay(30000).Wait();
                //Process.Start(@"..\..\StoringFolder\ScriptsBat\AddOneLine_RecursiveDir.bat");

                //await Task.Delay(3 * 1000);

                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                
                Console.WriteLine("-------------------------" + 
                    Environment.NewLine + 
                    $"All Threads = {Process.GetCurrentProcess().Threads.Count}" +
                    Environment.NewLine +
                "-------------------------");

                myThreads = myThreads.Where(tr => tr.ThreadState == System.Threading.ThreadState.Running).ToList();
            }
        }

        // Работающий ожидатель
        public async Task WaiterSelf(int i)
        {
            await Task.Delay(i * 1000);
            Console.WriteLine($"Waited [{i}]");
        }

        public /*Task*/ void StartCreatingBackup(object fileObj)
        {
            FileInfo file = (FileInfo)fileObj;
            // Вытаскиваем имя файла без txt
            string justFileName = file.Name.Replace(".txt", "");

            // Получаем полный путь для папки файла
            string directoryPath;
            if (file.DirectoryName != _filesDirectory.FullName)
            {
                directoryPath = $"{_copyFilesDirectory.FullName}\\" +
                    $"{file.DirectoryName.Substring(_filesDirectory.FullName.Length + 1)}";
            }
            else
            {
                directoryPath = $"{_copyFilesDirectory.FullName}\\.rootDir";
            }
            directoryPath += $"\\{justFileName}__file";

            // Если путь не существует, то создаём новый
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileNameWithDate = $"{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}.txt";

            // Интернируем строку, т.к. пользоваться ей мы будем много раз
            string.Intern(directoryPath);

            
            Console.ForegroundColor = ConsoleColor.Red;
            // Копируем файл в папку для копий
            Thread copyThread = new Thread(() => CopyNewStateFile(file, directoryPath, fileNameWithDate));
            copyThread.Name = "copyThread -> " + DateTime.Now.ToShortTimeString();
            myThreads.Add(copyThread);
            copyThread.Start();
            // Создаём для него МетаИнфу
            Thread metaThread = new Thread(() => CreateMetaInformationForFile(file, directoryPath, $".{fileNameWithDate}"));
            metaThread.Name = "metaThread-> " + DateTime.Now.ToShortTimeString();
            myThreads.Add(metaThread);
            metaThread.Start();
            //Task.WaitAll(copyTask, metaInfTask);
            //return Task.CompletedTask;
        }

        // Функция копирования файлов
        public /*Task*/ void CopyNewStateFile(FileInfo file, string directoryPath, string fileNameWithDate)
        {
            Console.WriteLine($"Поток номер -> {Thread.CurrentThread.ManagedThreadId} " +
                $"| Имя потока -> {Thread.CurrentThread.Name}");

            // Копируем файл в путь
            File.Copy(file.FullName, directoryPath + $"\\{fileNameWithDate}", true);


            Console.WriteLine("-------------------------" + 
                Environment.NewLine + 
                $"File \"{file.Name}\" was backuped with name -> \"{fileNameWithDate}\"!" + 
                Environment.NewLine +
                "-------------------------");

            //return Task.CompletedTask;
        }

        // Функция создания метаИнфы
        public /*Task*/ void CreateMetaInformationForFile(FileInfo file, string directoryPath, string fileNameWithDate)
        {
            Console.WriteLine($"Поток номер -> {Thread.CurrentThread.ManagedThreadId} " +
                $"| Имя потока -> {Thread.CurrentThread.Name}");

            // Создаём файл МетаИнфы и записываем:
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
            
            Console.WriteLine("-------------------------" +
                Environment.NewLine +
                $"For file \"{file.Name}\" was created meta information -> \"{fileNameWithDate}\"!" +
                Environment.NewLine +
                "-------------------------");

            //return Task.CompletedTask;
        }
    }
}
