namespace LuftBornTask.APIs.ViewModels
{
    public class ProductRequestVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
