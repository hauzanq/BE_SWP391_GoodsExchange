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
                    Status = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUploadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    { new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"), new DateTime(2000, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "thaonguyen@gmail.com", true, "Thảo", "Nguyễn", "$2a$11$DURfilXfa6lkUl118rhD4.P0U0UBg19XD.oS3HuLdDRX50Q1ci9Ta", "0912345678", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "thaonguyen" },
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$XExaEJxD4misG.2WIL7VtuwOacqjJsio2SX4oK98FMXG9dvm3u7ia", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "admin" },
                    { new Guid("0da4bfc5-3a37-4a66-91d5-fe9e1d086564"), new DateTime(2004, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khangvo@gmail.com", true, "Khang", "Võ", "$2a$11$GJmf1FIoVBld1pm0yYZN4.FSrmC9Fk46kjY4xklZdWQDrUySDHHIC", "0923456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "khangvo" },
                    { new Guid("15b2d60f-bdfc-4ce1-8b03-f8b8c3d7480d"), new DateTime(1998, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhpham@gmail.com", true, "Anh", "Phạm", "$2a$11$3Ingj5v6rzRhmOvwmwFZouGlFqykSWhfXmtAEkaie79e8L0kgPQBC", "0908765432", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "anhpham" },
                    { new Guid("273c8fa3-b3f4-4c6d-87da-07bc63a4c436"), new DateTime(1995, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhtran@gmail.com", true, "Minh", "Trần", "$2a$11$ufZSAr1x8TFGYv1aKzG76eiwBFtaaPkMbsljeDlA8V0kOtWCW3Ehm", "0987654321", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhtran" },
                    { new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhphuoc@gmail.com", true, "Minh", "Phuoc", "$2a$11$I0/VjOAlE6as5gm5GiK5wenwR9Qt/x/uj2ggqQfr58T8vZlztRBGG", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhphuoc" },
                    { new Guid("7c87c075-2c90-4c6e-8e8b-b903ec331072"), new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tranvanc@gmail.com", true, "Tran", "Van C", "$2a$11$ns6ZH1TGeUSTpw8vDjes1uoSnpMIaxrh1ZU5E0FCF/fpSkxHapiVy", "0904567890", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tranvanc" },
                    { new Guid("803b1549-478c-421b-bd4b-2b6111836609"), new DateTime(2001, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lethib@gmail.com", true, "Le", "Thi B", "$2a$11$WKK0Bh82x6ytg5KNYfk.Tei/PyCtSCgxQUls1jC6Q4zrWoluKnqmS", "0903456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lethib" },
                    { new Guid("8130b91b-2e32-4aad-8479-3e01dd8813e3"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang01@gmail.com", true, "Hậu", "Giang", "$2a$11$7enLGkIVuejT8t6LgBFCceMATUm9AzsrEwB1BU8sX/B8YiExeLCbW", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang45" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$PGu3ZcH/NrlTILNH56zd4O0G8RaODo1tXLHUocUoaA5PYcexBJHfa", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phuongthao" },
                    { new Guid("97108917-856a-42af-937b-7b0e2e735b20"), new DateTime(1997, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "huongtrinh@gmail.com", true, "Hương", "Trịnh", "$2a$11$J0Jh2mINub2LxdTKsRsV9.QG/HcP37BWBfYnMavYP7pk/wQnPSeRe", "0937894561", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "huongtrinh" },
                    { new Guid("a6f939d3-eb8a-4884-8084-b1dc99559167"), new DateTime(2004, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dovane@gmail.com", true, "Do", "Van E", "$2a$11$VCtVaDPI6X6eqcQTupplX.b7vzOJkSkqZO46udaNyHvLolQHnTYxO", "0906789012", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "dovane" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$Upaq5cTmycAJKkdk.2CFuu46Dt.COtBn0uqQuv0e10alqIXRfipp6", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "moderator" },
                    { new Guid("bf0b0b85-e98e-4da7-af44-b7198685cb78"), new DateTime(2002, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "tudo@gmail.com", true, "Tú", "Đỗ", "$2a$11$xQIBKi9fOMACCfFzvhkIhu8kq8iNx8ipPMbW3OofXlO1ulAI4cLLS", "0981234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tudo" },
                    { new Guid("ce914c08-8b01-483a-8b16-1c0e27284cc8"), new DateTime(1999, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhoang@gmail.com", true, "Lan", "Hoàng", "$2a$11$m5LGI2jnmIeyu6L9vQaI1e/YL/i73hYcWWd.8bNf77zkmjB.Zzq2i", "0934567890", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lanhoang" },
                    { new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"), new DateTime(1996, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "namle@gmail.com", true, "Nam", "Lê", "$2a$11$zD.wCseyt6QmnN4uRGiyiOPaOZ0JQZaqPCslpK.G6Ag/fbhpKZN0O", "0901234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "namle" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$dFB6HNKrVgIitxwQxegvaeTeI4Ixlf827D453dfvwBnE3ICdWWlDW", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang" },
                    { new Guid("f370712b-0452-479c-a0d4-b4d2a7ace38f"), new DateTime(2003, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthid@gmail.com", true, "Pham", "Thi D", "$2a$11$2KbbxJeP1hLFrOAGm1DrfOgziV00N3ta0RiajqS4uG1zTA8xG0YCe", "0905678901", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phamthid" },
                    { new Guid("f58236aa-5912-489a-8ffb-001d61e611c8"), new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyenvana@gmail.com", true, "Nguyen", "Van A", "$2a$11$ekx3HoAu93fluPRwcdmdCuu38hnDOi9Vgo36nuLtYsVysIoNhyXa.", "0902345678", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "nguyenvana" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$VrgO4TVhne0JKdG73mLzce6Ute.1iDl3Q0NKV5YXHhgXv9/IEFBlq", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quoctrieu" },
                    { new Guid("fefd096b-eb6f-4843-a9fb-fdde2f08e960"), new DateTime(2001, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "quynhphan@gmail.com", true, "Quỳnh", "Phan", "$2a$11$C7ha6RvWDGzd//6Y02/gCeUJVcFoEwFHSQTidAK4qceN9ZNsx60pW", "0908761234", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quynhphan" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ProductName", "Status", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("01aad690-b271-486c-aae2-42993aeb18b9"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Coloring Pencil Pack", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6755), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("0e18960a-1a99-4d16-8c78-830223a57946"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.", "Mechanical Pencil Set", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6680), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("1dd24a05-a6d5-471f-9cdf-cd83203a87e8"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", "Premium Ink Refills", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6717), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("285549ee-a02c-468f-87f8-941ddeed3386"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", "Biology: Principles and Explorations", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6709), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("32cfa938-c0ed-4c83-9425-d621c36f2d5c"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Microscope with LED Illumination", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6737), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("340dc0a5-3a7d-4cdc-bfad-320401dd65ed"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", "Laboratory Measuring Cylinder", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6691), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("35bb966d-c405-44f8-9831-c0b4c7e20c3e"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A pack of colorful sticky notes for quick reminders, notes, and organizing your tasks.", "Premium Ink Refills", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6758), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("45bcfb67-2869-411d-be9a-f649afa44482"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", "Introduction to Algebra", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6747), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("63827fb5-e13f-4386-a254-c540f8261337"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", "Digital Thermometer", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6698), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("7527f9de-ed04-427b-b0a7-ca0c1ab87f27"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", "Physics for Beginners", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6686), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("76f13dae-939e-44c5-91f6-fce97dd22f9e"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "Microscope with LED Illumination", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6701), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("7e6a2bdc-d4f8-4703-8d49-aa52b8244269"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", "Mechanical Pencil Set", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6770), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("7e8331d5-2ad5-45bf-96c0-fa8b3524560e"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "Assorted Sticky Notes", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6683), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("83025556-ac61-447f-8e4a-f30e80a288b0"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Premium Ink Refills", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6695), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("84a19fb2-7912-41f4-b646-fb23b717d7b5"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A precision measuring cylinder designed for accurate measurement of liquids in laboratory settings.", "Coloring Pencil Pack", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6711), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("b1c34b49-2040-4fbc-b4ee-a6030b8e00f3"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", "Spiral Notebook", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6666), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("b2b06fa5-9926-4e89-a6d3-1f7732a9ecb1"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "History of Ancient Civilizations", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6739), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("c3c59e74-e2b8-4758-a373-42277552d373"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A comprehensive textbook covering fundamental algebraic concepts and problem-solving techniques.", "Premium Ink Refills", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6743), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("c5d6a1fd-2414-42b6-9b70-5e1cd3f03e39"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", "Spiral Notebook", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6749), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("ddd095c3-e3c6-43ca-bdac-48ad0c615576"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable glass beakers with clear measurement markings for laboratory use.", "Student Backpack", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6763), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("e666c628-e973-4fcc-a297-f978832ed133"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", "Microscope with LED Illumination", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6751), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("ed017538-cd55-419d-8661-c6996767386f"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.", "Elegant Fountain Pen", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6713), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("f8837775-a83c-4dc5-9dee-5eabdced85f5"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", "Elegant Fountain Pen", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6719), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("fd55975f-e3e1-41e9-9629-9a4f2086d065"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of durable glass beakers with clear measurement markings for laboratory use.", "Physics for Beginners", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6765), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("ff25d9a1-6694-4215-9ae7-6ae6695650ae"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", "Physics for Beginners", 0, new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6705), new Guid("fda6e282-e429-4364-a445-136b570e2fde") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0265c12f-1b62-4b58-8d42-aa88beeefa81"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6754), 20480L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("e666c628-e973-4fcc-a297-f978832ed133") },
                    { new Guid("04d029b6-f569-4b01-845a-b90c42894f9a"), "Image for Spiral Notebook", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6673), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("b1c34b49-2040-4fbc-b4ee-a6030b8e00f3") },
                    { new Guid("0f4ba951-97c2-4824-bb4b-c6c04f3b139c"), "Image for Digital Thermometer", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6700), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("63827fb5-e13f-4386-a254-c540f8261337") },
                    { new Guid("166823dc-ae77-43b0-84ad-d24de9429abc"), "Image for Physics for Beginners", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6769), 24576L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("fd55975f-e3e1-41e9-9629-9a4f2086d065") },
                    { new Guid("25dbd596-4a3f-4041-bf48-85ef27994749"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6704), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("76f13dae-939e-44c5-91f6-fce97dd22f9e") },
                    { new Guid("2fd407a7-2d7d-4fa4-a4c8-a29393fe0a8b"), "Image for Spiral Notebook", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6750), 19456L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("c5d6a1fd-2414-42b6-9b70-5e1cd3f03e39") },
                    { new Guid("343b0ad9-70ac-49c7-a1c6-1dbf325e6456"), "Image for Mechanical Pencil Set", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6771), 25600L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("7e6a2bdc-d4f8-4703-8d49-aa52b8244269") },
                    { new Guid("359662e5-cdbd-409b-acd1-66ac3d188284"), "Image for Physics for Beginners", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6707), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("ff25d9a1-6694-4215-9ae7-6ae6695650ae") },
                    { new Guid("471ceeab-6fd0-44a3-b323-e3a3522faa6d"), "Image for Premium Ink Refills", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6758), 22528L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("35bb966d-c405-44f8-9831-c0b4c7e20c3e") },
                    { new Guid("65155024-393b-4d81-a604-b5b4c79d3a23"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6694), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("340dc0a5-3a7d-4cdc-bfad-320401dd65ed") },
                    { new Guid("6eb5e0b0-9003-4dcb-a6ba-1b591648a6e1"), "Image for Premium Ink Refills", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6745), 17408L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("c3c59e74-e2b8-4758-a373-42277552d373") },
                    { new Guid("6fc8fc89-ba98-43a9-b962-7f2c1e16865e"), "Image for Student Backpack", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6764), 23552L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("ddd095c3-e3c6-43ca-bdac-48ad0c615576") },
                    { new Guid("72bad161-c5f0-4d20-94ee-2abf37fd6056"), "Image for Physics for Beginners", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6690), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("7527f9de-ed04-427b-b0a7-ca0c1ab87f27") },
                    { new Guid("73db5777-91a6-4404-b8e4-9c22cd591c00"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6742), 16384L, "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-simple-and-cute-school-supplies-stationery-suppliesstationerystaplerpenpencil-casecorrection-fluidrubber-png-image_674963.jpg", new Guid("b2b06fa5-9926-4e89-a6d3-1f7732a9ecb1") },
                    { new Guid("76195e36-7bf5-4427-933d-269f2e1b2dae"), "Image for Introduction to Algebra", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6748), 18432L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("45bcfb67-2869-411d-be9a-f649afa44482") },
                    { new Guid("90aab4c2-b489-4e4d-9bb5-bb58571a8923"), "Image for Assorted Sticky Notes", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6685), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("7e8331d5-2ad5-45bf-96c0-fa8b3524560e") },
                    { new Guid("9771e0a8-7cdc-416d-b436-28cfbb85631b"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6712), 11264L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjY5xXwr880MJU4ZMkHoS4Kk9uBvJlVOocsyyr8c-SZIhpInnWpbdrTbxLSwIGRTqLtQE&usqp=CAU", new Guid("84a19fb2-7912-41f4-b646-fb23b717d7b5") },
                    { new Guid("9d7da31b-9313-470e-90de-7f4e080d095d"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6716), 12288L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTzAg7kp7XyYhhU7MlQDH_Or2jTxIPZuyl3j_hA01ywVLBmo5qSkIeTbDE4C7DaKdDlyI&usqp=CAU", new Guid("ed017538-cd55-419d-8661-c6996767386f") },
                    { new Guid("da287161-4685-4b75-ad8a-5319a0804857"), "Image for Mechanical Pencil Set", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6682), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("0e18960a-1a99-4d16-8c78-830223a57946") },
                    { new Guid("e2dbfd91-7fb5-4a70-ad0e-7e5a725ea0a9"), "Image for Premium Ink Refills", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6697), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("83025556-ac61-447f-8e4a-f30e80a288b0") },
                    { new Guid("e8c14d25-a3e9-43ba-99d1-889cbddcbd07"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6757), 21504L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("01aad690-b271-486c-aae2-42993aeb18b9") },
                    { new Guid("e9f30e3e-b7ff-4d44-bc08-afc11d7457b4"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6738), 15360L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEabvTjiSCtzyqA2S0JmFbTYH53qBm-WjioQ&s", new Guid("32cfa938-c0ed-4c83-9425-d621c36f2d5c") },
                    { new Guid("f399f94c-d94e-4851-bd48-23ece9d58bd9"), "Image for Biology: Principles and Explorations", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6710), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("285549ee-a02c-468f-87f8-941ddeed3386") },
                    { new Guid("fa173d77-fa12-4408-85e7-56f271d3ff46"), "Image for Premium Ink Refills", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6718), 13312L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo-afGGvOl-uAIciO_inSUbq8c2WrvHEA8zA&s", new Guid("1dd24a05-a6d5-471f-9cdf-cd83203a87e8") },
                    { new Guid("fd6bc68a-d66b-4594-8b04-81a29f95862b"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 31, 18, 31, 18, 69, DateTimeKind.Utc).AddTicks(6720), 14336L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNhUHteq_2myNyWXDG0-tdUmElcHC5ZufUbA&s", new Guid("f8837775-a83c-4dc5-9dee-5eabdced85f5") }
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
