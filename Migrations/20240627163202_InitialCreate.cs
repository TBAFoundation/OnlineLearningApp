using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningApp.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Username = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Password = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Email = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Role = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.UserId);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Courses",
            columns: table => new
            {
                CourseId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                CourseName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Duration = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                InstructorId = table.Column<int>(type: "int", nullable: false),
                InstructorId0 = table.Column<int>(name: "Instructor Id", type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Courses", x => x.CourseId);
                table.ForeignKey(
                    name: "FK_Courses_Users_InstructorId",
                    column: x => x.InstructorId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Modules",
            columns: table => new
            {
                ModuleId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                ModuleName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Content = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CourseId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Modules", x => x.ModuleId);
                table.ForeignKey(
                    name: "FK_Modules_Courses_CourseId",
                    column: x => x.CourseId,
                    principalTable: "Courses",
                    principalColumn: "CourseId",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "StudentCourses",
            columns: table => new
            {
                StudentId = table.Column<int>(type: "int", nullable: false),
                CourseId = table.Column<int>(type: "int", nullable: false),
                EnrollmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                table.ForeignKey(
                    name: "FK_StudentCourses_Courses_CourseId",
                    column: x => x.CourseId,
                    principalTable: "Courses",
                    principalColumn: "CourseId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_StudentCourses_Users_StudentId",
                    column: x => x.StudentId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Quizzes",
            columns: table => new
            {
                QuizId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                QuizName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                ModuleId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                table.ForeignKey(
                    name: "FK_Quizzes_Modules_ModuleId",
                    column: x => x.ModuleId,
                    principalTable: "Modules",
                    principalColumn: "ModuleId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Quizzes_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "UserId");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Questions",
            columns: table => new
            {
                QuestionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                QuestionText = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                QuestionType = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                QuizId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Questions", x => x.QuestionId);
                table.ForeignKey(
                    name: "FK_Questions_Quizzes_QuizId",
                    column: x => x.QuizId,
                    principalTable: "Quizzes",
                    principalColumn: "QuizId",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Options",
            columns: table => new
            {
                OptionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                OptionText = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false),
                QuestionId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Options", x => x.OptionId);
                table.ForeignKey(
                    name: "FK_Options_Questions_QuestionId",
                    column: x => x.QuestionId,
                    principalTable: "Questions",
                    principalColumn: "QuestionId",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Courses_InstructorId",
            table: "Courses",
            column: "InstructorId");

        migrationBuilder.CreateIndex(
            name: "IX_Modules_CourseId",
            table: "Modules",
            column: "CourseId");

        migrationBuilder.CreateIndex(
            name: "IX_Options_QuestionId",
            table: "Options",
            column: "QuestionId");

        migrationBuilder.CreateIndex(
            name: "IX_Questions_QuizId",
            table: "Questions",
            column: "QuizId");

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_ModuleId",
            table: "Quizzes",
            column: "ModuleId");

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_UserId",
            table: "Quizzes",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_StudentCourses_CourseId",
            table: "StudentCourses",
            column: "CourseId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Options");

        migrationBuilder.DropTable(
            name: "StudentCourses");

        migrationBuilder.DropTable(
            name: "Questions");

        migrationBuilder.DropTable(
            name: "Quizzes");

        migrationBuilder.DropTable(
            name: "Modules");

        migrationBuilder.DropTable(
            name: "Courses");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
