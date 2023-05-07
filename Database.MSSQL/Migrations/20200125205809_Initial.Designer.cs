﻿// <auto-generated />
using System;
using Database.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.MSSQL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200125205809_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.MSSQL.Models.EndpointModel", b =>
                {
                    b.Property<string>("Endpoint")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("Database.MSSQL.Models.HealthModel", b =>
                {
                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("ElapsedMilliseconds")
                        .HasColumnType("int");

                    b.Property<int>("StatusCode")
                        .HasColumnType("int");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Timestamp");

                    b.ToTable("HealthData");
                });
#pragma warning restore 612, 618
        }
    }
}