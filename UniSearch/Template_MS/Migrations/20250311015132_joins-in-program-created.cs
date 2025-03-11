using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class joinsinprogramcreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROGRAMS_LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS",
                column: "LANGUAGESLANGUAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROGRAMS_UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS",
                column: "UNIVERSITIESUNIVERSITY_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROGRAMS_LANGUAGES_LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS",
                column: "LANGUAGESLANGUAGE_ID",
                principalTable: "LANGUAGES",
                principalColumn: "LANGUAGE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROGRAMS_UNIVERSITIES_UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS",
                column: "UNIVERSITIESUNIVERSITY_ID",
                principalTable: "UNIVERSITIES",
                principalColumn: "UNIVERSITY_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROGRAMS_LANGUAGES_LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS");

            migrationBuilder.DropForeignKey(
                name: "FK_PROGRAMS_UNIVERSITIES_UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS");

            migrationBuilder.DropIndex(
                name: "IX_PROGRAMS_LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS");

            migrationBuilder.DropIndex(
                name: "IX_PROGRAMS_UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS");

            migrationBuilder.DropColumn(
                name: "LANGUAGESLANGUAGE_ID",
                table: "PROGRAMS");

            migrationBuilder.DropColumn(
                name: "UNIVERSITIESUNIVERSITY_ID",
                table: "PROGRAMS");
        }
    }
}
