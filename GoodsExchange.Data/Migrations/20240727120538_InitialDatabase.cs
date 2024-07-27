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
                name: "ExchangeRequests",
                columns: table => new
                {
                    ExchangeRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ReceiverStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRequests", x => x.ExchangeRequestId);
                    table.ForeignKey(
                        name: "FK_ExchangeRequests_Products_CurrentProductId",
                        column: x => x.CurrentProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ExchangeRequests_Products_TargetProductId",
                        column: x => x.TargetProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ExchangeRequests_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ExchangeRequests_Users_SenderId",
                        column: x => x.SenderId,
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
                    ExchangeRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_ExchangeRequests_ExchangeRequestId",
                        column: x => x.ExchangeRequestId,
                        principalTable: "ExchangeRequests",
                        principalColumn: "ExchangeRequestId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "School Supplies" },
                    { new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Stationery" },
                    { new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "Lab Equipment" },
                    { new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Textbooks" }
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
                    { new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"), new DateTime(2000, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "thaonguyen@gmail.com", true, "Thảo", "Nguyễn", "$2a$11$eWqbYpgcOAlM/YyYUcsOx.sD9gHc3oYKwASUFQlG/kLBj5ip.Toby", "0912345678", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "thaonguyen" },
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$4rhg0Bk0DhaWT9bvW.gjquz3vk/eDUmepgsgmTwZJ7W1hKNhpZR8S", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "admin" },
                    { new Guid("0da4bfc5-3a37-4a66-91d5-fe9e1d086564"), new DateTime(2004, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khangvo@gmail.com", true, "Khang", "Võ", "$2a$11$DJED2zeYxhi/PjXznbWwiuNJci2mSrjfuWyoW0QEearb7xn2Lu8di", "0923456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "khangvo" },
                    { new Guid("15b2d60f-bdfc-4ce1-8b03-f8b8c3d7480d"), new DateTime(1998, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhpham@gmail.com", true, "Anh", "Phạm", "$2a$11$pgo0EiVb6m6q8pCTQXCir.lp2HX4JfRmzbBcH8d6StUsuDxssuEZe", "0908765432", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "anhpham" },
                    { new Guid("273c8fa3-b3f4-4c6d-87da-07bc63a4c436"), new DateTime(1995, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhtran@gmail.com", true, "Minh", "Trần", "$2a$11$rAk2XwQ0epsKDB9SasFmQOhd9BYCAxDjOa6jIfby3svUbtednvuj2", "0987654321", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhtran" },
                    { new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhphuoc@gmail.com", true, "Minh", "Phuoc", "$2a$11$Lho0i/GyETa94iJIrU3KluQ5yJ/KuBjdhGvD7o6WNdvUNfa7k3yMq", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhphuoc" },
                    { new Guid("7c87c075-2c90-4c6e-8e8b-b903ec331072"), new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tranvanc@gmail.com", true, "Tran", "Van C", "$2a$11$eCq4AW.GtWd/817beRYHCebvXDjxqK4.qj2J95FJWhJiAaMxO4EFK", "0904567890", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tranvanc" },
                    { new Guid("803b1549-478c-421b-bd4b-2b6111836609"), new DateTime(2001, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lethib@gmail.com", true, "Le", "Thi B", "$2a$11$N/xEb45QUQwhHfG/u5mafO3AlIHWqcK5UbCzEn8OmMLp1hp3HrSiW", "0903456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lethib" },
                    { new Guid("8130b91b-2e32-4aad-8479-3e01dd8813e3"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang01@gmail.com", true, "Hậu", "Giang", "$2a$11$qpnr2IVd3NJGc8vldUnIEOI7bKSYA5h2FqkK7tvmc4hbktnEoBRvm", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang45" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$Z4n/kFTYHzMAIibvUJ822OuzdWvq52vj68liY1JzTV58A.xfKoPr2", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phuongthao" },
                    { new Guid("97108917-856a-42af-937b-7b0e2e735b20"), new DateTime(1997, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "huongtrinh@gmail.com", true, "Hương", "Trịnh", "$2a$11$rjQu11pGuBl1fn3cUF/khekPHi9XUIXSpTPL0xrJdQcAEQ9nNxHfG", "0937894561", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "huongtrinh" },
                    { new Guid("a6f939d3-eb8a-4884-8084-b1dc99559167"), new DateTime(2004, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dovane@gmail.com", true, "Do", "Van E", "$2a$11$DDWtBJxUanwmmV/f5.UrzOMvygxPgvySTFWfGDqjIvihNg8mUPI.G", "0906789012", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "dovane" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$B2r2B/CQ46yDqJbvnJsl7OcEWfMGWW1J4Gc22bnbwhcu7wKKrn2nC", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "moderator" },
                    { new Guid("bf0b0b85-e98e-4da7-af44-b7198685cb78"), new DateTime(2002, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "tudo@gmail.com", true, "Tú", "Đỗ", "$2a$11$jk1eEcsEUAMPgxzmBJ67Jumu9eex1Dqv3Sahu/ExG3U8OeKGwMfOa", "0981234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tudo" },
                    { new Guid("ce914c08-8b01-483a-8b16-1c0e27284cc8"), new DateTime(1999, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhoang@gmail.com", true, "Lan", "Hoàng", "$2a$11$9UlO8vFcwyNpSeudxhzEZuoDxl4U84LheKvRxsqnNGuaGjJhsN6CS", "0934567890", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lanhoang" },
                    { new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"), new DateTime(1996, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "namle@gmail.com", true, "Nam", "Lê", "$2a$11$FS1vzJhQdW.2Ur3cWmwsfeIutpcu2/iFMSBAGaHGRNzJRCSp66mgy", "0901234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "namle" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$s4MzxAMfEiilgD.9IiK8wO70bsinuycKXpXl2D.DU2/jlZREVWqiy", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang" },
                    { new Guid("f370712b-0452-479c-a0d4-b4d2a7ace38f"), new DateTime(2003, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthid@gmail.com", true, "Pham", "Thi D", "$2a$11$tFD/Ob/5ZnrBh2Jqh4R22umC6Y47Zn7vwzSzxLGv.kjpCA/.XVTYm", "0905678901", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phamthid" },
                    { new Guid("f58236aa-5912-489a-8ffb-001d61e611c8"), new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyenvana@gmail.com", true, "Nguyen", "Van A", "$2a$11$cka3EZij1YEnFT9WpC890uwvmUGmiZToaciZXPlOTY115YMSFKaUe", "0902345678", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "nguyenvana" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$LU342DJ5CKi7HLFYQRmFPON4v216s1th6zEGTrNePmpjbfdmuwUoG", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quoctrieu" },
                    { new Guid("fefd096b-eb6f-4843-a9fb-fdde2f08e960"), new DateTime(2001, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "quynhphan@gmail.com", true, "Quỳnh", "Phan", "$2a$11$94JUvcD5F7bEYSAwnAhKMONxWXkiHHB5Xf.p9JmrDebUObHnzvgWa", "0908761234", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quynhphan" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "IsReviewed", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("123d7ea7-1a69-4db8-b006-4409df8e395a"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8443), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", true, true, true, "High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8443), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("382b8273-b6b8-4dc9-96d8-1c92b3a7780c"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8469), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A comprehensive textbook covering fundamental algebraic concepts and problem-solving techniques.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8469), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("3ffaed44-6b46-4179-9de3-cfaf12067512"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8463), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", true, true, true, "Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8463), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("402d882e-cdb4-46a9-856c-2c9c41b5ca2c"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8488), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", true, true, true, "Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8488), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("41ccd1ce-b6b5-4b60-8cfe-95f284e68074"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8435), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.", true, true, true, "Elegant Fountain Pen", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8435), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("4df59c01-5a6a-4c97-b8a2-f98f2e608e19"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8416), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "An introductory textbook designed to help students understand the basic principles of physics.", true, true, true, "High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8416), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("52767fa0-d6ca-4116-961a-c9bfbc57102d"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8445), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.", true, true, true, "Assorted Sticky Notes", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8445), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("6ed94cce-8110-4b46-8237-c372cdd0bf21"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8490), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8490), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("769cd96a-d451-4b76-84cb-fc7c50c6712c"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8411), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of durable glass beakers with clear measurement markings for laboratory use.", true, true, true, "Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8411), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("7a3061a2-3b1e-43cf-93bc-097d8b1523d5"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8465), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A pack of colorful sticky notes for quick reminders, notes, and organizing your tasks.", true, true, true, "History of Ancient Civilizations", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8465), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("7afdb14c-6349-414a-a893-8e4983459f5f"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8472), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", true, true, true, "Assorted Sticky Notes", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8472), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("7ec45539-79e4-4702-a74b-6f5d77c1bcfc"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8494), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Mechanical Pencil Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8493), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("88ce4ef8-54b6-4384-a199-27f7349f4031"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8403), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.", true, true, true, "Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8400), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("9ad52309-8602-47ba-87b7-6873fedf95e3"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8474), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8474), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("a7427fa4-862e-43ff-8213-180e4306f062"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8476), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of durable glass beakers with clear measurement markings for laboratory use.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8476), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("b02d36ec-5aa4-4c44-8e47-900a6ca578f3"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8414), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", true, true, true, "Biology: Principles and Explorations", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8413), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("b5b33943-d3ee-4262-9070-490f61a8c077"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8424), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A pack of colorful sticky notes for quick reminders, notes, and organizing your tasks.", true, true, true, "Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8424), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("b9897b12-1b11-43d9-8bdd-76fb6cf7318a"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8429), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of durable glass beakers with clear measurement markings for laboratory use.", true, true, true, "High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8428), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("c136f105-ce3e-4a7c-889d-f756ca62ea98"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8426), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", true, true, true, "Coloring Pencil Pack", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8426), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("c400856a-60ce-491a-830d-1b44abe1f24a"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8486), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A digital thermometer with high precision for measuring temperatures in scientific experiments.", true, true, true, "Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8485), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("e2af08b7-0b3c-4bd7-84bd-df502861c540"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8480), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", true, true, true, "Elegant Fountain Pen", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8479), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("eac851f8-3e1d-4137-96fc-48f9ee393c65"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8440), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", true, true, true, "Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8439), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("f3fe6a0a-b51b-4863-948a-d74973599283"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8432), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A digital thermometer with high precision for measuring temperatures in scientific experiments.", true, true, true, "Mechanical Pencil Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8432), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("f45f5eb1-6175-4a30-9c22-6674e80585e1"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8438), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", true, true, true, "Premium Ink Refills", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8437), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("fe929c27-5166-4259-8610-be6d282e8813"), new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8421), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An introductory textbook designed to help students understand the basic principles of physics.", true, true, true, "Digital Thermometer", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8420), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("1f890c58-742d-4866-8a67-9a3f115b7e8f"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8471), 17408L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("382b8273-b6b8-4dc9-96d8-1c92b3a7780c") },
                    { new Guid("24f78a9c-851e-46e0-aa73-68b7d388af84"), "Image for High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8444), 13312L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo-afGGvOl-uAIciO_inSUbq8c2WrvHEA8zA&s", new Guid("123d7ea7-1a69-4db8-b006-4409df8e395a") },
                    { new Guid("339971d6-1ca5-4921-9809-e7dcc8b2a0fd"), "Image for Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8475), 19456L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("9ad52309-8602-47ba-87b7-6873fedf95e3") },
                    { new Guid("34996122-9a02-499a-aa7c-9d25687bed50"), "Image for Assorted Sticky Notes", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8446), 14336L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNhUHteq_2myNyWXDG0-tdUmElcHC5ZufUbA&s", new Guid("52767fa0-d6ca-4116-961a-c9bfbc57102d") },
                    { new Guid("44be8ee1-47d3-4b62-976a-0d1a200ceb96"), "Image for Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8412), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("769cd96a-d451-4b76-84cb-fc7c50c6712c") },
                    { new Guid("6f88284d-c9fe-46e1-ab11-20a9a4f550b3"), "Image for High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8431), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("b9897b12-1b11-43d9-8bdd-76fb6cf7318a") },
                    { new Guid("75cd11b9-0cd1-47c2-a9d0-02e0dcd2c932"), "Image for Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8464), 15360L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEabvTjiSCtzyqA2S0JmFbTYH53qBm-WjioQ&s", new Guid("3ffaed44-6b46-4179-9de3-cfaf12067512") },
                    { new Guid("813a1df5-4b16-4836-aa50-dfab1f5efff5"), "Image for Biology: Principles and Explorations", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8415), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("b02d36ec-5aa4-4c44-8e47-900a6ca578f3") },
                    { new Guid("86af5310-27ab-4c5d-ab46-d9a43945ec13"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8485), 21504L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("e2af08b7-0b3c-4bd7-84bd-df502861c540") },
                    { new Guid("8c502d35-2a0f-44a2-8875-35dc2bbc2952"), "Image for Premium Ink Refills", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8439), 11264L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjY5xXwr880MJU4ZMkHoS4Kk9uBvJlVOocsyyr8c-SZIhpInnWpbdrTbxLSwIGRTqLtQE&usqp=CAU", new Guid("f45f5eb1-6175-4a30-9c22-6674e80585e1") },
                    { new Guid("94d648a9-d02c-4770-bfe7-74e190863949"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8492), 24576L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("6ed94cce-8110-4b46-8237-c372cdd0bf21") },
                    { new Guid("9c79166e-34bb-4cae-96d8-93e260c07d8e"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8468), 16384L, "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-simple-and-cute-school-supplies-stationery-suppliesstationerystaplerpenpencil-casecorrection-fluidrubber-png-image_674963.jpg", new Guid("7a3061a2-3b1e-43cf-93bc-097d8b1523d5") },
                    { new Guid("9c7b0f5a-b54c-4acf-85d4-646f8cda2e87"), "Image for Mechanical Pencil Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8434), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("f3fe6a0a-b51b-4863-948a-d74973599283") },
                    { new Guid("abd1588e-a362-4a7f-9708-fd0502fa1e91"), "Image for Mechanical Pencil Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8494), 25600L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("7ec45539-79e4-4702-a74b-6f5d77c1bcfc") },
                    { new Guid("abfcace1-cdd5-4818-9ae9-0c5fbdcab013"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8442), 12288L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTzAg7kp7XyYhhU7MlQDH_Or2jTxIPZuyl3j_hA01ywVLBmo5qSkIeTbDE4C7DaKdDlyI&usqp=CAU", new Guid("eac851f8-3e1d-4137-96fc-48f9ee393c65") },
                    { new Guid("ac66efc5-801b-4feb-8f53-024ca254bf7c"), "Image for Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8487), 22528L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("c400856a-60ce-491a-830d-1b44abe1f24a") },
                    { new Guid("b9e2f682-778c-4e0f-9612-160c6123322d"), "Image for Glass Beaker Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8425), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("b5b33943-d3ee-4262-9070-490f61a8c077") },
                    { new Guid("d1af3652-1549-4d2b-900f-71ceae098c8a"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8489), 23552L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("402d882e-cdb4-46a9-856c-2c9c41b5ca2c") },
                    { new Guid("d3e1a158-6c4c-4d5c-a1ca-9a39523fc136"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8436), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("41ccd1ce-b6b5-4b60-8cfe-95f284e68074") },
                    { new Guid("d688d069-ffde-4f8e-8e11-01d4f0023bcd"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8478), 20480L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("a7427fa4-862e-43ff-8213-180e4306f062") },
                    { new Guid("e0da6f80-a2c4-43f0-83b6-f2b4a8e2bd61"), "Image for Assorted Sticky Notes", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8473), 18432L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("7afdb14c-6349-414a-a893-8e4983459f5f") },
                    { new Guid("e5e831cb-7a86-4c8e-a249-fdde5d76d3e0"), "Image for High-Quality Ruler Set", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8419), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("4df59c01-5a6a-4c97-b8a2-f98f2e608e19") },
                    { new Guid("f235b67d-ac97-49e6-85e2-edfe7710cfb0"), "Image for Spiral Notebook", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8409), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("88ce4ef8-54b6-4384-a199-27f7349f4031") },
                    { new Guid("f76bae87-8ea6-4f6d-b1ae-0b54e83b0d5e"), "Image for Digital Thermometer", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8422), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("fe929c27-5166-4259-8610-be6d282e8813") },
                    { new Guid("f796c674-83bb-465f-9042-e3cebcb50848"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 27, 12, 5, 38, 688, DateTimeKind.Utc).AddTicks(8427), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("c136f105-ce3e-4a7c-889d-f756ca62ea98") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_CurrentProductId",
                table: "ExchangeRequests",
                column: "CurrentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_ReceiverId",
                table: "ExchangeRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_SenderId",
                table: "ExchangeRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_TargetProductId",
                table: "ExchangeRequests",
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
                name: "IX_Transactions_ExchangeRequestId",
                table: "Transactions",
                column: "ExchangeRequestId",
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
                name: "ExchangeRequests");

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
