using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITCanCook_DataAcecss.Migrations
{
    public partial class addcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "522df3d4-7e14-4e5e-80b1-e3ac5db7ed98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "beda1e4d-9f11-4dba-9a48-8ac58eab9edb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc84d40f-a2a7-42e4-8bdd-b09611cfd171");

            migrationBuilder.AddColumn<int>(
                name: "Energy",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "224b4d6a-2f54-4bfb-8184-2cac2a43b31b", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "787f8a58-7d4f-427b-ac96-81bd3a6984b9", "2", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c4436bd-9776-4805-83e9-4665d16dd835", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "224b4d6a-2f54-4bfb-8184-2cac2a43b31b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "787f8a58-7d4f-427b-ac96-81bd3a6984b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c4436bd-9776-4805-83e9-4665d16dd835");

            migrationBuilder.DropColumn(
                name: "Energy",
                table: "Recipe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "522df3d4-7e14-4e5e-80b1-e3ac5db7ed98", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "beda1e4d-9f11-4dba-9a48-8ac58eab9edb", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc84d40f-a2a7-42e4-8bdd-b09611cfd171", "2", "Chef", "Chef" });
        }
    }
}
