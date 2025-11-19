using AutoMapper;
using MindCare.Application.DTOs;
using MindCare.Application.Interfaces;
using MindCare.Domain.Entities;
using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;
using MindCare.Infrastructure.Data;

namespace MindCare.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDTO?> GetByIdAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee == null ? null : _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        var employees = _context.Employees.Where(e => e.IsActive).ToList();
        return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO dto)
    {
        var contactInfo = new ContactInfo(dto.Phone, dto.EmergencyContact, dto.EmergencyPhone);
        var employee = new Employee(dto.Name, dto.Email, dto.Department, dto.Position, contactInfo);
        
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task<bool> UpdateAsync(int id, CreateEmployeeDTO dto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        employee.UpdateBasicInfo(dto.Name, dto.Email, dto.Department, dto.Position);
        
        var contactInfo = new ContactInfo(dto.Phone, dto.EmergencyContact, dto.EmergencyPhone);
        employee.UpdateContactInfo(contactInfo);
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        employee.Deactivate();
        await _context.SaveChangesAsync();
        return true;
    }
}

