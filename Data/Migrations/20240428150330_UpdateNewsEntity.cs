using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "tags",
                table: "News",
                newName: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "News",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "News",
                newName: "tags");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "News",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
