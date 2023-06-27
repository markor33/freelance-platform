using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    public partial class ProposalCreatedPropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProposalStatus",
                table: "Proposals",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "JobStatus",
                table: "Jobs",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Proposals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Proposals",
                newName: "ProposalStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Jobs",
                newName: "JobStatus");
        }
    }
}
