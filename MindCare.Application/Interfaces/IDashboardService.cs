using MindCare.Application.DTOs;

namespace MindCare.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDTO> GetSummaryAsync();
}

