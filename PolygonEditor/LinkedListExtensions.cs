using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor
{
    public static class LinkedListExtensions
    {
        public static void AppendRange<T>(this LinkedList<T> source, IEnumerable<T> items)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (T item in items)
            {
                source.AddLast(item);
            }
        }

        public static void PrependRange<T>(this LinkedList<T> source, IEnumerable<T> items)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            LinkedListNode<T> first = source.First;
            foreach (T item in items)
            {
                source.AddBefore(first, item);
            }
        }
    }
}
