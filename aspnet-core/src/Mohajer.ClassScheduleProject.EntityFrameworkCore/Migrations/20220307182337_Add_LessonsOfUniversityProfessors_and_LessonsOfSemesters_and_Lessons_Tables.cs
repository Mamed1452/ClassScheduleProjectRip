using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class Add_LessonsOfUniversityProfessors_and_LessonsOfSemesters_and_Lessons_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    NameLesson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    ClassificationLesson = table.Column<int>(type: "int", nullable: false),
                    NumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    NumberOfGroups = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClassroomBuildingId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_ClassroomBuildings_ClassroomBuildingId",
                        column: x => x.ClassroomBuildingId,
                        principalTable: "ClassroomBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonsOfSemesters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    LessonOfSemesterType = table.Column<int>(type: "int", nullable: false),
                    NumberOfClassesToBeFormed = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    SemesterId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonsOfSemesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonsOfSemesters_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonsOfSemesters_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonsOfUniversityProfessors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    UniversityProfessorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_LessonsOfUniversityProfessors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonsOfUniversityProfessors_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonsOfUniversityProfessors_UniversityProfessors_UniversityProfessorId",
                        column: x => x.UniversityProfessorId,
                        principalTable: "UniversityProfessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ClassroomBuildingId",
                table: "Lessons",
                column: "ClassroomBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TenantId",
                table: "Lessons",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfSemesters_LessonId",
                table: "LessonsOfSemesters",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfSemesters_SemesterId",
                table: "LessonsOfSemesters",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfSemesters_TenantId",
                table: "LessonsOfSemesters",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfUniversityProfessors_LessonId",
                table: "LessonsOfUniversityProfessors",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfUniversityProfessors_TenantId",
                table: "LessonsOfUniversityProfessors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsOfUniversityProfessors_UniversityProfessorId",
                table: "LessonsOfUniversityProfessors",
                column: "UniversityProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonsOfSemesters");

            migrationBuilder.DropTable(
                name: "LessonsOfUniversityProfessors");

            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
