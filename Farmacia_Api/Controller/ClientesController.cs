using Farmacia_Api.Dtos.ClientesDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Text;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService clienteService;

        public ClientesController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet("clientes")]

        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                IEnumerable<ReadClienteDto> lst = await clienteService.RecuperarClientesAsync();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error en el servidor: {ex.Message}"
                });
            }
        }

        [HttpGet("clientes/{id}")]

        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            try
            {
                ReadClienteDto? cliente = await clienteService.RecuperarClientePorIdAsync(id);

                if (cliente == null)
                {
                    return NotFound(new
                    {
                        mensaje = "No se encontro el cliente"
                    });
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error en el servidor: {ex.Message}"
                });
            }
        }
    }
}
