using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Расширьте строку следующим методом:
 * - проверка, на каком языке написано слово в строке. Ограничимся четырьмя вариантами – Russian,
 * English, Number and Mixed. Совокупность нескольких слов, микс символов или букв (из разных
 * языков) относить к последней категории. 
 * 
 * Если в строке имеются пробелы, знаки препинания и прочие символы – 
 * можете также откидывать к последней категории. Словом на русском языке
 * считайте любую последовательность русских символов (АаАа - подходит). На английском –
 * аналогично, но с англоязычными символами. *  * Использование LINQ и наличие интерфейса – аналогично предыдущему заданию.
 */

namespace Task_3_3_2_SuperString
{
    public delegate bool CheckStyle(string text);

    class Program
    {
        static void Main(string[] args)
        {
            string ruStr = "Тестовая";
            string engStr = "Test";
            string num = "1521";
            string mixed_1 = "Petr1";
            string mixed_2 = "Petr!";
            string mixed_3 = "Petr Первый";
            string mixed_4 = "Petr Perviy";
            string mixed_5 = "PetrПервый";
            string[] wordList = new string[] {
                ruStr, engStr, num, mixed_1, mixed_2, mixed_3, mixed_4, mixed_5
            };

            foreach (var word in wordList)
            {
                Console.WriteLine($"Word is = {word}");
                Console.WriteLine($"   Check is = {"IsRussian",10} == {word.IsRussian()}");
                Console.WriteLine($"   Check is = {"IsEnglish", 10} == {word.IsEnglish()}");
                Console.WriteLine($"   Check is = {"IsNumber",10} == {word.IsNumber()}");
                Console.WriteLine($"   Check is = {"IsMixed",10} == {word.IsMixed()}");
                Console.WriteLine("===========================================");
                Console.WriteLine();
            }

            
            // Если есть желание глянуть делегат версию)
            //FastCheckWithDelegates(wordList);

            Console.ReadKey();
        }

        // Просто проверка. Здесь фактически методы расширения не используются как методы расширения,
        // поэтому этот метод можно просто рассматривать как проверку всех методов из класса с методами
        // расширений через делегаты :)
        static void FastCheckWithDelegates(string[] wordList)
        {
            CheckStyle[] checks = new CheckStyle[] {
                MyExtensions.IsRussian, MyExtensions.IsEnglish, MyExtensions.IsNumber, MyExtensions.IsMixed
            };

            //for (int i = 0; i < wordList.Length; i++)
            //{
            //    Console.WriteLine($"Word is = {wordList[i]}");
            //    for (int j = 0; j < checks.Length; j++)
            //    {
            //        Console.WriteLine(
            //            $"   Check is = {checks[j].Method.Name,10} == {checks[j].Invoke(wordList[i])}");
            //        //Console.WriteLine($"   Result is = {checks[j].Invoke(wordList[i])}");
            //    }
            //    Console.WriteLine("===========================================");
            //    Console.WriteLine();
            //}

            // Refactored
            foreach (var word in wordList)
            {
                Console.WriteLine($"Word is = {word}");
                foreach (var check in checks)
                {
                    Console.WriteLine($"   Check is = {check.Method.Name,10} == {check.Invoke(word)}");
                }
                Console.WriteLine("===========================================");
                Console.WriteLine();
            }
        }
    }
}
