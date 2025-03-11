using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    /// <inheritdoc />
    public partial class universityspellingerrorfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UNIVERSITY_iMAGE",
                table: "UNIVERSITIES",
                newName: "UNIVERSITY_IMAGE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UNIVERSITY_IMAGE",
                table: "UNIVERSITIES",
                newName: "UNIVERSITY_iMAGE");
        }
    }
}
