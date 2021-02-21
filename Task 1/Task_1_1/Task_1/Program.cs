using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //T_1_1_1_Rectangle();
            //T_1_1_2_Triangle();
            //T_1_1_3_AnotherTriangle();
            //T_1_1_4_XMAS_TREE();
            //T_1_1_5_SUM_OF_NUMBERS();
            //T_1_1_6_FONT_ADJUSTMENT();
            //T_1_1_7_ARRAY_PROCESSING();
            //T_1_1_8_NO_POSITIVE();
            //T_1_1_9_NON_NEGATIVE_SUM();
            //T_1_1_10_2D_ARRAY();

            string currentCommand = "";
            while (currentCommand != "0")
            {
                Console.Clear();
                Console.WriteLine(
                    @"Введите комманду : 
    1: T_1_1_1_Rectangle
    2: T_1_1_2_Triangle
    3: T_1_1_3_AnotherTriangle
    4: T_1_1_4_XMAS_TREE
    5: T_1_1_5_SUM_OF_NUMBERS
    6: T_1_1_6_FONT_ADJUSTMENT
    7: T_1_1_7_ARRAY_PROCESSING
    8: T_1_1_8_NO_POSITIVE
    9: T_1_1_9_NON_NEGATIVE_SUM
    10: T_1_1_10_2D_ARRAY
    0: Exit
"
);
                currentCommand = Console.ReadLine();
                switch (currentCommand)
                {
                    case "1":
                        T_1_1_1_Rectangle();
                        break;
                    case "2":
                        T_1_1_2_Triangle();
                        break;
                    case "3":
                        T_1_1_3_AnotherTriangle();
                        break;
                    case "4":
                        T_1_1_4_XMAS_TREE();
                        break;
                    case "5":
                        T_1_1_5_SUM_OF_NUMBERS();
                        break;
                    case "6":
                        T_1_1_6_FONT_ADJUSTMENT();
                        break;
                    case "7":
                        T_1_1_7_ARRAY_PROCESSING();
                        break;
                    case "8":
                        T_1_1_8_NO_POSITIVE();
                        break;
                    case "9":
                        T_1_1_9_NON_NEGATIVE_SUM();
                        break;
                    case "10":
                        T_1_1_10_2D_ARRAY();
                        break;

                    case "0":
                        currentCommand = "0";
                        break;
                }
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        // Написать программу, которая определяет площадь прямоугольника со сторонами a и b. 
        // Если пользователь вводит некорректные значения (отрицательные или ноль), 
        // должно выдаваться сообщение об ошибке. 
        // Возможность ввода пользователем строки вида «абвгд» или нецелых чисел игнорировать.
        static void T_1_1_1_Rectangle()
        {
            Console.Write("Введите сторону a = ");
            int a = T_1_1_1_Rectangle__Helper(Console.ReadLine());
            if (a == -1)
                return;

            Console.Write("Введите сторону b = ");
            int b = T_1_1_1_Rectangle__Helper(Console.ReadLine());
            if (b == -1)
                return;
            // Можно сделать это тернарником, но не уверен что это читабельнее

            Console.WriteLine($"Площадь прямоугольника со сторонами {a} и {b} равна {a * b}");
        }

        // Чтобы не дублировать одну и ту же проверку
        static int T_1_1_1_Rectangle__Helper(string strNum)
        {
            int num = 0;

            if (int.TryParse(strNum, out num) == false)
            {
                Console.WriteLine("Введено число неверного формата!");
                return -1;
            }
            else
                if (num <= 0)
            {
                Console.WriteLine("Число должно быть больше нуля");
                return -1;
            }

            return num;
        }

        // Написать программу, которая запрашивает с клавиатуры число N и выводит 
        // на экран следующее «изображение», состоящее из N строк:
        static void T_1_1_2_Triangle()
        {
            Console.WriteLine("Введите N");
            int N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        static void T_1_1_3_AnotherTriangle()
        {
            Console.WriteLine("Введите N");
            int N = int.Parse(Console.ReadLine());
            Console.Clear();

            // Цикл для N строк дерева
            for (int i = 0; i < N; i++)
            {
                // Цикл для пробелов слева от дерева
                for (int j = 0; j < N - i - 1; j++)
                {
                    Console.Write(" ");
                }

                // Цикл для левой части дерева
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write("*");
                }

                // Цикл для правой части дерева
                for (int j = 0; j < i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        // Написать программу, которая запрашивает с клавиатуры число N 
        // и выводит на экран следующее «изображение», состоящее из N треугольников:
        static void T_1_1_4_XMAS_TREE()
        {
            Console.WriteLine("Введите N");
            int N = int.Parse(Console.ReadLine());
            Console.Clear();

            // Количество полуёло
            for (int k = N; k >= 0; k--)
            {
                // Цикл для N-k строк дерева
                for (int i = 0; i < N - k; i++)
                {
                    // Цикл для пробелов слева от дерева
                    for (int j = 0; j < N - i - 1; j++)
                    {
                        Console.Write(" ");
                    }

                    // Цикл для левой части дерева
                    for (int j = 0; j < i + 1; j++)
                    {
                        Console.Write("*");
                    }

                    // Цикл для правой части дерева
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }

            }
        }

        // Если выписать все натуральные числа меньше 10, кратные 3 или 5, то получим 3, 5, 6 и 9. 
        // Сумма этих чисел будет равна 23. 
        // Напишите программу, которая выводит на экран сумму всех чисел меньше 1000, кратных 3 или 5.
        static void T_1_1_5_SUM_OF_NUMBERS()
        {
            int sum = 0;
            List<int> lst = new List<int>();
            for (int num = 3; num <= 1000; num++)
            {
                if (num % 3 == 0 || num % 5 == 0)
                {
                    lst.Add(num);
                    sum += num;
                }
            }
            Console.WriteLine(sum);

            //for (int i = 0; i < lst.Count; i++)
            //{
            //    Console.Write($"{lst[i]} ");
            //}
        }

        // Для форматирования текста надписи можно использовать различные начертания:
        // полужирное, курсивное и подчёркнутое, а также их сочетания.
        // Предложите способ хранения информации о форматировании текста надписи и напишите программу,
        // которая позволяет устанавливать и изменять начертание:
        static void T_1_1_6_FONT_ADJUSTMENT()
        {
            Dictionary<string, bool> fontSettings = new Dictionary<string, bool>();

            fontSettings.Add("Bold", false);
            fontSettings.Add("Italic", false);
            fontSettings.Add("Underline", true);

            string currentCommand = "";
            while (currentCommand != "0")
            {
                Console.Write("Параметры надписи: ");

                // Выбираем активные из словаря для вывода
                var activeSettings = fontSettings.Where(x => x.Value == true).Select(x => x.Key).ToList();
                if (activeSettings.Count != 0)
                {
                    foreach (var setting in activeSettings)
                    {
                        Console.Write($"{setting} ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("None");
                }

                Console.WriteLine(
                    $@"Введите:
        1: bold
        2: italic
        3: underline
        0: exit"
                );

                // Меню выбора
                currentCommand = Console.ReadLine();
                switch (currentCommand)
                {
                    case "1":
                        fontSettings["Bold"] = fontSettings["Bold"] ? false : true;
                        break;

                    case "2":
                        fontSettings["Italic"] = fontSettings["Italic"] ? false : true;
                        break;

                    case "3":
                        fontSettings["Underline"] = fontSettings["Underline"] ? false : true;
                        break;

                    default:
                        break;
                }

                if (currentCommand == "0")
                    break;
            }
        }

        // Написать программу, которая генерирует случайным образом элементы массива 
        // (число элементов в массиве и их тип определяются разработчиком), 
        // определяет для него максимальное и минимальное значения, сортирует 
        // массив и выводит полученный результат на экран.
        //
        // Примечание: LINQ запросы и готовые функции языка(Sort, Max и т.д.) 
        // использовать в данном задании запрещается.
        static void T_1_1_7_ARRAY_PROCESSING()
        {
            List<int> arr = new List<int>();
            //arr.AddRange(new int[] { 5, 7, 2, 1, 4, 6, 3 });

            // Генератор псевдослучайных величин с сидом раз уж нельзя использовать встроенные функции языка :)
            for (int i = 0; i < 30; i++)
            {
                int a = 45;
                int c = 21;
                int m = 151;
                int seed = i * 5;
                seed = (a * seed + c) % m;

                if (arr.Contains(seed) == false)
                {
                    arr.Add(seed);
                }
                else
                {
                    seed = (a * (seed * 15) % 31 + c) % m * 1000;
                    arr.Add(seed);
                }
            }

            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }

            // Min/Max Values
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] > maxValue)
                    maxValue = arr[i];
                if (arr[i] < minValue)
                    minValue = arr[i];
            }

            // Сортировка(по сути неоптимальная выборка)
            for (int i = 0; i < arr.Count - 1; i++)
            {
                for (int j = i; j < arr.Count; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }

            // Вывод
            Console.WriteLine();
            Console.WriteLine("Sorted:");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }

        // Написать программу, которая заменяет все положительные элементы в трёхмерном массиве на нули. 
        // Число элементов в массиве и их тип определяются разработчиком.
        static void T_1_1_8_NO_POSITIVE()
        {
            Random rnd = new Random(42);

            int[,,] thirdDim = new int[5, 5, 5];

            // Заполнение 3d массива
            for (int i = 0; i < thirdDim.GetLength(0); i++)
            {
                for (int j = 0; j < thirdDim.GetLength(1); j++)
                {
                    for (int z = 0; z < thirdDim.GetLength(2); z++)
                    {
                        thirdDim[i, j, z] = rnd.Next(-5, 5);
                    }
                }
            }

            // Замена положительных элементов на 0
            for (int i = 0; i < thirdDim.GetLength(0); i++)
            {
                for (int j = 0; j < thirdDim.GetLength(1); j++)
                {
                    for (int z = 0; z < thirdDim.GetLength(2); z++)
                    {
                        if (thirdDim[i, j, z] > 0)
                            thirdDim[i, j, z] = 0;
                    }
                }
            }

            // Не стал выводить трёхмерный массив т.к. в этом мало смысла, но можно вывести его 2d части.
        }

        // Написать программу, которая определяет сумму неотрицательных элементов в одномерном массиве. 
        // Число элементов в массиве и их тип определяются разработчиком.
        static void T_1_1_9_NON_NEGATIVE_SUM()
        {
            List<int> arr = new List<int>();
            Random rnd = new Random(5);

            for (int i = 0; i < 20; i++)
            {
                arr.Add(rnd.Next(-5, 5));
            }

            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Сумма положительных чисел = " + arr.Where(x => x > 0).Sum());
        }

        // Элемент двумерного массива считается стоящим на чётной позиции, если сумма
        // номеров его позиций по обеим размерностям является чётным числом 
        // (например, [1,1] — чётная позиция, а [1,2] — нет). 
        // Определить сумму элементов массива, стоящих на чётных позициях.
        static void T_1_1_10_2D_ARRAY()// А должна ли считаться [0, 0] как чётная позиция?
        {
            int[,] arr = new int[5, 5];
            Random rnd = new Random(5);

            // Случайное заполнение
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = rnd.Next(10);
                }
            }

            // Вывод
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

            // Подсчитывание суммы
            int sum = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i + j % 2 == 0)
                        sum += arr[i, j];
                }
            }

            Console.WriteLine("Сумма элементов на чётных позициях = " + sum);
        }
    }
}
