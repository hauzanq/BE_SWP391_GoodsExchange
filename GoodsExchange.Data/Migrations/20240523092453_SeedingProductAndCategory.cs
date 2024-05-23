using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class SeedingProductAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Stationery" },
                    { new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Drawing Supplies" },
                    { new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Tech Devices" },
                    { new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Books and Materials" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "Status", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("14670f4f-e528-490c-8fec-c47c9ef7a1df"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "password123", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), true, "", "johndoe" },
                    { new Guid("1679790e-7b6a-401e-b6ad-523e131cf15a"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.lee@example.com", "David", "Lee", "passworddef", "555-4725836", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "davidlee" },
                    { new Guid("44ae76e6-c528-47fe-9682-c537e906b1f5"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", "Johnson", "password789", "555-2468013", new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), false, "", "michaeljohnson" },
                    { new Guid("48d97624-3e0c-43e7-9226-39226bece954"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "password456", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), true, "", "janesmith" },
                    { new Guid("ca8fcf7b-3f45-48eb-b85f-e769b5456c37"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", "Davis", "passwordabc", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), true, "", "emilydavis" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate" },
                values: new object[,]
                {
                    { new Guid("4b06683a-d623-4a5b-88c8-fc7ddea6e561"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("635ed14e-9859-4daa-b152-8ee7d62e6b46"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("733aebf8-441c-400f-8050-3665a67759ca"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8b17adbe-2da6-46c1-ae33-3a43ec988f6c"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9f036ca5-361c-42b8-aebc-c4c6b55481ff"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c39e1231-f3ef-4e4a-b652-bd3578360fe4"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9b824ca-9b89-4e4d-ac98-ef620f8c50b5"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d188f86a-b7bd-4c14-bc77-09265f85729e"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e72cc9a5-49f5-4896-a186-28c37380b963"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ea3f9d0a-fd3f-4d8e-a065-594036f9eb4d"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4b06683a-d623-4a5b-88c8-fc7ddea6e561"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("635ed14e-9859-4daa-b152-8ee7d62e6b46"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("733aebf8-441c-400f-8050-3665a67759ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8b17adbe-2da6-46c1-ae33-3a43ec988f6c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9f036ca5-361c-42b8-aebc-c4c6b55481ff"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c39e1231-f3ef-4e4a-b652-bd3578360fe4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c9b824ca-9b89-4e4d-ac98-ef620f8c50b5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d188f86a-b7bd-4c14-bc77-09265f85729e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e72cc9a5-49f5-4896-a186-28c37380b963"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ea3f9d0a-fd3f-4d8e-a065-594036f9eb4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("14670f4f-e528-490c-8fec-c47c9ef7a1df"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1679790e-7b6a-401e-b6ad-523e131cf15a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("44ae76e6-c528-47fe-9682-c537e906b1f5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("48d97624-3e0c-43e7-9226-39226bece954"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ca8fcf7b-3f45-48eb-b85f-e769b5456c37"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"));

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
        }
    }
}
