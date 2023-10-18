using Aplicacion.Repository;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly VeterinariaContext context;
        public UnitOfWork(VeterinariaContext _context)
        {
            context = _context;
        }
        private ICita _citas;
        private IEspecie _especies;
        private ILaboratorio _laboratorios;
        public IMascota _mascotas;
        public IMedicamento _medicamentos;
        public IMovimiento _movimientos;
        public IPropietario _propietarios;
        public IRaza _razas;
        public ITipoMovimiento _tipoMovimientos;
        public ITratamientoMedico _tratamientoMedicos;
        public IVeterinario _veterinarios;
        public IProveedor _proveedores;

        public ICita Citas
        {
            get
            {
                if (_citas == null)
                {
                    _citas = new CitaRepository(context);
                }
            return _citas;
            }
        }
        public IEspecie Especies
        {
            get{
                if (_especies == null)
                {
                    _especies = new EspecieRepository(context);
                }
            return _especies;
            }
        }

        public ILaboratorio Laboratorios
        {
            get{
                if(_laboratorios == null)
                {
                    _laboratorios = new LaboratorioRepository(context);
                }
            return _laboratorios;
            }
        }

        public IMascota Mascotas
        {
            get{
                if(_mascotas == null)
                {
                    _mascotas = new MascotaRepository(context);
                }
            return _mascotas;
            }
        }

        public IMedicamento Medicamentos
        {
            get{
                if(_medicamentos == null)
                {
                    _medicamentos = new MedicamentoRepository(context);
                }
            return _medicamentos;
            }
        }

        public IMovimiento Movimientos
        {
            get{
                if(_movimientos == null)
                {
                    _movimientos = new MovimientoRepository(context);
                }
            return _movimientos;
            }
        }

        public IPropietario Propietarios
        {
            get{
                if(_propietarios == null){
                    _propietarios = new PropietarioRepository(context);
                }
            return _propietarios;
            }
        }

        public IRaza Razas
        {
            get{
                if(_razas == null){
                    _razas = new RazaRepository(context);
                }
            return _razas;
            }
        }
        public ITipoMovimiento TipoMovimientos
        {
            get{
                if(_tipoMovimientos == null)
                {
                    _tipoMovimientos = new TipoMovimientoRepository(context);
                }
            return _tipoMovimientos;
            }
        }

        public ITratamientoMedico TratamientoMedicos
        {
            get{
                if(_tratamientoMedicos == null)
                {
                    _tratamientoMedicos = new TratamientoMedicoRepository(context);
                }
            return _tratamientoMedicos;
            }
        }

        public IVeterinario Veterinarios
        {
            get{
                if(_veterinarios == null)
                {
                    _veterinarios = new VeterinarioRepository(context);
                }
            return _veterinarios;
            }
        }


        public void Dispose()
        {
         context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}