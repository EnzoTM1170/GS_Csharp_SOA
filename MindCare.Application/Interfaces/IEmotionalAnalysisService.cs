using MindCare.Application.DTOs;

namespace MindCare.Application.Interfaces;

public interface IEmotionalAnalysisService
{
    Task<EmotionalAnalysisDTO?> GetByIdAsync(int id);
    Task<IEnumerable<EmotionalAnalysisDTO>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<EmotionalAnalysisDTO>> GetAllAsync();
    Task<EmotionalAnalysisDTO> CreateAsync(CreateEmotionalAnalysisDTO dto);
    Task<bool> DeleteAsync(int id);
}

