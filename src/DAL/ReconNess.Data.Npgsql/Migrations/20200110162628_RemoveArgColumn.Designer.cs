﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReconNess.Data.Npgsql;

namespace ReconNess.Data.Npgsql.Migrations
{
    [DbContext(typeof(ReconNessContext))]
    [Migration("20200110162628_RemoveArgColumn")]
    partial class RemoveArgColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ReconNess.Entities.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Command")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBySubdomain")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastRun")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("OnlyIfIsAlive")
                        .HasColumnType("boolean");

                    b.Property<string>("Script")
                        .HasColumnType("text");

                    b.Property<bool>("SkipIfRanBefore")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("ReconNess.Entities.AgentCategory", b =>
                {
                    b.Property<Guid>("AgentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("AgentId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AgentCategory");
                });

            modelBuilder.Entity("ReconNess.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ReconNess.Entities.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("ReconNess.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<Guid?>("SubdomainId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("SubdomainId")
                        .IsUnique();

                    b.HasIndex("TargetId")
                        .IsUnique();

                    b.ToTable("Note");
                });

            modelBuilder.Entity("ReconNess.Entities.Reference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Categories")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reference");
                });

            modelBuilder.Entity("ReconNess.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SubdomainId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("SubdomainId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ReconNess.Entities.Subdomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("FromAgents")
                        .HasColumnType("text");

                    b.Property<bool?>("HasHttpOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<bool?>("IsAlive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsMainPortal")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool?>("Takeover")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("Subdomains");
                });

            modelBuilder.Entity("ReconNess.Entities.SubdomainLabel", b =>
                {
                    b.Property<Guid>("SubdomainId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid");

                    b.HasKey("SubdomainId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("SubdomainLabel");
                });

            modelBuilder.Entity("ReconNess.Entities.Target", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BugBountyProgramUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("InScope")
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OutOfScope")
                        .HasColumnType("text");

                    b.Property<string>("RootDomain")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("ReconNess.Entities.AgentCategory", b =>
                {
                    b.HasOne("ReconNess.Entities.Agent", "Agent")
                        .WithMany("AgentCategories")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReconNess.Entities.Category", "Category")
                        .WithMany("AgentCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReconNess.Entities.Note", b =>
                {
                    b.HasOne("ReconNess.Entities.Subdomain", "Subdomain")
                        .WithOne("Notes")
                        .HasForeignKey("ReconNess.Entities.Note", "SubdomainId");

                    b.HasOne("ReconNess.Entities.Target", "Target")
                        .WithOne("Notes")
                        .HasForeignKey("ReconNess.Entities.Note", "TargetId");
                });

            modelBuilder.Entity("ReconNess.Entities.Service", b =>
                {
                    b.HasOne("ReconNess.Entities.Subdomain", "Subdomain")
                        .WithMany("Services")
                        .HasForeignKey("SubdomainId");
                });

            modelBuilder.Entity("ReconNess.Entities.Subdomain", b =>
                {
                    b.HasOne("ReconNess.Entities.Target", "Target")
                        .WithMany("Subdomains")
                        .HasForeignKey("TargetId");
                });

            modelBuilder.Entity("ReconNess.Entities.SubdomainLabel", b =>
                {
                    b.HasOne("ReconNess.Entities.Label", "Label")
                        .WithMany("Subdomains")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReconNess.Entities.Subdomain", "Subdomain")
                        .WithMany("Labels")
                        .HasForeignKey("SubdomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
