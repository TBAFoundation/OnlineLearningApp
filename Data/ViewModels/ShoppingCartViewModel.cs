
namespace OnlineLearningApp;

public class ShoppingCartViewModel
{
    public ShoppingCart ShoppingCart { get; set; } = default!;
    public decimal ShoppingCartTotal { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
}
