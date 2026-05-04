using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDepartmentDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Departments");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc576fd7-26b8-42c0-95f7-0765d46a7f7d", "AQAAAAIAAYagAAAAEAdrFJivtIHwGgXxe4914N4cFV4iJkIpAGjvRK89CapvEdJ3vqK9vURgiS1AI606PQ==", "ea47109d-71c4-4964-83f5-34a9b1443d64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35fd30c6-2233-4c55-86c9-f8675723b1ba", "AQAAAAIAAYagAAAAEELy8pxLaslRWCTJhNvM7CFhvHJMKmsqORHHuIktNmhdf/tIQW5qTq6zs9PpBa1sLQ==", "7de7dd38-5020-4f4b-8320-8d62f75126e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73b45d6d-d679-446c-aa2d-d37a7a32ef3f", "AQAAAAIAAYagAAAAEGPxCxuJEiTChYMAXIlB5+9cmiJs//t4mf1FWGl5kqnEl/gwDtg/F6hqGtDwqPE5yg==", "1e7259e5-8dc2-4a77-82e3-b50b66cd5195" });
        }
    }
}
