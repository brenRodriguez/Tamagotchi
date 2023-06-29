﻿// <auto-generated />
using Tamagochi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Tamagochi.Migrations
{
    [DbContext(typeof(TamagochiDatabaseContext))]
    [Migration("20230615011311_estados")]
    partial class estados
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tamagochi.Models.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NombreMascota")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("TiempoMaximoSinAlimentar")
                        .HasColumnType("bigint");

                    b.Property<int>("TipoDeMascota")
                        .HasColumnType("int");

                    b.Property<long>("UltimaVezAlimentado")
                        .HasColumnType("bigint");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioUserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioUserID");

                    b.ToTable("Mascota");
                });

            modelBuilder.Entity("Tamagochi.Models.Usuario", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Tamagochi.Models.Mascota", b =>
                {
                    b.HasOne("Tamagochi.Models.Usuario", "Usuario")
                        .WithMany("Mascotas")
                        .HasForeignKey("UsuarioUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Tamagochi.Models.Usuario", b =>
                {
                    b.Navigation("Mascotas");
                });
#pragma warning restore 612, 618
        }
    }
}
