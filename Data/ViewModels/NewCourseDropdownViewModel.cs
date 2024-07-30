
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
    public string UserId { get; set; } = default!;
    public string FullName { get; set; } = default!;
}