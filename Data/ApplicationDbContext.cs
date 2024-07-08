using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Data;
public class OnlineLearningAppDbContext : DbContext
{
    public OnlineLearningAppDbContext(DbContextOptions<OnlineLearningAppDbContext> options) : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Course_Module> Courses_Modules { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(a => a.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Courses)
            .WithOne(c => c.Instructor)
            .HasForeignKey(c => c.InstructorId);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Orders)
            .WithOne(o => o.Account)
            .HasForeignKey(o => o.AccountId);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(a => a.Courses)
            .HasForeignKey(c => c.InstructorId);

        modelBuilder.Entity<Course_Module>()
            .HasOne(cm => cm.Course)
            .WithMany(c => c.Courses_Modules)
            .HasForeignKey(cm => cm.CourseId);

        modelBuilder.Entity<Course_Module>()
            .HasOne(cm => cm.Module)
            .WithMany(m => m.Courses_Modules)
            .HasForeignKey(cm => cm.ModuleId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<Quiz>()
            .HasOne(q => q.Module)
            .WithMany(m => m.Quizzes)
            .HasForeignKey(q => q.ModuleId);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Quiz)
            .WithMany(quiz => quiz.Questions)
            .HasForeignKey(q => q.QuizId);

        modelBuilder.Entity<Option>()
            .HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId);
    }
}