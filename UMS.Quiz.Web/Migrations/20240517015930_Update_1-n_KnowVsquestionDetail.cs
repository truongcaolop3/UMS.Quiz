using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class Update_1n_KnowVsquestionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KnowledgeId",
                table: "QuestionDetail",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropIndex(
                name: "IX_QuestionDetail_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropColumn(
                name: "KnowledgeId",
                table: "QuestionDetail");
        }
    }
}
