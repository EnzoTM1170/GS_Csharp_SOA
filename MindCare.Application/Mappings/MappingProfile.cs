using AutoMapper;
using MindCare.Application.DTOs;
using MindCare.Domain.Entities;
using MindCare.Domain.Enums;

namespace MindCare.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Employee mappings
        CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.ContactInfo.Phone));

        // HealthMetric mappings
        CreateMap<HealthMetric, HealthMetricDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));

        // EmotionalAnalysis mappings
        CreateMap<EmotionalAnalysis, EmotionalAnalysisDTO>()
            .ForMember(dest => dest.SentimentScore, opt => opt.MapFrom(src => src.Sentiment.Score))
            .ForMember(dest => dest.Confidence, opt => opt.MapFrom(src => src.Sentiment.Confidence))
            .ForMember(dest => dest.DominantEmotion, opt => opt.MapFrom(src => src.Sentiment.DominantEmotion))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()))
            .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel.ToString()))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));

        // StressAlert mappings
        CreateMap<StressAlert, StressAlertDTO>()
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity.ToString()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));
    }
}

