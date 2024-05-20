using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class add_AccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "TopicTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Terms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "QuizQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "QuizQuestionAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "QuestionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Knowledges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Knowledges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ExamQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ExamDetailCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ExamDetailAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Exam",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TopicTemplate");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "QuizQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "QuestionDetail");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ExamDetailCandidates");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ExamDetailAnswer");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Exam");
        }
    }
}
