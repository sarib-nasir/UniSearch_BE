using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSearch.Migrations
{
    public partial class RemoveBranchDetailFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchDetail_UserLogin_InputBy",
                table: "BranchDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchDetail_UserLogin_ModifyBy",
                table: "BranchDetail");

            migrationBuilder.DropIndex(
                name: "IX_BranchDetail_InputBy",
                table: "BranchDetail");

            migrationBuilder.DropIndex(
                name: "IX_BranchDetail_ModifyBy",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "InputBy",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "InputDate",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "InputIP",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "ModifyIP",
                table: "BranchDetail");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "BranchDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InputBy",
                table: "BranchDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InputDate",
                table: "BranchDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputIP",
                table: "BranchDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifyBy",
                table: "BranchDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "BranchDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIP",
                table: "BranchDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "BranchDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BranchDetail_InputBy",
                table: "BranchDetail",
                column: "InputBy");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDetail_ModifyBy",
                table: "BranchDetail",
                column: "ModifyBy");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchDetail_UserLogin_InputBy",
                table: "BranchDetail",
                column: "InputBy",
                principalTable: "UserLogin",
                principalColumn: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchDetail_UserLogin_ModifyBy",
                table: "BranchDetail",
                column: "ModifyBy",
                principalTable: "UserLogin",
                principalColumn: "LoginId");
        }
    }
}
