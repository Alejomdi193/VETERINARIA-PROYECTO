using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers.Errors
{
    public class Pager<T> where T : class
    {
        public string Search { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int Total { get; private set; }

        public List<T> Registers { get; private set; }

        public Pager(List<T> registers, int total, int pageIndex, int pageSize, string search)
        {
            Registers = registers;
            Total = total;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Search = search;
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(Total / (double)PageSize);
            }
            set
            {
                this.TotalPages = value;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
            set
            {
                this.HasPreviousPage = value;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
            set
            {
                this.HasNextPage = value;
            }
        }
    }
}