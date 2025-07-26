namespace LuftBornTask.APIs.ViewModels.Product
{
    public class ProductRequestVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
