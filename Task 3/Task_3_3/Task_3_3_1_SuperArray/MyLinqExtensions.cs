
using System.Collections.Generic;
using System.Linq;

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
    public static class MyLinqExtensions
    {
        // Ну раз разрешён Linq :D
        public static int MySum(this IEnumerable<int> source)
        {
            return source.Sum();
        }

        public static double MyAverage(this IEnumerable<int> source)
        {
            return source.Average();
        }

        public static int MyMostCountedElement(this IEnumerable<int> source)
        {
            var pairElementCount = source.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            var maxCount = pairElementCount.Max(x => x.Value);

            return pairElementCount.Where(x => x.Value == maxCount).
                First().Key;
        }

        public static Dictionary<int, int> MyElementFrequency(this IEnumerable<int> source)
        {
            return source.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}

