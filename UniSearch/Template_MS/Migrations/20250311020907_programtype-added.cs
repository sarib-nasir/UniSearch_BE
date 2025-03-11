using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class programtypeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROGRAM_TYPE",
                table: "PROGRAMS");

            migrationBuilder.AddColumn<Guid>(
                name: "PROGRAM_TYPE_ID",
                table: "PROGRAMS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PROGRAM_TYPE",
                columns: table => new
                {
                    PROGRAM_TYPE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PROGRAM_TYPE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROGRAM_TYPE", x => x.PROGRAM_TYPE_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROGRAMS_PROGRAM_TYPE_ID",
                table: "PROGRAMS",
                column: "PROGRAM_TYPE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROGRAMS_PROGRAM_TYPE_PROGRAM_TYPE_ID",
                table: "PROGRAMS",
                column: "PROGRAM_TYPE_ID",
                principalTable: "PROGRAM_TYPE",
                principalColumn: "PROGRAM_TYPE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROGRAMS_PROGRAM_TYPE_PROGRAM_TYPE_ID",
                table: "PROGRAMS");

            migrationBuilder.DropTable(
                name: "PROGRAM_TYPE");

            migrationBuilder.DropIndex(
                name: "IX_PROGRAMS_PROGRAM_TYPE_ID",
                table: "PROGRAMS");

            migrationBuilder.DropColumn(
                name: "PROGRAM_TYPE_ID",
                table: "PROGRAMS");

            migrationBuilder.AddColumn<string>(
                name: "PROGRAM_TYPE",
                table: "PROGRAMS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
