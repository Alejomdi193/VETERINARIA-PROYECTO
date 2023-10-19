using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistencia;
public class VeterinariaContext : DbContext
{

    public VeterinariaContext(DbContextOptions<VeterinariaContext> options) : base(options)
    {
    }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<Especie> Especies { get; set; }
    public DbSet<Laboratorio> Laboratorios { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<MedicamentoProveedor> MedicamentoProveedores { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }
    public DbSet<MovimientoMedicamento> MovimientoMedicamentos { get; set; }
    public DbSet<Propietario> Propietarios { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Raza> Razas { get; set; }
    public DbSet<TipoMovimiento> TipoMovimientos { get; set; }
    public DbSet<TratamientoMedico> TratamientoMedicos { get; set; }
    public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }

 
    protected override void OnModelCreating (ModelBuilder modelBuilder)


    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }
        
        
}
