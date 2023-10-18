using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
           builder.Property(p => p.Nombre)
           .HasColumnName("Nombre")
           .HasColumnType("varchar")
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(p => p.Direccion)
           .HasColumnName("Direccion")
           .HasColumnType("varchar")
           .IsRequired()
           .HasMaxLength(150);

            builder.Property(p => p.Telefono)
           .HasColumnName("Telefono")
           .HasColumnType("int")
           .IsRequired()
           .HasMaxLength(100);


        }
    }
}