using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class add_ListOfResltId_To_listOfModeSpace_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ListOfAllCalculatedResultId",
                table: "ListOfClassScheduleModeSpaces",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListOfAllCalculatedResultId",
                table: "ListOfClassScheduleModeSpaces");
        }
    }
}
