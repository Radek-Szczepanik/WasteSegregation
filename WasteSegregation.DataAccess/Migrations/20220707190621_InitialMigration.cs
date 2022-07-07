using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteSegregation.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WasteBags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YellowBag = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    GreenBag = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BlueBag = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BrownBag = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteBags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteBags");
        }
    }
}
