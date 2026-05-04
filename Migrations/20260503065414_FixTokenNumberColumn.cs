using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class FixTokenNumberColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e18f730-6d48-4479-a7ec-7290c0b1a6b0", "AQAAAAIAAYagAAAAELjuJnFgquv4Dv7hB6wViePELIHoDjkvWgyXlg5VJ3uDjT0GefpU2hmmRbuPXIT4Rg==", "40704bf9-0060-4b8e-898a-e5c3ab0d9c2e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c3cb6d0-aa39-4b2c-a4ae-8fe6b6e32116", "AQAAAAIAAYagAAAAEF9f01O4ORKlPODmWe8m5XAeTd0+ket00+521ngA78QzyyuGC5ss4VQQbcXLc9W1TQ==", "73af88f1-65f2-4ec9-bad0-653dcb6dc331" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba1ddb5e-95e8-4af3-92dd-f2ad1b06b913", "AQAAAAIAAYagAAAAECEOVEH9ZRwwi/gs2I+S1/Oijg5Mo7bFhmiI6Ib+R6Iw8IXjrl2twqHIkpM0zKOZNA==", "0b655df0-146f-47d9-90e7-9e4dfb7a1b5f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
