using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmApp.Migrations
{
    /// <inheritdoc />
    public partial class appUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUser",
                table: "Varliks",
                newName: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Varliks",
                newName: "AppUser");
        }
    }
}
