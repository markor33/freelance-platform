using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class FreelancerCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("0861e3b9-bc3a-4776-a201-dbfc8951c432"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("14eafc5a-48bc-4f37-b754-7c1752e50371"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("37ddd9da-9400-44aa-8d3e-363edf42e802"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("bdf727cc-8fdf-452b-8ba7-286348e19341"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e8924677-e632-48af-bf6d-ebd44d95b4a7"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("fcab2f53-9b19-4d63-b64f-6766706a4b71"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("c85558d7-f546-4ce5-bde9-a6f367139e51"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("ded938f0-20c6-4423-89ce-3abfcc1e11a2"));

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Freelancers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0"), "Graphic designer", "Graphic designer" },
                    { new Guid("a586df51-7668-4cee-8ebf-671b2f0c9fef"), "Software engineer", "Software engineer" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Name", "ProfessionId" },
                values: new object[,]
                {
                    { new Guid("2ac263c8-df6a-4aae-a8c1-8fb3b0a76049"), "Programming language", "C#", new Guid("a586df51-7668-4cee-8ebf-671b2f0c9fef") },
                    { new Guid("2b03c6b5-1ae1-4bc4-bdee-1bd113acb1f2"), "Web framework", "ASP>NET CORE", new Guid("a586df51-7668-4cee-8ebf-671b2f0c9fef") },
                    { new Guid("331789b8-c928-4abc-b515-4b8e2fdd0e44"), "Design software", "Adobe Photoshop", new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0") },
                    { new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"), "Design software", "Adobe Illustrator", new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0") },
                    { new Guid("7fbde210-bfee-46b1-b62a-dcf9557a0fdf"), "Programming language", "Java", new Guid("a586df51-7668-4cee-8ebf-671b2f0c9fef") },
                    { new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"), "Design software", "CorelDRAW Graphics Suite", new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("2ac263c8-df6a-4aae-a8c1-8fb3b0a76049"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("2b03c6b5-1ae1-4bc4-bdee-1bd113acb1f2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("331789b8-c928-4abc-b515-4b8e2fdd0e44"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("7fbde210-bfee-46b1-b62a-dcf9557a0fdf"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0"));

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: new Guid("a586df51-7668-4cee-8ebf-671b2f0c9fef"));

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Freelancers");

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("c85558d7-f546-4ce5-bde9-a6f367139e51"), "Software engineer", "Software engineer" },
                    { new Guid("ded938f0-20c6-4423-89ce-3abfcc1e11a2"), "Graphic designer", "Graphic designer" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Name", "ProfessionId" },
                values: new object[,]
                {
                    { new Guid("0861e3b9-bc3a-4776-a201-dbfc8951c432"), "Programming language", "Java", new Guid("c85558d7-f546-4ce5-bde9-a6f367139e51") },
                    { new Guid("14eafc5a-48bc-4f37-b754-7c1752e50371"), "Design software", "Adobe Photoshop", new Guid("ded938f0-20c6-4423-89ce-3abfcc1e11a2") },
                    { new Guid("37ddd9da-9400-44aa-8d3e-363edf42e802"), "Programming language", "C#", new Guid("c85558d7-f546-4ce5-bde9-a6f367139e51") },
                    { new Guid("bdf727cc-8fdf-452b-8ba7-286348e19341"), "Design software", "Adobe Illustrator", new Guid("ded938f0-20c6-4423-89ce-3abfcc1e11a2") },
                    { new Guid("e8924677-e632-48af-bf6d-ebd44d95b4a7"), "Design software", "CorelDRAW Graphics Suite", new Guid("ded938f0-20c6-4423-89ce-3abfcc1e11a2") },
                    { new Guid("fcab2f53-9b19-4d63-b64f-6766706a4b71"), "Web framework", "ASP>NET CORE", new Guid("c85558d7-f546-4ce5-bde9-a6f367139e51") }
                });
        }
    }
}
