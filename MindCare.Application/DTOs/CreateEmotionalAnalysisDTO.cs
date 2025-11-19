namespace MindCare.Application.DTOs;

public class CreateEmotionalAnalysisDTO
{
    public int EmployeeId { get; set; }
    public DateTime AnalyzedAt { get; set; }
    public double SentimentScore { get; set; }
    public double Confidence { get; set; }
    public string DominantEmotion { get; set; } = string.Empty;
    public string TextContent { get; set; } = string.Empty;
    public int Source { get; set; }
}

