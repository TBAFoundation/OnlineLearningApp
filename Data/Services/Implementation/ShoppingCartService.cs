using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class ShoppingCartService : IShoppingCartService
{
    private readonly OnlineLearningAppDbContext _context;
    public string ShoppingCartId { get; set; } = default!;
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCartService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public static ShoppingCartService GetShoppingCart(IServiceProvider services)
    {
        var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
        var context = services.GetService<OnlineLearningAppDbContext>()!;

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        session?.SetString("CartId", cartId);

        return new ShoppingCartService(context) { ShoppingCartId = cartId };
    }

    public void AddItemToCart(Course course)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
            s => s.Course.CourseId == course.CourseId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Course = course,
                Amount = 1
            };
            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        _context.SaveChanges();
    }

    public void RemoveItemFromCart(Course course)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
            s => s.Course.CourseId == course.CourseId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        _context.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??= _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Include(s => s.Course)
            .ToList();
    }

    public decimal GetShoppingCartTotal()
    {
        return _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Course.Price * c.Amount)
            .Sum();
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .ToListAsync();

        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
