using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken");

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("1c87ef07-0cd5-4492-880b-181c9c335939"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("3aad9647-f342-49d8-ad2e-8ca2e24e9c40"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("57de5b6e-19f7-437e-a992-1ee12852726a"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("700c273b-da8c-4c42-a029-0f2a71ff1384"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("791c8aae-ed62-49bc-8941-51dd6807f78d"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("8d141c98-5822-43e8-99cd-9861c952a0d7"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("96937468-e51d-4a52-a980-60fb1ebb3a05"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("a7867e17-bae6-4ab9-b800-c8448ebfdd09"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("e695ee73-a3f9-4f54-bc32-fcc5d2e378b2"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("e990fc8d-9bb1-4615-8806-2080b990ac7a"));

            migrationBuilder.RenameTable(
                name: "UserToken",
                newName: "UserTokens");

            migrationBuilder.RenameIndex(
                name: "IX_UserToken_UserId",
                table: "UserTokens",
                newName: "IX_UserTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "IsActive", "IsApprove", "ProductId", "Reason", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { new Guid("0e8403f4-2ac3-478a-b91c-5ed716dba653"), new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Hate speech", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("3e5c05b7-089b-4f46-a9d3-29205a2a4504"), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Misleading information", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("45b06421-5a4d-41be-9054-7499126b9873"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("79e9860d-efdc-43b6-8ca2-b077798f62ea"), "Harassment", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("48d62029-11c6-45b7-87f9-b0543de297d4"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("d6afe29b-0a86-4e4f-b29d-28571e906767"), "Violation of privacy", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("77b5c9de-47e9-45a9-be10-32e4de9d9655"), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("77e9bb4d-286e-4f0d-ab61-fac48c135cab"), "Illegal activity", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("87604f1f-1598-4288-8b6a-c33ca56e5cfd"), new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("dc3e969c-5a30-4028-8a96-db3f0dcd53de"), "Copyright infringement", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("9476090d-9dfa-45d1-9485-985cd4dbaa03"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("eee1a0c9-77c3-4fc1-b6a2-da34cf31c219"), "Spam content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("b88627d0-6699-46fc-a0ba-bc200da60151"), new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("6ede9679-64bd-48af-a1ef-b04f55ee8fa3"), "Violation of terms of service", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("d22ad452-1794-490b-9c12-12bd035ae40a"), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("ec851619-5b2f-4a01-b2c8-ea4ec62c85ce"), "Fraud", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("fa9f151e-4927-4390-bbef-bb1f87eb2da2"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("ef26caec-cebe-47cc-8e2f-baecbf5047fc"), "Inappropriate content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens");

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("0e8403f4-2ac3-478a-b91c-5ed716dba653"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("3e5c05b7-089b-4f46-a9d3-29205a2a4504"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("45b06421-5a4d-41be-9054-7499126b9873"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("48d62029-11c6-45b7-87f9-b0543de297d4"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("77b5c9de-47e9-45a9-be10-32e4de9d9655"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("87604f1f-1598-4288-8b6a-c33ca56e5cfd"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("9476090d-9dfa-45d1-9485-985cd4dbaa03"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("b88627d0-6699-46fc-a0ba-bc200da60151"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("d22ad452-1794-490b-9c12-12bd035ae40a"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: new Guid("fa9f151e-4927-4390-bbef-bb1f87eb2da2"));

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "UserToken");

            migrationBuilder.RenameIndex(
                name: "IX_UserTokens_UserId",
                table: "UserToken",
                newName: "IX_UserToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "IsActive", "IsApprove", "ProductId", "Reason", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { new Guid("1c87ef07-0cd5-4492-880b-181c9c335939"), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Misleading information", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("3aad9647-f342-49d8-ad2e-8ca2e24e9c40"), new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("dc3e969c-5a30-4028-8a96-db3f0dcd53de"), "Copyright infringement", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("57de5b6e-19f7-437e-a992-1ee12852726a"), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("77e9bb4d-286e-4f0d-ab61-fac48c135cab"), "Illegal activity", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("700c273b-da8c-4c42-a029-0f2a71ff1384"), new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("864a10b2-8045-469d-a4f3-d52433195fa5"), "Hate speech", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("791c8aae-ed62-49bc-8941-51dd6807f78d"), new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("6ede9679-64bd-48af-a1ef-b04f55ee8fa3"), "Violation of terms of service", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("8d141c98-5822-43e8-99cd-9861c952a0d7"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("eee1a0c9-77c3-4fc1-b6a2-da34cf31c219"), "Spam content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") },
                    { new Guid("96937468-e51d-4a52-a980-60fb1ebb3a05"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("ef26caec-cebe-47cc-8e2f-baecbf5047fc"), "Inappropriate content", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("a7867e17-bae6-4ab9-b800-c8448ebfdd09"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("d6afe29b-0a86-4e4f-b29d-28571e906767"), "Violation of privacy", new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad"), new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c") },
                    { new Guid("e695ee73-a3f9-4f54-bc32-fcc5d2e378b2"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("79e9860d-efdc-43b6-8ca2-b077798f62ea"), "Harassment", new Guid("50248ca1-b632-4e16-b1a4-9aadd8e08e7c"), new Guid("d6446689-2743-460b-82c3-d25b21f87b13") },
                    { new Guid("e990fc8d-9bb1-4615-8806-2080b990ac7a"), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("ec851619-5b2f-4a01-b2c8-ea4ec62c85ce"), "Fraud", new Guid("d6446689-2743-460b-82c3-d25b21f87b13"), new Guid("99d274e6-fa23-4d1c-8f8a-097b3886caad") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                table: "UserToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
