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
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Status = table.Column<int>(type: "int", nullable: false)
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
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
                    { new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"), new DateTime(2000, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "thaonguyen@gmail.com", true, "Thảo", "Nguyễn", "$2a$11$l61RnPLgmQbQYOvA9.hV6O6KNqNS4ylq8cO1dWVKmZ8nMYT6/vDTu", "0912345678", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "thaonguyen" },
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(2003, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhoa@gmail.com", true, "Minh", "Khoa", "$2a$11$56uLC.cQNb2IGVUfdicrt.iwe0Ywg6q7E//CQWDFZQfwsrUOYYzKm", "0123456789", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "admin" },
                    { new Guid("0da4bfc5-3a37-4a66-91d5-fe9e1d086564"), new DateTime(2004, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khangvo@gmail.com", true, "Khang", "Võ", "$2a$11$jFXTloKHsOCBl9.shfOZ6.Iv9d81FvIuWqk3cNQT0qvq2uyUvUiAm", "0923456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "khangvo" },
                    { new Guid("15b2d60f-bdfc-4ce1-8b03-f8b8c3d7480d"), new DateTime(1998, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhpham@gmail.com", true, "Anh", "Phạm", "$2a$11$X/NiU3x0pPSFQ8kOfhFuYOJ3335zFl7DSO5YUEhZDsZ5S6ARVmf3q", "0908765432", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "anhpham" },
                    { new Guid("273c8fa3-b3f4-4c6d-87da-07bc63a4c436"), new DateTime(1995, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhtran@gmail.com", true, "Minh", "Trần", "$2a$11$G8KaqgbxV5cTtMe6GRWNW.Fp60rECE0pPDMBY0EaMRtyz/Ds9ZqXy", "0987654321", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhtran" },
                    { new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhphuoc@gmail.com", true, "Minh", "Phuoc", "$2a$11$IdeeF7oARJyKC2NFLDzkWOty0kvgeLDUJxW9Hr38jwXO2pqV0UY/W", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "minhphuoc" },
                    { new Guid("7c87c075-2c90-4c6e-8e8b-b903ec331072"), new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tranvanc@gmail.com", true, "Tran", "Van C", "$2a$11$9NCm9tgKpfOAHO.Lte/IEOXcu8LlnMjuzLnqCgGzP95jKalzhAyqq", "0904567890", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tranvanc" },
                    { new Guid("803b1549-478c-421b-bd4b-2b6111836609"), new DateTime(2001, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lethib@gmail.com", true, "Le", "Thi B", "$2a$11$EH7kPU2FMPc/cOAcadAl/e5rbHq5LdgnBZY4LXpz6fxb8hxbH5tWS", "0903456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lethib" },
                    { new Guid("8130b91b-2e32-4aad-8479-3e01dd8813e3"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang01@gmail.com", true, "Hậu", "Giang", "$2a$11$Xd006IQLTQKERKIf2C9EoelUYecx1fg57TRmAjdSS4fihvzH35DNa", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang45" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(2003, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuongthao@gmail.com", true, "Phuong", "Thao", "$2a$11$2FFHny/MEK/U.6cqbaKoPOIpyi0zFi/KU3un65cYyXkDcJ.VYXcgy", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phuongthao" },
                    { new Guid("97108917-856a-42af-937b-7b0e2e735b20"), new DateTime(1997, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "huongtrinh@gmail.com", true, "Hương", "Trịnh", "$2a$11$ogoMuWMXypDiP.8uk4YupeWzumTCLt1uweIEGbNOfH4GdQ06xVRFe", "0937894561", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "huongtrinh" },
                    { new Guid("a6f939d3-eb8a-4884-8084-b1dc99559167"), new DateTime(2004, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dovane@gmail.com", true, "Do", "Van E", "$2a$11$/K73b4mGd0cyYo/1jRtVtuI879KM6kKf5dcJLY/PXwKeNoZczBRca", "0906789012", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "dovane" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthanh@gmail.com", true, "Pham", "Thanh", "$2a$11$miEaizgcJOu2DP.PzI2VNe1tbWdIRMINOM6uV/hpxC5tndx18Mc9m", "0123456789", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "moderator" },
                    { new Guid("bf0b0b85-e98e-4da7-af44-b7198685cb78"), new DateTime(2002, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "tudo@gmail.com", true, "Tú", "Đỗ", "$2a$11$2hgXAMbh5JQn8NsyP/5.BeA5iXNb0uhMHHE77mT0Pc9mT./gG73GS", "0981234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "tudo" },
                    { new Guid("ce914c08-8b01-483a-8b16-1c0e27284cc8"), new DateTime(1999, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhoang@gmail.com", true, "Lan", "Hoàng", "$2a$11$wpB2SGAOnu6LsTAGBfdjG.Ayduj7kSXGAw/6VTPn8BD2YP9dJvhUG", "0934567890", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "lanhoang" },
                    { new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"), new DateTime(1996, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "namle@gmail.com", true, "Nam", "Lê", "$2a$11$iQAAG2VgBj9NIuezPOe.F.NlFDxUXWsgYCCwp7LOqzdmxMRFkPUb.", "0901234567", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "namle" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(2003, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "haugiang@gmail.com", true, "Hau", "Giang", "$2a$11$KyhGum1m5JYn9o9O7ImqYuikQ8l9EOAkztUqahNP/lPR5F8fl.wAS", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "haugiang" },
                    { new Guid("f370712b-0452-479c-a0d4-b4d2a7ace38f"), new DateTime(2003, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamthid@gmail.com", true, "Pham", "Thi D", "$2a$11$dicFqpYdxm4JrvoiUh4NS.C3m9fq7GCh4gqwqOwrap18yTONtpgzm", "0905678901", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "phamthid" },
                    { new Guid("f58236aa-5912-489a-8ffb-001d61e611c8"), new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyenvana@gmail.com", true, "Nguyen", "Van A", "$2a$11$uk.4NZ4L1WNc/8RDFJdr3epAZ3ifz9rwRnnFJ0eQ6HSUpYWdNa8ni", "0902345678", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "nguyenvana" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quoctrieu@gmail.com", true, "Quoc", "Trieu", "$2a$11$LiuauohHd1ybsse8yA.i/ec1cUhp1s0AHkvZgW2/Td2dK2j6g84Ga", "0123456789", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quoctrieu" },
                    { new Guid("fefd096b-eb6f-4843-a9fb-fdde2f08e960"), new DateTime(2001, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "quynhphan@gmail.com", true, "Quỳnh", "Phan", "$2a$11$8brpM3HD2Ok/ZHazm6nm4edXC0HbvIGj5uQCQfpOLYSS8bxffZP.a", "0908761234", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1", "quynhphan" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ProductName", "Status", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("0372d78b-88e0-4317-b659-3df3719b3fea"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.", "Glass Beaker Set", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5539), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("051607ff-d719-4277-80b5-17703b79136f"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.", "Spiral Notebook", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5628), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("1e9081ec-56be-48eb-89ce-c840d3337f3e"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.", "Coloring Pencil Pack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5545), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("228e52af-be2c-40bc-b8bb-3b39be03c532"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A precision measuring cylinder designed for accurate measurement of liquids in laboratory settings.", "Student Backpack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5605), new Guid("97108917-856a-42af-937b-7b0e2e735b20") },
                    { new Guid("36536f7a-f473-4325-9ab5-e333d1e97df2"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of durable glass beakers with clear measurement markings for laboratory use.", "Student Backpack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5626), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("442fb45a-97f6-4f58-9368-e03b606750c6"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Student Backpack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5584), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("4ac8b894-e496-41b6-b732-5d98055a9b15"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", "Glass Beaker Set", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5537), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("56460b03-ca19-41a5-b605-955c1e5f240e"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "Digital Thermometer", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5635), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("5b7cc0de-05ab-422c-9575-e4764c5fe692"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.", "Assorted Sticky Notes", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5612), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("63eaf0f5-ebfc-480c-8730-1f0adfe9bb44"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An introductory textbook designed to help students understand the basic principles of physics.", "Coloring Pencil Pack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5529), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("653ecca0-f579-424f-93ec-6c1994de91f1"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "Laboratory Measuring Cylinder", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5575), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("65cbb412-720c-4cf9-80e2-92901ab75196"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.", "Assorted Sticky Notes", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5591), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("65ea01bf-7222-4553-9374-ca0d78b5b445"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A pack of colorful sticky notes for quick reminders, notes, and organizing your tasks.", "Laboratory Measuring Cylinder", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5543), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("6e3d0baa-0036-4f83-9908-b35497424618"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", "Elegant Fountain Pen", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5622), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") },
                    { new Guid("782b0f8a-6fbf-4bce-9189-08e6aed996f1"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.", "High-Quality Ruler Set", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5624), new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf") },
                    { new Guid("7d8f3c19-2418-4f54-9105-c0b849304cc3"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A comprehensive textbook covering fundamental algebraic concepts and problem-solving techniques.", "Coloring Pencil Pack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5593), new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a") },
                    { new Guid("8017d3e4-ae18-407d-886c-9457d23318e5"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A precision measuring cylinder designed for accurate measurement of liquids in laboratory settings.", "Elegant Fountain Pen", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5532), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("83e7590d-045c-4f30-b356-1e8d4e648bf0"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.", "Premium Ink Refills", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5520), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("8e657720-4a86-4f51-a844-ac608e263156"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.", "High-Quality Ruler Set", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5582), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("a981ad7f-6a1c-4234-843a-73513e7c3724"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "An introductory textbook designed to help students understand the basic principles of physics.", "Assorted Sticky Notes", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5609), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("aacaf3f1-7da9-45a0-8f57-bea892da86e0"), new Guid("93838a5d-b3c7-4c47-ba22-f6e26755203f"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Premium Ink Refills", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5618), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("c7f846b1-9d04-4a16-8b53-a1564ebf6566"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.", "Assorted Sticky Notes", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5633), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("ce6525fd-00ea-4374-9739-4696d06dec1b"), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.", "Student Backpack", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5615), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("e36a849f-17ee-4f9c-9478-3f39efd3e39b"), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "A pack of vibrant coloring pencils for creative drawing and coloring activities.", "Digital Thermometer", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5578), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("e56b53f8-923d-4ade-88c7-07299bc3a8fe"), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.", "Digital Thermometer", 1, new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5588), new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0fbd229c-dcaf-4337-820e-52149e8064fc"), "Image for Spiral Notebook", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5630), 23552L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("051607ff-d719-4277-80b5-17703b79136f") },
                    { new Guid("1262a627-0f51-4fee-af04-0ab85522b9d1"), "Image for Digital Thermometer", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5636), 25600L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("56460b03-ca19-41a5-b605-955c1e5f240e") },
                    { new Guid("126944c3-2f21-4c27-a4d1-e114ceb1bc8e"), "Image for Assorted Sticky Notes", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5592), 13312L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo-afGGvOl-uAIciO_inSUbq8c2WrvHEA8zA&s", new Guid("65cbb412-720c-4cf9-80e2-92901ab75196") },
                    { new Guid("19c07d7b-c497-4aeb-b025-18d7075cc769"), "Image for Glass Beaker Set", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5541), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("0372d78b-88e0-4317-b659-3df3719b3fea") },
                    { new Guid("1e58f226-f798-4682-99ee-a507c3abc88c"), "Image for Assorted Sticky Notes", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5614), 17408L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("5b7cc0de-05ab-422c-9575-e4764c5fe692") },
                    { new Guid("1fc8efdb-8139-419c-8e42-c11641e308e3"), "Image for Student Backpack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5616), 18432L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("ce6525fd-00ea-4374-9739-4696d06dec1b") },
                    { new Guid("416dbb40-ac46-44ac-99cf-740d240fae79"), "Image for Assorted Sticky Notes", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5634), 24576L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("c7f846b1-9d04-4a16-8b53-a1564ebf6566") },
                    { new Guid("42257822-861b-413a-ab7d-dabe00c5aaac"), "Image for Coloring Pencil Pack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5594), 14336L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNhUHteq_2myNyWXDG0-tdUmElcHC5ZufUbA&s", new Guid("7d8f3c19-2418-4f54-9105-c0b849304cc3") },
                    { new Guid("49005e74-3366-4551-9135-a081603ed130"), "Image for Coloring Pencil Pack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5546), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("1e9081ec-56be-48eb-89ce-c840d3337f3e") },
                    { new Guid("57dc262c-b787-4f14-9655-3b3abd40f0de"), "Image for Coloring Pencil Pack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5530), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("63eaf0f5-ebfc-480c-8730-1f0adfe9bb44") },
                    { new Guid("5c02982f-e409-4e45-8469-671e5e8d9af8"), "Image for High-Quality Ruler Set", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5625), 21504L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("782b0f8a-6fbf-4bce-9189-08e6aed996f1") },
                    { new Guid("5d8d968a-fa43-4400-bdd1-3a255628d735"), "Image for Premium Ink Refills", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5618), 19456L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("aacaf3f1-7da9-45a0-8f57-bea892da86e0") },
                    { new Guid("6a31642b-7ce9-465c-82c1-0021f9954777"), "Image for Student Backpack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5627), 22528L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("36536f7a-f473-4325-9ab5-e333d1e97df2") },
                    { new Guid("757d1b97-0c4d-4257-9953-960d92fa8459"), "Image for Glass Beaker Set", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5538), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("4ac8b894-e496-41b6-b732-5d98055a9b15") },
                    { new Guid("798b82d2-5749-4614-bbf4-87da2e0481fa"), "Image for Student Backpack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5585), 11264L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjY5xXwr880MJU4ZMkHoS4Kk9uBvJlVOocsyyr8c-SZIhpInnWpbdrTbxLSwIGRTqLtQE&usqp=CAU", new Guid("442fb45a-97f6-4f58-9368-e03b606750c6") },
                    { new Guid("80ae4146-a9e4-4711-831c-b7b9d042f01c"), "Image for Elegant Fountain Pen", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5623), 20480L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("6e3d0baa-0036-4f83-9908-b35497424618") },
                    { new Guid("8775d6e7-070a-42da-90c2-ac9afa65a85a"), "Image for Digital Thermometer", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5589), 12288L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTzAg7kp7XyYhhU7MlQDH_Or2jTxIPZuyl3j_hA01ywVLBmo5qSkIeTbDE4C7DaKdDlyI&usqp=CAU", new Guid("e56b53f8-923d-4ade-88c7-07299bc3a8fe") },
                    { new Guid("88e2e96a-9493-424b-b61e-0f539d329302"), "Image for Assorted Sticky Notes", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5610), 16384L, "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-simple-and-cute-school-supplies-stationery-suppliesstationerystaplerpenpencil-casecorrection-fluidrubber-png-image_674963.jpg", new Guid("a981ad7f-6a1c-4234-843a-73513e7c3724") },
                    { new Guid("a56a6ff3-5bf8-4925-b5b9-b4cafd253c2d"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5544), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("65ea01bf-7222-4553-9374-ca0d78b5b445") },
                    { new Guid("b1be195f-437a-44e6-a2a6-337247248d78"), "Image for High-Quality Ruler Set", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5583), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("8e657720-4a86-4f51-a844-ac608e263156") },
                    { new Guid("b545095c-0421-4284-abc4-3b6e7ca6cab9"), "Image for Student Backpack", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5606), 15360L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEabvTjiSCtzyqA2S0JmFbTYH53qBm-WjioQ&s", new Guid("228e52af-be2c-40bc-b8bb-3b39be03c532") },
                    { new Guid("b87eb31c-2a91-4142-9220-4737406d83d4"), "Image for Elegant Fountain Pen", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5533), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("8017d3e4-ae18-407d-886c-9457d23318e5") },
                    { new Guid("c73a8700-44db-4c3c-99ed-3628bd36756f"), "Image for Premium Ink Refills", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5526), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("83e7590d-045c-4f30-b356-1e8d4e648bf0") },
                    { new Guid("ce60f39e-fb8e-43c3-b046-721b6e238a4f"), "Image for Laboratory Measuring Cylinder", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5576), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("653ecca0-f579-424f-93ec-6c1994de91f1") },
                    { new Guid("da3b3d57-c3cc-4b9b-b205-37751c7399e1"), "Image for Digital Thermometer", new DateTime(2024, 8, 1, 9, 23, 37, 481, DateTimeKind.Utc).AddTicks(5579), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("e36a849f-17ee-4f9c-9478-3f39efd3e39b") }
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
