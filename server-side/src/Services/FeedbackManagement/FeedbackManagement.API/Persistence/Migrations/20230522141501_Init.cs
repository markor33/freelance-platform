using System;
using FeedbackManagement.API.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackManagement.API.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishedContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientFeedback = table.Column<Feedback>(type: "jsonb", nullable: true),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FreelancerFeedback = table.Column<Feedback>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedContracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishedContracts");
        }
    }
}
