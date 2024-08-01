﻿// <auto-generated />
using System;
using LibraryMvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryMvc.Migrations
{
    [DbContext(typeof(LibraryMvcContext))]
    [Migration("20240731121200_changeGenreType")]
    partial class changeGenreType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryMvc.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Hight")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SetId")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<string>("genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryMvc.Models.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("genre")
                        .IsUnique();

                    b.ToTable("Library");
                });

            modelBuilder.Entity("LibraryMvc.Models.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("LibraryMvc.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Hight")
                        .HasColumnType("int");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LibraryId");

                    b.ToTable("Shelf");
                });

            modelBuilder.Entity("LibraryMvc.Models.Book", b =>
                {
                    b.HasOne("LibraryMvc.Models.Set", "Set")
                        .WithMany()
                        .HasForeignKey("SetId");

                    b.HasOne("LibraryMvc.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfId");

                    b.Navigation("Set");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("LibraryMvc.Models.Shelf", b =>
                {
                    b.HasOne("LibraryMvc.Models.Library", "Library")
                        .WithMany()
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });
#pragma warning restore 612, 618
        }
    }
}
