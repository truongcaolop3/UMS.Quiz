using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class update_all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_knowledgesKnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_QuizQuestion_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropIndex(
                name: "IX_QuestionDetail_knowledgesKnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropIndex(
                name: "IX_QuestionDetail_QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId",
                table: "QuestionDetail");

            migrationBuilder.DropColumn(
                name: "knowledgesKnowledgeId",
                table: "QuestionDetail");

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

            migrationBuilder.AddColumn<int>(
                name: "QuizQuestionId",
                table: "QuestionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "knowledgesKnowledgeId",
                table: "QuestionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    QuizNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.QuizQuestionId);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Knowledges_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_knowledgesKnowledgeId",
                table: "QuestionDetail",
                column: "knowledgesKnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDetail_QuizQuestionId",
                table: "QuestionDetail",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_KnowledgeId",
                table: "QuizQuestion",
                column: "KnowledgeId",
                unique: true);

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
        }
    }
}
