﻿// <auto-generated />
using System;
using FileSearcher;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileSearcher.Migrations
{
    [DbContext(typeof(HistoryDbContext))]
    [Migration("20240103200135_InitialMigraion")]
    partial class InitialMigraion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("FileSearcher.HistoryEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileTypes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Results")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Root")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SearchMode")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
