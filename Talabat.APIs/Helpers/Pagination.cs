using Talabat.APIs.DTOs;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> productsToReturn)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Date = productsToReturn;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Date { get; set; }

    }
}
