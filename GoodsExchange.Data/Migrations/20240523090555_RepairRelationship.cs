using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class RepairRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Ratings_ProductId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reports_ProductId",
                table: "Products");

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

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "Status", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("6b840da9-37fe-4f7f-ba3b-e96138685bfe"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", "Davis", "passwordabc", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), true, "", "emilydavis" },
                    { new Guid("6ea3fd32-96a2-4b0b-862a-3de2158b8b68"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "password123", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), true, "", "johndoe" },
                    { new Guid("863aa0e5-1331-4f9f-a27c-ebfaaaf84ae9"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", "Johnson", "password789", "555-2468013", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "michaeljohnson" },
                    { new Guid("c898cf09-4b01-4e79-9b8c-f396c318d69e"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.lee@example.com", "David", "Lee", "passworddef", "555-4725836", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "davidlee" },
                    { new Guid("f306b2a2-fbe5-41c0-bd1f-75eb00ef3fa2"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "password456", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), true, "", "janesmith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProductId",
                table: "Reports",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Products_ProductId",
                table: "Reports",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Products_ProductId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ProductId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6b840da9-37fe-4f7f-ba3b-e96138685bfe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6ea3fd32-96a2-4b0b-862a-3de2158b8b68"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("863aa0e5-1331-4f9f-a27c-ebfaaaf84ae9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c898cf09-4b01-4e79-9b8c-f396c318d69e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f306b2a2-fbe5-41c0-bd1f-75eb00ef3fa2"));

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Products");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Ratings_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Ratings",
                principalColumn: "RatingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reports_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
