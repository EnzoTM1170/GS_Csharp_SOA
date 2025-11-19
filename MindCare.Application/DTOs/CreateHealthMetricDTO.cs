namespace MindCare.Application.DTOs;

public class CreateHealthMetricDTO
{
    public int EmployeeId { get; set; }
    public DateTime RecordedAt { get; set; }
    public int Type { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int Source { get; set; }
}

