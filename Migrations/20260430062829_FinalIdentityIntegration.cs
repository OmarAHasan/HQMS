using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class FinalIdentityIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Users_DoctorId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ClinicId_TokenId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_Status",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Clinics");

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "Clinics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ClinicId",
                table: "Tokens",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DoctorUserId",
                table: "Clinics",
                column: "DoctorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorUserId",
                table: "Clinics",
                column: "DoctorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorUserId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ClinicId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_DoctorUserId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "Clinics");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Clinics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123", 2, "admin" },
                    { 2, "123", 1, "doctor" },
                    { 3, "123", 0, "reception" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ClinicId_TokenId",
                table: "Tokens",
                columns: new[] { "ClinicId", "TokenId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Status",
                table: "Tokens",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Users_DoctorId",
                table: "Clinics",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }
    }
}
