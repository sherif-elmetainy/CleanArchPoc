using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeArt.Poc.Storage.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class CustomerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1L, "Sherif", "El-Metainy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
