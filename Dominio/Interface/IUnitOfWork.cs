using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IUnitOfWork
    {
        ICita Citas {get;}
        IEspecie Especies {get;}
        ILaboratorio Laboratorios {get;}
        IMascota Mascotas{get;}
        IMedicamento Medicamentos{get;}
        IMovimiento Movimientos {get;}
        IPropietario Propietarios {get;}
        IRaza Razas {get;}
        ITipoMovimiento TipoMovimientos {get;}
        ITratamientoMedico TratamientoMedicos {get;}
        IVeterinario Veterinarios {get;}

        Task SaveAsync();
    }
}