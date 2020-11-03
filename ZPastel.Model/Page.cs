using System.Collections.Generic;

namespace ZPastel.Model
{
    public class Page<T> where T : class
    {
        public IReadOnlyList<T> Items { get; }
        public bool HasMore { get; }

        public Page(IReadOnlyList<T> items, bool hasMore)
        {
            Items = items;
            HasMore = hasMore;
        }
    }
}
