using MindCare.Domain.Entities;
using MindCare.Domain.Enums;
using MindCare.Domain.ValueObjects;

namespace MindCare.Infrastructure.Data;

public static class DbSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        // Verificar se já existem dados suficientes (pelo menos 5 funcionários)
        var employeeCount = context.Employees.Count();
        if (employeeCount >= 5)
        {
            Console.WriteLine($"Banco de dados já possui {employeeCount} funcionários. Dados já populados.");
            return; // Já tem dados suficientes, não precisa popular novamente
        }

        if (employeeCount > 0)
        {
            Console.WriteLine($"Banco possui apenas {employeeCount} funcionários. Limpando e repopulando...");
            // Limpar dados existentes para repopular
            context.StressAlerts.RemoveRange(context.StressAlerts);
            context.EmotionalAnalyses.RemoveRange(context.EmotionalAnalyses);
            context.HealthMetrics.RemoveRange(context.HealthMetrics);
            context.Employees.RemoveRange(context.Employees);
            context.SaveChanges();
        }

        Console.WriteLine("Iniciando população do banco de dados com dados de exemplo...");

        var random = new Random();
        var employees = new List<Employee>();

        // Criar funcionários com diferentes níveis de estresse
        var employeeData = new[]
        {
            // Funcionários com ALTO ESTRESSE (para demonstrar alertas)
            new { Name = "Maria Silva", Email = "maria.silva@empresa.com", Department = "TI", Position = "Desenvolvedora Sênior", StressLevel = 4.5 },
            new { Name = "João Santos", Email = "joao.santos@empresa.com", Department = "Vendas", Position = "Gerente de Vendas", StressLevel = 4.8 },
            new { Name = "Ana Costa", Email = "ana.costa@empresa.com", Department = "RH", Position = "Analista de RH", StressLevel = 4.2 },
            
            // Funcionários com ESTRESSE MÉDIO
            new { Name = "Pedro Oliveira", Email = "pedro.oliveira@empresa.com", Department = "Marketing", Position = "Coordenador de Marketing", StressLevel = 3.2 },
            new { Name = "Carla Mendes", Email = "carla.mendes@empresa.com", Department = "Financeiro", Position = "Analista Financeiro", StressLevel = 3.5 },
            
            // Funcionários com BAIXO ESTRESSE (saudáveis)
            new { Name = "Lucas Ferreira", Email = "lucas.ferreira@empresa.com", Department = "TI", Position = "Desenvolvedor Pleno", StressLevel = 2.1 },
            new { Name = "Julia Alves", Email = "julia.alves@empresa.com", Department = "Marketing", Position = "Designer", StressLevel = 1.8 },
            new { Name = "Rafael Souza", Email = "rafael.souza@empresa.com", Department = "Vendas", Position = "Vendedor", StressLevel = 2.3 }
        };

        foreach (var empData in employeeData)
        {
            var contactInfo = new ContactInfo(
                phone: $"(11) 9{random.Next(1000, 9999)}-{random.Next(1000, 9999)}",
                emergencyContact: $"Contato de Emergência {empData.Name}",
                emergencyPhone: $"(11) {random.Next(3000, 9999)}-{random.Next(1000, 9999)}"
            );

            var employee = new Employee(
                name: empData.Name,
                email: empData.Email,
                department: empData.Department,
                position: empData.Position,
                contactInfo: contactInfo
            );

            employees.Add(employee);
            context.Employees.Add(employee);
        }

        context.SaveChanges();

        // Criar métricas de saúde para cada funcionário
        foreach (var employee in employees)
        {
            var empData = employeeData.First(e => e.Email == employee.Email);
            var stressLevel = empData.StressLevel;

            // Criar múltiplas métricas nos últimos 7 dias
            for (int i = 0; i < 7; i++)
            {
                var recordedAt = DateTime.UtcNow.AddDays(-i);

                // Frequência Cardíaca (afetada pelo estresse)
                var baseHeartRate = stressLevel > 4 ? 95 + random.Next(-5, 10) : 70 + random.Next(-5, 10);
                var heartRate = new HealthMetric(
                    employeeId: employee.Id,
                    recordedAt: recordedAt,
                    type: MetricTypeEnum.HeartRate,
                    value: baseHeartRate,
                    unit: "bpm",
                    source: MetricSource.Wearable
                );
                context.HealthMetrics.Add(heartRate);

                // Qualidade do Sono (afetada pelo estresse)
                var sleepQuality = stressLevel > 4 
                    ? 5.5 + random.NextDouble() * 1.5  // Sono ruim (5.5-7)
                    : 7.5 + random.NextDouble() * 1.5; // Sono bom (7.5-9)
                var sleep = new HealthMetric(
                    employeeId: employee.Id,
                    recordedAt: recordedAt,
                    type: MetricTypeEnum.SleepQuality,
                    value: Math.Round(sleepQuality, 1),
                    unit: "horas",
                    source: MetricSource.Wearable
                );
                context.HealthMetrics.Add(sleep);

                // Temperatura Corporal
                var temperature = new HealthMetric(
                    employeeId: employee.Id,
                    recordedAt: recordedAt,
                    type: MetricTypeEnum.BodyTemperature,
                    value: Math.Round(36.5 + random.NextDouble() * 0.7, 1),
                    unit: "°C",
                    source: MetricSource.Wearable
                );
                context.HealthMetrics.Add(temperature);

                // Nível de Estresse
                var stress = new HealthMetric(
                    employeeId: employee.Id,
                    recordedAt: recordedAt,
                    type: MetricTypeEnum.StressLevel,
                    value: Math.Round(stressLevel + (random.NextDouble() - 0.5) * 0.5, 1),
                    unit: "nível",
                    source: MetricSource.Wearable
                );
                context.HealthMetrics.Add(stress);
            }
        }

        context.SaveChanges();

        // Criar análises emocionais (textos com sentimento negativo para funcionários estressados)
        var negativeTexts = new[]
        {
            "Estou me sentindo muito sobrecarregado com todas as demandas. Não consigo dar conta de tudo.",
            "A pressão está muito alta e estou me sentindo ansioso constantemente.",
            "Estou cansado e frustrado com a quantidade de trabalho. Preciso de uma pausa.",
            "As reuniões estão me deixando estressado. Não tenho tempo para focar no que realmente importa.",
            "Me sinto esgotado mentalmente. O trabalho está afetando minha saúde."
        };

        var positiveTexts = new[]
        {
            "Estou me sentindo bem e motivado com os projetos atuais.",
            "O trabalho está fluindo bem e estou satisfeito com os resultados.",
            "Estou energizado e pronto para novos desafios.",
            "A equipe está trabalhando muito bem junta. Estou feliz com o progresso.",
            "Me sinto bem e equilibrado entre trabalho e vida pessoal."
        };

        foreach (var employee in employees)
        {
            var empData = employeeData.First(e => e.Email == employee.Email);
            var isStressed = empData.StressLevel > 4;

            // Criar 2-3 análises emocionais
            for (int i = 0; i < (isStressed ? 3 : 2); i++)
            {
                var analyzedAt = DateTime.UtcNow.AddDays(-random.Next(1, 5));
                var text = isStressed 
                    ? negativeTexts[random.Next(negativeTexts.Length)]
                    : positiveTexts[random.Next(positiveTexts.Length)];

                var score = isStressed 
                    ? 0.2 + random.NextDouble() * 0.2  // Score baixo (negativo)
                    : 0.7 + random.NextDouble() * 0.2; // Score alto (positivo)

                var emotion = isStressed 
                    ? new[] { "Ansioso", "Frustrado", "Estressado", "Preocupado" }[random.Next(4)]
                    : new[] { "Motivado", "Satisfeito", "Energizado", "Feliz" }[random.Next(4)];

                var sentiment = new SentimentScore(
                    score: Math.Round(score, 2),
                    confidence: Math.Round(0.7 + random.NextDouble() * 0.2, 2),
                    dominantEmotion: emotion
                );

                var analysis = new EmotionalAnalysis(
                    employeeId: employee.Id,
                    analyzedAt: analyzedAt,
                    sentiment: sentiment,
                    textContent: text,
                    source: AnalysisSource.Teams
                );

                context.EmotionalAnalyses.Add(analysis);
            }
        }

        context.SaveChanges();

        // Criar alertas de estresse para funcionários com alto estresse
        foreach (var employee in employees)
        {
            var empData = employeeData.First(e => e.Email == employee.Email);
            
            if (empData.StressLevel > 4)
            {
                // Criar 1-2 alertas críticos/altos
                var alertCount = random.Next(1, 3);
                for (int i = 0; i < alertCount; i++)
                {
                    var triggeredAt = DateTime.UtcNow.AddHours(-random.Next(1, 48));
                    var severity = empData.StressLevel > 4.5 
                        ? AlertSeverity.Critical 
                        : AlertSeverity.High;

                    var messages = new[]
                    {
                        $"Nível de estresse elevado detectado: {empData.StressLevel:F1}/5.0. Recomenda-se intervenção imediata.",
                        $"Frequência cardíaca acima do normal combinada com baixa qualidade do sono. Risco de burnout.",
                        $"Análise emocional indica sentimento negativo persistente. Necessário acompanhamento.",
                        $"Múltiplas métricas indicam deterioração da saúde mental. Ação preventiva recomendada."
                    };

                    var alert = new StressAlert(
                        employeeId: employee.Id,
                        triggeredAt: triggeredAt,
                        severity: severity,
                        message: messages[random.Next(messages.Length)],
                        type: AlertType.Combined
                    );

                    context.StressAlerts.Add(alert);
                }
            }
            else if (empData.StressLevel > 3)
            {
                // Criar 1 alerta médio
                var triggeredAt = DateTime.UtcNow.AddHours(-random.Next(12, 72));
                var alert = new StressAlert(
                    employeeId: employee.Id,
                    triggeredAt: triggeredAt,
                    severity: AlertSeverity.Medium,
                    message: $"Nível de estresse moderado detectado: {empData.StressLevel:F1}/5.0. Monitorar continuamente.",
                    type: AlertType.Physiological
                );

                context.StressAlerts.Add(alert);
            }
        }

        // Garantir um exemplo evidente de funcionário crítico (Maria Silva)
        var highlightEmployee = employees.FirstOrDefault(e => e.Email == "maria.silva@empresa.com");
        if (highlightEmployee != null)
        {
            var manualAlert = new StressAlert(
                employeeId: highlightEmployee.Id,
                triggeredAt: DateTime.UtcNow.AddMinutes(-15),
                severity: AlertSeverity.Critical,
                message: "Maria relatou exaustão e teve três noites seguidas com menos de 6h de sono.",
                type: AlertType.Combined
            );

            context.StressAlerts.Add(manualAlert);
        }

        context.SaveChanges();
        
        Console.WriteLine($"✅ Banco de dados populado com sucesso!");
        Console.WriteLine($"   - {employees.Count} funcionários criados");
        Console.WriteLine($"   - Métricas de saúde dos últimos 7 dias");
        Console.WriteLine($"   - Análises emocionais");
        Console.WriteLine($"   - Alertas de estresse");
    }
}

