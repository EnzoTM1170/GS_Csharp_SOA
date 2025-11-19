using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;

namespace MindCare.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Department { get; private set; }
    public string Position { get; private set; }
    public EmployeeStatus Status { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    
    // Navegação
    public virtual ICollection<HealthMetric> HealthMetrics { get; private set; }
    public virtual ICollection<EmotionalAnalysis> EmotionalAnalyses { get; private set; }
    public virtual ICollection<StressAlert> StressAlerts { get; private set; }

    protected Employee() 
    {
        HealthMetrics = new List<HealthMetric>();
        EmotionalAnalyses = new List<EmotionalAnalysis>();
        StressAlerts = new List<StressAlert>();
    }

    public Employee(string name, string email, string department, string position, ContactInfo contactInfo)
        : this()
    {
        Name = name;
        Email = email;
        Department = department;
        Position = position;
        ContactInfo = contactInfo;
        Status = EmployeeStatus.Active;
    }

    public void UpdateContactInfo(ContactInfo newContactInfo)
    {
        ContactInfo = newContactInfo;
        UpdateTimestamp();
    }

    public void ChangeStatus(EmployeeStatus newStatus)
    {
        Status = newStatus;
        UpdateTimestamp();
    }

    public void UpdateBasicInfo(string name, string email, string department, string position)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
        if (!string.IsNullOrWhiteSpace(department))
            Department = department;
        if (!string.IsNullOrWhiteSpace(position))
            Position = position;
        UpdateTimestamp();
    }
}

