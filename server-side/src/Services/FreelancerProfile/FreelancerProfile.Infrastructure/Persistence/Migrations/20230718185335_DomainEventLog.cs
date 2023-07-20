using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class DomainEventLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionId",
                table: "Freelancers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "DomainEventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    EventData = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEventLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers");

            migrationBuilder.DropTable(
                name: "DomainEventLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionId",
                table: "Freelancers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
