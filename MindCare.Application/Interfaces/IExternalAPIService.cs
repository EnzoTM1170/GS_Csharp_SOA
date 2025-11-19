namespace MindCare.Application.Interfaces;

public interface IExternalAPIService
{
    Task<SentimentAnalysisResult> AnalyzeSentimentAsync(string text);
    Task<HealthDataResult> GetHealthDataFromWearableAsync(string deviceId);
}

public class SentimentAnalysisResult
{
    public double Score { get; set; }
    public double Confidence { get; set; }
    public string DominantEmotion { get; set; } = string.Empty;
}

public class HealthDataResult
{
    public double HeartRate { get; set; }
    public double SleepQuality { get; set; }
    public double BodyTemperature { get; set; }
    public double StressLevel { get; set; }
    public DateTime RecordedAt { get; set; }
}

