using Microsoft.EntityFrameworkCore;
using MindCare.Domain.Entities;

namespace MindCare.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<HealthMetric> HealthMetrics { get; set; }
    public DbSet<EmotionalAnalysis> EmotionalAnalyses { get; set; }
    public DbSet<StressAlert> StressAlerts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Employee configuration
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
            
            // Value Object - ContactInfo
            entity.OwnsOne(e => e.ContactInfo, ci =>
            {
                ci.Property(c => c.Phone).IsRequired().HasMaxLength(20);
                ci.Property(c => c.EmergencyContact).HasMaxLength(100);
                ci.Property(c => c.EmergencyPhone).HasMaxLength(20);
            });

            entity.HasIndex(e => e.Email).IsUnique();
        });

        // HealthMetric configuration
        modelBuilder.Entity<HealthMetric>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Unit).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Value).HasColumnType("REAL");
            
            entity.HasOne(e => e.Employee)
                .WithMany(e => e.HealthMetrics)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // EmotionalAnalysis configuration
        modelBuilder.Entity<EmotionalAnalysis>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TextContent).IsRequired().HasMaxLength(5000);
            
            // Value Object - SentimentScore
            entity.OwnsOne(e => e.Sentiment, s =>
            {
                s.Property(s => s.Score).HasColumnType("REAL");
                s.Property(s => s.Confidence).HasColumnType("REAL");
                s.Property(s => s.DominantEmotion).IsRequired().HasMaxLength(50);
            });
            
            entity.HasOne(e => e.Employee)
                .WithMany(e => e.EmotionalAnalyses)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // StressAlert configuration
        modelBuilder.Entity<StressAlert>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Employee)
                .WithMany(e => e.StressAlerts)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

