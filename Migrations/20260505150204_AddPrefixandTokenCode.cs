using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class AddPrefixandTokenCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenCode",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "TEMP");

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5542224-b8af-4324-8f38-6aefd7fca625", "AQAAAAIAAYagAAAAENpataqJRH1n+Qbj0OmnyFdwH1XUl6MHFZCF/FkV9XAPYxOD+ABui0ENkBaMGn2Zbw==", "760842db-2545-41e4-a717-06671dfa3fa7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92af0f00-f734-4b60-8b32-3cdc792a424b", "AQAAAAIAAYagAAAAENz6RiSHW7IQN7Gk7cwT19lZrXCjLazpoNqmHUgn6+qSM/G0x+rk0i6qWVYkCPUqpw==", "ed310e58-969e-4d3f-976b-5ca3021d0cfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22da11eb-c4fb-4899-8a9e-677435bbf2b6", "AQAAAAIAAYagAAAAEN9pJaaDVrhTELDVbeYf5bccrvBwXRGE+2P2zVMjBAfDF9ck3Xn2Enb69N6xKnsjJQ==", "4cac73ec-ce6f-4c4b-8bfb-e550eba0896e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCode",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Departments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56d0ac77-5751-4cae-9605-27b65eb6b2c8", "AQAAAAIAAYagAAAAEEHOHC/SmhpRbb0j2aCQZI/u290Yeob9rXmSoat5TWGRjubPH7sxMXV9PfP6Jp6hGw==", "9f5a47c1-8469-423a-bf40-fecb253eef19" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "beeb448d-38d3-44ea-bc1e-4035f33faf42", "AQAAAAIAAYagAAAAEMEyr7LvoirphprfEXShFrfkNKj78JHC6riuw75Ez+xzPLo8c22mKTxcrIeP+3v98g==", "7df6c00d-48d8-4b9a-a390-11f98ed137df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2a3e2e4-306a-40c6-bbd3-83b40da22bf7", "AQAAAAIAAYagAAAAENsykIvB86/6JAQRj099U5TDU4Xq+VeT5DxBb60tHbMeA3FsNC+HPohtHUG9qo9nFA==", "63fb4909-1b5e-4386-a90a-f3e9a3571af3" });
        }
    }
}
