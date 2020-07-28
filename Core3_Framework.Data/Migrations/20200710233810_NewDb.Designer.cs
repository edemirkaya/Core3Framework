﻿// <auto-generated />
using System;
using Core3_Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Core3_Framework.Data.Migrations
{
    [DbContext(typeof(AppDb))]
    [Migration("20200710233810_NewDb")]
    partial class NewDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.AuditTables", b =>
                {
                    b.Property<string>("TableName")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("TableName");

                    b.ToTable("AuditTable");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Audits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AdditionalValue")
                        .HasColumnType("text");

                    b.Property<string>("KeyValues")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("NewValues")
                        .HasColumnType("text");

                    b.Property<string>("OldValues")
                        .HasColumnType("text");

                    b.Property<string>("ProcessOwner")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("ProcessTime")
                        .HasColumnType("timestamp");

                    b.Property<string>("ProcessType")
                        .IsRequired()
                        .HasColumnType("character varying(6)")
                        .HasMaxLength(6);

                    b.Property<Guid?>("RelationId")
                        .HasColumnType("uuid");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.CacheItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CacheItem");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SeoURL")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ProductName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("QuantityPerUnit")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.UserRoles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("User_Id")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnName("Role_Id")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId")
                        .HasName("IX_Role_Id");

                    b.HasIndex("UserId")
                        .HasName("IX_User_Id");

                    b.ToTable("User_Role");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Username")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Products", b =>
                {
                    b.HasOne("Core3_Framework.Contracts.DataContracts.Categories", "Categories")
                        .WithMany("Products")
                        .HasForeignKey("CategoriesId");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.Roles", b =>
                {
                    b.HasOne("Core3_Framework.Contracts.DataContracts.Users", null)
                        .WithMany("Roles")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("Core3_Framework.Contracts.DataContracts.UserRoles", b =>
                {
                    b.HasOne("Core3_Framework.Contracts.DataContracts.Roles", "Roles")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_dbo.User_Role_dbo.Role_Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core3_Framework.Contracts.DataContracts.Users", "Users")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.User_Role_dbo.User_User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}