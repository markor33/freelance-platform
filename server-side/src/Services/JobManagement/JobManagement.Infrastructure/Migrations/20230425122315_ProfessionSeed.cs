using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    public partial class ProfessionSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "Software engineer", "Software engineer" },
                    { new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"), "Graphic designer", "Graphic designer" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Name", "ProfessionId" },
                values: new object[,]
                {
                    { new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"), "Design software", "Adobe Illustrator", new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d") },
                    { new Guid("93098c08-85ff-4c31-994b-5dec79c17d79"), "Programming language", "C#", new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9") },
                    { new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"), "Design software", "CorelDRAW Graphics Suite", new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d") },
                    { new Guid("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"), "Web framework", "ASP.NET CORE", new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("93098c08-85ff-4c31-994b-5dec79c17d79"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"));
        }
    }
}
