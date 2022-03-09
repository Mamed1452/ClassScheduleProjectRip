using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class Set_some_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhatTimeOfDayIndex",
                table: "WorkTimeInDays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonsOfSemesterName",
                table: "LessonsOfSemesters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "LessonId",
                table: "ClassScheduleResults",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomBuildingId",
                table: "ClassScheduleResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ListOfClassScheduleModeSpaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ListOfClassScheduleModeSpaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfClassScheduleModeSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListOfMainDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ListOfMainDomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfMainDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    MainDomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListOfMainDomainId = table.Column<long>(type: "bigint", nullable: false),
                    LessonsOfSemesterId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainDomains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainDomains_LessonsOfSemesters_LessonsOfSemesterId",
                        column: x => x.LessonsOfSemesterId,
                        principalTable: "LessonsOfSemesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainDomains_ListOfMainDomains_ListOfMainDomainId",
                        column: x => x.ListOfMainDomainId,
                        principalTable: "ListOfMainDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleModeSpaces_ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces",
                column: "ListOfClassScheduleModeSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfClassScheduleModeSpaces_TenantId",
                table: "ListOfClassScheduleModeSpaces",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfMainDomains_TenantId",
                table: "ListOfMainDomains",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDomains_LessonsOfSemesterId",
                table: "MainDomains",
                column: "LessonsOfSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDomains_ListOfMainDomainId",
                table: "MainDomains",
                column: "ListOfMainDomainId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDomains_TenantId",
                table: "MainDomains",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassScheduleModeSpaces_ListOfClassScheduleModeSpaces_ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces",
                column: "ListOfClassScheduleModeSpaceId",
                principalTable: "ListOfClassScheduleModeSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassScheduleModeSpaces_ListOfClassScheduleModeSpaces_ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces");

            migrationBuilder.DropTable(
                name: "ListOfClassScheduleModeSpaces");

            migrationBuilder.DropTable(
                name: "MainDomains");

            migrationBuilder.DropTable(
                name: "ListOfMainDomains");

            migrationBuilder.DropIndex(
                name: "IX_ClassScheduleModeSpaces_ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces");

            migrationBuilder.DropColumn(
                name: "WhatTimeOfDayIndex",
                table: "WorkTimeInDays");

            migrationBuilder.DropColumn(
                name: "LessonsOfSemesterName",
                table: "LessonsOfSemesters");

            migrationBuilder.DropColumn(
                name: "ClassroomBuildingId",
                table: "ClassScheduleResults");

            migrationBuilder.DropColumn(
                name: "ListOfClassScheduleModeSpaceId",
                table: "ClassScheduleModeSpaces");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "ClassScheduleResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
