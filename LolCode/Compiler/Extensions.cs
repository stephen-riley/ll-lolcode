using System;
using System.Collections.Generic;

namespace LolCode.Compiler
{
    public static class Extensions
    {
        public static void Apply<T>(this IList<T> list, Action<T> lambda)
        {
            foreach (var el in list)
            {
                lambda(el);
            }
        }
    }
}