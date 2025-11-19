using System.Windows;
using System.Windows.Controls;
using MindCare.WPF.Views;

namespace MindCare.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(new DashboardView());
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new DashboardView());
            UpdateButtonStyles(sender as Button);
        }

        private void MonitoringButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new MonitoringView());
            UpdateButtonStyles(sender as Button);
        }

        private void UpdateButtonStyles(Button? activeButton)
        {
            if (activeButton == null) return;

            var sidebar = activeButton.Parent as StackPanel;
            if (sidebar == null) return;

            foreach (var child in sidebar.Children)
            {
                if (child is Button btn)
                {
                    if (btn == activeButton)
                    {
                        btn.Background = System.Windows.Media.Brushes.DarkRed;
                        btn.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        btn.Background = System.Windows.Media.Brushes.DarkGray;
                        btn.FontWeight = FontWeights.Normal;
                    }
                }
            }
        }
    }
}

