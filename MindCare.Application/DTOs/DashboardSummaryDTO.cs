namespace MindCare.Application.DTOs;

public class DashboardSummaryDTO
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int HighRiskEmployees { get; set; }
    public int ActiveAlerts { get; set; }
    public double AverageStressLevel { get; set; }
    public double AverageSleepQuality { get; set; }
    public List<StressAlertDTO> RecentAlerts { get; set; } = new();
    public EmployeeHighlightDTO? FeaturedEmployee { get; set; }
}

