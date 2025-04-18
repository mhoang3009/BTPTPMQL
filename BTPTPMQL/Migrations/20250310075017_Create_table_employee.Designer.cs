﻿// <auto-generated />
using BTPTPMQL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTPTPMQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250310075017_Create_table_employee")]
    partial class Create_table_employee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("BTPTPMQL.Models.HeThongPhanPhoi", b =>
                {
                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenHTPP")
                        .HasColumnType("TEXT");

                    b.HasKey("MaHTPP");

                    b.ToTable("HeThongPhanPhoi");
                });

            modelBuilder.Entity("BTPTPMQL.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Person");

                    b.HasDiscriminator().HasValue("Person");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BTPTPMQL.Models.Employee", b =>
                {
                    b.HasBaseType("BTPTPMQL.Models.Person");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.ToTable("Person");

                    b.HasDiscriminator().HasValue("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
