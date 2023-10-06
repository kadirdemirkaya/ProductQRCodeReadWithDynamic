using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductQRCodeReadWithDynamic.Migrations
{
    public partial class columnLastChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationDateTime",
                table: "Notifications",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HubConnections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "HubConnections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "HubConnections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "HubConnections",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HubConnections");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "HubConnections");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "HubConnections");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "HubConnections");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Notifications",
                newName: "NotificationDateTime");
        }
    }
}
