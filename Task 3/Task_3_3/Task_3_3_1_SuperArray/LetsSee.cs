using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_3_3_1_SuperArray
{
    public class LetsSee
    {
        public delegate int MyExtensionDelegate(IEnumerable<int> source);
        public delegate int EveryElementDelegate(int firstElement, int secondElement);

        int[] arrExample_1 = new int[] {
            5, 7, 1, 2, 3, 8, 9, 3, 1, 0, 1, 2, 1254, 21,
            421, 124, 12, 214, 214, 561, 7, 567, 70, 0, 0, 0
        };

        int[] arrExample_2 = new int[] { 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 0, 0, 1, 2, 3, };

        int[] choosedArray;

        public void StartShow()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выбери тестовый массив:");
                Console.WriteLine($"1 : {string.Join(" ", arrExample_1)}");
                Console.WriteLine($"2 : {string.Join(" ", arrExample_2)}");

                string command = Console.ReadLine().Trim();
                switch (command)
                {
                    case "1":
                        choosedArray = arrExample_1;
                        break;
                    case "2":
                        choosedArray = arrExample_2;
                        break;
                    case "0":
                        return;
                    default:
                        continue;
                }
                TaskMenu();
            }
        }

        void DoActionsWithElements(int[] array, EveryElementDelegate function, int num)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = function.Invoke(array[i], num);
            }
        }

        void TaskMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Исходный массив :");
                Console.WriteLine($"[{string.Join(" ", choosedArray)}]");
                Console.WriteLine();

                Console.WriteLine(
                    @"Выберите какое из заданий хотите проверить:
    1 : Delegates
    2 : Extensions
    0 : Вернуться назад");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        DelegateTaskMenu();
                        break;
                    case "2":
                        ExtensionsMenu();
                        break;
                    case "0":
                        return;
                    default:
                        continue;
                }
            }
        }

        void ExtensionsMenu()
        {
            while (true)
            {
                var justToShowOnCopy = choosedArray.ToArray();
                Console.Clear();

                Console.WriteLine("Исходный массив :");
                Console.WriteLine($"[{string.Join(" ", choosedArray)}]");
                Console.WriteLine();

                Console.WriteLine(
                    @"Выберите какое из заданий хотите проверить:
    1 : Сумма
    2 : Среднее значение
    3 : Первый найденный элемент с наибольшим количеством вхождений
    4 : Частота всех элементов
    0 : Вернуться назад");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        Console.WriteLine(justToShowOnCopy.MySum());
                        break;
                    case "2":
                        Console.WriteLine(justToShowOnCopy.MyAverage());
                        break;
                    case "3":
                        Console.WriteLine(justToShowOnCopy.MyMostCountedElement());
                        break;
                    case "4":
                        foreach (var item in justToShowOnCopy.MyElementFrequency())
                        {
                            Console.WriteLine($"[{item.Key} = {item.Value}]");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        continue;
                }

                Console.WriteLine("Нажми на любую кнопку, чтобы продолжить");
                Console.ReadKey();
            }
        }
        
        void DelegateTaskMenu()
        {
            while (true)
            {
                var justToShowOnCopy = choosedArray.ToArray();
                Console.Clear();

                Console.WriteLine("Исходный массив :");
                Console.WriteLine($"[{string.Join(" ", choosedArray)}]");
                Console.WriteLine();

                Console.WriteLine(
                    @"Выберите какое из заданий хотите проверить:
    1 : FloorDivide на 2
    2 : Multiply на 2 
    3 : Power в 2
    0 : Вернуться назад");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        DoActionsWithElements(justToShowOnCopy, new EveryElementDelegate(MyDelegates.FloorDivide), 2);
                        Console.WriteLine(string.Join(" ", justToShowOnCopy));
                        break;
                    case "2":
                        DoActionsWithElements(justToShowOnCopy, new EveryElementDelegate(MyDelegates.Multiply), 2);
                        Console.WriteLine(string.Join(" ", justToShowOnCopy));
                        break;
                    case "3":
                        DoActionsWithElements(justToShowOnCopy, new EveryElementDelegate(MyDelegates.Power), 2);
                        Console.WriteLine(string.Join(" ", justToShowOnCopy));
                        break;
                    case "0":
                        return;
                    default:
                        continue;
                }

                Console.WriteLine("Нажми на любую кнопку, чтобы продолжить");
                Console.ReadKey();
            }
        }
    }
}

