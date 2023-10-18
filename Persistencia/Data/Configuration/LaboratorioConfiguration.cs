using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class LaboratorioConfiguration : IEntityTypeConfiguration<Laboratorio>
    {
        public void Configure(EntityTypeBuilder<Laboratorio> builder)
        {
            builder.ToTable("Laboratorio");

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.Correo)
            .HasColumnName("correo")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);


            builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Especialidad)
            .HasColumnName("especialidad")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(150);
        }
    }
}