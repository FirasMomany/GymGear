namespace API.RequestHelpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex,int pageSized, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSized;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
