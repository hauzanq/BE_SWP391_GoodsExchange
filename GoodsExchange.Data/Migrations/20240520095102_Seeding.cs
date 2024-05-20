using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "Moderator" },
                    { new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), "Buyer" },
                    { new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "Seller" },
                    { new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "Status", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("1fa42a62-f50d-4ac6-91ef-d6fd6f55523d"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "password456", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), true, "", "janesmith" },
                    { new Guid("31397fee-71c6-4212-9753-04be30505f18"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "password123", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), true, "", "johndoe" },
                    { new Guid("31690ab6-2820-4e6f-b2f7-bcd8ee87abb3"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", "Johnson", "password789", "555-2468013", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "michaeljohnson" },
                    { new Guid("4f096f46-759a-4575-ab1b-0a5af76fc4b7"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.lee@example.com", "David", "Lee", "passworddef", "555-4725836", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "davidlee" },
                    { new Guid("b30a70c8-dc20-4d71-b945-80cdfd7d4cf6"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", "Davis", "passwordabc", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), true, "", "emilydavis" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1fa42a62-f50d-4ac6-91ef-d6fd6f55523d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("31397fee-71c6-4212-9753-04be30505f18"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("31690ab6-2820-4e6f-b2f7-bcd8ee87abb3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("4f096f46-759a-4575-ab1b-0a5af76fc4b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b30a70c8-dc20-4d71-b945-80cdfd7d4cf6"));
        }
    }
}
