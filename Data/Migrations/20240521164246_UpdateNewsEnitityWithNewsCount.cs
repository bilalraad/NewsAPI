using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsEnitityWithNewsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikedByUsersCount",
                table: "News",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedByUsersCount",
                table: "News");
        }
    }
}
