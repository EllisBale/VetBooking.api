using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetBooking.api.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "ExamRoom",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PetId1",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId1",
                table: "Appointments",
                column: "PetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Owners_OwnerId",
                table: "Appointments",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_PetId1",
                table: "Appointments",
                column: "PetId1",
                principalTable: "Pets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Owners_OwnerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_PetId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PetId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ExamRoom",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PetId1",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
