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

        [HttpPost("clientes")]

        public async Task<IActionResult> CrearCliente([FromBody] CreateClienteDto clienteDto)
        {
            try
            {
                bool exito = await clienteService.CrearClienteAsync(clienteDto);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        message = "No se pudo guardar el cliente"
                    });
                }

                return Ok(new
                {
                    message = "Se pudo guardar el cliente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error en el servidor: {ex.Message}"
                });
            }
        }

        [HttpPut("clientes/{clienteId}")]

        public async Task<IActionResult> ActualizarCliente([FromBody] UpdateClienteDto clienteDto, int clienteId)
        {
            try
            {
                bool exito = await clienteService.ActualizarClienteAsync(clienteDto, clienteId);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        message = "No se pudo guardar el cliente"
                    });
                }

                return Ok(new
                {
                    message = "Se pudo guardar el cliente"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error en el servidor: {ex.Message}"
                });
            }
        }

        [HttpDelete("clientes/{clienteId}")]

        public async Task<IActionResult> BorrarCliente(int clienteId)
        {
            try
            {
                bool exito = await clienteService.BorrarClienteAsync(clienteId);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        message = "No se pudo borrar el cliente"
                    });
                }

                return Ok(new
                {
                    message = "Se pudo borrar el cliente"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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
