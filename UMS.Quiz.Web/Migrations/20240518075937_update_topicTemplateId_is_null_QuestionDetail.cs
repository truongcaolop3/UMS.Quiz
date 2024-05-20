using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class update_topicTemplateId_is_null_QuestionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_TopicTemplate_TopicTemplateID",
                table: "QuestionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "TopicTemplateID",
                table: "QuestionDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_TopicTemplate_TopicTemplateID",
                table: "QuestionDetail",
                column: "TopicTemplateID",
                principalTable: "TopicTemplate",
                principalColumn: "TopicTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionDetail_TopicTemplate_TopicTemplateID",
                table: "QuestionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "TopicTemplateID",
                table: "QuestionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionDetail_TopicTemplate_TopicTemplateID",
                table: "QuestionDetail",
                column: "TopicTemplateID",
                principalTable: "TopicTemplate",
                principalColumn: "TopicTemplateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
