using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteSegregation.WebAPI.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstateWastes");

            migrationBuilder.CreateTable(
                name: "WasteBags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlueBag = table.Column<byte>(type: "tinyint", nullable: true),
                    GreenBag = table.Column<byte>(type: "tinyint", nullable: true),
                    YellowBag = table.Column<byte>(type: "tinyint", nullable: true),
                    BrownBag = table.Column<byte>(type: "tinyint", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealEstateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteBags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteBags_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WasteBags_RealEstateId",
                table: "WasteBags",
                column: "RealEstateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteBags");

            migrationBuilder.CreateTable(
                name: "RealEstateWastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    BlueBag = table.Column<byte>(type: "tinyint", nullable: true),
                    BrownBag = table.Column<byte>(type: "tinyint", nullable: true),
                    GreenBag = table.Column<byte>(type: "tinyint", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YellowBag = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateWastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateWastes_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateWastes_RealEstateId",
                table: "RealEstateWastes",
                column: "RealEstateId");
        }
    }
}
