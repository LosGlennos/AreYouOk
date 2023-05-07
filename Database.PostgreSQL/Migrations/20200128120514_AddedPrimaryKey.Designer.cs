﻿// <auto-generated />
using System;
using Database.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Database.PostgreSQL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200128120514_AddedPrimaryKey")]
    partial class AddedPrimaryKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Database.PostgreSQL.Models.EndpointModel", b =>
                {
                    b.Property<string>("Endpoint")
                        .HasColumnName("endpoint")
                        .HasColumnType("text");

                    b.HasKey("Endpoint")
                        .HasName("pk_endpoints");

                    b.ToTable("endpoints");
                });

            modelBuilder.Entity("Database.PostgreSQL.Models.HealthModel", b =>
                {
                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ElapsedMilliseconds")
                        .HasColumnName("elapsed_milliseconds")
                        .HasColumnType("integer");

                    b.Property<int>("StatusCode")
                        .HasColumnName("status_code")
                        .HasColumnType("integer");

                    b.Property<bool>("Success")
                        .HasColumnName("success")
                        .HasColumnType("boolean");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("text");

                    b.HasKey("Timestamp")
                        .HasName("pk_health_data");

                    b.ToTable("health_data");
                });
#pragma warning restore 612, 618
        }
    }
}