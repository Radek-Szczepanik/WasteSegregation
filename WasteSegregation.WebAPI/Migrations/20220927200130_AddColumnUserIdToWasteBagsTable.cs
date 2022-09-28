using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteSegregation.WebAPI.Migrations
{
    public partial class AddColumnUserIdToWasteBagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WasteBags",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WasteBags");
        }
    }
}
