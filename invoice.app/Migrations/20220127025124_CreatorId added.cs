using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.app.Migrations
{
    public partial class CreatorIdadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinCodes_AspNetUsers_CreatorId",
                table: "JoinCodes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "JoinCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "JoinCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinCodes_AspNetUsers_CreatorId",
                table: "JoinCodes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinCodes_AspNetUsers_CreatorId",
                table: "JoinCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "JoinCodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "JoinCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_JoinCodes_AspNetUsers_CreatorId",
                table: "JoinCodes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
