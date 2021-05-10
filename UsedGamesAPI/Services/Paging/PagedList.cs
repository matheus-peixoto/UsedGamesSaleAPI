using System.Collections.Generic;
using System.Linq;

namespace UsedGamesAPI.Services.Paging
{
    public class PagedList<T> : List<T>
    {
        const int defaultLimit = 50;
        const int defaultOffset = 0;
        public int Limit { get; private set; }
        public int Offset { get; private set; }

        public PagedList()
        {
            Limit = defaultLimit;
            Offset = defaultOffset;
        }

        public PagedList(List<T> items, int limit, int offset)
        {
            Limit = limit;
            Offset = offset;

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(List<T> orderedList)
        {
            orderedList = orderedList.Skip(defaultOffset).Take(defaultLimit).ToList();
            return new PagedList<T>(orderedList, defaultLimit, defaultOffset);
        }

        public static PagedList<T> ToPagedList(List<T> orderedList, int limit, int offset)
        {
            orderedList = orderedList.Skip(offset).Take(limit).ToList();
            return new PagedList<T>(orderedList, limit, offset);
        }

        public static PagedList<T> ToPagedList(List<T> orderedList, IQueryable<T> source, int limit, int offset)
        {
            orderedList.AddRange(source.Skip(offset).Take(limit).ToList());

            return new PagedList<T>(orderedList, limit, offset);
        }
    }
}
