using System.Collections.Generic;
using System.Linq;


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

