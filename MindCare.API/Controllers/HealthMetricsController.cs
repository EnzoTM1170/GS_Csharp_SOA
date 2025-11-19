using Microsoft.AspNetCore.Mvc;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using FluentValidation;

namespace MindCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthMetricsController : ControllerBase
{
    private readonly IHealthMetricService _healthMetricService;
    private readonly ILogger<HealthMetricsController> _logger;

    public HealthMetricsController(IHealthMetricService healthMetricService, ILogger<HealthMetricsController> logger)
    {
        _healthMetricService = healthMetricService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthMetricDTO>>> GetAll()
    {
        try
        {
            var metrics = await _healthMetricService.GetAllAsync();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar métricas de saúde");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HealthMetricDTO>> GetById(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var metric = await _healthMetricService.GetByIdAsync(id);
            if (metric == null)
                return NotFound($"Métrica com ID {id} não encontrada");

            return Ok(metric);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar métrica {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<HealthMetricDTO>>> GetByEmployeeId(int employeeId)
    {
        try
        {
            if (employeeId <= 0)
                return BadRequest("ID do funcionário inválido");

            var metrics = await _healthMetricService.GetByEmployeeIdAsync(employeeId);
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar métricas do funcionário {EmployeeId}", employeeId);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<HealthMetricDTO>> Create([FromBody] CreateHealthMetricDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var metric = await _healthMetricService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = metric.Id }, metric);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validação falhou ao criar métrica");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar métrica");
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

            var result = await _healthMetricService.DeleteAsync(id);
            if (!result)
                return NotFound($"Métrica com ID {id} não encontrada");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar métrica {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}

