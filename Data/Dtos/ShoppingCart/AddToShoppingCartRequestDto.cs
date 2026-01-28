namespace Ecommerce_Api.Data.Dtos.shoppingCart
{
    public class AddToShoppingCartRequestDto
    {
        public string ProductNumber { get; set; } = null!;
        public int Quantity { get; set; }
    }
}