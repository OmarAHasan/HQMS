using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalQueueMS.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "815216ec-f673-49e5-baef-569f5909a450", "AQAAAAIAAYagAAAAEHEZzOpy4FDpVpLwYF3ZfElBaQjmOItNLzQJD9zYwH0sQt0VIczsjNDQ13vyq0j9rw==", "281c95fb-ee71-4a29-856b-feb756300b8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b508dc1-946b-4e83-846d-bf7760016c65", "AQAAAAIAAYagAAAAEFhkycMQLazPov1Xr90ukQ2V6a0iEtLVaFcKUEeRYHc7LftPyqtRKYqkEH4jiK/onw==", "e32a7bd4-4235-435f-b864-48ce83b405eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "048dd586-f254-40ca-a0c1-9a19a4b78abe", "AQAAAAIAAYagAAAAENUDa7J4GByo3N7DDE2Cet/q/E3SJVpJeh05M9qly7tIKP5gyOLkmscbiWpG7X8fVA==", "d589cb0c-4fcd-41af-aadb-001aa5ddbf2c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
