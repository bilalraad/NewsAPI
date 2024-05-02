using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_News_NewsId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_NewsId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "News",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "News");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsId",
                table: "Photos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_NewsId",
                table: "Photos",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_News_NewsId",
                table: "Photos",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");
        }
    }
}
