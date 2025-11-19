using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;

namespace MindCare.Domain.Entities;

public class EmotionalAnalysis : BaseEntity
{
    public int EmployeeId { get; private set; }
    public DateTime AnalyzedAt { get; private set; }
    public SentimentScore Sentiment { get; private set; }
    public string TextContent { get; private set; }
    public AnalysisSource Source { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    
    // Navegação
    public virtual Employee Employee { get; private set; }

    protected EmotionalAnalysis() { }

    public EmotionalAnalysis(int employeeId, DateTime analyzedAt, SentimentScore sentiment, 
        string textContent, AnalysisSource source)
    {
        EmployeeId = employeeId;
        AnalyzedAt = analyzedAt;
        Sentiment = sentiment;
        TextContent = textContent;
        Source = source;
        RiskLevel = CalculateRiskLevel();
    }

    private RiskLevel CalculateRiskLevel()
    {
        if (Sentiment.Score <= 0.2)
            return RiskLevel.High;
        if (Sentiment.Score <= 0.4)
            return RiskLevel.Medium;
        return RiskLevel.Low;
    }
}

