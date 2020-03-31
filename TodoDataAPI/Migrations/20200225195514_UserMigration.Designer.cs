﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TodoDataAPI;

namespace TodoDataAPI.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20200225195514_UserMigration")]
    partial class UserMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TodoDataAPI.Models.Todo", b =>
                {
                    b.Property<int>("TodoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<bool>("Done");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("UserID");

                    b.HasKey("TodoID");

                    b.HasIndex("UserID");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("TodoDataAPI.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TodoDataAPI.Models.Todo", b =>
                {
                    b.HasOne("TodoDataAPI.Models.User")
                        .WithMany("Todos")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
