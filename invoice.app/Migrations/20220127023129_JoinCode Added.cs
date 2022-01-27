using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.app.Migrations
{
    public partial class JoinCodeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinCodes",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Expiration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinCodes", x => new { x.Code, x.ForEmail });
                    table.ForeignKey(
                        name: "FK_JoinCodes_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JoinCodes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinCodes_CreatorId",
                table: "JoinCodes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinCodes_OrganizationId",
                table: "JoinCodes",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinCodes");
        }
    }
}
