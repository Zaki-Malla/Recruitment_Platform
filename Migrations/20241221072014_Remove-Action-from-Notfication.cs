using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruitment_Platform.Migrations
{
    /// <inheritdoc />
    public partial class RemoveActionfromNotfication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "TbNotifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "TbNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
