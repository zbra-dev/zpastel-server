using System.Collections.Generic;

namespace ZPastel.API.Resources
{
    public class PageResource<T> where T : class
    {
        public IReadOnlyList<T> Items { get; }
        public bool HasMore { get; }

        public PageResource(IReadOnlyList<T> items, bool hasMore)
        {
            Items = items;
            HasMore = hasMore;
        }
    }
}
