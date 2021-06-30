using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Electro.Model.Database.Migrations
{
    public partial class ChangeNamingNavigationPropertyEntityCharacteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b867520a-92db-4658-be39-84da53a601c0"),
                column: "ConcurrencyStamp",
                value: "7e2ce77f-6877-44c8-a944-cea3adc9dbcb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("21f7b496-c675-4e8a-a34c-fc5ec0762fdb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d2e3ef7-6e4d-40b9-be87-bbd9622fee2c", "AQAAAAEAACcQAAAAEFbO/WPqfoVxTDVdo2EJl/J6Zwu05j/poMc42HgvB1YCkPP2HwLwCqrwJedGwrT78Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b867520a-92db-4658-be39-84da53a601c0"),
                column: "ConcurrencyStamp",
                value: "2adf7802-91fb-4c21-8b6b-d089472c8073");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("21f7b496-c675-4e8a-a34c-fc5ec0762fdb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d53cd3ea-9712-419e-aaee-b6687b873480", "AQAAAAEAACcQAAAAEMYQzjthn+LnkabVbXX36pfJKMJz6UNQ4CjGCpf+suNvvIPxwVHpxtP88Ej6WP335g==" });
        }
    }
}
