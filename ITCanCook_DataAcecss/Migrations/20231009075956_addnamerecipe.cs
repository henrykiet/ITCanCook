using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITCanCook_DataAcecss.Migrations
{
    public partial class addnamerecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4774695f-2ef7-42af-87ad-f173824c0df4", "2", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0dae3a1-0da4-4eec-8077-9bae8cb94ed1", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3e734ee-ae72-49a3-8a40-4a0d076de214", "3", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4774695f-2ef7-42af-87ad-f173824c0df4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0dae3a1-0da4-4eec-8077-9bae8cb94ed1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e734ee-ae72-49a3-8a40-4a0d076de214");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipe");

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
    }
}
