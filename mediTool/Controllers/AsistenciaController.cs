using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.Asistencias;
using Utils.DTOs.Asistencia;

namespace mediTool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciaController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpGet("turno/{turnoId}")]
        public async Task<IActionResult> GetByTurno(int turnoId)
        {
            var asistencia = await _asistenciaService.ObtenerPorTurno(turnoId);
            if (asistencia == null) return NotFound();
            return Ok(asistencia);
        }

        [HttpGet("facturables")]
        public async Task<IActionResult> GetFacturables([FromQuery] int pacienteId, [FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var asistencias = await _asistenciaService.ObtenerFacturables(pacienteId, desde, hasta);
            return Ok(asistencias);
        }

        [HttpGet("resumen/turnofijo/{turnoFijoId}")]
        public async Task<IActionResult> GetResumenPorTurnoFijo(int turnoFijoId)
        {
            var resumen = await _asistenciaService.ObtenerResumenPorTurnoFijo(turnoFijoId);
            return Ok(resumen);
        }

        [HttpPost("turno/{turnoId}")]
        public async Task<IActionResult> Registrar(int turnoId, [FromQuery] bool asistio, [FromQuery] bool? justificada, [FromQuery] string? observaciones)
        {
            var result = await _asistenciaService.RegistrarAsistencia(turnoId, asistio, justificada, observaciones);
            return Ok(result);
        }

        [HttpPut("turno/{turnoId}")]
        public async Task<IActionResult> Update(int turnoId, [FromBody] ActualizarAsistenciaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _asistenciaService.ActualizarAsistencia(turnoId, dto);
            return NoContent();
        }
    }
}
