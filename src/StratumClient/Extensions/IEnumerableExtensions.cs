using System;
using System.Collections.Generic;
using System.Linq;

namespace Stratum.Extensions
{
    internal static class IEnumerableExtensions
    {
        private static Random _random = new Random();

        public static T Random<T>(this IEnumerable<T> ts)
        {
            return ts.Skip(_random.Next(0, ts.Count())).First();
        }
    }
}