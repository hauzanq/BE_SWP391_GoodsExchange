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
                    EmailConfirm = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                name: "PreOrders",
                columns: table => new
                {
                    PreOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BuyerConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SellerConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrders", x => x.PreOrderId);
                    table.ForeignKey(
                        name: "FK_PreOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreOrders_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PreOrders_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_PreOrders_PreOrderId",
                        column: x => x.PreOrderId,
                        principalTable: "PreOrders",
                        principalColumn: "PreOrderId");
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
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "John", "Doe", "$2a$11$Q7PwN8HEihUas9qWm3QW9OxgZOHU29ZWLih0rUTLRLHLWCPI4Zodq", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhihi@gmail.com", "Michael", "Johnson", "$2a$11$BQZd.hzDCNsNfwnI7GgpneFohWf000JE51gTR7.BVLn1CYipJ3uAq", "555-2468013", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhihi" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "moderator@gmail.com", "Jane", "Smith", "$2a$11$wOPg1YYEvVUsYOAj9giZF.h3ohezDDVpu9WhnAxkXRyb0mURCuVGS", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhehe@gmail.com", "David", "Lee", "$2a$11$nQO0F.QGScu/azZEg3mQnuZHQz8CenuC7PqNtZjstwhZdkkZJnUC2", "555-4725836", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhehe" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhaha@gmail.com", "Emily", "Davis", "$2a$11$4DCNpgkrJdDjjXFAMnWWGeeYLKvbQKKoINUnsfDRnw5j4MorFoSsi", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhaha" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("19f427a7-2934-46f3-ad57-ead6ea7c4859"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5607), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 100f, "Product 10", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5607), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("1a649ca3-177a-4dae-97f0-0cca205ad9a2"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5559), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 30f, "Product 3", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5559), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("1dc0d125-87ec-41d3-adcc-50e0721f1a74"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5545), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 10f, "Product 1", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5541), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("29f79264-756d-49c7-bbf3-35f0a2b47d21"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5602), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 90f, "Product 9", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5602), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("54080d7e-7af3-44a3-90f4-caaa8b5fafc0"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5594), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 70f, "Product 7", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5593), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("5e0c276e-7b91-4a38-821b-351be2121360"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5590), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 60f, "Product 6", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5590), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("61b5c65d-c6e1-422e-96fa-07a3f0891c46"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5568), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 40f, "Product 4", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5568), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("a6d48aa1-b3f8-4e5d-a9b6-9b720c0461f2"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5599), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 80f, "Product 8", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5598), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("d6ebfda9-2d13-4af6-8e04-303ac07daf12"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5555), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 20f, "Product 2", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5555), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("dc9ceabd-610e-4497-8ad3-7050251853b9"), new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5585), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 50f, "Product 5", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5584), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("30183408-56a1-4076-8798-9f80bbe5a580"), "Image for product 3", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5566), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("1a649ca3-177a-4dae-97f0-0cca205ad9a2") },
                    { new Guid("3d962619-c1d9-404b-bbd1-3c5974f2a37a"), "Image for product 9", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5604), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("29f79264-756d-49c7-bbf3-35f0a2b47d21") },
                    { new Guid("4d4ce3c9-5c95-4124-8e0a-14a4de357b46"), "Image for product 10", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5608), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("19f427a7-2934-46f3-ad57-ead6ea7c4859") },
                    { new Guid("4e6cbf95-9d5f-4e09-8945-1f73a088b45a"), "Image for product 7", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5597), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("54080d7e-7af3-44a3-90f4-caaa8b5fafc0") },
                    { new Guid("72d0d9ea-f0ee-473a-af52-a9054bc4a77a"), "Image for product 4", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5569), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("61b5c65d-c6e1-422e-96fa-07a3f0891c46") },
                    { new Guid("7b575147-f1a0-4b5b-8705-975ea37fb4d5"), "Image for product 8", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5600), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("a6d48aa1-b3f8-4e5d-a9b6-9b720c0461f2") },
                    { new Guid("d8ea3b05-676c-4399-95ac-60eb235ce68d"), "Image for product 6", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5592), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("5e0c276e-7b91-4a38-821b-351be2121360") },
                    { new Guid("ef1e03f7-3b2b-4c6d-a2cb-d32f6f9f6549"), "Image for product 2", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5557), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("d6ebfda9-2d13-4af6-8e04-303ac07daf12") },
                    { new Guid("f2dade84-238e-47ed-b70d-b3de9bf6ea28"), "Image for product 1", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5553), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("1dc0d125-87ec-41d3-adcc-50e0721f1a74") },
                    { new Guid("f72b8d80-e51e-4c14-bde6-302cffb17ef7"), "Image for product 5", new DateTime(2024, 7, 21, 19, 12, 42, 988, DateTimeKind.Utc).AddTicks(5588), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("dc9ceabd-610e-4497-8ad3-7050251853b9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_BuyerId",
                table: "PreOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_ProductId",
                table: "PreOrders",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_SellerId",
                table: "PreOrders",
                column: "SellerId");

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
                name: "IX_Transactions_PreOrderId",
                table: "Transactions",
                column: "PreOrderId",
                unique: true);

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
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PreOrders");

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
