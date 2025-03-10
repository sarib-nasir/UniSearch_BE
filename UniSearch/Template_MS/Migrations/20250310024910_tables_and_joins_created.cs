using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class tables_and_joins_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRIES",
                columns: table => new
                {
                    COUNTRY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COUNTRY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRIES", x => x.COUNTRY_ID);
                });

            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    COURSE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COURSE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEMESTERS = table.Column<int>(type: "int", nullable: true),
                    SEMESTER_START = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COURSE_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IELTS_SCORE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPLICATION_DEADLINE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LINKS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LANGUAGE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIVERSITY_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSES", x => x.COURSE_ID);
                });

            migrationBuilder.CreateTable(
                name: "LANGUAGES",
                columns: table => new
                {
                    LANGUAGE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LANGUAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LANGUAGES", x => x.LANGUAGE_ID);
                });

            migrationBuilder.CreateTable(
                name: "UNIVERSITIES",
                columns: table => new
                {
                    UNIVERSITY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UNIVERSITY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIVERSITY_iMAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIVERSITY_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    COUNTRY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIVERSITIES", x => x.UNIVERSITY_ID);
                    table.ForeignKey(
                        name: "FK_UNIVERSITIES_COUNTRIES_COUNTRY_ID",
                        column: x => x.COUNTRY_ID,
                        principalTable: "COUNTRIES",
                        principalColumn: "COUNTRY_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UNIVERSITIES_COUNTRY_ID",
                table: "UNIVERSITIES",
                column: "COUNTRY_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COURSES");

            migrationBuilder.DropTable(
                name: "LANGUAGES");

            migrationBuilder.DropTable(
                name: "UNIVERSITIES");

            migrationBuilder.DropTable(
                name: "COUNTRIES");
        }
    }
}
