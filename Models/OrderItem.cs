using System.ComponentModel.DataAnnotations;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class OrderItem
{
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public Order Order { get; set; } = default!;
    [Required]
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public int Quantity { get; set; }
}
