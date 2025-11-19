namespace MindCare.Application.DTOs;

public class EmotionalAnalysisDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime AnalyzedAt { get; set; }
    public double SentimentScore { get; set; }
    public double Confidence { get; set; }
    public string DominantEmotion { get; set; } = string.Empty;
    public string TextContent { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
}

