using System;

/* Задание
 * Расширьте массивы чисел методом, производящим действия с каждым конкретным элементом.
 * Действие должно передаваться в метод с помощью делегата.
 * 
 * Кроме указанного функционала выше, добавьте методы расширения для поиска:
 * - суммы всех элементов;
 * - среднего значения в массиве;
 * - самого часто повторяемого элемента;
 * На данном этапе LINQ использовать разрешается.
 * 
 * Консольный интерфейс-демонстрация для данного задания не обязателен, но постарайтесь
 * сделать интерфейсы ваших сущностей максимально понятными и готовыми к тестам.
 */

namespace Task_3_3_1_SuperArray
{
    public static class MyDelegates
    {
        public static int FloorDivide(int firstNum, int secondNum)
        {
            if (firstNum == 0 || secondNum == 0)
            {
                return 0;
            }

            return (int)Math.Floor((double)firstNum / secondNum);
        }

        public static int Multiply(int firstNum, int secondNum)
        {
            return firstNum * secondNum;
        }

        public static int Power(int num, int power)
        {
            return (int)Math.Pow(num, power);
        }
    }
}

