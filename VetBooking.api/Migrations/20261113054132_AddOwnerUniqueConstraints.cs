using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetBooking.api.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Vets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { "Vet@example.com", "07700 900000" });

            migrationBuilder.CreateIndex(
                name: "IX_Vets_Email",
                table: "Vets",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vets_PhoneNumber",
                table: "Vets",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Email",
                table: "Owners",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_PhoneNumber",
                table: "Owners",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vets_Email",
                table: "Vets");

            migrationBuilder.DropIndex(
                name: "IX_Vets_PhoneNumber",
                table: "Vets");

            migrationBuilder.DropIndex(
                name: "IX_Owners_Email",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_PhoneNumber",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Vets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { "", "" });
        }
    }
}
