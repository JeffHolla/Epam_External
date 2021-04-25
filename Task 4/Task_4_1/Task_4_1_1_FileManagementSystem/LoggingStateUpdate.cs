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
            StateChecker();
        }

        //Вспомогательная функция для проверки логов -> Добавляет одну строчку к каждому файлу в store директории каждые seconds
        public void AddLineToAllFiles(int seconds)
        {
            // Блок дополнения файлов
            Thread.Sleep(seconds * 1000);
            Process.Start(@"..\..\StoringFolder\ScriptsBat\AddOneLine_RecursiveDir.bat");
        }

        public void UpdateListOfFiles()
        {
            _files = _filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories);
        }

        private void StateChecker()
        {
            while (true)
            {
                // Чтобы не триггерить ядро процессора и в принципе, чтобы создать задержку обновлений
                Thread.Sleep(1000);
                for (int i = 0; i < _files.Length; i++)
                {
                    foreach (var file in _filesDirectory.GetFiles("*.txt", SearchOption.AllDirectories))
                    {
                        if (_files[i].Name == file.Name && _files[i].FullName == file.FullName)
                        {
                            if (_files[i].LastWriteTime != file.LastWriteTime)
                            {
                                Thread t = new Thread(new ParameterizedThreadStart(StartCreatingBackup));
                                //t.Name = "StartCreatingBackup -> " + DateTime.Now.ToShortTimeString();
                                t.Start(_files[i]);
                                
                                _files[i] = file;
                            }
                        }
                    }
                }
            }
        }

        // Подготовка для старта записи копии файла и его метаИнформации
        private void StartCreatingBackup(object fileObj)
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

            string fileNameWithDate = $"{justFileName}_{file.LastWriteTime.ToString().Replace(":", "!")}";

            // Интернируем строку, т.к. пользоваться ей мы будем много раз
            string.Intern(directoryPath);

            // Копируем файл в папку для копий
            Thread copyThread = new Thread(() => CopyNewStateFile(file, directoryPath, $"{fileNameWithDate}.txt"));
            //copyThread.Name = "copyThread -> " + DateTime.Now.ToShortTimeString();
            copyThread.Start();

            // Создаём для него МетаИнфу
            Thread metaThread = new Thread(() => CreateMetaInformationForFile(file, directoryPath, $".{fileNameWithDate}"));
            //metaThread.Name = "metaThread-> " + DateTime.Now.ToShortTimeString();
            metaThread.Start();
        }

        // Функция копирования файлов
        private void CopyNewStateFile(FileInfo file, string directoryPath, string fileNameWithDate)
        {
            // Копируем файл в путь
            File.Copy(file.FullName, directoryPath + $"\\{fileNameWithDate}", true);

            Console.WriteLine("-------------------------" +
                Environment.NewLine +
                $"File \"{file.Name}\" was backuped with name -> \"{fileNameWithDate}\"!" +
                Environment.NewLine +
                "-------------------------");
        }

        // Функция создания метаИнфы
        private void CreateMetaInformationForFile(FileInfo file, string directoryPath, string fileNameWithDate)
        {
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
        }
    }
}
