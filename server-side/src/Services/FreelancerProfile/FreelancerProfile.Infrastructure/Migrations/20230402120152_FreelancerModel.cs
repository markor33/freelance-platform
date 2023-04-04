using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class FreelancerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Freelancers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Freelancers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_UserId",
                table: "Freelancers",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Freelancers");
        }
    }
}
