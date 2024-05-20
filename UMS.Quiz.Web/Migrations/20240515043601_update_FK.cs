using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class update_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "QuizQuestionId",
                table: "QuestionDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "QuizQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "QuizQuestionId",
                table: "QuestionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "QuizQuestionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
