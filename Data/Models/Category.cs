namespace Ecommerce_Api.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;

        public string CategoryNumber { get; set; } = Guid.NewGuid().ToString();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}