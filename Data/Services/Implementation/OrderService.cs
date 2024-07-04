﻿using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp;

public class OrderService : IOrderService
{
    private readonly OnlineLearningAppDbContext _context;

    public OrderService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}