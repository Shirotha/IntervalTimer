using System;
using System.Globalization;
using System.Windows.Data;

namespace Shirotha.IntervalTimer.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is null ? "0:00" : (((TimeSpan)value) + TimeSpan.FromSeconds(1)).ToString(@"m\:ss");
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is null ? TimeSpan.FromSeconds(0) : (TimeSpan.Parse((string)value) - TimeSpan.FromSeconds(1));
    }
}
