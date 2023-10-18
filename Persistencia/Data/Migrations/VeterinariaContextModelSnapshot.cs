﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

#nullable disable

namespace Persistencia.Data.Migrations
{
    [DbContext(typeof(VeterinariaContext))]
    partial class VeterinariaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dominio.Entidades.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("Fecha");

                    b.Property<int>("IdMascotaFk")
                        .HasColumnType("int");

                    b.Property<int>("IdVeterinarioFk")
                        .HasColumnType("int");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("Motivo");

                    b.HasKey("Id");

                    b.HasIndex("IdMascotaFk");

                    b.HasIndex("IdVeterinarioFk");

                    b.ToTable("Cita", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Especie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Especie", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Laboratorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("correo");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("especialidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<int>("Telefono")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("telefono");

                    b.HasKey("Id");

                    b.ToTable("Laboratorio", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNac")
                        .HasMaxLength(100)
                        .HasColumnType("date")
                        .HasColumnName("FechaNac");

                    b.Property<int>("IdPropietarioFk")
                        .HasColumnType("int");

                    b.Property<int>("IdRazaFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdPropietarioFk");

                    b.HasIndex("IdRazaFk");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("Dominio.Entidades.Medicamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdLaboratorioFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.Property<double>("Precio")
                        .HasMaxLength(10000)
                        .HasColumnType("double")
                        .HasColumnName("Precio");

                    b.Property<int>("Stock")
                        .HasMaxLength(10000)
                        .HasColumnType("int")
                        .HasColumnName("Stock");

                    b.HasKey("Id");

                    b.HasIndex("IdLaboratorioFk");

                    b.ToTable("Medicamento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.MedicamentoProveedor", b =>
                {
                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.Property<int>("IdProveedorFk")
                        .HasColumnType("int");

                    b.HasKey("IdMedicamentoFk", "IdProveedorFk");

                    b.HasIndex("IdProveedorFk");

                    b.ToTable("medicamentoProveedor", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("Cantidad");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasMaxLength(100)
                        .HasColumnType("date")
                        .HasColumnName("FechaMovimiento");

                    b.Property<int>("IdTipoMovimientoFk")
                        .HasColumnType("int");

                    b.Property<int>("Precio")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("Precio");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoMovimientoFk");

                    b.ToTable("Movimiento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.MovimientoMedicamento", b =>
                {
                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.Property<int>("IdMovimientoFk")
                        .HasColumnType("int");

                    b.HasKey("IdMedicamentoFk", "IdMovimientoFk");

                    b.HasIndex("IdMovimientoFk");

                    b.ToTable("movimientoMedicamento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Propietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Correo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.Property<int>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Propietario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("Direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.Property<int>("Telefono")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Dominio.Entidades.Raza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdEspecieFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdEspecieFk");

                    b.ToTable("Razas");
                });

            modelBuilder.Entity("Dominio.Entidades.TipoMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Descripcion");

                    b.HasKey("Id");

                    b.ToTable("TipoMovimiento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.TratamientoMedico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Dosis")
                        .HasMaxLength(1000)
                        .HasColumnType("int")
                        .HasColumnName("Dosis");

                    b.Property<DateTime>("FechaAdministracion")
                        .HasMaxLength(1000)
                        .HasColumnType("date")
                        .HasColumnName("FechaAdministracion");

                    b.Property<int>("IdCitasFk")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar")
                        .HasColumnName("Observacion");

                    b.HasKey("Id");

                    b.HasIndex("IdCitasFk");

                    b.HasIndex("IdMedicamentoFk");

                    b.ToTable("TratamientoMedicos");
                });

            modelBuilder.Entity("Dominio.Entidades.Veterinario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Correo");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Especialidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Nombre");

                    b.Property<int>("Telefono")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Veterinario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Cita", b =>
                {
                    b.HasOne("Dominio.Entidades.Mascota", "Mascota")
                        .WithMany("Citas")
                        .HasForeignKey("IdMascotaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Veterinario", "Veterinario")
                        .WithMany("Citas")
                        .HasForeignKey("IdVeterinarioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mascota");

                    b.Navigation("Veterinario");
                });

            modelBuilder.Entity("Dominio.Entidades.Mascota", b =>
                {
                    b.HasOne("Dominio.Entidades.Propietario", "Propietario")
                        .WithMany("Mascotas")
                        .HasForeignKey("IdPropietarioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Raza", "Raza")
                        .WithMany("Mascotas")
                        .HasForeignKey("IdRazaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Propietario");

                    b.Navigation("Raza");
                });

            modelBuilder.Entity("Dominio.Entidades.Medicamento", b =>
                {
                    b.HasOne("Dominio.Entidades.Laboratorio", "Laboratorio")
                        .WithMany("Medicamentos")
                        .HasForeignKey("IdLaboratorioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laboratorio");
                });

            modelBuilder.Entity("Dominio.Entidades.MedicamentoProveedor", b =>
                {
                    b.HasOne("Dominio.Entidades.Medicamento", "Medicamento")
                        .WithMany("MedicamentoProveedores")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Proveedor", "Proveedor")
                        .WithMany("MedicamentoProveedores")
                        .HasForeignKey("IdProveedorFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicamento");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Dominio.Entidades.Movimiento", b =>
                {
                    b.HasOne("Dominio.Entidades.TipoMovimiento", "TipoMovimiento")
                        .WithMany("Movimientos")
                        .HasForeignKey("IdTipoMovimientoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoMovimiento");
                });

            modelBuilder.Entity("Dominio.Entidades.MovimientoMedicamento", b =>
                {
                    b.HasOne("Dominio.Entidades.Medicamento", "Medicamento")
                        .WithMany("MovimientoMedicamentos")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Movimiento", "Movimiento")
                        .WithMany("MovimientoMedicamentos")
                        .HasForeignKey("IdMovimientoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicamento");

                    b.Navigation("Movimiento");
                });

            modelBuilder.Entity("Dominio.Entidades.Raza", b =>
                {
                    b.HasOne("Dominio.Entidades.Especie", "Especie")
                        .WithMany("Razas")
                        .HasForeignKey("IdEspecieFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especie");
                });

            modelBuilder.Entity("Dominio.Entidades.TratamientoMedico", b =>
                {
                    b.HasOne("Dominio.Entidades.Cita", "Cita")
                        .WithMany("TratamientoMedicos")
                        .HasForeignKey("IdCitasFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Medicamento", "Medicamento")
                        .WithMany("TratamientoMedicos")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Medicamento");
                });

            modelBuilder.Entity("Dominio.Entidades.Cita", b =>
                {
                    b.Navigation("TratamientoMedicos");
                });

            modelBuilder.Entity("Dominio.Entidades.Especie", b =>
                {
                    b.Navigation("Razas");
                });

            modelBuilder.Entity("Dominio.Entidades.Laboratorio", b =>
                {
                    b.Navigation("Medicamentos");
                });

            modelBuilder.Entity("Dominio.Entidades.Mascota", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Dominio.Entidades.Medicamento", b =>
                {
                    b.Navigation("MedicamentoProveedores");

                    b.Navigation("MovimientoMedicamentos");

                    b.Navigation("TratamientoMedicos");
                });

            modelBuilder.Entity("Dominio.Entidades.Movimiento", b =>
                {
                    b.Navigation("MovimientoMedicamentos");
                });

            modelBuilder.Entity("Dominio.Entidades.Propietario", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("Dominio.Entidades.Proveedor", b =>
                {
                    b.Navigation("MedicamentoProveedores");
                });

            modelBuilder.Entity("Dominio.Entidades.Raza", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("Dominio.Entidades.TipoMovimiento", b =>
                {
                    b.Navigation("Movimientos");
                });

            modelBuilder.Entity("Dominio.Entidades.Veterinario", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
