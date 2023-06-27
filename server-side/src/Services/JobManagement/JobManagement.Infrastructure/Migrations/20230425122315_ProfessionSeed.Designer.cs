﻿// <auto-generated />
using System;
using JobManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    [DbContext(typeof(JobManagementContext))]
    [Migration("20230425122315_ProfessionSeed")]
    partial class ProfessionSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", b =>
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
                            Id = new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9"),
                            Description = "Software engineer",
                            Name = "Software engineer"
                        },
                        new
                        {
                            Id = new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"),
                            Description = "Graphic designer",
                            Name = "Graphic designer"
                        });
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", b =>
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
                            ProfessionId = new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9")
                        },
                        new
                        {
                            Id = new Guid("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"),
                            Description = "Web framework",
                            Name = "ASP.NET CORE",
                            ProfessionId = new Guid("523c9ba1-4e91-4a75-85c3-cf386c078aa9")
                        },
                        new
                        {
                            Id = new Guid("5d741f6a-f024-4dca-8b1f-afccec1f72ea"),
                            Description = "Design software",
                            Name = "Adobe Illustrator",
                            ProfessionId = new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d")
                        },
                        new
                        {
                            Id = new Guid("e190ca8a-5252-4b00-8128-f21d9918efaf"),
                            Description = "Design software",
                            Name = "CorelDRAW Graphics Suite",
                            ProfessionId = new Guid("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d")
                        });
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProposalId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProposalId");

                    b.ToTable("Answers", (string)null);
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Proposal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FreelancerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uuid");

                    b.Property<int?>("ProposalStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Proposals", (string)null);
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<int>("Credits")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExperienceLevel")
                        .HasColumnType("integer");

                    b.Property<int>("JobStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProfessionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionId");

                    b.ToTable("Jobs", (string)null);
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.Property<Guid>("JobsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillsId")
                        .HasColumnType("uuid");

                    b.HasKey("JobsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", "Profession")
                        .WithMany("Skills")
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Answer", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Proposal", null)
                        .WithMany("Answers")
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Proposal", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.JobAggregate.Job", null)
                        .WithMany("Proposals")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects.Payment", "Payment", b1 =>
                        {
                            b1.Property<Guid>("ProposalId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("Type")
                                .HasColumnType("integer");

                            b1.HasKey("ProposalId");

                            b1.ToTable("Proposals");

                            b1.WithOwner()
                                .HasForeignKey("ProposalId");
                        });

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Question", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.JobAggregate.Job", null)
                        .WithMany("Questions")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Job", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects.Payment", "Payment", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("Type")
                                .HasColumnType("integer");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.Navigation("Payment")
                        .IsRequired();

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.HasOne("JobManagement.Domain.AggregatesModel.JobAggregate.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.FreelancerAggregate.Entities.Profession", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Entities.Proposal", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("JobManagement.Domain.AggregatesModel.JobAggregate.Job", b =>
                {
                    b.Navigation("Proposals");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
