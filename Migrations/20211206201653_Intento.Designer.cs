﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subastas.Data;

namespace Subastas.Migrations
{
    [DbContext(typeof(SubastaProyectosContext))]
    [Migration("20211206201653_Intento")]
    partial class Intento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Subastas.Models.Propuesta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<char>("CambioDeTipo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<float?>("Grade")
                        .HasColumnType("float");

                    b.Property<char>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubastaID")
                        .HasColumnType("int");

                    b.Property<string>("TituloPropuesta")
                        .HasColumnType("text");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Propuesta");
                });

            modelBuilder.Entity("Subastas.Models.Rol", b =>
                {
                    b.Property<int>("RolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreRol")
                        .HasColumnType("text");

                    b.HasKey("RolID");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("Subastas.Models.Subasta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<char>("CambioDeTipo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<float?>("Grade")
                        .HasColumnType("float");

                    b.Property<string>("NombreProyecto")
                        .HasColumnType("text");

                    b.Property<char>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Subasta");
                });

            modelBuilder.Entity("Subastas.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("text");

                    b.Property<string>("RFC")
                        .HasColumnType("text");

                    b.Property<int>("RolID")
                        .HasColumnType("int");

                    b.Property<string>("TagUsuario")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("RolID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Subastas.Models.Propuesta", b =>
                {
                    b.HasOne("Subastas.Models.Usuario", null)
                        .WithMany("Propuesta")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Subastas.Models.Subasta", b =>
                {
                    b.HasOne("Subastas.Models.Usuario", null)
                        .WithMany("Subasta")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Subastas.Models.Usuario", b =>
                {
                    b.HasOne("Subastas.Models.Rol", null)
                        .WithMany("Usuario")
                        .HasForeignKey("RolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
