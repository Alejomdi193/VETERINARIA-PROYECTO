using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class TratamientoMedicoConfiguration : IEntityTypeConfiguration<TratamientoMedico>
    {
        public void Configure(EntityTypeBuilder<TratamientoMedico> builder)
        {
            builder.Property(p => p.Dosis)
            .HasColumnName("Dosis")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(1000);

            builder.Property(p => p.FechaAdministracion)
            .HasColumnName("FechaAdministracion")
            .HasColumnType("date")
            .IsRequired()
            .HasMaxLength(1000);

            builder.Property(p => p.Observacion)
            .HasColumnName("Observacion")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(1000);

            builder.HasOne(p => p.Cita)
            .WithMany(p => p.TratamientoMedicos)
            .HasForeignKey(p => p.IdCitasFk);

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.TratamientoMedicos)
            .HasForeignKey(p => p.IdMedicamentoFk);


        }
    }
}