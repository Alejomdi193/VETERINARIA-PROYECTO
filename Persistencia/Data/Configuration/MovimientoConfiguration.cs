using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimiento");

            builder.Property(p => p.Cantidad)
            .HasColumnName("Cantidad")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(100);


            builder.Property(p => p.Precio)
            .HasColumnName("Precio")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.FechaMovimiento)
            .HasColumnName("FechaMovimiento")
            .HasColumnType("date")
            .IsRequired()
            .HasMaxLength(100);

            builder.HasOne(p => p.TipoMovimiento)
            .WithMany(p => p.Movimientos)
            .HasForeignKey(p => p.IdTipoMovimientoFk);


               builder.HasMany(p => p.Medicamentos)
            .WithMany(p => p.Movimientos)
            .UsingEntity<MovimientoMedicamento>(
                    j => j
                    .HasOne(p => p.Medicamento)
                    .WithMany(p => p.MovimientoMedicamentos)
                    .HasForeignKey(p => p.IdMedicamentoFk),

                    j => j
                    .HasOne(p => p.Movimiento)
                    .WithMany(p => p.MovimientoMedicamentos)
                    .HasForeignKey(p => p.IdMovimientoFk),

                    j =>
                    {
                        j.ToTable("movimientoMedicamento");
                        j.HasKey(t => new { t.IdMedicamentoFk,t.IdMovimientoFk });
                    });
            
        }
    }
}