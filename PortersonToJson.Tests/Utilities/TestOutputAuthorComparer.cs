using System.Collections;
using System.Linq;
using PortersonToJson.Entities.Output;

namespace PortersonToJson.Tests.Utilities
{
    internal class TestOutputAuthorComparer : IComparer
    {
        private static readonly IComparer BookComparer = new TestOutputBookComparer();

        public int Compare(object? x, object? y)
        {
            if (x == null)
            {
                return y == null ? 0 : int.MinValue;
            }
            else if (y == null)
            {
                return int.MaxValue;
            }
            else if (x is Author a && y is Author b)
            {
                var name = a.Name.CompareTo(b.Name);
                if (name != 0) return name;

                var booksCount = a.Books.Count.CompareTo(b.Books.Count);
                if (booksCount != 0) return booksCount;

                var books = a.Books.Zip(b.Books, BookComparer.Compare).FirstOrDefault(it => it != 0);
                if (books != 0) return books;

                return 0;
            }
            else
            {
                return int.MaxValue;
            }
        }
    }
}
