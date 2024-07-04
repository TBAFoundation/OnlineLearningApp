using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningApp;

public class ShoppingCartSummary : ViewComponent
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartSummary(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCartService.GetShoppingCartItems();

        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCartItems = items,
            ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal()
        };

        return View(shoppingCartViewModel);
    }
}
