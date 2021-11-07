using System.Collections;
using System.Text.RegularExpressions;
using PortersonToJson.Entities.Output;

namespace PortersonToJson.Tests.Utilities
{
    internal class TestOutputBookComparer : IComparer
    {
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
            else if (x is Book a && y is Book b)
            {
                var id = a.Id.CompareTo(b.Id);
                if (id != 0) return id;

                var title = a.Title.CompareTo(b.Title);
                if (title != 0) return title;

                var genre = a.Genre.CompareTo(b.Genre);
                if (genre != 0) return genre;

                var price = a.Price.CompareTo(b.Price);
                if (price != 0) return price;

                var publishDate = a.PublishDate.CompareTo(b.PublishDate);
                if (publishDate != 0) return publishDate;

                var description = Sanitize(a.Description).CompareTo(Sanitize(b.Description));
                if (description != 0) return description;

                return 0;
            }
            else
            {
                return int.MaxValue;
            }
        }

        private static string Sanitize(string input)
        {
            return Regex.Replace(input, @"\s+", string.Empty);
        }
    }
}
