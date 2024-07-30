namespace Bank_Deposits.ViewModels
{
    public class PageViewModel<T>
    {
        public string Info { get; set; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        private IEnumerable<T> _items;

        public PageViewModel(IEnumerable<T> pageDate, int page, int pageSize)
        {
            pageDate = pageDate.Reverse();
            var count = pageDate.Count();
            _items = pageDate.Skip((page - 1) * pageSize).Take(pageSize);

            PageNumber = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public IEnumerable<T> Items { get { return _items; } }

    }
}
