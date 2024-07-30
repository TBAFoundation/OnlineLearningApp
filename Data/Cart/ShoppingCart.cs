using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineLearningApp.Data.Cart;

public class ShoppingCart
{
    private readonly OnlineLearningAppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;

    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart(OnlineLearningAppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _session = _httpContextAccessor.HttpContext!.Session;
        ShoppingCartId = _session.GetString("CartId") ?? Guid.NewGuid().ToString();
        _session.SetString("CartId", ShoppingCartId);
    }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        var context = services.GetRequiredService<OnlineLearningAppDbContext>();
        var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
        return new ShoppingCart(context, httpContextAccessor);
    }

    public async Task AddItemToCartAsync(Course course)
    {
        var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Course.Id == course.Id && n.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Course = course,
                Amount = 1
            };

            await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemFromCartAsync(Course course)
    {
        var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Course.Id == course.Id && n.ShoppingCartId == ShoppingCartId);

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
        await _context.SaveChangesAsync();
    }

    public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync()
    {
        ShoppingCartItems = await _context.ShoppingCartItems
            .Where(n => n.ShoppingCartId == ShoppingCartId)
            .Include(n => n.Course)
            .ToListAsync();
        return ShoppingCartItems;
    }

    public async Task<decimal> GetShoppingCartTotalAsync()
    {
        return await _context.ShoppingCartItems
            .Where(n => n.ShoppingCartId == ShoppingCartId)
            .Select(n => n.Course.Price * n.Amount)
            .SumAsync();
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}