namespace MindCare.Application.DTOs;

public class HealthMetricDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime RecordedAt { get; set; }
    public string Type { get; set; } = string.Empty;
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public bool IsAbnormal { get; set; }
}

