﻿// <auto-generated />
using System;
using FreelancerProfile.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelancerProfile.Infrastructure.Migrations
{
    [DbContext(typeof(FreelancerProfileContext))]
    [Migration("20230529222733_FreelancerProfilePicture")]
    partial class FreelancerProfilePicture
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Certification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FreelancerId");

                    b.ToTable("Certifications", (string)null);
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FreelancerId");

                    b.ToTable("Educations", (string)null);
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Employment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FreelancerId");

                    b.ToTable("Employments", (string)null);
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Languages", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "English",
                            ShortName = "en"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Serbian",
                            ShortName = "sr"
                        },
                        new
                        {
                            Id = 3,
                            Name = "German",
                            ShortName = "de"
                        });
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.LanguageKnowledge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<int>("ProfiencyLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FreelancerId");

                    b.HasIndex("LanguageId");

                    b.ToTable("LanguageKnowledges", (string)null);
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.PortfolioProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FreelancerId");

                    b.ToTable("PortfolioProjects", (string)null);
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Professions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb"),
                            Description = "Software engineer",
                            Name = "Software engineer"
                        },
                        new
                        {
                            Id = new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0"),
                            Description = "Graphic designer",
                            Name = "Graphic designer"
                        });
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("ProfessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionId");

                    b.ToTable("Skills", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("93098c08-85ff-4c31-994b-5dec79c17d79"),
                            Description = "Programming language",
                            Name = "C#",
                            ProfessionId = new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb")
                        },
                        new
                        {
                            Id = new Guid("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"),
                            Description = "Web framework",
                            Name = "ASP.NET CORE",
                            ProfessionId = new Guid("d6861f65-0950-4c7f-b5b1-de644f923fbb")
                        },
                        new
                        {
                            Id = new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"),
                            Description = "Design software",
                            Name = "Adobe Illustrator",
                            ProfessionId = new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0")
                        },
                        new
                        {
                            Id = new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"),
                            Description = "Design software",
                            Name = "CorelDRAW Graphics Suite",
                            ProfessionId = new Guid("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0")
                        });
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Availability")
                        .HasColumnType("integer");

                    b.Property<int>("Credits")
                        .HasColumnType("integer");

                    b.Property<int>("ExperienceLevel")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsProfilePublic")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("ProfessionId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Freelancers", (string)null);
                });

            modelBuilder.Entity("FreelancerSkill", b =>
                {
                    b.Property<Guid>("FreelancersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillsId")
                        .HasColumnType("uuid");

                    b.HasKey("FreelancersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("FreelancerSkill");
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Certification", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany("Certifications")
                        .HasForeignKey("FreelancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.DateRange", "Attended", b1 =>
                        {
                            b1.Property<Guid>("CertificationId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("date");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date");

                            b1.HasKey("CertificationId");

                            b1.ToTable("Certifications");

                            b1.WithOwner()
                                .HasForeignKey("CertificationId");
                        });

                    b.Navigation("Attended")
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Education", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany("Educations")
                        .HasForeignKey("FreelancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.DateRange", "Attended", b1 =>
                        {
                            b1.Property<Guid>("EducationId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("date");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date");

                            b1.HasKey("EducationId");

                            b1.ToTable("Educations");

                            b1.WithOwner()
                                .HasForeignKey("EducationId");
                        });

                    b.Navigation("Attended")
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Employment", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany("Employments")
                        .HasForeignKey("FreelancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.DateRange", "Period", b1 =>
                        {
                            b1.Property<Guid>("EmploymentId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("date");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date");

                            b1.HasKey("EmploymentId");

                            b1.ToTable("Employments");

                            b1.WithOwner()
                                .HasForeignKey("EmploymentId");
                        });

                    b.Navigation("Period")
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.LanguageKnowledge", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany("LanguageKnowledges")
                        .HasForeignKey("FreelancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.PortfolioProject", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany("PortfolioProjects")
                        .HasForeignKey("FreelancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.DateRange", "Period", b1 =>
                        {
                            b1.Property<Guid>("PortfolioProjectId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("date");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date");

                            b1.HasKey("PortfolioProjectId");

                            b1.ToTable("PortfolioProjects");

                            b1.WithOwner()
                                .HasForeignKey("PortfolioProjectId");
                        });

                    b.Navigation("Period")
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", "Profession")
                        .WithMany("Skills")
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("FreelancerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("TimeZoneId")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("FreelancerId");

                            b1.ToTable("Freelancers");

                            b1.WithOwner()
                                .HasForeignKey("FreelancerId");

                            b1.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.Address", "Address", b2 =>
                                {
                                    b2.Property<Guid>("ContactFreelancerId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Number")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("ZipCode")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ContactFreelancerId");

                                    b2.ToTable("Freelancers");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactFreelancerId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();
                        });

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.HourlyRate", "HourlyRate", b1 =>
                        {
                            b1.Property<Guid>("FreelancerId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("FreelancerId");

                            b1.ToTable("Freelancers");

                            b1.WithOwner()
                                .HasForeignKey("FreelancerId");
                        });

                    b.OwnsOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects.ProfileSummary", "ProfileSummary", b1 =>
                        {
                            b1.Property<Guid>("FreelancerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("FreelancerId");

                            b1.ToTable("Freelancers");

                            b1.WithOwner()
                                .HasForeignKey("FreelancerId");
                        });

                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("HourlyRate")
                        .IsRequired();

                    b.Navigation("Profession");

                    b.Navigation("ProfileSummary")
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerSkill", b =>
                {
                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", null)
                        .WithMany()
                        .HasForeignKey("FreelancersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Freelancer", b =>
                {
                    b.Navigation("Certifications");

                    b.Navigation("Educations");

                    b.Navigation("Employments");

                    b.Navigation("LanguageKnowledges");

                    b.Navigation("PortfolioProjects");
                });
#pragma warning restore 612, 618
        }
    }
}