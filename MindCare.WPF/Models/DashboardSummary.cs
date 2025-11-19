using System.Collections.Generic;

namespace MindCare.WPF.Models;

public class DashboardSummary
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int HighRiskEmployees { get; set; }
    public int ActiveAlerts { get; set; }
    public double AverageStressLevel { get; set; }
    public double AverageSleepQuality { get; set; }
    public List<StressAlertModel> RecentAlerts { get; set; } = new();
    public EmployeeHighlightModel? FeaturedEmployee { get; set; }
}

