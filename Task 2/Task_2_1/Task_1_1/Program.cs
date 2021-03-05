using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomStrings;


/*
 * Напишите собственный класс, описывающий строку как массив символов. Реализуйте для этого
 * класса типовые операции (сравнение, конкатенация, поиск символов, конвертация из/в массив
 * символов). Подумайте, какие функции вы бы добавили к имеющемуся в .NET функционалу строк
 * (достаточно 1-2 функций).
 * Вариант со * - подумайте над использованием в своем классе функционала индексатора
 * (indexer). Реализуйте его для своей строки.
 * Вариант с ** - попробуйте создать из своей сборки переносимую библиотеку (DLL). Осмысленно
 * назовите её, а также namespace и сам класс. Попробуйте использовать написанный вами класс в
 * другом проекте.
 */
namespace Task_1_1
{
    // * Класс используется из под dll.
    // * сравнение                        -- done
    // * конкатенация                     -- done
    // * поиск символов                   -- done?
    // * конвертация из массив            -- done
    // * конвертация в массив символов    -- done
    class Program
    {
        static void Main(string[] args)
        {
            char[] arr = new char[] { '1', '2', '3', '4', '5' };
            CustomString customStr = new CustomString(arr);
            
            // Демонстрация вставки
            customStr = customStr.Insert(2, 'J');
            Console.WriteLine(customStr);

            // Демонстрация удаления
            customStr = customStr.Remove(2);
            Console.WriteLine(customStr);

            Console.WriteLine();

            // Демонстрация индексатора
            for (int i = 0; i < customStr.Length; i++)
            {
                Console.Write(customStr[i]);
            }
            Console.WriteLine();

            // Демонстрация первого найденного символа
            Console.WriteLine(customStr.FindFirstChar('1'));

            // Демонстрация последнего найденного символа (или первого найденного с конца)
            Console.WriteLine(customStr.FindLastChar('2'));

            // Демонстрация преобразования в Массив char
            foreach (var item in customStr.ToArray())
            {
                Console.Write(item + "|");
            }
            Console.WriteLine();

            // Демонстрация конкатенации
            CustomString cstmStr = new CustomString(new char[] { 'H', 'e', 'y', '!' });
            Console.WriteLine(cstmStr + customStr);

            // Демонстрация сравнения
            Console.WriteLine(cstmStr == customStr);

            CustomString equalStrToFirst = new CustomString(new char[] { '1', '2', '3', '4', '5' });
            Console.WriteLine(equalStrToFirst == customStr);

            Console.ReadKey();
        }
    }   
}

