using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    public partial class AddBranchDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchDetail",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    InputBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InputIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyIP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDetail", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_BranchDetail_UserLogin_InputBy",
                        column: x => x.InputBy,
                        principalTable: "UserLogin",
                        principalColumn: "LoginId");
                    table.ForeignKey(
                        name: "FK_BranchDetail_UserLogin_ModifyBy",
                        column: x => x.ModifyBy,
                        principalTable: "UserLogin",
                        principalColumn: "LoginId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchDetail_InputBy",
                table: "BranchDetail",
                column: "InputBy");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDetail_ModifyBy",
                table: "BranchDetail",
                column: "ModifyBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchDetail");
        }
    }
}
