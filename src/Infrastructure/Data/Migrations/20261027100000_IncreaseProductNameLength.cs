using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Tss.Infrastructure.Data;

namespace Tss.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TssContext))]
    [Migration("20261027100000_IncreaseProductNameLength")]
    public partial class IncreaseProductNameLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE `product` MODIFY COLUMN `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotSupportedException(
                "Reducing product name length could truncate existing data."
            );
        }
    }
}
