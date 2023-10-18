using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Cita");

            builder.Property(p => p.Fecha)
            .HasColumnName("Fecha")
            .HasColumnType("date")
            .IsRequired();

            builder.Property(p => p.Motivo)
            .HasColumnName("Motivo")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(300);

            builder.HasOne(p => p.Mascota)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.IdMascotaFk);

            builder.HasOne(p => p.Veterinario)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.IdVeterinarioFk);

        }
    }
}