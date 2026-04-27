using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndDoctorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clinics_ClinicId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClinicId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ClinicId_TokenNumber",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ClinicId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalledAt",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "IssuedAt",
                table: "Tokens",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PatientCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Clinics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ClinicId_TokenId",
                table: "Tokens",
                columns: new[] { "ClinicId", "TokenId" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Users_DoctorId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ClinicId_TokenId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tokens",
                newName: "IssuedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CalledAt",
                table: "Tokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Tokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClinicId",
                table: "Users",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ClinicId_TokenNumber",
                table: "Tokens",
                columns: new[] { "ClinicId", "TokenNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ClinicId",
                table: "Patients",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clinics_ClinicId",
                table: "Users",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "ClinicId");
        }
    }
}
