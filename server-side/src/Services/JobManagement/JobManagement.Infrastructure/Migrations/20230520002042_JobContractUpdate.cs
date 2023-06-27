using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    public partial class JobContractUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts",
                column: "JobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
