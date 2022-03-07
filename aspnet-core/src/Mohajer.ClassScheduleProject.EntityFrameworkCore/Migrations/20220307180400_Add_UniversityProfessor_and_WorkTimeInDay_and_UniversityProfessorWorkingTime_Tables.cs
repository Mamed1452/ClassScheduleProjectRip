using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class Add_UniversityProfessor_and_WorkTimeInDay_and_UniversityProfessorWorkingTime_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UniversityProfessors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UniversityProfessorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_UniversityProfessors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTimeInDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    NameWorkTimeInDay = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WhatTimeOfDay = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_WorkTimeInDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityProfessorWorkingTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UniversityProfessorId = table.Column<int>(type: "int", nullable: false),
                    WorkTimeInDayId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_UniversityProfessorWorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniversityProfessorWorkingTimes_UniversityProfessors_UniversityProfessorId",
                        column: x => x.UniversityProfessorId,
                        principalTable: "UniversityProfessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityProfessorWorkingTimes_WorkTimeInDays_WorkTimeInDayId",
                        column: x => x.WorkTimeInDayId,
                        principalTable: "WorkTimeInDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UniversityProfessors_TenantId",
                table: "UniversityProfessors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityProfessorWorkingTimes_TenantId",
                table: "UniversityProfessorWorkingTimes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityProfessorWorkingTimes_UniversityProfessorId",
                table: "UniversityProfessorWorkingTimes",
                column: "UniversityProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityProfessorWorkingTimes_WorkTimeInDayId",
                table: "UniversityProfessorWorkingTimes",
                column: "WorkTimeInDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimeInDays_TenantId",
                table: "WorkTimeInDays",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniversityProfessorWorkingTimes");

            migrationBuilder.DropTable(
                name: "UniversityProfessors");

            migrationBuilder.DropTable(
                name: "WorkTimeInDays");
        }
    }
}
