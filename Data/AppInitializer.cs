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
            if (context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            // Seed Accounts
            var accounts = new Account[]
            {
                new Account { UserId = Guid.NewGuid().ToString(), Username = "admin", PasswordHash = "admin123", FullName = "Administrator", Email = "admin@example.com", Role = UserRoles.Admin },
                new Account { UserId = Guid.NewGuid().ToString(), Username = "teacher1", PasswordHash = "teacher123", FullName = "Teacher One", Email = "teacher1@example.com", Role = UserRoles.Instructor },
                new Account { UserId = Guid.NewGuid().ToString(), Username = "student1", PasswordHash = "student123", FullName = "Student One", Email = "student1@example.com", Role = UserRoles.Student }
            };
            foreach (var account in accounts)
            {
                context.Accounts.Add(account);
            }
            context.SaveChanges();

            // Seed Courses
            var courses = new Course[]
            {
                new Course { CourseName = "Data Science 101", Description = "Introduction to Data Science", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), Price = 500m, Category = CourseCategory.DataScience, ImageURL = "datascience101.jpg", InstructorId = accounts[1].UserId },
                new Course { CourseName = "AI Fundamentals", Description = "Basics of AI", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), Price = 700m, Category = CourseCategory.AI, ImageURL = "aifundamentals.jpg", InstructorId = accounts[1].UserId },
                new Course { CourseName = "Programming with Python", Description = "Learn Python from scratch", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2), Price = 400m, Category = CourseCategory.Programming, ImageURL = "pythonprogramming.jpg", InstructorId = accounts[1].UserId },
                new Course { CourseName = "Machine Learning Basics", Description = "Introduction to Machine Learning", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(4), Price = 600m, Category = CourseCategory.MachineLearning, ImageURL = "mlbasics.jpg", InstructorId = accounts[1].UserId },
                new Course { CourseName = "Advanced Data Science", Description = "Advanced topics in Data Science", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(5), Price = 800m, Category = CourseCategory.DataScience, ImageURL = "advanced_datascience.jpg", InstructorId = accounts[1].UserId }
            };
            foreach (var course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            // Seed Modules
            var modules = new Module[]
            {
                new Module { ModuleName = "Introduction to Data Science", Content = "Content for Introduction to Data Science", CourseId = courses[0].CourseId },
                new Module { ModuleName = "Data Science Tools", Content = "Content for Data Science Tools", CourseId = courses[0].CourseId },
                new Module { ModuleName = "Introduction to AI", Content = "Content for Introduction to AI", CourseId = courses[1].CourseId },
                new Module { ModuleName = "AI Techniques", Content = "Content for AI Techniques", CourseId = courses[1].CourseId },
                new Module { ModuleName = "Getting Started with Python", Content = "Content for Getting Started with Python", CourseId = courses[2].CourseId },
                new Module { ModuleName = "Python Advanced Topics", Content = "Content for Python Advanced Topics", CourseId = courses[2].CourseId },
                new Module { ModuleName = "Basics of Machine Learning", Content = "Content for Basics of Machine Learning", CourseId = courses[3].CourseId },
                new Module { ModuleName = "Machine Learning Algorithms", Content = "Content for Machine Learning Algorithms", CourseId = courses[3].CourseId },
                new Module { ModuleName = "Advanced Data Science Techniques", Content = "Content for Advanced Data Science Techniques", CourseId = courses[4].CourseId },
                new Module { ModuleName = "Data Science Case Studies", Content = "Content for Data Science Case Studies", CourseId = courses[4].CourseId }
            };
            foreach (var module in modules)
            {
                context.Modules.Add(module);
            }
            context.SaveChanges();

            // Seed Quizzes
            var quizzes = new Quiz[]
            {
                new Quiz { QuizName = "Data Science Basics Quiz", Description = "Quiz on Data Science Basics", DateCreated = DateTime.Now, ModuleId = modules[0].ModuleId },
                new Quiz { QuizName = "AI Basics Quiz", Description = "Quiz on AI Basics", DateCreated = DateTime.Now, ModuleId = modules[2].ModuleId }
            };
            foreach (var quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }
            context.SaveChanges();

            // Seed Questions
            var questions = new Question[]
            {
                new Question { QuestionText = "What is Data Science?", QuestionType = "Multiple Choice", QuizId = quizzes[0].QuizId },
                new Question { QuestionText = "What is AI?", QuestionType = "Multiple Choice", QuizId = quizzes[1].QuizId }
            };
            foreach (var question in questions)
            {
                context.Questions.Add(question);
            }
            context.SaveChanges();

            // Seed Options
            var options = new Option[]
            {
                new Option { OptionText = "A field of study", IsCorrect = true, QuestionId = questions[0].QuestionId },
                new Option { OptionText = "A field of study", IsCorrect = true, QuestionId = questions[1].QuestionId }
            };
            foreach (var option in options)
            {
                context.Options.Add(option);
            }
            context.SaveChanges();

            // Seed Orders
            var orders = new Order[]
            {
                new Order { AccountId = accounts[2].UserId, Email = accounts[2].Email, OrderDate = DateTime.Now, TotalAmount = 1200m }
            };
            foreach (var order in orders)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();

            // Seed OrderItems
            var orderItems = new OrderItem[]
            {
                new OrderItem { OrderId = orders[0].Id, CourseId = courses[0].CourseId, Quantity = 1, Price = courses[0].Price },
                new OrderItem { OrderId = orders[0].Id, CourseId = courses[1].CourseId, Quantity = 1, Price = courses[1].Price }
            };
            foreach (var orderItem in orderItems)
            {
                context.OrderItems.Add(orderItem);
            }
            context.SaveChanges();

            // Seed ShoppingCartItems
            var shoppingCartItems = new ShoppingCartItem[]
            {
                new ShoppingCartItem { ShoppingCartId = Guid.NewGuid().ToString(), Course = courses[0], Amount = 1 },
                new ShoppingCartItem { ShoppingCartId = Guid.NewGuid().ToString(), Course = courses[1], Amount = 1 }
            };
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                context.ShoppingCartItems.Add(shoppingCartItem);
            }
            context.SaveChanges();
        }
    }
}
