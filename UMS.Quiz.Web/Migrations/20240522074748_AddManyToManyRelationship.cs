using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    public partial class AddManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledges_TopicTemplate_TopicTemplateID",
                table: "Knowledges");

            migrationBuilder.DropIndex(
                name: "IX_Knowledges_TopicTemplateID",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "TopicTemplateID",
                table: "Knowledges");

            migrationBuilder.CreateTable(
                name: "TopicTemplateKnowledges",
                columns: table => new
                {
                    TopicTemplateID = table.Column<int>(type: "int", nullable: false),
                    KnowledgeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTemplateKnowledges", x => new { x.TopicTemplateID, x.KnowledgeID });
                    table.ForeignKey(
                        name: "FK_TopicTemplateKnowledges_Knowledges_KnowledgeID",
                        column: x => x.KnowledgeID,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTemplateKnowledges_TopicTemplate_TopicTemplateID",
                        column: x => x.TopicTemplateID,
                        principalTable: "TopicTemplate",
                        principalColumn: "TopicTemplateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicTemplateKnowledges_KnowledgeID",
                table: "TopicTemplateKnowledges",
                column: "KnowledgeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicTemplateKnowledges");

            migrationBuilder.AddColumn<int>(
                name: "TopicTemplateID",
                table: "Knowledges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Knowledges_TopicTemplateID",
                table: "Knowledges",
                column: "TopicTemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledges_TopicTemplate_TopicTemplateID",
                table: "Knowledges",
                column: "TopicTemplateID",
                principalTable: "TopicTemplate",
                principalColumn: "TopicTemplateID");
        }
    }
}
