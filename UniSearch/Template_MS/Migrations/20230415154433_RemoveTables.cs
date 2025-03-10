using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniSearch.Migrations
{
    public partial class RemoveTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGS");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOGS",
                columns: table => new
                {
                    LOGD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    API_REQUESTDATA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    API_RESPONSEDATA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INPUT_BROWSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INPUT_BRO_VERSION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INPUT_BY = table.Column<int>(type: "int", nullable: true),
                    INPUT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    INPUT_IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INPUT_MAC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_CLIENT_API = table.Column<bool>(type: "bit", nullable: true),
                    STATUS_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TYPE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.LOGD_ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserLoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_UserLogin_UserRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_RoleId",
                table: "UserLogin",
                column: "RoleId");
        }
    }
}
