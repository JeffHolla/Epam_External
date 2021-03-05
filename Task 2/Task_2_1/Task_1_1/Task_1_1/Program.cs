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
    class Program
    {
        static void Main(string[] args)
        {
            char[] arr = new char[] { '1', '2', '3', '4', '5' };
            CustomString customStr = new CustomString(arr);
            
            customStr = customStr.Insert(2, 'J');
            Console.WriteLine(customStr);

            customStr = customStr.Remove(2);
            Console.WriteLine(customStr);

            Console.WriteLine();

            for (int i = 0; i < customStr.Length; i++)
            {
                Console.Write(customStr[i]);
            }
            Console.WriteLine();

            Console.WriteLine(customStr.FindFirstChar('1'));

            Console.WriteLine(customStr.FindLastChar('2'));

            char[] test = customStr.ToArray();

            foreach (var item in customStr.ToArray())
            {
                Console.Write(item + "|");
            }

            Console.ReadKey();
        }
    }

    ///* Просто записанный сюда класс. Сам класс используется из под dll.
    // * сравнение                        -- done
    // * конкатенация                     -- done
    // * поиск символов                   -- done?
    // * конвертация из массив            -- done
    // * конвертация в массив символов    -- done
    // */
    //class CustomString
    //{
    //    private char[] custStr;

    //    public int Length { get { return custStr.Length; } }

    //    public CustomString()
    //    {
    //        custStr = new char[25];
    //    }

    //    public CustomString(int capacity)
    //    {
    //        custStr = new char[capacity];
    //    }

    //    public CustomString(char[] array)
    //    {
    //        custStr = new char[array.Length];

    //        for (int i = 0; i < array.Length; i++)
    //        {
    //            custStr[i] = array[i];
    //        }
    //    }


    //    public char this[int index] {
    //        get {
    //            if (index > custStr.Length || index < 0)
    //                throw new IndexOutOfRangeException("Неверный индекс строки");
    //            else
    //                return custStr[index];
    //        }
    //        set {
    //            if (index > custStr.Length || index < 0)
    //                throw new IndexOutOfRangeException("Неверный индекс строки");
    //            else
    //                custStr[index] = value;
    //        }
    //    }

    //    public char FindFirstChar(char chr)
    //    {
    //        for (int i = 0; i < this.Length; i++)
    //        {
    //            if (custStr[i] == chr)
    //                return custStr[i];
    //        }
            
    //        throw new InvalidOperationException("Введённый элемент не найден");
    //    }

    //    public char[] ToArray()
    //    {
    //        char[] toReturn = new char[custStr.Length];

    //        for (int i = 0; i < custStr.Length; i++)
    //        {
    //            toReturn[i] = custStr[i];
    //        }

    //        return toReturn;
    //    }


    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null)
    //            return false;

    //        CustomString custStr = obj as CustomString;
    //        if (custStr as CustomString == null)
    //            return false;

    //        return this == custStr;
    //    }

    //    private static bool IsEqual(CustomString str1, CustomString str2)
    //    {
    //        bool isEqual = true;

    //        for (int i = 0; i < str1.Length; i++)
    //        {
    //            if (str1[i] != str2[i])
    //                isEqual = false;
    //        }

    //        return isEqual;
    //    }

    //    public static bool operator ==(CustomString str1, CustomString str2)
    //    {
    //        if (str1.Length != str2.Length)
    //            return false;

    //        return IsEqual(str1, str2) ? true : false;
    //    }

    //    public static bool operator !=(CustomString str1, CustomString str2)
    //    {
    //        if (str1.Length != str2.Length)
    //            return true;

    //        return IsEqual(str1, str2) ? false : true;
    //    }

    //    public static CustomString operator +(CustomString str1, CustomString str2)
    //    {
    //        char[] tmpStr = new char[str1.Length + str2.Length];

    //        int currIndex = 0;

    //        for (int i = 0; i < str1.Length; i++, currIndex++)
    //        {
    //            tmpStr[currIndex] = str1[i];
    //        }

    //        for (int i = 0; i < str2.Length; i++, currIndex++)
    //        {
    //            tmpStr[currIndex] = str2[i];
    //        }

    //        return new CustomString(tmpStr);
    //    }

    //}
}
