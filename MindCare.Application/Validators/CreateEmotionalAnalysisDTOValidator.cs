using FluentValidation;
using MindCare.Application.DTOs;
using MindCare.Domain.Enums;

namespace MindCare.Application.Validators;

public class CreateEmotionalAnalysisDTOValidator : AbstractValidator<CreateEmotionalAnalysisDTO>
{
    public CreateEmotionalAnalysisDTOValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("ID do funcionário deve ser maior que zero");

        RuleFor(x => x.SentimentScore)
            .InclusiveBetween(0.0, 1.0).WithMessage("Score de sentimento deve estar entre 0 e 1");

        RuleFor(x => x.Confidence)
            .InclusiveBetween(0.0, 1.0).WithMessage("Confiança deve estar entre 0 e 1");

        RuleFor(x => x.DominantEmotion)
            .NotEmpty().WithMessage("Emoção dominante é obrigatória")
            .MaximumLength(50).WithMessage("Emoção dominante deve ter no máximo 50 caracteres");

        RuleFor(x => x.TextContent)
            .NotEmpty().WithMessage("Conteúdo do texto é obrigatório")
            .MaximumLength(5000).WithMessage("Conteúdo do texto deve ter no máximo 5000 caracteres");

        RuleFor(x => x.Source)
            .IsInEnum().WithMessage("Fonte da análise inválida");

        RuleFor(x => x.AnalyzedAt)
            .NotEmpty().WithMessage("Data de análise é obrigatória")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data não pode ser no futuro");
    }
}

