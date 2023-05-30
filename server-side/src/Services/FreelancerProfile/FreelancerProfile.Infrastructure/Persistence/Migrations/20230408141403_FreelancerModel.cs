using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    public partial class FreelancerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Contact_PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsProfilePublic = table.Column<bool>(type: "boolean", nullable: false),
                    ProfileSummary_Title = table.Column<string>(type: "text", nullable: false),
                    ProfileSummary_Description = table.Column<string>(type: "text", nullable: false),
                    HourlyRate_Amount = table.Column<float>(type: "real", nullable: false),
                    HourlyRate_Currency = table.Column<string>(type: "text", nullable: false),
                    Availability = table.Column<int>(type: "integer", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "integer", nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelancers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Freelancers_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
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
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    Attended_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Attended_End = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certifications_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolName = table.Column<string>(type: "text", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: false),
                    Attended_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Attended_End = table.Column<DateOnly>(type: "date", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Period_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Period_End = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employments_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageKnowledges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    ProfiencyLevel = table.Column<int>(type: "integer", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageKnowledges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageKnowledges_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageKnowledges_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Period_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Period_End = table.Column<DateOnly>(type: "date", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioProjects_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
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
                    { new Guid("5d9c0a7f-cf11-4c29-930a-0842fe6be2ad"), "Graphic designer", "Graphic designer" },
                    { new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb"), "Software engineer", "Software engineer" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Name", "ProfessionId" },
                values: new object[,]
                {
                    { new Guid("336dda85-0a33-42b8-a591-b18a29dbcaa6"), "Design software", "Adobe Photoshop", new Guid("5d9c0a7f-cf11-4c29-930a-0842fe6be2ad") },
                    { new Guid("39855c82-eb88-4794-b539-85407fbb59c5"), "Design software", "Adobe Illustrator", new Guid("5d9c0a7f-cf11-4c29-930a-0842fe6be2ad") },
                    { new Guid("4320c74f-4b82-4a54-abf7-6097fbfd0078"), "Programming language", "Java", new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb") },
                    { new Guid("75b05ed6-9847-4564-ab1d-8f34acf657bc"), "Design software", "CorelDRAW Graphics Suite", new Guid("5d9c0a7f-cf11-4c29-930a-0842fe6be2ad") },
                    { new Guid("93098c08-85ff-4c31-994b-5dec79c17d79"), "Programming language", "C#", new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb") },
                    { new Guid("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"), "Web framework", "ASP>NET CORE", new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_FreelancerId",
                table: "Certifications",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_FreelancerId",
                table: "Educations",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_FreelancerId",
                table: "Employments",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_ProfessionId",
                table: "Freelancers",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_UserId",
                table: "Freelancers",
                column: "UserId",
                unique: true);

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
                name: "IX_PortfolioProjects_FreelancerId",
                table: "PortfolioProjects",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ProfessionId",
                table: "Skills",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Employments");

            migrationBuilder.DropTable(
                name: "FreelancerSkill");

            migrationBuilder.DropTable(
                name: "LanguageKnowledges");

            migrationBuilder.DropTable(
                name: "PortfolioProjects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Freelancers");

            migrationBuilder.DropTable(
                name: "Professions");
        }
    }
}
