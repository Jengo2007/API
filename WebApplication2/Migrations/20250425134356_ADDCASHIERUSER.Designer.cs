﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication2.Persistence;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(CashierContext))]
    [Migration("20250425134356_ADDCASHIERUSER")]
    partial class ADDCASHIERUSER
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication2.Entities.Cashiers", b =>
                {
                    b.Property<Guid>("CashierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("CashierID");

                    b.Property<string>("CashierName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("CashierName");

                    b.Property<int>("CashierPhoneNumber")
                        .HasColumnType("integer")
                        .HasColumnName("CashierPhoneNumber");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("CashierID");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Cashiers");
                });

            modelBuilder.Entity("WebApplication2.Entities.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication2.Entities.Cashiers", b =>
                {
                    b.HasOne("WebApplication2.Entities.Users", "User")
                        .WithOne("Cashier")
                        .HasForeignKey("WebApplication2.Entities.Cashiers", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication2.Entities.Users", b =>
                {
                    b.Navigation("Cashier");
                });
#pragma warning restore 612, 618
        }
    }
}
