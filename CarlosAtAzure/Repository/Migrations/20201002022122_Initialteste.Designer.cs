﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(WebApiAmigoContext))]
    [Migration("20201002022122_Initialteste")]
    partial class Initialteste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Amigo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EstadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PaisId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlFoto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PaisId");

                    b.ToTable("Amigos");
                });

            modelBuilder.Entity("Domain.AmigosDoAmigo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AmigoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConviteId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AmigoId");

                    b.ToTable("AmigoDoAmigo");
                });

            modelBuilder.Entity("Domain.Estado.Estado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PaisId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UrlFoto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("Domain.Pais.Pais", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlFoto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("Domain.Amigo", b =>
                {
                    b.HasOne("Domain.Estado.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.HasOne("Domain.Pais.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId");
                });

            modelBuilder.Entity("Domain.AmigosDoAmigo", b =>
                {
                    b.HasOne("Domain.Amigo", "Amigo")
                        .WithMany("AmigosDoAmigo")
                        .HasForeignKey("AmigoId");
                });

            modelBuilder.Entity("Domain.Estado.Estado", b =>
                {
                    b.HasOne("Domain.Pais.Pais", "Pais")
                        .WithMany("ListEstado")
                        .HasForeignKey("PaisId");
                });
#pragma warning restore 612, 618
        }
    }
}
