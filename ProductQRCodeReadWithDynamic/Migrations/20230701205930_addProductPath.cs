using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductQRCodeReadWithDynamic.Migrations
{
    public partial class addProductPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPath",
                table: "Products");
        }
    }
}
