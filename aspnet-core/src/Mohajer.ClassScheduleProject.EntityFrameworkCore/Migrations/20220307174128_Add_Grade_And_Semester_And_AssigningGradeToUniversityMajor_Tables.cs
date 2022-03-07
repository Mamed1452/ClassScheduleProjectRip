using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.ClassScheduleProject.Migrations
{
    public partial class Add_Grade_And_Semester_And_AssigningGradeToUniversityMajor_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ClassroomBuildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ClassroomBuildingName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberOfClassrooms = table.Column<int>(type: "int", nullable: false),
                    ClassificationClassroomBuilding = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ClassroomBuildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UniversityDepartmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_UniversityDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityMajors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UniversityMajorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UniversityMajorType = table.Column<int>(type: "int", nullable: false),
                    UniversityDepartmentId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UniversityMajors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniversityMajors_UniversityDepartments_UniversityDepartmentId",
                        column: x => x.UniversityDepartmentId,
                        principalTable: "UniversityDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssigningGradeToUniversityMajors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    NameAssignedGradeToUniversityMajor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    UniversityMajorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AssigningGradeToUniversityMajors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssigningGradeToUniversityMajors_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigningGradeToUniversityMajors_UniversityMajors_UniversityMajorId",
                        column: x => x.UniversityMajorId,
                        principalTable: "UniversityMajors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssigningUniversityMajorToClassroomBuildings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    MaximumRestrictionsOnUsingClassroomsAtTheSameTime = table.Column<int>(type: "int", nullable: false),
                    UniversityMajorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AssigningUniversityMajorToClassroomBuildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssigningUniversityMajorToClassroomBuildings_ClassroomBuildings_ClassroomBuildingId",
                        column: x => x.ClassroomBuildingId,
                        principalTable: "ClassroomBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigningUniversityMajorToClassroomBuildings_UniversityMajors_UniversityMajorId",
                        column: x => x.UniversityMajorId,
                        principalTable: "UniversityMajors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    SemesterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AssigningGradeToUniversityMajorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_AssigningGradeToUniversityMajors_AssigningGradeToUniversityMajorId",
                        column: x => x.AssigningGradeToUniversityMajorId,
                        principalTable: "AssigningGradeToUniversityMajors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssigningGradeToUniversityMajors_GradeId",
                table: "AssigningGradeToUniversityMajors",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigningGradeToUniversityMajors_TenantId",
                table: "AssigningGradeToUniversityMajors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigningGradeToUniversityMajors_UniversityMajorId",
                table: "AssigningGradeToUniversityMajors",
                column: "UniversityMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigningUniversityMajorToClassroomBuildings_ClassroomBuildingId",
                table: "AssigningUniversityMajorToClassroomBuildings",
                column: "ClassroomBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigningUniversityMajorToClassroomBuildings_TenantId",
                table: "AssigningUniversityMajorToClassroomBuildings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigningUniversityMajorToClassroomBuildings_UniversityMajorId",
                table: "AssigningUniversityMajorToClassroomBuildings",
                column: "UniversityMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomBuildings_TenantId",
                table: "ClassroomBuildings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TenantId",
                table: "Grades",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_AssigningGradeToUniversityMajorId",
                table: "Semesters",
                column: "AssigningGradeToUniversityMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_TenantId",
                table: "Semesters",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityDepartments_TenantId",
                table: "UniversityDepartments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMajors_TenantId",
                table: "UniversityMajors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMajors_UniversityDepartmentId",
                table: "UniversityMajors",
                column: "UniversityDepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssigningUniversityMajorToClassroomBuildings");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "ClassroomBuildings");

            migrationBuilder.DropTable(
                name: "AssigningGradeToUniversityMajors");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "UniversityMajors");

            migrationBuilder.DropTable(
                name: "UniversityDepartments");

        }
    }
}