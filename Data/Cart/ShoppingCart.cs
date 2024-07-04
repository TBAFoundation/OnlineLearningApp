using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class ShoppingCart
{
    private readonly IShoppingCartService _shoppingCartService;
    public ShoppingCart(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public void AddItemToCart(Course course)
    {
        _shoppingCartService.AddItemToCart(course);
    }

    public void RemoveItemFromCart(Course course)
    {
        _shoppingCartService.RemoveItemFromCart(course);
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return _shoppingCartService.GetShoppingCartItems();
    }

    public decimal GetShoppingCartTotal()
    {
        return _shoppingCartService.GetShoppingCartTotal();
    }

    public Task ClearShoppingCartAsync()
    {
        return _shoppingCartService.ClearShoppingCartAsync();
    }
}