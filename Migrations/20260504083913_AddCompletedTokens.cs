using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletedTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Tokens",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Tokens");

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
    }
}
