namespace LuftBornTask.APIs.ViewModels
{
    public class PaginatedResponseVM<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
