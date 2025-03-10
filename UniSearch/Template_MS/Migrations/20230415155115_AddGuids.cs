using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniSearch.Migrations
{
    public partial class AddGuids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOGS",
                columns: table => new
                {
                    LOGD_ID = table.Column<Guid>(nullable: false),
                    INPUT_BY = table.Column<Guid>(nullable: true),
                    INPUT_DATE = table.Column<DateTime>(nullable: true),
                    INPUT_IP = table.Column<string>(nullable: true),
                    INPUT_MAC = table.Column<string>(nullable: true),
                    INPUT_BROWSER = table.Column<string>(nullable: true),
                    INPUT_BRO_VERSION = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    STATUS_CODE = table.Column<string>(nullable: true),
                    ACTION = table.Column<string>(nullable: true),
                    API_REQUESTDATA = table.Column<string>(nullable: true),
                    API_RESPONSEDATA = table.Column<string>(nullable: true),
                    TYPE_ID = table.Column<string>(nullable: true),
                    IS_CLIENT_API = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.LOGD_ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGS");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
