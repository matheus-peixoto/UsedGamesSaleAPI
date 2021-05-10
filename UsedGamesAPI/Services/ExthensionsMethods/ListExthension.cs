using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Services.Paging;

namespace UsedGamesAPI.Services.ExthensionsMethods
{
    public static class ListExthension
    {
        public static PagedList<T> ToPagedList<T>(this List<T> list)
        {
            return PagedList<T>.ToPagedList(list);
        }

        public static PagedList<T> ToPagedList<T>(this List<T> list, int limit, int offset)
        {
            return PagedList<T>.ToPagedList(list, limit, offset);
        }

        public static PagedList<T> ToPagedList<T> (this List<T> list, IQueryable<T> source, int limit, int offset)
        {
            return PagedList<T>.ToPagedList(list, source, limit, offset);
        }
    }
}
