using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Tss.Infrastructure.Data;

namespace Tss.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TssContext))]
    [Migration("20261027050000_ConvertProductTextToUtf8mb4")]
    public partial class ConvertProductTextToUtf8mb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE `product` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;"
            );
            migrationBuilder.Sql(
                "ALTER TABLE `attribute` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotSupportedException(
                "Converting product and attribute text back to latin1 could lose data."
            );
        }
    }
}
