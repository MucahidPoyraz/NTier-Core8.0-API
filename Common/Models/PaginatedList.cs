namespace Common.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }   // Sayfadaki veriler
        public int PageIndex { get; private set; }   // Şu anki sayfa
        public int TotalPages { get; private set; }  // Toplam sayfa sayısı
        public int TotalCount { get; private set; }  // Toplam veri sayısı

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
