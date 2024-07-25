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
<<<<<<<< HEAD:GoodsExchange.Data/Migrations/20240722041138_InitialDatabase.cs
========
                name: "PreOrders",
                columns: table => new
                {
                    PreOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
>>>>>>>> feature/manage-accounts:GoodsExchange.Data/Migrations/20240725060127_InitialDatabase.cs
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
<<<<<<<< HEAD:GoodsExchange.Data/Migrations/20240722041138_InitialDatabase.cs
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "System", "Admin", "$2a$11$3n8VBhsSYvbFlJocImTa0OQaoHviVdme.fGNN.myxfyPhvmxWT.Ee", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$dYgTcg7hsujA2.P2rBm.EOQKwgYGNNUvv76ey8RnhOG8MaZ8Buuuy", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "phuongthao" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$16s.JUn1x9C6.5.XuSHMlePEQ4e.Dvi/Cy4s8lasrBIKIgGXZMs5a", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$LUVYDnjnVLWCsnHxlMEOXef8Rr4VfCyq4EE.kmcKdrnLwhT.trns.", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "haugiang" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$j4JVrlWNrzA8FmmHIO5dCeaz/rBF8DJXFBzpQ503YHc5wWDCjMzkm", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "quoctrieu" }
========
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$kqMQMObY.wQ4JS5MStoueOsNoS.0dsSwTR3GUZUJZ0VmElqwG6OxC", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$YpV8h6zYlJlDOMSRZkrKauOHPJx2c.evfI.zaqAdb/Y9cWXlszyNS", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "phuongthao" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$5MtHH5pjK63OmvH5eDskmeIGnmf7RkFgKAws/B0FRdqxpHL9HXGEO", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$15hGN3pkeLC2fsOcJl.shuCU8KiMEzgOxa0mqLqBTVJpPi6z1jimW", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "haugiang" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$dEzV7FYKYZvICkOXFcMNAuj9qESZrQOZniQ.VqlfKsbDLTjud/olK", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "quoctrieu" }
>>>>>>>> feature/manage-accounts:GoodsExchange.Data/Migrations/20240725060127_InitialDatabase.cs
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "MaxPriceDifference", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
<<<<<<<< HEAD:GoodsExchange.Data/Migrations/20240722041138_InitialDatabase.cs
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
========
                    { new Guid("126bf07d-8012-4a79-9a73-e3c2adeb2fd8"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4846), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 0f, 90f, "Product 9", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4846), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("7c36c0ee-9ba9-4f5b-8675-1b0b29fc8752"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4826), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 0f, 50f, "Product 5", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4826), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("8163a18b-a474-4f55-90b2-7d973115177e"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4850), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 0f, 100f, "Product 10", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4850), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("b0142c14-b880-40d9-98e3-55e517414dea"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4780), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 0f, 10f, "Product 1", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4778), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("bf6ad13c-c8fa-4c94-9d9e-85af596fd1b3"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4800), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 0f, 20f, "Product 2", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4799), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("c11bbc17-96af-4830-bdfe-28ccecbc9c03"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4838), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 0f, 70f, "Product 7", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4838), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("e2a57082-c5c3-479d-a3b3-ec3270e350c2"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4835), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 0f, 60f, "Product 6", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4835), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("e4ee609d-e08e-4e3c-95bd-55c91ddf4f9a"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4818), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 0f, 40f, "Product 4", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4818), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("e85c8390-98c4-413e-8c33-a00f585dc7a2"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4803), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 0f, 30f, "Product 3", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4803), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("effa8cfb-7227-4175-a599-906539474a74"), new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4843), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 0f, 80f, "Product 8", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4843), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
>>>>>>>> feature/manage-accounts:GoodsExchange.Data/Migrations/20240725060127_InitialDatabase.cs
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
<<<<<<<< HEAD:GoodsExchange.Data/Migrations/20240722041138_InitialDatabase.cs
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
========
                    { new Guid("1d9a4082-205c-4ecc-a0a0-350be44e6c6f"), "Image for product 1", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4792), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("b0142c14-b880-40d9-98e3-55e517414dea") },
                    { new Guid("44141e4c-ac2d-482b-975d-03babe4f6d37"), "Image for product 6", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4836), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("e2a57082-c5c3-479d-a3b3-ec3270e350c2") },
                    { new Guid("49e6ab27-24bb-4e0b-884d-5fd76276ebaf"), "Image for product 10", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4851), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("8163a18b-a474-4f55-90b2-7d973115177e") },
                    { new Guid("6d53ebfc-2497-4560-a30b-e3c8fe1c6d0b"), "Image for product 2", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4801), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("bf6ad13c-c8fa-4c94-9d9e-85af596fd1b3") },
                    { new Guid("761da536-d2c1-4ef4-a860-7f5d6bc7b586"), "Image for product 4", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4820), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("e4ee609d-e08e-4e3c-95bd-55c91ddf4f9a") },
                    { new Guid("79fba7ad-c5b6-475d-b365-6f2144008df4"), "Image for product 5", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4828), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("7c36c0ee-9ba9-4f5b-8675-1b0b29fc8752") },
                    { new Guid("8cbc8811-04b1-4e3b-95d9-f30f5bc8d09d"), "Image for product 9", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4847), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("126bf07d-8012-4a79-9a73-e3c2adeb2fd8") },
                    { new Guid("9a757039-1a7c-4029-b634-1f5f21920c31"), "Image for product 7", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4841), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("c11bbc17-96af-4830-bdfe-28ccecbc9c03") },
                    { new Guid("9ab6e772-15d4-4137-8bf1-22b8cb63d133"), "Image for product 3", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4816), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("e85c8390-98c4-413e-8c33-a00f585dc7a2") },
                    { new Guid("c1787415-fe12-4c65-8078-c0779f03f400"), "Image for product 8", new DateTime(2024, 7, 25, 6, 1, 27, 580, DateTimeKind.Utc).AddTicks(4844), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("effa8cfb-7227-4175-a599-906539474a74") }
>>>>>>>> feature/manage-accounts:GoodsExchange.Data/Migrations/20240725060127_InitialDatabase.cs
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
