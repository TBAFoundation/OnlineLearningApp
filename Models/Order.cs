using System.ComponentModel.DataAnnotations;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class Order
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string AccountId { get; set; } = default!;
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    // Navigation property
    public virtual Account Account { get; set; } = default!;
}