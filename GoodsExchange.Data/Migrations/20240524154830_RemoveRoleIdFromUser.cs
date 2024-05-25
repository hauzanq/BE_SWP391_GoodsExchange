using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class RemoveRoleIdFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("41e810c4-5b7b-426d-9f03-c07e05f5f7d6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4cce3fa4-f770-4444-8d46-0ce725d9849f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("569eb778-377d-44ad-972a-63a5e3b5bbdb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("754a289e-1434-4213-9dd0-6ffda8e1c0bf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8d8b3c74-1c3f-40af-af76-224a186b5362"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a58ef700-9264-44a3-9ead-8ab29643c612"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ab8c274e-6993-4943-9e6b-22b1bab4804d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b9fb0979-1238-4639-9cc5-4005bd004a0f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d217145d-f571-4fa8-8fd0-1cc664a4f4cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e80311af-5660-482f-a4b1-1d3274510edb"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsApproved", "Price", "ProductImageUrl", "ProductName", "Status", "UploadDate" },
                values: new object[,]
                {
                    { new Guid("41e810c4-5b7b-426d-9f03-c07e05f5f7d6"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4cce3fa4-f770-4444-8d46-0ce725d9849f"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", true, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("569eb778-377d-44ad-972a-63a5e3b5bbdb"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", true, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("754a289e-1434-4213-9dd0-6ffda8e1c0bf"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", true, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8d8b3c74-1c3f-40af-af76-224a186b5362"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", true, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a58ef700-9264-44a3-9ead-8ab29643c612"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", true, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ab8c274e-6993-4943-9e6b-22b1bab4804d"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", true, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b9fb0979-1238-4639-9cc5-4005bd004a0f"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, 1.5f, "https://example.com/ruler.jpg", "Ruler", true, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d217145d-f571-4fa8-8fd0-1cc664a4f4cd"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", true, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e80311af-5660-482f-a4b1-1d3274510edb"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"),
                column: "RoleId",
                value: new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"),
                column: "RoleId",
                value: new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"),
                column: "RoleId",
                value: new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"),
                column: "RoleId",
                value: new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d6446689-2743-460b-82c3-d25b21f87b13"),
                column: "RoleId",
                value: new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"));
        }
    }
}
