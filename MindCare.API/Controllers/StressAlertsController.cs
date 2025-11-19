using Microsoft.AspNetCore.Mvc;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;

namespace MindCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StressAlertsController : ControllerBase
{
    private readonly IStressAlertService _stressAlertService;
    private readonly ILogger<StressAlertsController> _logger;

    public StressAlertsController(IStressAlertService stressAlertService, ILogger<StressAlertsController> logger)
    {
        _stressAlertService = stressAlertService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StressAlertDTO>>> GetAll()
    {
        try
        {
            var alerts = await _stressAlertService.GetActiveAlertsAsync();
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar alertas de estresse");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StressAlertDTO>> GetById(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var alert = await _stressAlertService.GetByIdAsync(id);
            if (alert == null)
                return NotFound($"Alerta com ID {id} não encontrado");

            return Ok(alert);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar alerta {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<StressAlertDTO>>> GetByEmployeeId(int employeeId)
    {
        try
        {
            if (employeeId <= 0)
                return BadRequest("ID do funcionário inválido");

            var alerts = await _stressAlertService.GetByEmployeeIdAsync(employeeId);
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar alertas do funcionário {EmployeeId}", employeeId);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPost("{id}/acknowledge")]
    public async Task<IActionResult> Acknowledge(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var result = await _stressAlertService.AcknowledgeAlertAsync(id);
            if (!result)
                return NotFound($"Alerta com ID {id} não encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao reconhecer alerta {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var result = await _stressAlertService.DeleteAsync(id);
            if (!result)
                return NotFound($"Alerta com ID {id} não encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar alerta {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}

