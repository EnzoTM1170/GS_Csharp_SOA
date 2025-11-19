using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MindCare.WPF.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isAbnormal)
            return isAbnormal ? Brushes.Red : Brushes.Green;
        return Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

