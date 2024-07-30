using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp;

public class OrderService : IOrderService
{
    private readonly OnlineLearningAppDbContext _context;

    public OrderService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
    {
        var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Course).Include(n => n.Account).ToListAsync();

        if (userRole != "Admin")
        {
            orders = orders.Where(n => n.AccountId.ToString() == userId).ToList();
        }

        return orders;
    }

    public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
    {
        var order = new Order()
        {
            AccountId = userId,
            Email = userEmailAddress
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new OrderItem()
            {
                Amount = item.Amount,
                CourseId = item.Course.Id,
                OrderId = order.Id,
                Price = item.Course.Price
            };
            await _context.OrderItems.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}