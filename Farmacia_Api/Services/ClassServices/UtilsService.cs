using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.UtilsDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class UtilsService : IUtilsService
    {
        private readonly IUtilsRepository _Repository;
        public UtilsService(IUtilsRepository repo)
        {
            _Repository = repo;
        }

        public async Task<ReadBarrioDto> TraerBarrioPorId(int id)
        {
            if (validar(id))
            {
                throw new ArgumentException("El id no puede ser mayor a diez");
            }
            var barrio = await _Repository.GetBarriosByAsync(id);
            var barrioDto = mapeo(barrio);
            return barrioDto;
        }

        public async Task<List<ReadBarrioDto>> TraerBarrios()
        {
            var barrios = await _Repository.GetAllBarriosAsync();
            var barriosDto = mapeo(barrios);
            return barriosDto;
        }

        public async Task<List<ReadObraSocialDto>> TraerObraSocial()
        {
            var OSociales = await _Repository.GetAllObraSocialAsync();
            var OSocialesDto = mapeoOs(OSociales);
            return OSocialesDto;
        }

        public async Task<ReadObraSocialDto> TraerObraSocialPorId(int id)
        {
            if (validar(id))
            {
                throw new ArgumentException("El id no puede ser mayor a diez");
            }

            var OSocial = await _Repository.GetObraSocialByAsync(id);
            var OSocialDto = mapeoOs(OSocial);
            return OSocialDto;
        }
        /// <summary>
        // Metodos de mapeo
        /// </summary>
        private List<ReadBarrioDto> mapeo(List<Barrio> barrios)
        {
            List<ReadBarrioDto> lstDto = new List<ReadBarrioDto>();
            foreach (var b in barrios)
            {
                ReadBarrioDto dto = new ReadBarrioDto()
                {
                    IdBarrio = b.IdBarrio,
                    NombreBarrio = b.NombreBarrio,
                };
                lstDto.Add(dto);
            }
            return lstDto;
        }


        private ReadBarrioDto mapeo(Barrio barrio)
        {
            ReadBarrioDto dto = new ReadBarrioDto()
            {
                IdBarrio = barrio.IdBarrio,
                NombreBarrio = barrio.NombreBarrio,
            };
            return dto;
        }

        private List<ReadObraSocialDto> mapeoOs(List<ObrasSociale> oSociales)
        {
            List<ReadObraSocialDto> lstDto = new List<ReadObraSocialDto>();
            foreach (var o in oSociales)
            {
                ReadObraSocialDto dto = new ReadObraSocialDto()
                {
                    CodObraSocial = o.CodObraSocial,
                    Detalle = o.Detalle,
                };
                lstDto.Add(dto);
            }
            return lstDto;
        }

        private ReadObraSocialDto mapeoOs(ObrasSociale oSociales)
        {
            ReadObraSocialDto dto = new ReadObraSocialDto()
            {
                CodObraSocial = oSociales.CodObraSocial,
                Detalle = oSociales.Detalle,
            };
            return dto;
        }

        private bool validar(int id)
        {
            if(id > 10 ) return true;
            else return false;
        }
    }
}
