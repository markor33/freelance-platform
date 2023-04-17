using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientProfile.API.Infrastructure.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Contact_TimeZoneId = table.Column<string>(type: "text", nullable: false),
                    Contact_Address_Country = table.Column<string>(type: "text", nullable: false),
                    Contact_Address_City = table.Column<string>(type: "text", nullable: false),
                    Contact_Address_Street = table.Column<string>(type: "text", nullable: false),
                    Contact_Address_Number = table.Column<string>(type: "text", nullable: false),
                    Contact_Address_ZipCode = table.Column<string>(type: "text", nullable: false),
                    Contact_PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
