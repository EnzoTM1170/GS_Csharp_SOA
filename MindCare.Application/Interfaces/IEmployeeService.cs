using MindCare.Application.DTOs;

namespace MindCare.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDTO?> GetByIdAsync(int id);
    Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO dto);
    Task<bool> UpdateAsync(int id, CreateEmployeeDTO dto);
    Task<bool> DeleteAsync(int id);
}

