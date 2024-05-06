using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class Quiz_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TermName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermID);
                });

            migrationBuilder.CreateTable(
                name: "TopicTemplate",
                columns: table => new
                {
                    TopicTemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    PointGet = table.Column<int>(type: "int", nullable: false),
                    QuantityGet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTemplate", x => x.TopicTemplateID);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestions",
                columns: table => new
                {
                    ExamQuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamQuestionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionPoint = table.Column<int>(type: "int", nullable: false),
                    TopicTemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestions", x => x.ExamQuestionID);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_TopicTemplate_TopicTemplateID",
                        column: x => x.TopicTemplateID,
                        principalTable: "TopicTemplate",
                        principalColumn: "TopicTemplateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TopicTemplateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.KnowledgeId);
                    table.ForeignKey(
                        name: "FK_Knowledges_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID");
                    table.ForeignKey(
                        name: "FK_Knowledges_TopicTemplate_TopicTemplateID",
                        column: x => x.TopicTemplateID,
                        principalTable: "TopicTemplate",
                        principalColumn: "TopicTemplateID");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExamQuestionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK_Exam_ExamQuestions_ExamQuestionID",
                        column: x => x.ExamQuestionID,
                        principalTable: "ExamQuestions",
                        principalColumn: "ExamQuestionID");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizNumber = table.Column<int>(type: "int", nullable: false),
                    KnowledgeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.QuizQuestionId);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Knowledges_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId");
                });

            migrationBuilder.CreateTable(
                name: "ExamDetailCandidates",
                columns: table => new
                {
                    ExamDetailCandidatesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStudent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassworkStudent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDetailCandidates", x => x.ExamDetailCandidatesID);
                    table.ForeignKey(
                        name: "FK_ExamDetailCandidates_Exam_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ExamID");
                });

            migrationBuilder.CreateTable(
                name: "QuestionDetail",
                columns: table => new
                {
                    QuestionDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionPoint = table.Column<int>(type: "int", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false),
                    TopicTemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionDetail", x => x.QuestionDetailID);
                    table.ForeignKey(
                        name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "QuizQuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionDetail_TopicTemplate_TopicTemplateID",
                        column: x => x.TopicTemplateID,
                        principalTable: "TopicTemplate",
                        principalColumn: "TopicTemplateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamDetailAnswer",
                columns: table => new
                {
                    ExamDetailAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<float>(type: "real", nullable: false),
                    AllPoint = table.Column<float>(type: "real", nullable: false),
                    ExamDetailCandidatesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDetailAnswer", x => x.ExamDetailAnswerID);
                    table.ForeignKey(
                        name: "FK_ExamDetailAnswer_ExamDetailCandidates_ExamDetailCandidatesID",
                        column: x => x.ExamDetailCandidatesID,
                        principalTable: "ExamDetailCandidates",
                        principalColumn: "ExamDetailCandidatesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionAnswer",
                columns: table => new
                {
                    QuizQuestionAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    PercenterValue = table.Column<float>(type: "real", nullable: false),
                    QuestionDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionAnswer", x => x.QuizQuestionAnswerID);
                    table.ForeignKey(
                        name: "FK_QuizQuestionAnswer_QuestionDetail_QuestionDetailId",
                        column: x => x.QuestionDetailId,
                        principalTable: "QuestionDetail",
                        principalColumn: "QuestionDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ExamQuestionID",
                table: "Exam",
                column: "ExamQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetailAnswer_ExamDetailCandidatesID",
                table: "ExamDetailAnswer",
                column: "ExamDetailCandidatesID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetailCandidates_ExamID",
                table: "ExamDetailCandidates",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_TopicTemplateID",
                table: "ExamQuestions",
                column: "TopicTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledges_TermID",
                table: "Knowledges",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledges_TopicTemplateID",
                table: "Knowledges",
                column: "TopicTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_TopicTemplateID",
                table: "QuestionDetail",
                column: "TopicTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionAnswer_QuestionDetailId",
                table: "QuizQuestionAnswer",
                column: "QuestionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_KnowledgeId",
                table: "QuizQuestions",
                column: "KnowledgeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ExamDetailAnswer");

            migrationBuilder.DropTable(
                name: "QuizQuestionAnswer");

            migrationBuilder.DropTable(
                name: "ExamDetailCandidates");

            migrationBuilder.DropTable(
                name: "QuestionDetail");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "ExamQuestions");

            migrationBuilder.DropTable(
                name: "Knowledges");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "TopicTemplate");
        }
    }
}
