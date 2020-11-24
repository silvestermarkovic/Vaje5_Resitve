using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naloga1
{
    public static class Extensions
    {
        public static void ReadEnumerable<T>(this IEnumerable<T> list)
        {
            Console.Write("Elementi seznama so: ");
            int count = 0;
            foreach (var item in list)
            {
                count++;
                Console.WriteLine(item.ToString() + $"{(count == list.Count() ? Environment.NewLine : ",")} ");
            }
            Console.WriteLine();
        }
    }
}
