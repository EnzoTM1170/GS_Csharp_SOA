using MindCare.Application.DTOs;

namespace MindCare.Application.Interfaces;

public interface IStressAlertService
{
    Task<StressAlertDTO?> GetByIdAsync(int id);
    Task<IEnumerable<StressAlertDTO>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<StressAlertDTO>> GetActiveAlertsAsync();
    Task<bool> AcknowledgeAlertAsync(int id);
    Task<bool> DeleteAsync(int id);
}

