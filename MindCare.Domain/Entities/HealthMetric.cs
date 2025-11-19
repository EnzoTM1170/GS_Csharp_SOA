using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;

namespace MindCare.Domain.Entities;

public class HealthMetric : BaseEntity
{
    public int EmployeeId { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public MetricTypeEnum Type { get; private set; }
    public double Value { get; private set; }
    public string Unit { get; private set; }
    public MetricSource Source { get; private set; }
    
    // Navegação
    public virtual Employee Employee { get; private set; }

    protected HealthMetric() { }

    public HealthMetric(int employeeId, DateTime recordedAt, MetricTypeEnum type, double value, string unit, MetricSource source)
    {
        EmployeeId = employeeId;
        RecordedAt = recordedAt;
        Type = type;
        Value = value;
        Unit = unit;
        Source = source;
    }

    public bool IsAbnormal()
    {
        var metricType = GetMetricTypeValueObject();
        return metricType?.IsAbnormal(Value) ?? false;
    }

    private MetricTypeValueObject? GetMetricTypeValueObject()
    {
        return Type switch
        {
            MetricTypeEnum.HeartRate => MetricTypeValueObject.HeartRate,
            MetricTypeEnum.SleepQuality => MetricTypeValueObject.SleepQuality,
            MetricTypeEnum.BodyTemperature => MetricTypeValueObject.BodyTemperature,
            MetricTypeEnum.StressLevel => MetricTypeValueObject.StressLevel,
            MetricTypeEnum.ActivityLevel => MetricTypeValueObject.ActivityLevel,
            _ => null
        };
    }
}

