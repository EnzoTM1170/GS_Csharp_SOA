namespace MindCare.Domain.ValueObjects;

public class SentimentScore
{
    public double Score { get; private set; } // 0.0 (negativo) a 1.0 (positivo)
    public double Confidence { get; private set; } // 0.0 a 1.0
    public string DominantEmotion { get; private set; }

    protected SentimentScore() { }

    public SentimentScore(double score, double confidence, string dominantEmotion)
    {
        if (score < 0 || score > 1)
            throw new ArgumentException("Score deve estar entre 0 e 1", nameof(score));
        if (confidence < 0 || confidence > 1)
            throw new ArgumentException("Confiança deve estar entre 0 e 1", nameof(confidence));

        Score = score;
        Confidence = confidence;
        DominantEmotion = dominantEmotion ?? "Neutro";
    }

    public bool IsPositive => Score > 0.6;
    public bool IsNegative => Score < 0.4;
    public bool IsNeutral => Score >= 0.4 && Score <= 0.6;
}

