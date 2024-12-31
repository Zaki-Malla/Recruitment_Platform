using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruitment_Platform.Migrations
{
    /// <inheritdoc />
    public partial class Offersedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TbOffersForOpportunities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "TbOffersForOpportunities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "TbOffersForOpportunities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TbOffersForOpportunities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "TbOffersForOpportunities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserCv",
                table: "TbOffersForOpportunities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhyYou",
                table: "TbOffersForOpportunities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TbNotifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "UserCv",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "WhyYou",
                table: "TbOffersForOpportunities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TbNotifications");
        }
    }
}
