using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyFormWebApp.Migrations
{
    public partial class addingForeignKeyToValuesResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResultId",
                table: "SurveyResult",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResult_ResultId",
                table: "SurveyResult",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResult_Result_ResultId",
                table: "SurveyResult",
                column: "ResultId",
                principalTable: "Result",
                principalColumn: "ResultId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResult_Result_ResultId",
                table: "SurveyResult");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResult_ResultId",
                table: "SurveyResult");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "SurveyResult");
        }
    }
}
