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
    [Migration("20231016005640_clear")]
    partial class clear
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Electro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BorSkiftes")
                        .HasColumnType("bit");

                    b.Property<string>("ChecklistItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Defekt")
                        .HasColumnType("bit");

                    b.Property<string>("Kommentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OK")
                        .HasColumnType("bit");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOrderId");

                    b.ToTable("Electros");
                });

            modelBuilder.Entity("FunksjonsTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BorSkiftes")
                        .HasColumnType("bit");

                    b.Property<string>("ChecklistItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Defekt")
                        .HasColumnType("bit");

                    b.Property<string>("Kommentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OK")
                        .HasColumnType("bit");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOrderId");

                    b.ToTable("FunksjonsTests");
                });

            modelBuilder.Entity("Hydrolisk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BorSkiftes")
                        .HasColumnType("bit");

                    b.Property<string>("ChecklistItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Defekt")
                        .HasColumnType("bit");

                    b.Property<string>("Kommentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OK")
                        .HasColumnType("bit");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOrderId");

                    b.ToTable("Hydrolisks");
                });

            modelBuilder.Entity("Mechanical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BorSkiftes")
                        .HasColumnType("bit");

                    b.Property<string>("ChecklistItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Defekt")
                        .HasColumnType("bit");

                    b.Property<string>("Kommentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OK")
                        .HasColumnType("bit");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOrderId");

                    b.ToTable("Mechanicals");
                });

            modelBuilder.Entity("Trykk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BorSkiftes")
                        .HasColumnType("bit");

                    b.Property<string>("ChecklistItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Defekt")
                        .HasColumnType("bit");

                    b.Property<string>("Kommentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OK")
                        .HasColumnType("bit");

                    b.Property<int>("Ordrenummer")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOrderId");

                    b.ToTable("Trykks");
                });

            modelBuilder.Entity("ourWinchSist.Models.ServiceOrder", b =>
                {
                    b.Property<int>("ServiceOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceOrderId"));

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

                    b.HasKey("ServiceOrderId");

                    b.ToTable("ServiceOrders");
                });

            modelBuilder.Entity("Electro", b =>
                {
                    b.HasOne("ourWinchSist.Models.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceOrder");
                });

            modelBuilder.Entity("FunksjonsTest", b =>
                {
                    b.HasOne("ourWinchSist.Models.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceOrder");
                });

            modelBuilder.Entity("Hydrolisk", b =>
                {
                    b.HasOne("ourWinchSist.Models.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceOrder");
                });

            modelBuilder.Entity("Mechanical", b =>
                {
                    b.HasOne("ourWinchSist.Models.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceOrder");
                });

            modelBuilder.Entity("Trykk", b =>
                {
                    b.HasOne("ourWinchSist.Models.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceOrder");
                });
#pragma warning restore 612, 618
        }
    }
}
