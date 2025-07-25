
namespace LuftBornTask.Application.DTOs
{
    public class ProductFilterDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
