using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningApp.Data.Cart;
using System.Security.Claims;

namespace OnlineLearningApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _ordersService;

        public OrdersController(ICourseService courseService, ShoppingCart shoppingCart, IOrderService ordersService)
        {
            _courseService = courseService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        public async Task<IActionResult> GetCartItemCount()
        {
            var itemCount = await _shoppingCart.GetShoppingCartItemsAsync();
            return PartialView("_CartItemCountPartial", itemCount.Count);
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string userRole = User.FindFirstValue(ClaimTypes.Role)!;

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var items = await _shoppingCart.GetShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotalAsync()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _courseService.GetCourseByIdAsync(id);

            if (item != null)
            {
                await _shoppingCart.AddItemToCartAsync(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _courseService.GetCourseByIdAsync(id);

            if (item != null)
            {
                await _shoppingCart.RemoveItemFromCartAsync(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = await _shoppingCart.GetShoppingCartItemsAsync();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email)!;

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
