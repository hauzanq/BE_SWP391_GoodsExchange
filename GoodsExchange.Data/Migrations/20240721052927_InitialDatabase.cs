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
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "John", "Doe", "$2a$11$DOhmqxUcRU925GGFOzfYsu3HqfTSBgDdwW.pNcLgdtzI4m6WV6nWK", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhihi@gmail.com", "Michael", "Johnson", "$2a$11$k3Q0X2A7d4BzflmYO/an5ejlHZcKgcm0ap3vk37Ua.Z3PY9L/Kx1C", "555-2468013", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhihi" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "moderator@gmail.com", "Jane", "Smith", "$2a$11$/delu8/axUCsovbhjuqM5e7aLkLg1dOnhdD3JP4yhAHzd6.BXj916", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhehe@gmail.com", "David", "Lee", "$2a$11$Nm9ivNzEqCEMrVbgb03zk.ujTuzKVp/tj2ouJZw5PrUOTdmrt7SoS", "555-4725836", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhehe" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customerhaha@gmail.com", "Emily", "Davis", "$2a$11$Lwc.dHYw4Dzz4jepKZFpze5hsisBLJj60g1YqHEt7T2Pl2EjjAn/a", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhaha" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("0e09f661-e9f0-4ef8-9794-a2df01b58ea2"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6729), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 40f, "Product 4", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6729), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("25fadc22-7fed-4f85-9a8c-6b248ce48b5b"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6757), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 90f, "Product 9", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6757), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("78a50459-bd84-40d4-97b4-cdcea4a8bd01"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6764), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 100f, "Product 10", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6763), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("8e9b7a70-f1ac-427f-bec9-c02ad71fd4e5"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6754), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 80f, "Product 8", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6753), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("bb95dae9-946b-4cc7-988b-0ac4bb726635"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6751), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 70f, "Product 7", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6750), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("ca0f15a4-12e0-4ae3-90cb-6c5b8989c4cb"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6721), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 20f, "Product 2", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6721), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("e84825c6-790f-45e0-b20e-223c2ceb0925"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6688), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 10f, "Product 1", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6684), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("eb9ecc0b-b16b-4bb0-811f-f955bbeb6660"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6747), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 60f, "Product 6", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6746), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("f4d91682-b24a-4c81-91b4-16a0f8cf81e5"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6726), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 30f, "Product 3", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6725), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("fa15a70b-6b98-4842-b9d3-03bc6099d617"), new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6739), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 50f, "Product 5", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6738), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("143b4171-d19f-4d5d-b154-251b94e94f59"), "Image for product 10", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6765), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("78a50459-bd84-40d4-97b4-cdcea4a8bd01") },
                    { new Guid("297d6ab9-ba65-4313-8d9c-f83306ebbdf2"), "Image for product 9", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6759), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("25fadc22-7fed-4f85-9a8c-6b248ce48b5b") },
                    { new Guid("355b927d-6578-45bc-93ca-7e4cbd5cf219"), "Image for product 2", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6723), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("ca0f15a4-12e0-4ae3-90cb-6c5b8989c4cb") },
                    { new Guid("540b779f-67a6-44b9-b080-681d9303e3f6"), "Image for product 4", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6731), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("0e09f661-e9f0-4ef8-9794-a2df01b58ea2") },
                    { new Guid("8363d684-77b0-4c28-882a-f5fbb0be57de"), "Image for product 7", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6752), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("bb95dae9-946b-4cc7-988b-0ac4bb726635") },
                    { new Guid("9e3bb7d5-80e3-4049-87b4-b597fccf748c"), "Image for product 8", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6755), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("8e9b7a70-f1ac-427f-bec9-c02ad71fd4e5") },
                    { new Guid("b36f172e-1a2e-496b-a30b-ac1575c84783"), "Image for product 5", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6742), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("fa15a70b-6b98-4842-b9d3-03bc6099d617") },
                    { new Guid("d573b968-7a40-4f68-a2cf-e88032d8c09c"), "Image for product 6", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6748), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("eb9ecc0b-b16b-4bb0-811f-f955bbeb6660") },
                    { new Guid("e70a3ee4-dda5-4e0f-8b83-ef5e3854f4c3"), "Image for product 3", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6727), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("f4d91682-b24a-4c81-91b4-16a0f8cf81e5") },
                    { new Guid("f671abc8-1924-4c13-bc70-910a3cb2e425"), "Image for product 1", new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6698), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("e84825c6-790f-45e0-b20e-223c2ceb0925") }
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
