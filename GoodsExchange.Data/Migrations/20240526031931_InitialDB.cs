using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class InitialDB : Migration
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate" },
                values: new object[,]
                {
                    { new Guid("02b2f7e7-6d9b-413e-b2f1-2d045be2a2df"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("05d96e05-4d3a-4e3a-8bea-eeec2fcb33ec"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("0e0f7879-04dd-45ba-8bda-1e80216f6030"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3f87323e-7a89-43fc-b6d4-18919b4832cf"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7a52b01c-203c-4ce8-bd1b-9a65dc49af56"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("91047c2e-274e-4695-a7df-1642d3fa017c"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b441c407-5ccb-4e18-b8b8-74acb20c6eae"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c6f8e167-7df8-42ec-9473-a89998696daa"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d844fbc5-f0eb-4444-b16c-5f042a6ea073"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ed187a9a-5a8a-4c91-83e6-aa21b8a6759f"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("02b2f7e7-6d9b-413e-b2f1-2d045be2a2df"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("05d96e05-4d3a-4e3a-8bea-eeec2fcb33ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0e0f7879-04dd-45ba-8bda-1e80216f6030"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3f87323e-7a89-43fc-b6d4-18919b4832cf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7a52b01c-203c-4ce8-bd1b-9a65dc49af56"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("91047c2e-274e-4695-a7df-1642d3fa017c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b441c407-5ccb-4e18-b8b8-74acb20c6eae"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c6f8e167-7df8-42ec-9473-a89998696daa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d844fbc5-f0eb-4444-b16c-5f042a6ea073"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ed187a9a-5a8a-4c91-83e6-aa21b8a6759f"));

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
