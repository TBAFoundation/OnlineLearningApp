using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Data;

public class AppInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new OnlineLearningAppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<OnlineLearningAppDbContext>>()))
        {
            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Seed Users
            var users = new User[]
            {
                new User { Username = "admin", Password = "admin123", FullName = "Administrator", Email = "admin@example.com", Role = "Admin" },
                new User { Username = "teacher1", Password = "teacher123", FullName = "Teacher One", Email = "teacher1@example.com", Role = "Instructor" },
                new User { Username = "student1", Password = "student123", FullName = "Student One", Email = "student1@example.com", Role = "Student" }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            // Seed Courses
            var courses = new Course[]
            {
                new Course { CourseName = "Math 101", Description = "Basic Mathematics", Duration = "3 months", InstructorId = users[1].UserId },
                new Course { CourseName = "Physics 101", Description = "Basic Physics", Duration = "3 months", InstructorId = users[1].UserId }
            };
            foreach (var course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            // Seed Modules
            var modules = new Module[]
            {
                new Module { ModuleName = "Algebra", Content = "Algebra Content", CourseId = courses[0].CourseId },
                new Module { ModuleName = "Geometry", Content = "Geometry Content", CourseId = courses[0].CourseId },
                new Module { ModuleName = "Mechanics", Content = "Mechanics Content", CourseId = courses[1].CourseId }
            };
            foreach (var module in modules)
            {
                context.Modules.Add(module);
            }
            context.SaveChanges();

            // Seed Quizzes
            var quizzes = new Quiz[]
            {
                new Quiz { QuizName = "Algebra Quiz", Description = "Quiz on Algebra", DateCreated = DateTime.Now, ModuleId = modules[0].ModuleId },
                new Quiz { QuizName = "Geometry Quiz", Description = "Quiz on Geometry", DateCreated = DateTime.Now, ModuleId = modules[1].ModuleId },
                new Quiz { QuizName = "Mechanics Quiz", Description = "Quiz on Mechanics", DateCreated = DateTime.Now, ModuleId = modules[2].ModuleId }
            };
            foreach (var quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }
            context.SaveChanges();

            // Seed Questions
            var questions = new Question[]
            {
                new Question { QuestionText = "What is 2 + 2?", QuestionType = "Multiple Choice", QuizId = quizzes[0].QuizId },
                new Question { QuestionText = "What is the area of a circle?", QuestionType = "Multiple Choice", QuizId = quizzes[1].QuizId },
                new Question { QuestionText = "What is Newton's second law?", QuestionType = "Multiple Choice", QuizId = quizzes[2].QuizId }
            };
            foreach (var question in questions)
            {
                context.Questions.Add(question);
            }
            context.SaveChanges();

            // Seed Options
            var options = new Option[]
            {
                new Option { OptionText = "4", IsCorrect = true, QuestionId = questions[0].QuestionId },
                new Option { OptionText = "5", IsCorrect = false, QuestionId = questions[0].QuestionId },
                new Option { OptionText = "πr²", IsCorrect = true, QuestionId = questions[1].QuestionId },
                new Option { OptionText = "2πr", IsCorrect = false, QuestionId = questions[1].QuestionId },
                new Option { OptionText = "F = ma", IsCorrect = true, QuestionId = questions[2].QuestionId },
                new Option { OptionText = "E = mc²", IsCorrect = false, QuestionId = questions[2].QuestionId }
            };
            foreach (var option in options)
            {
                context.Options.Add(option);
            }
            context.SaveChanges();
        }
    }
}
