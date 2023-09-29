﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ourWinch.Utility;

#nullable disable

namespace ourWinch.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230929182413_Kunde")]
    partial class Kunde
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ourWinch.Models.Kunde", b =>
                {
                    b.Property<int>("KundeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KundeId"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Etternavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MobilNo")
                        .HasColumnType("int");

                    b.Property<string>("PostNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KundeId");

                    b.ToTable("Kunde");
                });

            modelBuilder.Entity("ourWinch.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("KommentarFelte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KundeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MottattDato")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Årsmodel")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ourWinch.Models.Service", b =>
                {
                    b.Property<int>("ServisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServisId"));

                    b.Property<decimal>("AvtaltLevering")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("JobTimer")
                        .HasColumnType("int");

                    b.Property<string>("KundeNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServisSkjema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServisStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServisId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("ourWinch.Models.ServisType", b =>
                {
                    b.Property<int>("ServisTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServisTypeId"));

                    b.Property<string>("Garanti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Reperasjon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServisTid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServisTypeId");

                    b.ToTable("ServisType");
                });

            modelBuilder.Entity("ourWinch.Models.SjekkListe", b =>
                {
                    b.Property<int>("SjekkListeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SjekkListeId"));

                    b.Property<string>("ElektroKommentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElektroSjekkId")
                        .HasColumnType("int");

                    b.Property<string>("ElektroSjekkListe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ElektroSjekkStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FonksjonKommentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FonksjonSjekkId")
                        .HasColumnType("int");

                    b.Property<string>("FonksjonSjekkListe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FonksjonSjekkStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HydrolikKommentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HydrolikSjekkId")
                        .HasColumnType("int");

                    b.Property<string>("HydrolikSjekkListe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HydrolikSjekkStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MekanikKommentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MekanikSjekkId")
                        .HasColumnType("int");

                    b.Property<string>("MekanikSjekkListe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MekanikSjekkStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("TrykkKommentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrykkSjekkId")
                        .HasColumnType("int");

                    b.Property<string>("TrykkSjekkListe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrykkSjekkStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SjekkListeId");

                    b.ToTable("SjekkListe");
                });
#pragma warning restore 612, 618
        }
    }
}
