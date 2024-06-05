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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProductImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                    RatingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Ratings_Users_RatingUserId",
                        column: x => x.RatingUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_TargetUserId",
                        column: x => x.TargetUserId,
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
                    IsApprove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
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
                    { new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Stationery" },
                    { new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Drawing Supplies" },
                    { new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Tech Devices" },
                    { new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Books and Materials" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "Moderator" },
                    { new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), "Buyer" },
                    { new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "Seller" },
                    { new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "IsActive", "LastName", "Password", "PhoneNumber", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", true, "Doe", "123456789", "555-1234567", "", "admin" },
                    { new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", true, "Davis", "123456789", "555-3691258", "", "buyer2" },
                    { new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", true, "Johnson", "123456789", "555-2468013", "", "buyer1" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", true, "Smith", "123456789", "555-7654321", "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.lee@example.com", "David", true, "Lee", "123456789", "555-4725836", "", "seller" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "Price", "ProductImageUrl", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("6ede9679-64bd-48af-a1ef-b04f55ee8fa3"), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "Scientific calculator with graphing capabilities", true, true, 59.99f, "https://example.com/graphing-calculator.jpg", "Graphing Calculator", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("77e9bb4d-286e-4f0d-ab61-fac48c135cab"), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "High school-level chemistry textbook", true, true, 29.99f, "https://example.com/chemistry-textbook.jpg", "Chemistry Textbook", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("79e9860d-efdc-43b6-8ca2-b077798f62ea"), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e0b58109-b173-442a-86d5-972e0bc3e093"), "High-performance tablet for educational use", true, true, 299.99f, "https://example.com/tablet-computer.jpg", "Tablet Computer", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "A5 size sketchbook with acid-free pages", true, true, 12.99f, "https://example.com/sketchbook.jpg", "Sketchbook", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("d6afe29b-0a86-4e4f-b29d-28571e906767"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Set of 4 fluorescent highlighters", true, true, 3.99f, "https://example.com/highlighter-set.jpg", "Highlighter Set", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("d96ccbb3-39c2-4d9e-b829-1705216664fa"), new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0fde948-4e6d-4412-a417-3eac5f927d44"), "Grade 7 mathematics practice workbook", true, true, 14.99f, "https://example.com/math-workbook.jpg", "Mathematics Workbook", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("dc3e969c-5a30-4028-8a96-db3f0dcd53de"), new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ce74fc86-9cdf-4805-960c-e4647f21f6cf"), "Set of 24 high-quality colored pencils", true, true, 9.99f, "https://example.com/colored-pencils.jpg", "Colored Pencils", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("ec851619-5b2f-4a01-b2c8-ea4ec62c85ce"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable 30cm plastic ruler", true, true, 1.5f, "https://example.com/ruler.jpg", "Ruler", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("eee1a0c9-77c3-4fc1-b6a2-da34cf31c219"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Premium ballpoint pen for everyday use", true, true, 2.99f, "https://example.com/ballpoint-pen.jpg", "Ballpoint Pen", new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("ef26caec-cebe-47cc-8e2f-baecbf5047fc"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94d367d0-61d1-4979-ba88-99b2f83fe9eb"), "Durable mechanical pencil with 0.5mm lead", true, true, 4.5f, "https://example.com/mechanical-pencil.jpg", "Mechanical Pencil", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda") },
                    { new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("ca5af2d0-6b92-49bb-91ff-2e5d9f1279d4"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017") },
                    { new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "IsActive", "ProductId", "Reason", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { new Guid("34c3914f-8f19-4e3e-91de-eb662a49abe0"), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Misleading information", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("3516a591-6fc7-4f06-af6d-d22b592d3b3a"), new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("dc3e969c-5a30-4028-8a96-db3f0dcd53de"), "Copyright infringement", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("3aed3257-a4e3-491d-a679-f18d2af5a08a"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("ef26caec-cebe-47cc-8e2f-baecbf5047fc"), "Inappropriate content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("7df1a416-b856-4477-851c-e96fbc04aa0c"), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("77e9bb4d-286e-4f0d-ab61-fac48c135cab"), "Illegal activity", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("87822d6f-2fcd-4d3b-a6d9-4df3e4af4dc6"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("d6afe29b-0a86-4e4f-b29d-28571e906767"), "Violation of privacy", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("937283db-8b5a-496d-b79d-4f052be09327"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("eee1a0c9-77c3-4fc1-b6a2-da34cf31c219"), "Spam content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("b6c5905b-f7df-48ef-9d27-adf314089ed7"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("79e9860d-efdc-43b6-8ca2-b077798f62ea"), "Harassment", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("c437e6f0-9273-40ac-9169-a65d14071b8c"), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("ec851619-5b2f-4a01-b2c8-ea4ec62c85ce"), "Fraud", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("e6f7b0e1-a386-450a-8990-44a17a53264d"), new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("6ede9679-64bd-48af-a1ef-b04f55ee8fa3"), "Violation of terms of service", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("ea0c8f8f-f249-43b4-b363-490dd233d245"), new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Hate speech", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
                });

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
                name: "IX_Ratings_RatingUserId",
                table: "Ratings",
                column: "RatingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TargetUserId",
                table: "Ratings",
                column: "TargetUserId");

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
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
