using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_TopicTemplate_TopicTemplateID",
                table: "ExamQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "TopicTemplateID",
                table: "ExamQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_TopicTemplate_TopicTemplateID",
                table: "ExamQuestions",
                column: "TopicTemplateID",
                principalTable: "TopicTemplate",
                principalColumn: "TopicTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_TopicTemplate_TopicTemplateID",
                table: "ExamQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "TopicTemplateID",
                table: "ExamQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_TopicTemplate_TopicTemplateID",
                table: "ExamQuestions",
                column: "TopicTemplateID",
                principalTable: "TopicTemplate",
                principalColumn: "TopicTemplateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
