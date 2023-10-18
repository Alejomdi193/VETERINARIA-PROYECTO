using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;

namespace Api.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Cita,CitaDto>().ReverseMap();
            CreateMap<Especie,EspecieDto>().ReverseMap();
            CreateMap<Laboratorio,LaboratorioDto>().ReverseMap();
            CreateMap<Mascota,MascotaDto>().ReverseMap();
            CreateMap<Medicamento,MedicamentoDto>().ReverseMap();
            CreateMap<Movimiento,MovimientoDto>().ReverseMap();
            CreateMap<Propietario,PropietarioDto>().ReverseMap();
            CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            CreateMap<Raza,RazaDto>().ReverseMap();
            CreateMap<TipoMovimiento,TipoMovimientoDto>().ReverseMap();
            CreateMap<TratamientoMedico,TratamientoMedico>().ReverseMap();
            CreateMap<Veterinario,VeterinarioDto>().ReverseMap();
        }
    }
}