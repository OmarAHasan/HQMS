using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class IdentitySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorUserId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Doctor", "DOCTOR" },
                    { "3", null, "Reception", "RECEPTION" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "10", 0, "fc576fd7-26b8-42c0-95f7-0765d46a7f7d", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEAdrFJivtIHwGgXxe4914N4cFV4iJkIpAGjvRK89CapvEdJ3vqK9vURgiS1AI606PQ==", null, false, "ea47109d-71c4-4964-83f5-34a9b1443d64", false, "admin" },
                    { "11", 0, "35fd30c6-2233-4c55-86c9-f8675723b1ba", null, false, false, null, null, "DOCTOR", "AQAAAAIAAYagAAAAEELy8pxLaslRWCTJhNvM7CFhvHJMKmsqORHHuIktNmhdf/tIQW5qTq6zs9PpBa1sLQ==", null, false, "7de7dd38-5020-4f4b-8320-8d62f75126e2", false, "doctor" },
                    { "12", 0, "73b45d6d-d679-446c-aa2d-d37a7a32ef3f", null, false, false, null, null, "RECEPTION", "AQAAAAIAAYagAAAAEGPxCxuJEiTChYMAXIlB5+9cmiJs//t4mf1FWGl5kqnEl/gwDtg/F6hqGtDwqPE5yg==", null, false, "1e7259e5-8dc2-4a77-82e3-b50b66cd5195", false, "reception" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "10" },
                    { "2", "11" },
                    { "3", "12" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorUserId",
                table: "Clinics",
                column: "DoctorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Patients_PatientId",
                table: "Tokens",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");
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

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "10" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "11" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12");

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
    }
}
