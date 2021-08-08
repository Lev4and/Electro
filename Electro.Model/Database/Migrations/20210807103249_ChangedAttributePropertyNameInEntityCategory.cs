using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Electro.Model.Database.Migrations
{
    public partial class ChangedAttributePropertyNameInEntityCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b867520a-92db-4658-be39-84da53a601c0"),
                column: "ConcurrencyStamp",
                value: "b4cb6308-2595-4460-9900-a4385166becf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("21f7b496-c675-4e8a-a34c-fc5ec0762fdb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df04619c-a417-46dc-8485-3e0c09fbde97", "AQAAAAEAACcQAAAAEHIdjJg7+IrCiv5RXLdV8OifHLLgIgmIUe+IIxhKsYj+unU7Bda2MLHg7OcqAqU6Gw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b867520a-92db-4658-be39-84da53a601c0"),
                column: "ConcurrencyStamp",
                value: "b83ee853-ed90-437a-a4a3-bc04bd7d9f3a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("21f7b496-c675-4e8a-a34c-fc5ec0762fdb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fea1b044-2b64-41eb-8969-744a0a5d37b8", "AQAAAAEAACcQAAAAEEQBUuc6/1TJgzhYhVNaks9JsBdfR0lIymsWNAiqH02q9OTtwJXdSRwU2VmPguNoZQ==" });
        }
    }
}
