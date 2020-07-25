using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tss.Infrastructure.Data.Migrations
{
    public partial class AddResetForgottenPasswordFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "reset_password_expiry_date",
                table: "customer",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "reset_password_token",
                table: "customer",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reset_password_expiry_date",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "reset_password_token",
                table: "customer");
        }
    }
}
