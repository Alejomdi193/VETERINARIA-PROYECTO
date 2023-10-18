using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("Medicamento");

            builder.Property(p => p.Nombre)
            .HasColumnName("Nombre")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.Stock)
            .HasColumnName("Stock")
            .HasColumnType("int")
            .IsRequired()
            .HasMaxLength(10000);

            builder.Property(p => p.Precio)
            .HasColumnName("Precio")
            .HasColumnType("double")
            .IsRequired()
            .HasMaxLength(10000);

            builder.HasOne(p => p.Laboratorio)
            .WithMany(p => p.Medicamentos)
            .HasForeignKey(p => p.IdLaboratorioFk);

            builder.HasMany(p => p.Proveedores)
            .WithMany(p => p.Medicamentos)
            .UsingEntity<MedicamentoProveedor>(
                    j => j
                    .HasOne(p => p.Proveedor)
                    .WithMany(p => p.MedicamentoProveedores)
                    .HasForeignKey(p => p.IdProveedorFk),

                    j => j
                    .HasOne(p => p.Medicamento)
                    .WithMany(p => p.MedicamentoProveedores)
                    .HasForeignKey(p => p.IdMedicamentoFk),

                    j =>
                    {
                        j.ToTable("medicamentoProveedor");
                        j.HasKey(t => new { t.IdMedicamentoFk,t.IdProveedorFk });
                    });

            



        }
    }
}