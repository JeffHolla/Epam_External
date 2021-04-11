using System;

/* Задание
 * К вам пришёл редактор модного журнала. Ему очень нужна программа, которую он описал ниже.
 * 
 * Задан английский текст. Ваша задача понять, какие слова автор «любит» больше всего и
 * подловить его на однообразности речи. Или, наоборот, похвалить за разнообразие.
 * 
 * Для каждого слова в тексте указать, сколько раз оно встречается.
 * 
 * Подумайте, имеет ли значение регистр, какие разделители могут использоваться в тексте.
 * Попробуйте использовать свои наработки из Task 1.2. «String, not Sting».
 * 
 * Ввод и вывод также придумайте сами. В рамках консоли постарайтесь создать приятный и
 * понятный интерфейс – вашей программой будет пользоваться профессионал журналистики.
 */

namespace Task_3_1_2
{
    public static class TextAnalizatorHandler
    {
        public enum InputOutput
        {
            Input,
            Output
        }

        public static void Start(string text)
        {
            TextAnalizator analizator = new TextAnalizator(text);

            MenuHandler(analizator);
        }

        public static void MenuHandler(TextAnalizator analizator)
        {
            bool isWorking = true;
            while (isWorking) {
                Console.Clear();
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1: Вывести все уникальные слова
2: Вывести слова и их количество, с которым они появляются в тексте
3: Вывести частоту появления слов в тексте, относительно всех слов
4: Вывести частоту появления слов в тексте, относительно уникальных слов
0: Выйти из программы");

                PrintMessage(messageType: InputOutput.Input);
                string command = Console.ReadLine();
                Console.Clear();
                switch (command.Trim())
                {
                    case "1":
                        PrintMessage("Вы выбрали: \"Вывести все уникальные слова\"");
                        analizator.PrintUniqueWords();
                        break;
                    case "2":
                        PrintMessage("Вы выбрали: \"Вывести слова и их количество, с которым они появляются в тексте\"");
                        analizator.PrintWordCount();
                        break;
                    case "3":
                        PrintMessage("Вы выбрали: \"Вывести частоту появления слов в тексте, относительно всех слов\"");
                        analizator.PrintWordFrequencyFromAllWords();
                        break;
                    case "4":
                        PrintMessage("Вы выбрали: \"Вывести частоту появления слов в тексте, относительно уникальных слов\"");
                        analizator.PrintWordFrequency();
                        break;
                    case "0":
                        isWorking = false;
                        continue;
                    default:
                        PrintMessage("Команда введена не верно");
                        break;
                }
                PrintMessage("Нажмите любую кнопку, чтобы очистить экран и вернуться в меню");
                Console.ReadKey();
            }
        }

        public static void PrintMessage(string message = "", InputOutput messageType = InputOutput.Output)
        {
            switch (messageType)
            {
                case InputOutput.Input:
                    Console.Write($"ВВОД: {message}");
                    break;
                case InputOutput.Output:
                    Console.WriteLine($"ВЫВОД: {message}");
                    break;
            }
        }
    }
}
