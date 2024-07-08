
namespace OnlineLearningApp;

public class NewCourseDropdownViewModel
{
    public NewCourseDropdownViewModel()
    {
        Instructors = new List<User>();
        Categories = new List<string>();
    }

    public List<User> Instructors { get; set; }
    public List<string> Categories { get; set; }

}
public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = default!;
}