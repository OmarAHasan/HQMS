using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientMobileNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Tokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20b320ef-3937-4f40-88b8-26720c2690db", "AQAAAAIAAYagAAAAEDBaHY26Ll9+6hJCwSQJQnjCHAZ3d+A7ZD2Ybs4NI6FhqUhjTrk3T/+fp/tWclqSbA==", "d2f51530-e4f1-462d-86da-fba6daf8dee8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1654dee9-18d7-447c-9e3b-89c38e507996", "AQAAAAIAAYagAAAAEKW2oI86344kmTPWmLLNHATQGQTRAfHartDQUl/b5/mn5dum6MTsam4kwHqXeLHmgw==", "bfe85d7e-36a4-4cc8-a5ab-4e47573a41ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e38f5e6-d8df-4455-a863-04de63e7288e", "AQAAAAIAAYagAAAAEAvhDorf7otqwudUVUFP24TgdeWqby1D6boQwwDrjW6POOg+lWCShKe8rnZ29iRtqQ==", "92774f4d-e24d-4085-ad9e-bef02649a6a4" });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_DepartmentId",
                table: "Tokens",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Departments_DepartmentId",
                table: "Tokens",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Departments_DepartmentId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_DepartmentId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Tokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c971ab8-4c0f-4a63-89cf-27799ec7b255", "AQAAAAIAAYagAAAAEEcIM9iYBtSPUrttrN5g3/AkKlpbYAyxhxK4Ap129JHD6IekUt5H3cH6RYmCZd/6tQ==", "75320610-e051-4dfc-9403-7b0e1db23e8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f26f62cd-a99b-4e41-864f-a6269f5f5f2f", "AQAAAAIAAYagAAAAELo4jD0ITuVeMEN/GVfap8UkRst4lxVbKaQm7ZcgVpoVLLS2dlBHyXxn/ySHYH7ORA==", "4fad577e-ca5b-47c6-abeb-073bfc040394" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7239ec8-85c7-45a2-8902-cc71cfe4fa9a", "AQAAAAIAAYagAAAAEDLqXhJkwcKfCcqHa3FY509vuYv4wZssgIKgXO/PVsdZutQbFRXwvSFnJc9tt4IgqQ==", "bb1f7d6e-52a8-4001-af61-2ec3a39d49e0" });
        }
    }
}
