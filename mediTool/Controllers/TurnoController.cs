using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.Turnos;
using Utils.DTOs.Turno;

namespace mediTool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;
        private readonly IGenerarInstanciasTurnoService _generarInstanciasService;

        public TurnoController(ITurnoService turnoService, IGenerarInstanciasTurnoService generarInstanciasService)
        {
            _turnoService = turnoService;
            _generarInstanciasService = generarInstanciasService;
        }

        [HttpGet("agenda")]
        public async Task<IActionResult> GetAgenda([FromQuery] DateTime desde, [FromQuery] DateTime hasta, [FromQuery] int? profesionalId)
        {
            var turnos = await _turnoService.ObtenerAgenda(desde, hasta, profesionalId);
            return Ok(turnos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var turno = await _turnoService.ObtenerPorId(id);
            return Ok(turno);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuelto([FromBody] CrearTurnoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _turnoService.CrearSuelto(dto);
            return Ok(result);
        }

        [HttpPatch("{id}/reprogramar")]
        public async Task<IActionResult> Reprogramar(int id, [FromQuery] DateTime nuevaFechaHora)
        {
            await _turnoService.Reprogramar(id, nuevaFechaHora);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromQuery] EstadoTurno nuevoEstado)
        {
            await _turnoService.CambiarEstado(id, nuevoEstado);
            return NoContent();
        }

        [HttpPost("generar-masivo")]
        public async Task<IActionResult> GenerarMasivo([FromQuery] DateTime hastaFecha)
        {
            await _generarInstanciasService.GenerarParaTodasLasReglasActivas(hastaFecha);
            return NoContent();
        }
    }
}
