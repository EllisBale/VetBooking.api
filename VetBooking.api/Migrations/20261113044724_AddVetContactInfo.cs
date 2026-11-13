using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetBooking.api.Migrations
{
    /// <inheritdoc />
    public partial class AddVetContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Vets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vets");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Vets");
        }
    }
}
