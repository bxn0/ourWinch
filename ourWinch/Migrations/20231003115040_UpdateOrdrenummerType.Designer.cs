﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ourWinch.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231003115040_UpdateOrdrenummerType")]
    partial class UpdateOrdrenummerType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ourWinchSist.Models.ServiceOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Etternavn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Feilbeskrivelse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fornavn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Garanti")
                        .HasColumnType("bit");

                    b.Property<string>("KommentarFraKunde")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MottattDato")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<string>("Produkttype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Reperasjon")
                        .HasColumnType("bit");

                    b.Property<string>("Serienummer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Servis")
                        .HasColumnType("bit");

                    b.Property<string>("Årsmodell")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiceOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
