using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class UpdateDatabaseAfterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0128ad95-b1cd-46e6-9a44-aef377c14d4f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("31eb1600-0a04-434d-8ee2-8b02f56c7e81"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5502fdc2-b361-4822-868a-af50f16fd57a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7807e6f0-b637-4b35-acfc-461df494150c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7b3ea1d5-13ea-4583-b0b0-5d83237221b8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a918e90f-ccee-4238-8fb5-3c460950b9cb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ba860139-aa3c-4c34-a3a5-643e93a33188"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("bdf991c7-2028-4f10-85e6-05d109008808"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d900e8fc-74ca-4e1d-95b1-93b38ce010bd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ecdec053-c070-490e-9e7d-c397a4c177d6"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("0385075b-685a-4927-ad5c-500893f30d3b"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("196998a7-d49a-408e-b810-b88cd992a839"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("30545dd8-5a28-4c9d-81cd-498749697625"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("7a330f74-7df8-48eb-a777-948b4ea967cd"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("8989b621-ae4a-4b31-886b-4101523394e5"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("9cb89464-7d81-429a-96c9-bfe121317b22"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("a57d96c0-5b91-4f77-a7dd-d24f377e50a7"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("bd796b33-afc6-4c60-8e18-746a09876c9b"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("d3e2bc81-0a62-4b11-9cf1-563116c56023"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("d983c610-6f62-4c75-b828-66fce19f57dd"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0385075b-685a-4927-ad5c-500893f30d3b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("196998a7-d49a-408e-b810-b88cd992a839"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("30545dd8-5a28-4c9d-81cd-498749697625"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7a330f74-7df8-48eb-a777-948b4ea967cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8989b621-ae4a-4b31-886b-4101523394e5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9cb89464-7d81-429a-96c9-bfe121317b22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a57d96c0-5b91-4f77-a7dd-d24f377e50a7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("bd796b33-afc6-4c60-8e18-746a09876c9b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d3e2bc81-0a62-4b11-9cf1-563116c56023"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d983c610-6f62-4c75-b828-66fce19f57dd"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("0128ad95-b1cd-46e6-9a44-aef377c14d4f"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("31eb1600-0a04-434d-8ee2-8b02f56c7e81"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("5502fdc2-b361-4822-868a-af50f16fd57a"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("7807e6f0-b637-4b35-acfc-461df494150c"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("7b3ea1d5-13ea-4583-b0b0-5d83237221b8"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("a918e90f-ccee-4238-8fb5-3c460950b9cb"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("ba860139-aa3c-4c34-a3a5-643e93a33188"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("bdf991c7-2028-4f10-85e6-05d109008808"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("d900e8fc-74ca-4e1d-95b1-93b38ce010bd"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("ecdec053-c070-490e-9e7d-c397a4c177d6"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") }
                });
        }
    }
}
