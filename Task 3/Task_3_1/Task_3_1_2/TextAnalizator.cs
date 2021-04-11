using System;
using System.Collections.Generic;
using System.Linq;

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
    public class TextAnalizator
    {
        private char[] newLineChars = Environment.NewLine.ToCharArray();
        private Dictionary<string, int> wordCount = new Dictionary<string, int>();
        public string[] Words { get; private set; }

        public string Text { set; get; }

        public TextAnalizator(string text)
        {
            Text = text;

            SplitTextToWords();

            CountWords();
        }

        private void SplitTextToWords()
        {
            var delimeters = new char[]
            { ' ' , '!', '"', ';', ':', ',', '.', '?', '(', ')', '-' }.
            Concat(newLineChars).
            ToArray();

            Words = Text.
                ToLower(). // привели к одному виду сразу весь текст
                Trim().
                Split(delimeters, StringSplitOptions.RemoveEmptyEntries).
                ToArray();
        }

        private void CountWords()
        {
            foreach (var word in Words)
            {
                if (!wordCount.ContainsKey(word))
                {
                    wordCount.Add(word, 1);
                }
                else
                {
                    wordCount[word]++;
                }
            }
        }
        
        // Не уверен какая из частот нужна
        // Выводит частоту слов относительно всех слов
        public void PrintWordFrequencyFromAllWords()
        {
            // Сортируем словарь по значению
            var sortedDictionary = wordCount.OrderByDescending(pair => pair.Value);

            foreach (var pair in sortedDictionary)
            {
                double wordFrequency = (double)pair.Value / Words.Length * 100;
                Console.WriteLine($"[{pair.Key}] : {wordFrequency : 0.00}%");
            }
        }

        // Не уверен какая из частот нужна
        // Выводит частоту слов относительно выбранных и подсчитанных слов
        public void PrintWordFrequency()
        {
            // Сортируем словарь по значению
            var sortedDictionary = wordCount.OrderByDescending(pair => pair.Value);

            foreach (var pair in sortedDictionary)
            {
                double wordFrequency = (double)pair.Value / wordCount.Count * 100;
                Console.WriteLine($"[{pair.Key}] : {wordFrequency: 0.00}%");
            }
        }

        public void PrintWordCount()
        {
            // Сортируем словарь по значению
            var sortedDictionary = wordCount.OrderByDescending(pair => pair.Value);

            foreach (var pair in sortedDictionary)
            {
                Console.WriteLine($"[{pair.Key}] : {pair.Value}");
            }
        }

        public void PrintUniqueWords()
        {
            foreach (var pair in wordCount)
            {
                Console.WriteLine($"[{pair.Key}]");
            }
        }
    }
}
