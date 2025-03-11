using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class coursesrenamedtoprograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COURSES");

            migrationBuilder.CreateTable(
                name: "PROGRAMS",
                columns: table => new
                {
                    PROGRAM_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PROGRAM_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEMESTERS = table.Column<int>(type: "int", nullable: true),
                    SEMESTER_START = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROGRAM_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IELTS_SCORE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPLICATION_DEADLINE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LINKS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LANGUAGE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIVERSITY_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROGRAMS", x => x.PROGRAM_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROGRAMS");

            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    COURSE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPLICATION_DEADLINE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COURSE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COURSE_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IELTS_SCORE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    LANGUAGE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LINKS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEMESTERS = table.Column<int>(type: "int", nullable: true),
                    SEMESTER_START = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIVERSITY_ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSES", x => x.COURSE_ID);
                });
        }
    }
}
