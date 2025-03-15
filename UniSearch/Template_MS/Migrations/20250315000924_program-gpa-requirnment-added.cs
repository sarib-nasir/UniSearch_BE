using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class programgparequirnmentadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PROGRAM_MINIMUM_GPA",
                table: "PROGRAMS",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROGRAM_MINIMUM_GPA",
                table: "PROGRAMS");
        }
    }
}
