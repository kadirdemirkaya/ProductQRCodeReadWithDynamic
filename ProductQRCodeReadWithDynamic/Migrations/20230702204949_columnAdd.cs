using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductQRCodeReadWithDynamic.Migrations
{
    public partial class columnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdateCount",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "Notifications");
        }
    }
}
