using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dt191gMom3Part2.Migrations
{
    public partial class ContextAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lending_AlbumId",
                table: "Lending");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LendingDate",
                table: "Lending",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lending_AlbumId",
                table: "Lending",
                column: "AlbumId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lending_AlbumId",
                table: "Lending");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LendingDate",
                table: "Lending",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Lending_AlbumId",
                table: "Lending",
                column: "AlbumId");
        }
    }
}
