using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetBooking.api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeceased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeceasedDate",
                table: "Pets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeceased",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeceasedDate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsDeceased",
                table: "Pets");
        }
    }
}
