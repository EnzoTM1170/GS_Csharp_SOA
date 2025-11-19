using AutoMapper;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using MindCare.Domain.Entities;
using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;
using MindCare.Infrastructure.Data;

namespace MindCare.Application.Services;

public class EmotionalAnalysisService : IEmotionalAnalysisService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmotionalAnalysisService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmotionalAnalysisDTO?> GetByIdAsync(int id)
    {
        var analysis = await _context.EmotionalAnalyses.FindAsync(id);
        return analysis == null ? null : _mapper.Map<EmotionalAnalysisDTO>(analysis);
    }

    public async Task<IEnumerable<EmotionalAnalysisDTO>> GetByEmployeeIdAsync(int employeeId)
    {
        var analyses = _context.EmotionalAnalyses
            .Where(a => a.EmployeeId == employeeId && a.IsActive)
            .OrderByDescending(a => a.AnalyzedAt)
            .ToList();

        return _mapper.Map<IEnumerable<EmotionalAnalysisDTO>>(analyses);
    }

    public async Task<IEnumerable<EmotionalAnalysisDTO>> GetAllAsync()
    {
        var analyses = _context.EmotionalAnalyses
            .Where(a => a.IsActive)
            .OrderByDescending(a => a.AnalyzedAt)
            .ToList();

        return _mapper.Map<IEnumerable<EmotionalAnalysisDTO>>(analyses);
    }

    public async Task<EmotionalAnalysisDTO> CreateAsync(CreateEmotionalAnalysisDTO dto)
    {
        var sentiment = new SentimentScore(dto.SentimentScore, dto.Confidence, dto.DominantEmotion);
        var analysis = new EmotionalAnalysis(
            dto.EmployeeId,
            dto.AnalyzedAt,
            sentiment,
            dto.TextContent,
            (AnalysisSource)dto.Source
        );

        _context.EmotionalAnalyses.Add(analysis);
        await _context.SaveChangesAsync();

        return _mapper.Map<EmotionalAnalysisDTO>(analysis);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var analysis = await _context.EmotionalAnalyses.FindAsync(id);
        if (analysis == null) return false;

        analysis.Deactivate();
        await _context.SaveChangesAsync();
        return true;
    }
}

