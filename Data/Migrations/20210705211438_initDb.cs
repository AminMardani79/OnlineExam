using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminModels",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AdminImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdminDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ActiveAccount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminModels", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "GradeModels",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsGradeDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeModels", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "TeacherModels",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    TeacherEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TeacherImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTeacherDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ActiveAccount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherModels", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "LessonModels",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsLessonDelete = table.Column<bool>(type: "bit", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModels", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_LessonModels_GradeModels_GradeId",
                        column: x => x.GradeId,
                        principalTable: "GradeModels",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentModels",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StudentMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    StudentPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveAccount = table.Column<bool>(type: "bit", nullable: false),
                    IsStudentDelete = table.Column<bool>(type: "bit", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentModels", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentModels_GradeModels_GradeId",
                        column: x => x.GradeId,
                        principalTable: "GradeModels",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLessonModels",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessonModels", x => new { x.LessonId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherLessonModels_LessonModels_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonModels",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessonModels_TeacherModels_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModels",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestModels",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TestDayTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TestFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TestDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Finish = table.Column<bool>(type: "bit", nullable: false),
                    IsComprehensiveTest = table.Column<bool>(type: "bit", nullable: false),
                    NegativePoint = table.Column<bool>(type: "bit", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModels", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_TestModels_GradeModels_GradeId",
                        column: x => x.GradeId,
                        principalTable: "GradeModels",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestModels_LessonModels_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonModels",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestModels_TeacherModels_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModels",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterWithStudent",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterWithStudent", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_MasterWithStudent_StudentModels_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentModels",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterWithStudent_TeacherModels_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModels",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerModels",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    AnswerNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    AnswerChecked = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AnswerFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerContext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDescriptive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerModels", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_AnswerModels_StudentModels_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentModels",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelPercentModels",
                columns: table => new
                {
                    LevelPercentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    TestLevel = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    TestScore = table.Column<double>(type: "float", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelPercentModels", x => x.LevelPercentId);
                    table.ForeignKey(
                        name: "FK_LevelPercentModels_StudentModels_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentModels",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelPercentModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    TestKeyAnswer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Descriptive = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestStudentsModels",
                columns: table => new
                {
                    TestStudentsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    IsShowingWorkBook = table.Column<bool>(type: "bit", nullable: false),
                    IsSubmitAnswer = table.Column<bool>(type: "bit", nullable: false),
                    EnterCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStudentsModels", x => x.TestStudentsId);
                    table.ForeignKey(
                        name: "FK_TestStudentsModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkBookModels",
                columns: table => new
                {
                    WorkBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    QuestionCounts = table.Column<int>(type: "int", nullable: false),
                    TrueAnswers = table.Column<int>(type: "int", nullable: false),
                    WrongAnswers = table.Column<int>(type: "int", nullable: false),
                    NoCheckedAnswers = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<double>(type: "float", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<double>(type: "float", nullable: false),
                    LessonScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkBookModels", x => x.WorkBookId);
                    table.ForeignKey(
                        name: "FK_WorkBookModels_StudentModels_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentModels",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkBookModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModels_StudentId",
                table: "AnswerModels",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModels_TestId",
                table: "AnswerModels",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModels_GradeId",
                table: "LessonModels",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelPercentModels_StudentId",
                table: "LevelPercentModels",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelPercentModels_TestId",
                table: "LevelPercentModels",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterWithStudent_TeacherId",
                table: "MasterWithStudent",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_TestId",
                table: "QuestionModels",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModels_GradeId",
                table: "StudentModels",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonModels_TeacherId",
                table: "TeacherLessonModels",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TestModels_GradeId",
                table: "TestModels",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestModels_LessonId",
                table: "TestModels",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TestModels_TeacherId",
                table: "TestModels",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStudentsModels_TestId",
                table: "TestStudentsModels",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBookModels_StudentId",
                table: "WorkBookModels",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBookModels_TestId",
                table: "WorkBookModels",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminModels");

            migrationBuilder.DropTable(
                name: "AnswerModels");

            migrationBuilder.DropTable(
                name: "LevelPercentModels");

            migrationBuilder.DropTable(
                name: "MasterWithStudent");

            migrationBuilder.DropTable(
                name: "QuestionModels");

            migrationBuilder.DropTable(
                name: "TeacherLessonModels");

            migrationBuilder.DropTable(
                name: "TestStudentsModels");

            migrationBuilder.DropTable(
                name: "WorkBookModels");

            migrationBuilder.DropTable(
                name: "StudentModels");

            migrationBuilder.DropTable(
                name: "TestModels");

            migrationBuilder.DropTable(
                name: "LessonModels");

            migrationBuilder.DropTable(
                name: "TeacherModels");

            migrationBuilder.DropTable(
                name: "GradeModels");
        }
    }
}
