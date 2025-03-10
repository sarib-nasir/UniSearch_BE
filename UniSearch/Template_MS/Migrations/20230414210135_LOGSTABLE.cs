using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniSearch.Migrations
{
    public partial class LOGSTABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOGS",
                columns: table => new
                {
                    LOGD_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INPUT_BY = table.Column<int>(nullable: true),
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
                    IS_CLIENT_API = table.Column<bool>(nullable: true),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.LOGD_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGS");
        }
    }
}
