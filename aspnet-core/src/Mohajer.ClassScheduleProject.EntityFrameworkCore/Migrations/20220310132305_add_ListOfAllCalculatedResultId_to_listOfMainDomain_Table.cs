using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class add_ListOfAllCalculatedResultId_to_listOfMainDomain_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ListOfAllCalculatedResultId",
                table: "ListOfMainDomains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MainDomainID",
                table: "ClassScheduleResults",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListOfAllCalculatedResultId",
                table: "ListOfMainDomains");

            migrationBuilder.DropColumn(
                name: "MainDomainID",
                table: "ClassScheduleResults");
        }
    }
}
