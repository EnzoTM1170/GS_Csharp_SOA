using AutoMapper;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using MindCare.Domain.Entities;
using MindCare.Domain.Enums;
using MindCare.Infrastructure.Data;

namespace MindCare.Application.Services;

public class HealthMetricService : IHealthMetricService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public HealthMetricService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HealthMetricDTO?> GetByIdAsync(int id)
    {
        var metric = await _context.HealthMetrics.FindAsync(id);
        if (metric == null) return null;

        var dto = _mapper.Map<HealthMetricDTO>(metric);
        dto.IsAbnormal = metric.IsAbnormal();
        return dto;
    }

    public async Task<IEnumerable<HealthMetricDTO>> GetByEmployeeIdAsync(int employeeId)
    {
        var metrics = _context.HealthMetrics
            .Where(m => m.EmployeeId == employeeId && m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .ToList();

        return metrics.Select(m =>
        {
            var dto = _mapper.Map<HealthMetricDTO>(m);
            dto.IsAbnormal = m.IsAbnormal();
            return dto;
        });
    }

    public async Task<IEnumerable<HealthMetricDTO>> GetAllAsync()
    {
        var metrics = _context.HealthMetrics
            .Where(m => m.IsActive)
            .OrderByDescending(m => m.RecordedAt)
            .ToList();

        return metrics.Select(m =>
        {
            var dto = _mapper.Map<HealthMetricDTO>(m);
            dto.IsAbnormal = m.IsAbnormal();
            return dto;
        });
    }

    public async Task<HealthMetricDTO> CreateAsync(CreateHealthMetricDTO dto)
    {
        var metric = new HealthMetric(
            dto.EmployeeId,
            dto.RecordedAt,
            (MetricTypeEnum)dto.Type,
            dto.Value,
            dto.Unit,
            (MetricSource)dto.Source
        );

        _context.HealthMetrics.Add(metric);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<HealthMetricDTO>(metric);
        result.IsAbnormal = metric.IsAbnormal();
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var metric = await _context.HealthMetrics.FindAsync(id);
        if (metric == null) return false;

        metric.Deactivate();
        await _context.SaveChangesAsync();
        return true;
    }
}

