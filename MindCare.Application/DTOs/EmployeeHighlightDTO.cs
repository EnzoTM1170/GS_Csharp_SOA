namespace MindCare.Application.DTOs;

public class EmployeeHighlightDTO
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public double StressLevel { get; set; }
    public double SleepQuality { get; set; }
    public string AlertSeverity { get; set; } = string.Empty;
    public string AlertMessage { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; }
}


