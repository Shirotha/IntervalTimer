using System;
using System.Globalization;
using System.Windows.Data;

namespace Shirotha.IntervalTimer.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is null ? 0.0 : ((TimeSpan)value).TotalSeconds;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is null ? new TimeSpan() : TimeSpan.FromSeconds((double)value);
    }
}
