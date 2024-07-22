using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailConfirm = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUploadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserUploadId",
                        column: x => x.UserUploadId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberStars = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsApprove = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Reports_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Reports_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "School Supplies" },
                    { new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Art Supplies" },
                    { new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "Moderator" },
                    { new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "Customer" },
                    { new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "EmailConfirm", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "System", "Admin", "$2a$11$3n8VBhsSYvbFlJocImTa0OQaoHviVdme.fGNN.myxfyPhvmxWT.Ee", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$dYgTcg7hsujA2.P2rBm.EOQKwgYGNNUvv76ey8RnhOG8MaZ8Buuuy", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "phuongthao" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$16s.JUn1x9C6.5.XuSHMlePEQ4e.Dvi/Cy4s8lasrBIKIgGXZMs5a", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$LUVYDnjnVLWCsnHxlMEOXef8Rr4VfCyq4EE.kmcKdrnLwhT.trns.", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "haugiang" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$j4JVrlWNrzA8FmmHIO5dCeaz/rBF8DJXFBzpQ503YHc5wWDCjMzkm", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "quoctrieu" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("54b29331-47f2-4a4f-883e-059760183989"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2916), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 90f, "Product 9", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2916), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("8273a702-e264-4086-b3fe-b9259dd55841"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2888), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 40f, "Product 4", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2888), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("8ef96aa6-4909-4ed9-8bb0-849dcabd3019"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2884), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 30f, "Product 3", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2884), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("9ed06e9a-b0de-44f4-8687-5714c5a46451"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2880), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 20f, "Product 2", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2880), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("bf7a2ead-b61d-47f1-b272-a5c9ffff03e0"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2913), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 80f, "Product 8", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2913), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("c2065469-632f-4e76-bd75-be725b92b50a"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2847), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 10f, "Product 1", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2843), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("c7b06ffa-6a3f-41a8-813b-94c7fa69a041"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2924), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 100f, "Product 10", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2923), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("cb688e5b-0218-48e9-bfb0-9d82a5fec85e"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2897), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 50f, "Product 5", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2897), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("dbef2450-3327-4a0b-822d-916969a40f39"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2905), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 60f, "Product 6", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2905), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("f48bd0e5-4bd4-468d-9f20-fb11f9ffc814"), new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2909), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 70f, "Product 7", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2909), new Guid("fda6e282-e429-4364-a445-136b570e2fde") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("05717875-9059-4c44-a507-bf2fa4d01b33"), "Image for product 2", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2882), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("9ed06e9a-b0de-44f4-8687-5714c5a46451") },
                    { new Guid("21453bfd-3614-40fd-87c5-d299830ef320"), "Image for product 8", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2914), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("bf7a2ead-b61d-47f1-b272-a5c9ffff03e0") },
                    { new Guid("4bc4715c-cef9-4114-95f6-9f617907a11f"), "Image for product 4", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2890), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("8273a702-e264-4086-b3fe-b9259dd55841") },
                    { new Guid("505c6eb3-5334-415a-8b9a-fe9df169e00d"), "Image for product 5", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2900), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("cb688e5b-0218-48e9-bfb0-9d82a5fec85e") },
                    { new Guid("5ead355e-aa46-4f3f-813b-ab5447bac0c8"), "Image for product 6", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2907), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("dbef2450-3327-4a0b-822d-916969a40f39") },
                    { new Guid("797ee6e2-fbc8-4940-a2a4-124a2f8c7fef"), "Image for product 9", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2919), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("54b29331-47f2-4a4f-883e-059760183989") },
                    { new Guid("7f28fac3-4af8-4b2d-b75b-7ee516f9f21c"), "Image for product 3", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2886), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("8ef96aa6-4909-4ed9-8bb0-849dcabd3019") },
                    { new Guid("908fad68-a621-44f5-87fe-2d5d46cd122b"), "Image for product 1", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2861), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("c2065469-632f-4e76-bd75-be725b92b50a") },
                    { new Guid("e521ad17-c285-4f7f-a5ac-390373749dca"), "Image for product 7", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2911), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("f48bd0e5-4bd4-468d-9f20-fb11f9ffc814") },
                    { new Guid("ece9fe52-0d07-4ab8-95ed-62e0ca2b6e61"), "Image for product 10", new DateTime(2024, 7, 22, 4, 11, 37, 914, DateTimeKind.Utc).AddTicks(2925), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("c7b06ffa-6a3f-41a8-813b-94c7fa69a041") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserUploadId",
                table: "Products",
                column: "UserUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ReceiverId",
                table: "Ratings",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SenderId",
                table: "Ratings",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProductId",
                table: "Reports",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReceiverId",
                table: "Reports",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SenderId",
                table: "Reports",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
