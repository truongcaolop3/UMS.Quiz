using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class delete_quizquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Knowledges_KnowledgeId",
                table: "QuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionDetail_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "QuizQuestions",
                newName: "QuizQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestions_KnowledgeId",
                table: "QuizQuestion",
                newName: "IX_QuizQuestion_KnowledgeId");

            migrationBuilder.AddColumn<int>(
                name: "knowledgesKnowledgeId",
                table: "QuestionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestion",
                table: "QuizQuestion",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_knowledgesKnowledgeId",
                table: "QuestionDetail",
                column: "knowledgesKnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_Knowledges_knowledgesKnowledgeId",
                table: "QuestionDetail",
                column: "knowledgesKnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_QuizQuestion_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Knowledges_KnowledgeId",
                table: "QuizQuestion",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_knowledgesKnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_QuizQuestion_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Knowledges_KnowledgeId",
                table: "QuizQuestion");

            migrationBuilder.DropIndex(
                name: "IX_QuestionDetail_knowledgesKnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestion",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "knowledgesKnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.RenameTable(
                name: "QuizQuestion",
                newName: "QuizQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestion_KnowledgeId",
                table: "QuizQuestions",
                newName: "IX_QuizQuestions_KnowledgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_KnowledgeId",
                table: "QuestionDetail",
                column: "KnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_QuizQuestions_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Knowledges_KnowledgeId",
                table: "QuizQuestions",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
