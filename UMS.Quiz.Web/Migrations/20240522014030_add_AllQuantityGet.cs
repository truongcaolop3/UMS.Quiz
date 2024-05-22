using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class add_AllQuantityGet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.AddColumn<int>(
                name: "AllQuantityGet",
                table: "TopicTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "QuizQuestionAnswer",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeId",
                table: "QuestionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail");

            migrationBuilder.DropColumn(
                name: "AllQuantityGet",
                table: "TopicTemplate");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "QuizQuestionAnswer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeId",
                table: "QuestionDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_Knowledges_KnowledgeId",
                table: "QuestionDetail",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId");
        }
    }
}
