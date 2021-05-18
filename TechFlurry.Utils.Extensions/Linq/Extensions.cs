using System;
using System.Collections.Generic;
using System.Linq;

namespace TechFlurry.Utils.Extensions.Linq
{
    public static class Extensions
    {
        private static readonly Random _random = new Random();
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int size)
        {

            Dictionary<double, T> randomSortTable = new Dictionary<double, T>();
            foreach (var item in enumerable)
            {
                randomSortTable.Add(_random.NextDouble(), item);
            }
            return randomSortTable.OrderBy(x => x.Key)
                                    .Take(size)
                                    .Select(x => x.Value);
        }
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, Func<T, bool> samplingCondition, int size = int.MaxValue)
        {

            Dictionary<double, T> randomSortTable = new Dictionary<double, T>();
            foreach (var item in enumerable.Where(x => samplingCondition.Invoke(x)))
            {
                randomSortTable.Add(_random.NextDouble(), item);
            }
            return randomSortTable.OrderBy(x => x.Key)
                                    .Take(size <= randomSortTable.Count
                                                ? size : randomSortTable.Count)
                                    .Select(x => x.Value);
        }
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
