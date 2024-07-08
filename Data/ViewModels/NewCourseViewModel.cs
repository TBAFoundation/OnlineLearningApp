using System.ComponentModel.DataAnnotations;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class NewCourseViewModel
{
    public int Id { get; set; }
    [Required]
    public string CourseName { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public CourseCategory Category { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    [Display(Name = "Price in $")]
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    [Required]
    public string ImageURL { get; set; } = default!;

    [Required]
    public int InstructorId { get; set; }

    public List<User> Instructors { get; set; } = new List<User>();
    public List<string> Categories { get; set; } = new List<string>();
public List<int> ModuleIds { get; set; } = new List<int>();}
