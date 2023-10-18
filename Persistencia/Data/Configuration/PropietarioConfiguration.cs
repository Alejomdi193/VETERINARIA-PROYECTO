using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PropietarioConfiguration : IEntityTypeConfiguration<Propietario>
    {
        public void Configure(EntityTypeBuilder<Propietario> builder)
        {
            builder.ToTable("Propietario");

            builder.Property(p => p.Nombre)
            .HasColumnName("Nombre")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.Correo)
            .HasColumnName("Correo")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);
                       
            builder.Property(p => p.Telefono)
            .HasColumnName("Telefono")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(20);
            


        }
    }
}