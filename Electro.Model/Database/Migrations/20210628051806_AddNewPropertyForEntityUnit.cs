using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Electro.Model.Database.Migrations
{
    public partial class AddNewPropertyForEntityUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Units",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Units");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b867520a-92db-4658-be39-84da53a601c0"),
                column: "ConcurrencyStamp",
                value: "c3ddf900-5df2-4259-b564-6a67a9a25f5e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("21f7b496-c675-4e8a-a34c-fc5ec0762fdb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a283dd11-dada-42da-bc94-98a2d85fad5e", "AQAAAAEAACcQAAAAEPHRaPcugF8NSdQpeJkK4ZszdirVYsffiINPCL/ynoxeCeVsx+BWSU33o3yxEjtLWg==" });
        }
    }
}
