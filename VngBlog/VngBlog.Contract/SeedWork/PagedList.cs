using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.SeedWork
{
    public class PagedList<T> : List<T>
    {
        private MetaData _metaData { get; }
        public PagedList(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
        {
            _metaData = new MetaData
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            };
            AddRange(items);
        }
        public MetaData GetMetaData()
        {
            return _metaData;
        }
    }
}
