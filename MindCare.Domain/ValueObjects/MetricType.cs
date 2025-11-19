using MindCare.Domain.Enums;

namespace MindCare.Domain.ValueObjects;

// Value Object para encapsular lógica de métricas
public class MetricTypeValueObject
{
    public MetricTypeEnum Type { get; private set; }
    public string Name { get; private set; }
    public double? MinNormal { get; private set; }
    public double? MaxNormal { get; private set; }

    protected MetricTypeValueObject() { }

    public MetricTypeValueObject(MetricTypeEnum type, string name, double? minNormal = null, double? maxNormal = null)
    {
        Type = type;
        Name = name;
        MinNormal = minNormal;
        MaxNormal = maxNormal;
    }

    public bool IsAbnormal(double value)
    {
        if (MinNormal.HasValue && value < MinNormal.Value)
            return true;
        if (MaxNormal.HasValue && value > MaxNormal.Value)
            return true;
        return false;
    }

    public static MetricTypeValueObject HeartRate => new(MetricTypeEnum.HeartRate, "Frequência Cardíaca", 60, 100);
    public static MetricTypeValueObject SleepQuality => new(MetricTypeEnum.SleepQuality, "Qualidade do Sono", 7, 10);
    public static MetricTypeValueObject BodyTemperature => new(MetricTypeEnum.BodyTemperature, "Temperatura Corporal", 36.1, 37.2);
    public static MetricTypeValueObject StressLevel => new(MetricTypeEnum.StressLevel, "Nível de Estresse", 0, 5);
    public static MetricTypeValueObject ActivityLevel => new(MetricTypeEnum.ActivityLevel, "Nível de Atividade", 0, 10);
}

