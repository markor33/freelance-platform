using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class FreelancerModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "Freelancers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceLevel",
                table: "Freelancers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "HourlyRate_Amount",
                table: "Freelancers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "HourlyRate_Currency",
                table: "Freelancers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfilePublic",
                table: "Freelancers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId",
                table: "Freelancers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProfileSummary_Description",
                table: "Freelancers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileSummary_Title",
                table: "Freelancers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageKnowledges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    ProfiencyLevel = table.Column<int>(type: "integer", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageKnowledges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageKnowledges_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LanguageKnowledges_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreelancerSkill",
                columns: table => new
                {
                    FreelancersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerSkill", x => new { x.FreelancersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_FreelancerSkill_Freelancers_FreelancersId",
                        column: x => x.FreelancersId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "English", "en" },
                    { 2, "Serbian", "sr" },
                    { 3, "German", "de" }
                });

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11409707-7807-4e21-9c49-04b70e34c5e8"), "Graphic designer", "Graphic designer" },
                    { new Guid("14c01c08-bc78-481e-b47d-b7354d8a6362"), "Software engineer", "Software engineer" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Name", "ProfessionId" },
                values: new object[,]
                {
                    { new Guid("05efc309-4190-45be-8a5d-0e05feb25c63"), "Web framework", "ASP>NET CORE", new Guid("14c01c08-bc78-481e-b47d-b7354d8a6362") },
                    { new Guid("2931d233-4d0f-4eb9-b1d4-ca8dff1abd1b"), "Design software", "Adobe Photoshop", new Guid("11409707-7807-4e21-9c49-04b70e34c5e8") },
                    { new Guid("80e7ea5c-9f97-4882-92bf-51f33e69dad2"), "Design software", "CorelDRAW Graphics Suite", new Guid("11409707-7807-4e21-9c49-04b70e34c5e8") },
                    { new Guid("bf691946-8211-4681-b0ac-3b8fa99936a3"), "Design software", "Adobe Illustrator", new Guid("11409707-7807-4e21-9c49-04b70e34c5e8") },
                    { new Guid("de38b093-b6b2-4090-912a-1cc492a21b24"), "Programming language", "Java", new Guid("14c01c08-bc78-481e-b47d-b7354d8a6362") },
                    { new Guid("decdb9a3-57dd-4168-ad02-6c9235ebb037"), "Programming language", "C#", new Guid("14c01c08-bc78-481e-b47d-b7354d8a6362") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_ProfessionId",
                table: "Freelancers",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkill_SkillsId",
                table: "FreelancerSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageKnowledges_FreelancerId",
                table: "LanguageKnowledges",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageKnowledges_LanguageId",
                table: "LanguageKnowledges",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ProfessionId",
                table: "Skills",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Professions_ProfessionId",
                table: "Freelancers");

            migrationBuilder.DropTable(
                name: "FreelancerSkill");

            migrationBuilder.DropTable(
                name: "LanguageKnowledges");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Freelancers_ProfessionId",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "HourlyRate_Amount",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "HourlyRate_Currency",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "IsProfilePublic",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "ProfileSummary_Description",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "ProfileSummary_Title",
                table: "Freelancers");
        }
    }
}
