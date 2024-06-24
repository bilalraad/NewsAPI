using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLikeFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsLikes",
                columns: table => new
                {
                    SourceUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetNewsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLikes", x => new { x.SourceUserId, x.TargetNewsId });
                    table.ForeignKey(
                        name: "FK_NewsLikes_News_TargetNewsId",
                        column: x => x.TargetNewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsLikes_Users_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsLikes_TargetNewsId",
                table: "NewsLikes",
                column: "TargetNewsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLikes");
        }
    }
}
