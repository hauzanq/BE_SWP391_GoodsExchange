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
                name: "ProductImage",
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
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
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
                columns: new[] { "UserId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "UserImageUrl", "UserName" },
                values: new object[,]
                {
                    { new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"), new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "123456789", "555-1234567", new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"), "", "admin" },
                    { new Guid("82c47d9c-b386-4050-a42c-95a220639c54"), new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", "Johnson", "123456", "555-2468013", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhihi" },
                    { new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "123456789", "555-7654321", new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"), "", "moderator" },
                    { new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.lee@example.com", "David", "Lee", "123456", "555-4725836", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhehe" },
                    { new Guid("fda6e282-e429-4364-a445-136b570e2fde"), new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", "Davis", "123456", "555-3691258", new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"), "", "customerhaha" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ApprovedDate", "CategoryId", "Description", "IsActive", "IsApproved", "Price", "ProductName", "UploadDate", "UserUploadId" },
                values: new object[,]
                {
                    { new Guid("088d56e7-60d4-4d13-a39c-a258da0c5590"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5051), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 8", true, true, 80f, "Product 8", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5050), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("10b437f0-6ff2-4481-b135-c1d7df966ffa"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5003), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 2", true, true, 20f, "Product 2", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5003), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("17e88ef3-88df-4181-a10e-fbd04da3968f"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5011), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 4", true, true, 40f, "Product 4", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5011), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("20b3e879-e41a-4134-88c4-819ca3ecb373"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5007), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 3", true, true, 30f, "Product 3", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5007), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("4ea4edac-5506-40c9-84a7-68d79d5b867f"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5022), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 6", true, true, 60f, "Product 6", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5022), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("6e37b3ae-c322-4880-b155-111bca1e6690"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5054), new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"), "Description for product 9", true, true, 90f, "Product 9", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5054), new Guid("82c47d9c-b386-4050-a42c-95a220639c54") },
                    { new Guid("6f4170fb-d03c-4335-80eb-bf6a62ee1009"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5014), new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"), "Description for product 5", true, true, 50f, "Product 5", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5014), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("8ece762f-bb55-4a1b-a37b-8be0d5e8757c"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5046), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 7", true, true, 70f, "Product 7", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5046), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("9cdcc7a7-0ae7-4ac3-8812-07ab635f2ba2"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(4983), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 1", true, true, 10f, "Product 1", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(4982), new Guid("fda6e282-e429-4364-a445-136b570e2fde") },
                    { new Guid("f34a3f99-0339-42b0-ae1d-fda0aeb570b6"), new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5061), new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"), "Description for product 10", true, true, 100f, "Product 10", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5061), new Guid("fda6e282-e429-4364-a445-136b570e2fde") }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "Caption", "DateCreated", "FileSize", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("1ceccd13-c07f-4757-b2fd-434c54cb0b5a"), "Image for product 4", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5012), 4096L, "https://img.lovepik.com/element/40148/8397.png_300.png", new Guid("17e88ef3-88df-4181-a10e-fbd04da3968f") },
                    { new Guid("2620b79d-2952-410c-a78e-230e7166ef8a"), "Image for product 6", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5023), 6144L, "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png", new Guid("4ea4edac-5506-40c9-84a7-68d79d5b867f") },
                    { new Guid("349eb320-34be-4ec2-ac82-4e05ce8884e9"), "Image for product 8", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5052), 8192L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg", new Guid("088d56e7-60d4-4d13-a39c-a258da0c5590") },
                    { new Guid("5e351632-537e-4700-9b1d-b4e5a3ed6a52"), "Image for product 10", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5062), 10240L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU", new Guid("f34a3f99-0339-42b0-ae1d-fda0aeb570b6") },
                    { new Guid("910ac126-1828-4f4d-b426-e0a512a56607"), "Image for product 9", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5058), 9216L, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU", new Guid("6e37b3ae-c322-4880-b155-111bca1e6690") },
                    { new Guid("92e5b652-37cf-4e2a-8279-c865ab525a06"), "Image for product 1", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5000), 1024L, "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg", new Guid("9cdcc7a7-0ae7-4ac3-8812-07ab635f2ba2") },
                    { new Guid("de9fec22-d61c-4398-a233-7c7dc6d0cc1e"), "Image for product 2", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5005), 2048L, "https://img.lovepik.com/element/40145/4924.png_860.png", new Guid("10b437f0-6ff2-4481-b135-c1d7df966ffa") },
                    { new Guid("ef27f366-1d42-4039-84ba-71701c4d73ea"), "Image for product 5", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5019), 5120L, "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png", new Guid("6f4170fb-d03c-4335-80eb-bf6a62ee1009") },
                    { new Guid("f17991fb-feab-49f5-9d27-d89c69f0a61a"), "Image for product 3", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5009), 3072L, "https://img.lovepik.com/element/40154/8917.png_300.png", new Guid("20b3e879-e41a-4134-88c4-819ca3ecb373") },
                    { new Guid("f8e226e7-83a4-444c-99b5-ec3c4fbf20d6"), "Image for product 7", new DateTime(2024, 6, 27, 7, 48, 25, 612, DateTimeKind.Utc).AddTicks(5048), 7168L, "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg", new Guid("8ece762f-bb55-4a1b-a37b-8be0d5e8757c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
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
                name: "ProductImage");

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
