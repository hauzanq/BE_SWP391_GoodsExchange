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
                    ExchangeRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    { new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"), new DateTime(2000, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "thaonguyen@gmail.com", true, "Thảo", "Nguyễn", "$2a$11$MbVX81Iykm4oEG1VRimFiOpJAOF1b./sL1Dds7SFZLxWJo60J.ieG", "0912345678", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "thaonguyen" },
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$RAn3nJBQCn9JSDrBkXDh7uVDKP0NI3itK2NKzbgyDjUqFcDdYZiAC", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "admin" },
                    { new Guid("0da4bfc5-3a37-4a66-91d5-fe9e1d086564"), new DateTime(2004, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khangvo@gmail.com", true, "Khang", "Võ", "$2a$11$epAdpZmFu5t00z/rvcvrw.CdCQSXBidRyH87XaOVyjIdKdkCbAsRW", "0923456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "khangvo" },
                    { new Guid("15b2d60f-bdfc-4ce1-8b03-f8b8c3d7480d"), new DateTime(1998, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhpham@gmail.com", true, "Anh", "Phạm", "$2a$11$LJ/kxUzt5FJY99ug5jg07.hKUeGTVf1OXue0eI3oehsedyUscNsf6", "0908765432", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "anhpham" },
                    { new Guid("273c8fa3-b3f4-4c6d-87da-07bc63a4c436"), new DateTime(1995, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhtran@gmail.com", true, "Minh", "Trần", "$2a$11$o5QMsj8mo9U9lbIWE/BHl.P92GAoalCO57nN7eSz9aeNFw5GWvQCe", "0987654321", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhtran" },
                    { new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhphuoc@gmail.com", true, "Minh", "Phuoc", "$2a$11$d1ATLh1Rj4aKMwesF4BmNO5q8UlVdG3NEGHfJWACmCcrIAxO.JVzS", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhphuoc" },
                    { new Guid("7c87c075-2c90-4c6e-8e8b-b903ec331072"), new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tranvanc@gmail.com", true, "Tran", "Van C", "$2a$11$4wly8it.trLNA.YMdPkSKu0mQPmtm5BuPt9/RuN6JxPMfzU/IBI0O", "0904567890", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tranvanc" },
                    { new Guid("803b1549-478c-421b-bd4b-2b6111836609"), new DateTime(2001, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lethib@gmail.com", true, "Le", "Thi B", "$2a$11$is6yWYZzjq6RyMLHogYMaOt3XdudVEftTuqhN7RbII.U40gRelv42", "0903456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lethib" },
                    { new Guid("8130b91b-2e32-4aad-8479-3e01dd8813e3"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang01@gmail.com", true, "Hậu", "Giang", "$2a$11$.Y/t2/z/JuJWOoNzffeAK.QOBoZR5rBpyj/qeEcPs.GLiqQvYudKW", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang45" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$2Ba/rq0bJ7M1nfwmulN4cuG3W./GOuHtYUHO9ifDjYiP3OviqVo0i", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phuongthao" },
                    { new Guid("97108917-856a-42af-937b-7b0e2e735b20"), new DateTime(1997, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "huongtrinh@gmail.com", true, "Hương", "Trịnh", "$2a$11$F0Vh6hfWh623h0M1iYNaYe6YyeGImKuJugGEBGTIXgnfG6YVti4em", "0937894561", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "huongtrinh" },
                    { new Guid("a6f939d3-eb8a-4884-8084-b1dc99559167"), new DateTime(2004, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dovane@gmail.com", true, "Do", "Van E", "$2a$11$2Wwdt0N4lwHm68CZVyasL.qZw2HoLyZ6BlgJe.DYI8tjs4zk/R8da", "0906789012", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "dovane" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$NH1tsWB1K7RADYIPHc0ynu4JdC8qVJgy/7NGc1EXdMiSfNABLbKae", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "moderator" },
                    { new Guid("bf0b0b85-e98e-4da7-af44-b7198685cb78"), new DateTime(2002, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "tudo@gmail.com", true, "Tú", "Đỗ", "$2a$11$VZCueGFyOmUJ0C04ArYSXuAZf4T6Gc5ocQCr3ga7G0GwuEUvSCU6m", "0981234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tudo" },
                    { new Guid("ce914c08-8b01-483a-8b16-1c0e27284cc8"), new DateTime(1999, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhoang@gmail.com", true, "Lan", "Hoàng", "$2a$11$taK9jUsm2fqLDujgC3fzveA7DDC7vcBxsShdgyrJqQMUjDSjsK4sS", "0934567890", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lanhoang" },
                    { new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"), new DateTime(1996, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "namle@gmail.com", true, "Nam", "Lê", "$2a$11$P2ohUFYGyTbMZjJ1tACK.u3nky3x1oCEtXeCxlfcqHu/f//fcsoN2", "0901234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "namle" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$udvbtxHnaK1..CafMpwgGu.FZRj4fiNsfMTF2x7gPXIbbZhWLCgh6", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang" },
                    { new Guid("f370712b-0452-479c-a0d4-b4d2a7ace38f"), new DateTime(2003, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthid@gmail.com", true, "Pham", "Thi D", "$2a$11$3sDF5hHucwflEagrhfZxZu3EvuqstAfGL7ZWPqdjJwVdEnYRelBY2", "0905678901", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phamthid" },
                    { new Guid("f58236aa-5912-489a-8ffb-001d61e611c8"), new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyenvana@gmail.com", true, "Nguyen", "Van A", "$2a$11$3ticIzPn.Q.ds79gmvcN2eSow6qti1k.4QLsHLQ5MxP/HVUNflAJi", "0902345678", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "nguyenvana" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$ByYQOxH0dizfB83234dpbeq2eARUHHdotAaliYJWpaHbTyMtw6PAy", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quoctrieu" },
                    { new Guid("fefd096b-eb6f-4843-a9fb-fdde2f08e960"), new DateTime(2001, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "quynhphan@gmail.com", true, "Quỳnh", "Phan", "$2a$11$gefGLkrM2A68WA00ZmO/x.EeEFXKXkgD3h27ig8Sa9XyglnLdjAaC", "0908761234", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quynhphan" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "IsReviewed", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("00479fec-e30e-453a-836f-ec877d23bf4e"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2269), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A detailed textbook offering a thorough overview of biological principles and scientific explorations.", true, true, true, "Glass Beaker Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2267), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("064b9695-9999-49e4-a8d0-cc876a1cc335"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2363), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", true, true, true, "Premium Ink Refills", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2363), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("0edeabad-da88-41d7-a389-a8a4d7d4077b"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2290), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", true, true, true, "Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2290), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("163a78bd-ea25-4630-af77-10145b7a894d"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2384), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", true, true, true, "History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2384), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("1f92d067-3bb9-4e08-89ad-eef74892f7c7"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2382), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", true, true, true, "Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2382), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("335269eb-4cf0-458e-8878-b02aae00baf1"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2344), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", true, true, true, "Glass Beaker Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2344), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("3de7dc87-8c58-411d-afb9-36719c7f0b1e"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2359), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Biology: Principles and Explorations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2359), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("5e1282df-8a00-4b9c-8e3b-7b6bc0417acf"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2337), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2336), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("75cead64-21e6-4dd2-bf47-da7ed35059e5"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2295), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of durable glass beakers with clear measurement markings for laboratory use.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2294), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("857b5383-d80e-4a33-af84-4ee7cc0b1fc8"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2370), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A detailed textbook offering a thorough overview of biological principles and scientific explorations.", true, true, true, "Introduction to Algebra", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2370), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("894f5213-c713-4191-910c-49d4a00a318b"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2376), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An introductory textbook designed to help students understand the basic principles of physics.", true, true, true, "Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2376), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("9a941f95-f24b-4096-a372-6edc0c3b8897"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2339), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", true, true, true, "History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2339), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("9ba40ccc-5cd2-4c1e-8191-20ed822a1401"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2333), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Mechanical Pencil Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2332), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("a907875a-7554-49f1-b590-b783da33863b"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2276), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", true, true, true, "Elegant Fountain Pen", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2275), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("aa38a577-73da-484b-b626-edca5f36b2cc"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2327), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A digital thermometer with high precision for measuring temperatures in scientific experiments.", true, true, true, "Elegant Fountain Pen", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2327), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("b65c4876-1aaf-4acb-b614-5b609c6c95aa"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2366), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A precision measuring cylinder designed for accurate measurement of liquids in laboratory settings.", true, true, true, "History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2366), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("bd51251d-6e29-4b1f-b28f-94b592291199"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2330), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", true, true, true, "Introduction to Algebra", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2330), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("be08180c-8524-45c9-9fd5-181b8b48e748"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2292), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", true, true, true, "Assorted Sticky Notes", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2292), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("bf64b289-fb7a-4e95-80be-2ccd40b26ea9"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2372), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "An introductory textbook designed to help students understand the basic principles of physics.", true, true, true, "Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2372), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("c0a2f7e8-9951-4209-b78b-4bd953ae6011"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2298), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", true, true, true, "Assorted Sticky Notes", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2297), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("c9b7cb12-f279-4cb6-8a7d-a78909f43cc3"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2301), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2301), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("d615c5be-e4d2-4270-81ec-2f6625052a39"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2380), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable glass beakers with clear measurement markings for laboratory use.", true, true, true, "History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2380), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("dd4854bf-8cca-44b7-965c-49628dc77802"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2374), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A digital thermometer with high precision for measuring temperatures in scientific experiments.", true, true, true, "Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2374), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("e56f14bf-40fa-408e-a7d5-3e1f100009c3"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2342), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", true, true, true, "Biology: Principles and Explorations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2341), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("f67a9513-5423-4a6d-b9b6-bcc6643922d9"), new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2361), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", true, true, true, "Digital Thermometer", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2361), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0c3be178-3c69-46ab-901e-2ae4a62f6674"), "Image for Biology: Principles and Explorations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2343), 13312L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo-afGGvOl-uAIciO_inSUbq8c2WrvHEA8zA&s", new Guid("e56f14bf-40fa-408e-a7d5-3e1f100009c3") },
                    { new Guid("281eb479-cdb2-4ee7-a25c-48a56b29d629"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2291), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("0edeabad-da88-41d7-a389-a8a4d7d4077b") },
                    { new Guid("4ac90e15-1402-4fdd-b5f0-6eb144914ce3"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2296), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("75cead64-21e6-4dd2-bf47-da7ed35059e5") },
                    { new Guid("4bf18a1a-50c9-47d2-9905-5491b6c58f76"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2373), 20480L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("bf64b289-fb7a-4e95-80be-2ccd40b26ea9") },
                    { new Guid("4c7ed437-b15b-4578-a2d3-47e43cddaa03"), "Image for Assorted Sticky Notes", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2293), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("be08180c-8524-45c9-9fd5-181b8b48e748") },
                    { new Guid("4ddbe1ab-6396-4cf9-9e08-ea01d476908b"), "Image for Microscope with LED Illumination", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2337), 11264L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjY5xXwr880MJU4ZMkHoS4Kk9uBvJlVOocsyyr8c-SZIhpInnWpbdrTbxLSwIGRTqLtQE&usqp=CAU", new Guid("5e1282df-8a00-4b9c-8e3b-7b6bc0417acf") },
                    { new Guid("4f6594bf-1120-499e-9643-870084473281"), "Image for Biology: Principles and Explorations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2360), 15360L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEabvTjiSCtzyqA2S0JmFbTYH53qBm-WjioQ&s", new Guid("3de7dc87-8c58-411d-afb9-36719c7f0b1e") },
                    { new Guid("528e692a-aeb2-45dc-9718-4dfc58db45dd"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2367), 18432L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("b65c4876-1aaf-4acb-b614-5b609c6c95aa") },
                    { new Guid("61508c5c-e2f6-4528-8398-d3626fbb8aeb"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2381), 23552L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("d615c5be-e4d2-4270-81ec-2f6625052a39") },
                    { new Guid("61517681-1fe8-4e63-80a8-87bc77fc2d64"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2383), 24576L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("1f92d067-3bb9-4e08-89ad-eef74892f7c7") },
                    { new Guid("6639913f-f309-44db-80dc-28b67efdb05b"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2375), 21504L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("dd4854bf-8cca-44b7-965c-49628dc77802") },
                    { new Guid("75989c98-7816-4e6a-9fe6-4c0aca2bf2d6"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2340), 12288L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTzAg7kp7XyYhhU7MlQDH_Or2jTxIPZuyl3j_hA01ywVLBmo5qSkIeTbDE4C7DaKdDlyI&usqp=CAU", new Guid("9a941f95-f24b-4096-a372-6edc0c3b8897") },
                    { new Guid("8e58d199-507e-4a7d-8589-f632bcd0eb70"), "Image for Premium Ink Refills", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2364), 17408L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("064b9695-9999-49e4-a8d0-cc876a1cc335") },
                    { new Guid("91481638-3df4-4aca-8cbf-1efc9be51116"), "Image for Introduction to Algebra", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2371), 19456L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("857b5383-d80e-4a33-af84-4ee7cc0b1fc8") },
                    { new Guid("920f6f19-fa58-475e-8f8b-2a679490d1e3"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2377), 22528L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("894f5213-c713-4191-910c-49d4a00a318b") },
                    { new Guid("a6d05f33-7a03-4a39-bd78-ea0e162110cf"), "Image for Glass Beaker Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2345), 14336L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNhUHteq_2myNyWXDG0-tdUmElcHC5ZufUbA&s", new Guid("335269eb-4cf0-458e-8878-b02aae00baf1") },
                    { new Guid("a7a165ff-93e2-4309-9d12-36391c317a5b"), "Image for Assorted Sticky Notes", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2298), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("c0a2f7e8-9951-4209-b78b-4bd953ae6011") },
                    { new Guid("ad601b38-3e03-482d-a359-a4cb26f45b6f"), "Image for Digital Thermometer", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2362), 16384L, "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-simple-and-cute-school-supplies-stationery-suppliesstationerystaplerpenpencil-casecorrection-fluidrubber-png-image_674963.jpg", new Guid("f67a9513-5423-4a6d-b9b6-bcc6643922d9") },
                    { new Guid("afdffca0-a42f-4276-92dc-d1199a16f939"), "Image for Coloring Pencil Pack", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2326), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("c9b7cb12-f279-4cb6-8a7d-a78909f43cc3") },
                    { new Guid("b2318a19-4d97-4c13-8c8a-c95258812ece"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2328), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("aa38a577-73da-484b-b626-edca5f36b2cc") },
                    { new Guid("b51cb85b-789b-45a6-920e-d9b0bb8fd9b3"), "Image for History of Ancient Civilizations", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2385), 25600L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("163a78bd-ea25-4630-af77-10145b7a894d") },
                    { new Guid("ba7b7ae7-7652-444f-a1d5-338a85f6e37c"), "Image for Glass Beaker Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2274), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("00479fec-e30e-453a-836f-ec877d23bf4e") },
                    { new Guid("e431a2ec-8866-4606-bcc3-1eea341bf52d"), "Image for Introduction to Algebra", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2331), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("bd51251d-6e29-4b1f-b28f-94b592291199") },
                    { new Guid("e57faf24-dc62-409a-9200-1ad4c2970a0c"), "Image for Mechanical Pencil Set", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2334), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("9ba40ccc-5cd2-4c1e-8191-20ed822a1401") },
                    { new Guid("fb52b7a7-7128-4ea8-a08e-ae2b87490393"), "Image for Elegant Fountain Pen", new DateTime(2024, 7, 27, 5, 46, 48, 353, DateTimeKind.Utc).AddTicks(2277), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("a907875a-7554-49f1-b590-b783da33863b") }
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
