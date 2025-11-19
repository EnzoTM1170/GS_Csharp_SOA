using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;

namespace MindCare.Domain.Entities;

public class StressAlert : BaseEntity
{
    public int EmployeeId { get; private set; }
    public DateTime TriggeredAt { get; private set; }
    public AlertSeverity Severity { get; private set; }
    public string Message { get; private set; }
    public AlertType Type { get; private set; }
    public bool IsAcknowledged { get; private set; }
    public DateTime? AcknowledgedAt { get; private set; }
    
    // Navegação
    public virtual Employee Employee { get; private set; }

    protected StressAlert() { }

    public StressAlert(int employeeId, DateTime triggeredAt, AlertSeverity severity, 
        string message, AlertType type)
    {
        EmployeeId = employeeId;
        TriggeredAt = triggeredAt;
        Severity = severity;
        Message = message;
        Type = type;
        IsAcknowledged = false;
    }

    public void Acknowledge()
    {
        IsAcknowledged = true;
        AcknowledgedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }
}

