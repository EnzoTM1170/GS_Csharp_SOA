using Microsoft.AspNetCore.Mvc;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using FluentValidation;

namespace MindCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmotionalAnalysesController : ControllerBase
{
    private readonly IEmotionalAnalysisService _emotionalAnalysisService;
    private readonly ILogger<EmotionalAnalysesController> _logger;

    public EmotionalAnalysesController(IEmotionalAnalysisService emotionalAnalysisService, ILogger<EmotionalAnalysesController> logger)
    {
        _emotionalAnalysisService = emotionalAnalysisService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmotionalAnalysisDTO>>> GetAll()
    {
        try
        {
            var analyses = await _emotionalAnalysisService.GetAllAsync();
            return Ok(analyses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar análises emocionais");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmotionalAnalysisDTO>> GetById(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var analysis = await _emotionalAnalysisService.GetByIdAsync(id);
            if (analysis == null)
                return NotFound($"Análise com ID {id} não encontrada");

            return Ok(analysis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar análise {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<EmotionalAnalysisDTO>>> GetByEmployeeId(int employeeId)
    {
        try
        {
            if (employeeId <= 0)
                return BadRequest("ID do funcionário inválido");

            var analyses = await _emotionalAnalysisService.GetByEmployeeIdAsync(employeeId);
            return Ok(analyses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar análises do funcionário {EmployeeId}", employeeId);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmotionalAnalysisDTO>> Create([FromBody] CreateEmotionalAnalysisDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var analysis = await _emotionalAnalysisService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = analysis.Id }, analysis);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validação falhou ao criar análise");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar análise");
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

            var result = await _emotionalAnalysisService.DeleteAsync(id);
            if (!result)
                return NotFound($"Análise com ID {id} não encontrada");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar análise {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}

