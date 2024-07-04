using System.ComponentModel.DataAnnotations;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class Order
{
    public int Id { get; set; }  
    [Required]
    public int AccountId { get; set; } // Changed from string UserId to int AccountId
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    // Navigation property
    public virtual Account Account { get; set; } = default!;
}
