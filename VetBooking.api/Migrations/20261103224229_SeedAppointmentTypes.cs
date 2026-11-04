using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetBooking.api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppointmentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppointmentTypes",
                columns: new[] { "Id", "DurationInMinutes", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 30, "Checkup", 50.00m },
                    { 2, 15, "Vaccination", 30.00m },
                    { 3, 120, "Surgery", 500.00m },
                    { 4, 60, "Dental Cleaning", 150.00m },
                    { 5, 45, "Emergency", 200.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppointmentTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
