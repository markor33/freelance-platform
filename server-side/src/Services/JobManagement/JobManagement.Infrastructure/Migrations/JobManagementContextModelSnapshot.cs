﻿// <auto-generated />
using System;
using JobManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobManagement.Infrastructure.Migrations
{
    [DbContext(typeof(JobManagementContext))]
    partial class JobManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Jobs", (string)null);
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

                            b1.Property<int>("PaymentType")
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
                    b.OwnsOne("JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects.Payment", "Payment", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("PaymentType")
                                .HasColumnType("integer");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.Navigation("Payment")
                        .IsRequired();
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
