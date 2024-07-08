using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class Course_Module
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = default!;
    [Required]
    public int ModuleId { get; set; }
    [ForeignKey("ModuleId")]
    public Module Module { get; set; } = default!;
}
