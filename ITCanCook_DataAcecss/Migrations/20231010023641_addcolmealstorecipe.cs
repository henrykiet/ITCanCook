using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITCanCook_DataAcecss.Migrations
{
    public partial class addcolmealstorecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Meals",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04e2da59-39f8-4ebd-ae49-36f5fb155ce3", "2", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cbf34e5-37df-4202-ab31-60eeacdf3283", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e775029-e28f-447c-a590-374bafb889f2", "3", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04e2da59-39f8-4ebd-ae49-36f5fb155ce3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cbf34e5-37df-4202-ab31-60eeacdf3283");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e775029-e28f-447c-a590-374bafb889f2");

            migrationBuilder.DropColumn(
                name: "Meals",
                table: "Recipe");

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
    }
}
