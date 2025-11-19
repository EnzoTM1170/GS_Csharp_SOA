using MindCare.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace MindCare.Application.Services;

public class ExternalAPIService : IExternalAPIService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalAPIService> _logger;

    public ExternalAPIService(HttpClient httpClient, ILogger<ExternalAPIService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<SentimentAnalysisResult> AnalyzeSentimentAsync(string text)
    {
        try
        {
            // Simulação de API externa de análise de sentimento
            // Em produção, isso chamaria uma API real como Azure Text Analytics, AWS Comprehend, etc.
            
            // Para demonstração, vamos usar uma API pública gratuita ou simular
            // Exemplo: usando uma API mock ou real
            
            // Simulação baseada em análise simples de palavras-chave
            var negativeWords = new[] { "estresse", "cansado", "sobrecarregado", "ansioso", "preocupado", "frustrado" };
            var positiveWords = new[] { "bem", "ótimo", "feliz", "satisfeito", "motivado", "energizado" };
            
            var lowerText = text.ToLower();
            var negativeCount = negativeWords.Count(w => lowerText.Contains(w));
            var positiveCount = positiveWords.Count(w => lowerText.Contains(w));
            
            double score = 0.5; // neutro por padrão
            string emotion = "Neutro";
            
            if (positiveCount > negativeCount)
            {
                score = 0.5 + (positiveCount * 0.1);
                emotion = "Positivo";
            }
            else if (negativeCount > positiveCount)
            {
                score = 0.5 - (negativeCount * 0.1);
                emotion = "Negativo";
            }
            
            score = Math.Max(0.0, Math.Min(1.0, score));
            var confidence = Math.Min(0.9, 0.5 + (Math.Abs(positiveCount - negativeCount) * 0.1));

            return new SentimentAnalysisResult
            {
                Score = score,
                Confidence = confidence,
                DominantEmotion = emotion
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao analisar sentimento");
            // Retornar valor padrão em caso de erro
            return new SentimentAnalysisResult
            {
                Score = 0.5,
                Confidence = 0.5,
                DominantEmotion = "Neutro"
            };
        }
    }

    public async Task<HealthDataResult> GetHealthDataFromWearableAsync(string deviceId)
    {
        try
        {
            // Simulação de API externa de wearable
            // Em produção, isso integraria com APIs de Fitbit, Apple Health, Garmin, etc.
            
            var random = new Random();
            
            return new HealthDataResult
            {
                HeartRate = 60 + random.Next(0, 40), // 60-100 bpm
                SleepQuality = 7 + random.NextDouble() * 2, // 7-9 horas
                BodyTemperature = 36.1 + random.NextDouble() * 1.1, // 36.1-37.2°C
                StressLevel = random.NextDouble() * 5, // 0-5
                RecordedAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter dados do wearable");
            throw;
        }
    }
}

