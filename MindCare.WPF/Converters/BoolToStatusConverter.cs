using System.Globalization;
using System.Windows.Data;

namespace MindCare.WPF.Converters;

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isAbnormal)
            return isAbnormal ? "⚠️ Anormal" : "✅ Normal";
        return "N/A";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

