

using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.FechaNac)
            .HasColumnName("FechaNac")
            .HasColumnType("date")
            .IsRequired()
            .HasMaxLength(100);

            builder.HasOne(p => p.Propietario)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdPropietarioFk);

          
            builder.HasOne(p => p.Raza)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdRazaFk);
        }
    }
}