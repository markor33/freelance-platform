using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class FreelancerProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Freelancers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Freelancers");
        }
    }
}
