using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyFormWebApp.Migrations
{
    public partial class addmorePropsToResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResultDate",
                table: "Result",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultDate",
                table: "Result");
        }
    }
}
