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
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
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
                    MaxPriceDifference = table.Column<float>(type: "real", nullable: false),
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
                    CurrentProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    BuyerConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SellerConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrders", x => x.PreOrderId);
                    table.ForeignKey(
                        name: "FK_PreOrders_Products_CurrentProductId",
                        column: x => x.CurrentProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_PreOrders_Products_TargetProductId",
                        column: x => x.TargetProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    { new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "User" },
                    { new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "EmailConfirm", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$rAu8lKC0F3RBLJiAmgqfA.iYcd2oxEUiRtuQ0H6XHNQMCuXaIdHJe", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$J9ZitJ30zjeLxzGP71TMAus7L6kx3AUhnBtkaf79IBX0AojtfzFEO", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "phuongthao" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$1LJ4bNdCGGw8RfXMdCuMLu4LwxxOEmNit8FGfKskjiR1RZM5XwotK", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$45x5bx.zn7Fj25RlYcq4I.hNrHtjon2tdpmiV4LqhJ.soSBE81ACq", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "haugiang" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$yuiAfGdKCjDZUCQJ.muDlu8PkK1XBtiWzpTEiM9rHQ8h6g4egkP3m", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "quoctrieu" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "MaxPriceDifference", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("010c482b-94c1-4a88-9953-0dd4b46dee21"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3402), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 0f, 10f, "Product 1", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3399), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("101f026f-f722-4f42-a21b-53f58991ef0c"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3489), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 0f, 90f, "Product 9", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3489), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("44334523-6396-436a-bb06-8d20108aea93"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3495), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 0f, 100f, "Product 10", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3494), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("70103561-b4c6-4970-8032-a033956096af"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3486), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 0f, 80f, "Product 8", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3486), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("810c8f7f-3a99-44e2-870f-46898a13da33"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3426), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 0f, 40f, "Product 4", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3426), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("b01dc1b5-143d-4a46-a17d-0850b31e3631"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3438), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 0f, 50f, "Product 5", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3438), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("bccde73e-ffdc-4cea-97b6-44358b7dfac0"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3423), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 0f, 30f, "Product 3", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3422), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("be048b47-5638-498c-9520-e0cd41dd50ed"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3410), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 0f, 20f, "Product 2", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3410), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("cbe91ff9-0c2a-4e66-be62-b4e5815c8171"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3477), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 0f, 60f, "Product 6", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3476), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("d52c55fb-3040-472c-ae33-effc58c59a48"), new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3483), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 0f, 70f, "Product 7", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3483), new Guid("fda6e282-e429-4364-a445-136b570e2fde") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("095f5d80-0941-4441-9dda-9136c0eb8679"), "Image for product 2", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3412), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("be048b47-5638-498c-9520-e0cd41dd50ed") },
                    { new Guid("2212b9bf-ff2f-4901-a194-1961bfaa5f61"), "Image for product 5", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3441), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("b01dc1b5-143d-4a46-a17d-0850b31e3631") },
                    { new Guid("277eb58d-a3dc-4dfc-a1ac-5c242bc44257"), "Image for product 9", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3492), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("101f026f-f722-4f42-a21b-53f58991ef0c") },
                    { new Guid("47ddb1da-fa4e-444e-b3b0-78a46fd6605d"), "Image for product 6", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3478), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("cbe91ff9-0c2a-4e66-be62-b4e5815c8171") },
                    { new Guid("8a887fbc-88a6-45cb-a466-dfa8aaaf5b9f"), "Image for product 4", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3427), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("810c8f7f-3a99-44e2-870f-46898a13da33") },
                    { new Guid("8b80c5ac-d735-4766-98dc-38f475a7f834"), "Image for product 7", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3484), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("d52c55fb-3040-472c-ae33-effc58c59a48") },
                    { new Guid("a367693a-6077-4479-b52f-0bcb9deff0ea"), "Image for product 10", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3496), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("44334523-6396-436a-bb06-8d20108aea93") },
                    { new Guid("d793bc32-4491-4f2d-821f-d8c0e0a2e719"), "Image for product 1", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3407), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("010c482b-94c1-4a88-9953-0dd4b46dee21") },
                    { new Guid("f0f5a55e-1d97-4f52-9558-75391877ae77"), "Image for product 8", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3487), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("70103561-b4c6-4970-8032-a033956096af") },
                    { new Guid("ff250afb-a98a-4159-990b-8ef8c47421cf"), "Image for product 3", new DateTime(2024, 7, 25, 15, 25, 19, 789, DateTimeKind.Utc).AddTicks(3424), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("bccde73e-ffdc-4cea-97b6-44358b7dfac0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_BuyerId",
                table: "PreOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_CurrentProductId",
                table: "PreOrders",
                column: "CurrentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_SellerId",
                table: "PreOrders",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_TargetProductId",
                table: "PreOrders",
                column: "TargetProductId");

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
