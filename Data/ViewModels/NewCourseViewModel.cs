using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp;

public class NewCourseViewModel
{
    [Required]
    public string CourseName { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public string Category { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required]
    public string ImageURL { get; set; } = default!;

    [Required]
    public int InstructorId { get; set; }

    public List<User> Instructors { get; set; } = new List<User>();
    public List<string> Categories { get; set; } = new List<string>();
}
