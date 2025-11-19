using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using MindCare.WPF.Models;

namespace MindCare.WPF.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiService(string baseUrl = "http://localhost:5000/api")
    {
        _baseUrl = baseUrl;
        
        // Configurar HttpClient para ignorar erros de certificado SSL em desenvolvimento
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = 
            (message, cert, chain, errors) => true; // Ignorar erros de certificado
        
        _httpClient = new HttpClient(handler);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    // Dashboard
    public async Task<DashboardSummary?> GetDashboardSummaryAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Dashboard/summary");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DashboardSummary>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar resumo do dashboard: {ex.Message}", ex);
        }
    }

    // Employees
    public async Task<List<EmployeeModel>?> GetEmployeesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Employees");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<EmployeeModel>>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar funcionários: {ex.Message}", ex);
        }
    }

    public async Task<EmployeeModel?> CreateEmployeeAsync(EmployeeModel employee)
    {
        try
        {
            var json = JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Employees", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeModel>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao criar funcionário: {ex.Message}", ex);
        }
    }

    // Health Metrics
    public async Task<List<HealthMetricModel>?> GetHealthMetricsAsync(int? employeeId = null)
    {
        try
        {
            var url = employeeId.HasValue 
                ? $"{_baseUrl}/HealthMetrics/employee/{employeeId}" 
                : $"{_baseUrl}/HealthMetrics";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HealthMetricModel>>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar métricas: {ex.Message}", ex);
        }
    }

    // Stress Alerts
    public async Task<List<StressAlertModel>?> GetStressAlertsAsync(int? employeeId = null)
    {
        try
        {
            var url = employeeId.HasValue 
                ? $"{_baseUrl}/StressAlerts/employee/{employeeId}" 
                : $"{_baseUrl}/StressAlerts";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<StressAlertModel>>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar alertas: {ex.Message}", ex);
        }
    }

    public async Task<bool> AcknowledgeAlertAsync(int alertId)
    {
        try
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/StressAlerts/{alertId}/acknowledge", null);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao reconhecer alerta: {ex.Message}", ex);
        }
    }
}

