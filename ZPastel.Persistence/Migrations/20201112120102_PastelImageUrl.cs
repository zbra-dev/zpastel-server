using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPastel.Persistence.Migrations
{
    public partial class PastelImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlavorImageUrl",
                table: "Pastel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlavorImageUrl",
                table: "Pastel");
        }
    }
}
