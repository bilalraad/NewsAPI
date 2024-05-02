using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photos",
                table: "News",
                newName: "PhotosUrls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotosUrls",
                table: "News",
                newName: "Photos");
        }
    }
}
