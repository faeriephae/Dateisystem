using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dateisystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class fullpath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "FileDir",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "FileDir");
        }
    }
}
