using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using MindCare.Domain.Enums;
using MindCare.Infrastructure.Data;

namespace MindCare.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;
    private readonly IStressAlertService _alertService;

    public DashboardService(ApplicationDbContext context, IStressAlertService alertService)
    {
        _context = context;
        _alertService = alertService;
    }

    public async Task<DashboardSummaryDTO> GetSummaryAsync()
    {
        var totalEmployees = _context.Employees.Count(e => e.IsActive);
        var activeEmployees = _context.Employees.Count(e => e.IsActive && e.Status == EmployeeStatus.Active);
        
        // Calcular funcionários de alto risco (com alertas críticos ou altos não reconhecidos)
        var highRiskAlerts = (await _alertService.GetActiveAlertsAsync()).ToList();
        var highRiskEmployeeIds = highRiskAlerts
            .Where(a => a.Severity == "Critical" || a.Severity == "High")
            .Select(a => a.EmployeeId)
            .Distinct()
            .Count();

        var activeAlerts = highRiskAlerts.Count();

        // Calcular média de estresse (últimas métricas)
        var recentStressMetrics = _context.HealthMetrics
            .Where(m => m.Type == MetricTypeEnum.StressLevel && m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .Take(100)
            .ToList();

        var averageStressLevel = recentStressMetrics.Any() 
            ? recentStressMetrics.Average(m => m.Value) 
            : 0.0;

        // Calcular média de qualidade do sono
        var recentSleepMetrics = _context.HealthMetrics
            .Where(m => m.Type == MetricTypeEnum.SleepQuality && m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .Take(100)
            .ToList();

        var averageSleepQuality = recentSleepMetrics.Any()
            ? recentSleepMetrics.Average(m => m.Value)
            : 0.0;

        var recentAlerts = highRiskAlerts.Take(5).ToList();

        var featuredEmployee = BuildFeaturedEmployee(highRiskAlerts);

        return new DashboardSummaryDTO
        {
            TotalEmployees = totalEmployees,
            ActiveEmployees = activeEmployees,
            HighRiskEmployees = highRiskEmployeeIds,
            ActiveAlerts = activeAlerts,
            AverageStressLevel = Math.Round(averageStressLevel, 2),
            AverageSleepQuality = Math.Round(averageSleepQuality, 2),
            RecentAlerts = recentAlerts,
            FeaturedEmployee = featuredEmployee
        };
    }

    private EmployeeHighlightDTO? BuildFeaturedEmployee(List<StressAlertDTO> alerts)
    {
        if (alerts == null || alerts.Count == 0)
            return null;

        var severityScore = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Critical", 3 },
            { "High", 2 },
            { "Medium", 1 },
            { "Low", 0 }
        };

        var prioritizedAlert = alerts
            .OrderByDescending(a => severityScore.TryGetValue(a.Severity, out var score) ? score : 0)
            .ThenByDescending(a => a.TriggeredAt)
            .FirstOrDefault();

        if (prioritizedAlert == null)
            return null;

        var employee = _context.Employees.FirstOrDefault(e => e.Id == prioritizedAlert.EmployeeId);

        var recentStress = _context.HealthMetrics
            .Where(m => m.EmployeeId == prioritizedAlert.EmployeeId && m.Type == MetricTypeEnum.StressLevel && m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .FirstOrDefault();

        var recentSleep = _context.HealthMetrics
            .Where(m => m.EmployeeId == prioritizedAlert.EmployeeId && m.Type == MetricTypeEnum.SleepQuality && m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .FirstOrDefault();

        return new EmployeeHighlightDTO
        {
            EmployeeId = prioritizedAlert.EmployeeId,
            Name = employee?.Name ?? prioritizedAlert.EmployeeName,
            Department = employee?.Department ?? string.Empty,
            Position = employee?.Position ?? string.Empty,
            AlertSeverity = prioritizedAlert.Severity,
            AlertMessage = prioritizedAlert.Message,
            StressLevel = recentStress?.Value ?? 0,
            SleepQuality = recentSleep?.Value ?? 0,
            LastUpdatedAt = recentStress?.RecordedAt ?? prioritizedAlert.TriggeredAt
        };
    }
}

