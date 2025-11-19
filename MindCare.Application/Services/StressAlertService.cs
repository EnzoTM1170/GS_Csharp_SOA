using AutoMapper;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using MindCare.Domain.Entities;
using MindCare.Domain.Enums;
using MindCare.Infrastructure.Data;

namespace MindCare.Application.Services;

public class StressAlertService : IStressAlertService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public StressAlertService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<StressAlertDTO?> GetByIdAsync(int id)
    {
        var alert = await _context.StressAlerts.FindAsync(id);
        return alert == null ? null : _mapper.Map<StressAlertDTO>(alert);
    }

    public async Task<IEnumerable<StressAlertDTO>> GetByEmployeeIdAsync(int employeeId)
    {
        var alerts = _context.StressAlerts
            .Where(a => a.EmployeeId == employeeId && a.IsActive)
            .OrderByDescending(a => a.TriggeredAt)
            .ToList();

        return _mapper.Map<IEnumerable<StressAlertDTO>>(alerts);
    }

    public async Task<IEnumerable<StressAlertDTO>> GetActiveAlertsAsync()
    {
        var alerts = _context.StressAlerts
            .Where(a => !a.IsAcknowledged && a.IsActive)
            .OrderByDescending(a => a.TriggeredAt)
            .ToList();

        return _mapper.Map<IEnumerable<StressAlertDTO>>(alerts);
    }

    public async Task<bool> AcknowledgeAlertAsync(int id)
    {
        var alert = await _context.StressAlerts.FindAsync(id);
        if (alert == null) return false;

        alert.Acknowledge();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var alert = await _context.StressAlerts.FindAsync(id);
        if (alert == null) return false;

        alert.Deactivate();
        await _context.SaveChangesAsync();
        return true;
    }
}

