using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class Add_ListOfAllCalculatedResults_and_ClassScheduleResults_and_NameClassScheduleModeSpaces_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassScheduleModeSpaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    NameClassScheduleModeSpaces = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    IsLock = table.Column<bool>(type: "bit", nullable: false),
                    UniversityProfessorId = table.Column<int>(type: "int", nullable: false),
                    WorkTimeInDayId = table.Column<long>(type: "bigint", nullable: false),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ClassScheduleModeSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleModeSpaces_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScheduleModeSpaces_UniversityProfessors_UniversityProfessorId",
                        column: x => x.UniversityProfessorId,
                        principalTable: "UniversityProfessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScheduleModeSpaces_WorkTimeInDays_WorkTimeInDayId",
                        column: x => x.WorkTimeInDayId,
                        principalTable: "WorkTimeInDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListOfAllCalculatedResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    NameCalculatedResult = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ListOfAllCalculatedResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassScheduleResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    WorkTimeInDayId = table.Column<long>(type: "bigint", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    UniversityProfessorId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    UniversityMajorId = table.Column<int>(type: "int", nullable: false),
                    UniversityDepartmentId = table.Column<int>(type: "int", nullable: false),
                    ListOfAllCalculatedResultId = table.Column<long>(type: "bigint", nullable: false),
                    ClassScheduleModeSpaceId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ClassScheduleResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleResults_ClassScheduleModeSpaces_ClassScheduleModeSpaceId",
                        column: x => x.ClassScheduleModeSpaceId,
                        principalTable: "ClassScheduleModeSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScheduleResults_ListOfAllCalculatedResults_ListOfAllCalculatedResultId",
                        column: x => x.ListOfAllCalculatedResultId,
                        principalTable: "ListOfAllCalculatedResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleModeSpaces_LessonId",
                table: "ClassScheduleModeSpaces",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleModeSpaces_TenantId",
                table: "ClassScheduleModeSpaces",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleModeSpaces_UniversityProfessorId",
                table: "ClassScheduleModeSpaces",
                column: "UniversityProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleModeSpaces_WorkTimeInDayId",
                table: "ClassScheduleModeSpaces",
                column: "WorkTimeInDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleResults_ClassScheduleModeSpaceId",
                table: "ClassScheduleResults",
                column: "ClassScheduleModeSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleResults_ListOfAllCalculatedResultId",
                table: "ClassScheduleResults",
                column: "ListOfAllCalculatedResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleResults_TenantId",
                table: "ClassScheduleResults",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfAllCalculatedResults_TenantId",
                table: "ListOfAllCalculatedResults",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassScheduleResults");

            migrationBuilder.DropTable(
                name: "ClassScheduleModeSpaces");

            migrationBuilder.DropTable(
                name: "ListOfAllCalculatedResults");
        }
    }
}
