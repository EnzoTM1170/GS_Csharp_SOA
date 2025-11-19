using MindCare.Application.DTOs;

namespace MindCare.Application.Interfaces;

public interface IHealthMetricService
{
    Task<HealthMetricDTO?> GetByIdAsync(int id);
    Task<IEnumerable<HealthMetricDTO>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<HealthMetricDTO>> GetAllAsync();
    Task<HealthMetricDTO> CreateAsync(CreateHealthMetricDTO dto);
    Task<bool> DeleteAsync(int id);
}

