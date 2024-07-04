using OnlineLearningApp.Models;
namespace OnlineLearningApp;
public interface IShoppingCartService
{   
    string ShoppingCartId { get; set; }
    List<ShoppingCartItem> ShoppingCartItems { get; set; }
    void AddItemToCart(Course course);
    void RemoveItemFromCart(Course course);
    List<ShoppingCartItem> GetShoppingCartItems();
    decimal GetShoppingCartTotal();
    Task ClearShoppingCartAsync();
}
