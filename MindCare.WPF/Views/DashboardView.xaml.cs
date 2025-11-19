using System.Windows;
using System.Windows.Controls;
using MindCare.WPF.Models;
using MindCare.WPF.Services;

namespace MindCare.WPF.Views;

public partial class DashboardView : UserControl
{
    private readonly ApiService _apiService;

    public DashboardView()
    {
        InitializeComponent();
        _apiService = new ApiService();
        LoadDashboardData();
    }

    private async void LoadDashboardData()
    {
        try
        {
            var summary = await _apiService.GetDashboardSummaryAsync();
            if (summary != null)
            {
                TotalEmployeesText.Text = summary.TotalEmployees.ToString();
                ActiveEmployeesText.Text = summary.ActiveEmployees.ToString();
                HighRiskText.Text = summary.HighRiskEmployees.ToString();
                ActiveAlertsText.Text = summary.ActiveAlerts.ToString();
                AvgStressText.Text = summary.AverageStressLevel.ToString("F2");
                AvgSleepText.Text = summary.AverageSleepQuality.ToString("F2");
                AlertsDataGrid.ItemsSource = summary.RecentAlerts;

                if (summary.FeaturedEmployee != null)
                {
                    HighlightCard.Visibility = Visibility.Visible;
                    NoHighlightCard.Visibility = Visibility.Collapsed;

                    var featured = summary.FeaturedEmployee;
                    HighlightNameText.Text = featured.Name;
                    HighlightRoleText.Text = $"{featured.Position} • {featured.Department}";
                    HighlightStressText.Text = featured.StressLevel.ToString("F1");
                    HighlightSleepText.Text = $"{featured.SleepQuality:F1} h";
                    HighlightSeverityText.Text = $"Severidade: {featured.AlertSeverity}";
                    HighlightMessageText.Text = featured.AlertMessage;
                    HighlightTimeText.Text = $"Atualizado em {featured.LastUpdatedAt:dd/MM HH:mm}";
                }
                else
                {
                    HighlightCard.Visibility = Visibility.Collapsed;
                    NoHighlightCard.Visibility = Visibility.Visible;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void AcknowledgeButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is int alertId)
        {
            try
            {
                var success = await _apiService.AcknowledgeAlertAsync(alertId);
                if (success)
                {
                    MessageBox.Show("Alerta reconhecido com sucesso!", "Sucesso", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDashboardData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reconhecer alerta: {ex.Message}", "Erro", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LoadDashboardData();
    }
}

