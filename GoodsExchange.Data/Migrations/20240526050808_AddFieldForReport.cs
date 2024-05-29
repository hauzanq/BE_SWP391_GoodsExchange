using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class AddFieldForReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("014e5790-db27-411a-9e53-dc1615ecb71c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3544651a-9d54-464a-9721-322acf8a1a18"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3f86d44f-3217-4895-864d-480d7123d265"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("68a2fd9c-1bee-485f-8fd9-82ed163f8bff"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("715fa57a-ae78-47e4-a82b-315d186cf698"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("74b67181-857d-4870-bc79-07c8e6b435d0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a74b7ce8-b8f8-40d4-b0a5-05507eab6a44"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a8d2027d-f034-473b-b3d4-c941251fe227"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a9cea1f2-dc30-4ee5-ad0f-4faa8701dac3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("aa1651fa-2455-4a62-8356-0eaaf2257202"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate" },
                values: new object[,]
                {
                    { new Guid("1480b424-e089-46d9-af4a-55f84a7c4f07"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2206107c-a77f-461c-800f-9bf87a6cd389"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("24d383c7-f644-49b5-a679-2ce074b92890"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("577a210a-3db6-4ef6-85f1-f0de92da33f4"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("58397f59-aed9-4c18-a396-3820505dd05e"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5954a910-2598-4b69-8508-7a07394f2b3d"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("64fe6e60-3fc6-4070-9372-9128a0b32e45"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("87b9299d-b192-4bb7-afa2-ef975509a12a"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e2fa0f4b-48cf-4fdf-987b-e2914b04addf"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f933f3b1-6aa4-4892-a17f-a819e3c1b796"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1480b424-e089-46d9-af4a-55f84a7c4f07"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2206107c-a77f-461c-800f-9bf87a6cd389"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("24d383c7-f644-49b5-a679-2ce074b92890"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("577a210a-3db6-4ef6-85f1-f0de92da33f4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("58397f59-aed9-4c18-a396-3820505dd05e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5954a910-2598-4b69-8508-7a07394f2b3d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("64fe6e60-3fc6-4070-9372-9128a0b32e45"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("87b9299d-b192-4bb7-afa2-ef975509a12a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e2fa0f4b-48cf-4fdf-987b-e2914b04addf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f933f3b1-6aa4-4892-a17f-a819e3c1b796"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "Reports");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate" },
                values: new object[,]
                {
                    { new Guid("014e5790-db27-411a-9e53-dc1615ecb71c"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3544651a-9d54-464a-9721-322acf8a1a18"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3f86d44f-3217-4895-864d-480d7123d265"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("68a2fd9c-1bee-485f-8fd9-82ed163f8bff"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("715fa57a-ae78-47e4-a82b-315d186cf698"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("74b67181-857d-4870-bc79-07c8e6b435d0"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a74b7ce8-b8f8-40d4-b0a5-05507eab6a44"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a8d2027d-f034-473b-b3d4-c941251fe227"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a9cea1f2-dc30-4ee5-ad0f-4faa8701dac3"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("aa1651fa-2455-4a62-8356-0eaaf2257202"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
