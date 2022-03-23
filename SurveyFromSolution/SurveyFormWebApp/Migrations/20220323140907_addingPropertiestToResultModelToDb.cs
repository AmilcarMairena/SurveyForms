using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyFormWebApp.Migrations
{
    public partial class addingPropertiestToResultModelToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(nullable: false),
                    SurveyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Result_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SurveyName = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValueResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyResultId = table.Column<Guid>(nullable: false),
                    FieldId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueResult_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValueResult_SurveyResult_SurveyResultId",
                        column: x => x.SurveyResultId,
                        principalTable: "SurveyResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Result_SurveyId",
                table: "Result",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueResult_FieldId",
                table: "ValueResult",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueResult_SurveyResultId",
                table: "ValueResult",
                column: "SurveyResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "ValueResult");

            migrationBuilder.DropTable(
                name: "SurveyResult");
        }
    }
}
