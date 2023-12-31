﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tamagochi.Context;

#nullable disable

namespace Tamagochi.Migrations
{
    [DbContext(typeof(TamagochiDatabaseContext))]
    partial class TamagochiDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tamagochi.Models.Estadistica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.Property<long>("TiempoDebil")
                        .HasColumnType("bigint");

                    b.Property<long>("TiempoHambrento")
                        .HasColumnType("bigint");

                    b.Property<long>("UltimaActualizacion")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MascotaId")
                        .IsUnique();

                    b.ToTable("Estadistica");
                });

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

                    b.Property<long>("TiempoDeCreacion")
                        .HasColumnType("bigint");

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

            modelBuilder.Entity("Tamagochi.Models.Estadistica", b =>
                {
                    b.HasOne("Tamagochi.Models.Mascota", "MascotaTrackeada")
                        .WithOne("Estadisticas")
                        .HasForeignKey("Tamagochi.Models.Estadistica", "MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MascotaTrackeada");
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

            modelBuilder.Entity("Tamagochi.Models.Mascota", b =>
                {
                    b.Navigation("Estadisticas")
                        .IsRequired();
                });

            modelBuilder.Entity("Tamagochi.Models.Usuario", b =>
                {
                    b.Navigation("Mascotas");
                });
#pragma warning restore 612, 618
        }
    }
}
