using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    public partial class UpdateRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Ratings_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Ratings",
                principalColumn: "RatingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reports_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Ratings_ProductId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reports_ProductId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
