using Microsoft.AspNetCore.Mvc;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using FluentValidation;

namespace MindCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
    {
        try
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar funcionários");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetById(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound($"Funcionário com ID {id} não encontrado");

            return Ok(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar funcionário {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await _employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validação falhou ao criar funcionário");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar funcionário");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDTO dto)
    {
        try
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateAsync(id, dto);
            if (!result)
                return NotFound($"Funcionário com ID {id} não encontrado");

            return NoContent();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validação falhou ao atualizar funcionário {Id}", id);
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar funcionário {Id}", id);
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

            var result = await _employeeService.DeleteAsync(id);
            if (!result)
                return NotFound($"Funcionário com ID {id} não encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar funcionário {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}

