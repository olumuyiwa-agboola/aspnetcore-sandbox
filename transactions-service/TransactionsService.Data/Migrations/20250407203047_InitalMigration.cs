using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Currency = table.Column<string>(type: "longtext", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    SenderName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SenderBankName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SenderAccountNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    BeneficiaryName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BeneficiaryBankName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BeneficiaryAccountNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Reference);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
