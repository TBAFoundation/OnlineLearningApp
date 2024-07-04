using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int ShoppingCartItemId { get; set; }
    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = default!;
    public string ShoppingCartId { get; set; } = default!;
    public int Amount { get; set; }
}
