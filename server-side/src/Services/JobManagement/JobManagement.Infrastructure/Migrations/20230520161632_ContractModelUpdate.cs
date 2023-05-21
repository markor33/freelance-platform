using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    public partial class ContractModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Contracts",
                newName: "Started");

            migrationBuilder.AddColumn<DateTime>(
                name: "Finished",
                table: "Contracts",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Started",
                table: "Contracts",
                newName: "Date");
        }
    }
}
