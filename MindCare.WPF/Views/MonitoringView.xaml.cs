using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MindCare.WPF.Models;
using MindCare.WPF.Services;

namespace MindCare.WPF.Views;

public partial class MonitoringView : UserControl
{
    private readonly ApiService _apiService;

    public MonitoringView()
    {
        InitializeComponent();
        _apiService = new ApiService();
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            var employees = await _apiService.GetEmployeesAsync();
            if (employees != null)
            {
                EmployeesDataGrid.ItemsSource = employees;
                
                var comboItems = new List<object> { new { Id = 0, Name = "Todos os funcionários" } };
                comboItems.AddRange(employees);
                EmployeeComboBox.ItemsSource = comboItems;
                EmployeeComboBox.DisplayMemberPath = "Name";
                EmployeeComboBox.SelectedValuePath = "Id";
                EmployeeComboBox.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (EmployeeComboBox.SelectedItem != null)
        {
            var selectedItem = EmployeeComboBox.SelectedItem;
            var idProperty = selectedItem.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                var employeeId = (int)idProperty.GetValue(selectedItem);
                await LoadMetrics(employeeId == 0 ? null : employeeId);
            }
        }
    }

    private async void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (EmployeesDataGrid.SelectedItem is EmployeeModel employee)
        {
            await LoadMetrics(employee.Id);
            if (EmployeeComboBox.ItemsSource is IEnumerable<object> items)
            {
                var selectedItem = items.FirstOrDefault(item => 
                {
                    var idProp = item.GetType().GetProperty("Id");
                    return idProp != null && (int)idProp.GetValue(item) == employee.Id;
                });
                if (selectedItem != null)
                    EmployeeComboBox.SelectedItem = selectedItem;
            }
        }
    }

    private async Task LoadMetrics(int? employeeId)
    {
        try
        {
            var metrics = await _apiService.GetHealthMetricsAsync(employeeId);
            if (metrics != null)
            {
                MetricsDataGrid.ItemsSource = metrics;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar métricas: {ex.Message}", "Erro", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}

