using System;
using System.Globalization;
using System.Windows.Data;

namespace Shirotha.IntervalTimer.Converters
{
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(double))]
    public class ScaleConverter : IValueConverter
    {
        public bool Invert { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Invert ? System.Convert.ToDouble(value, culture) / System.Convert.ToDouble(parameter, culture) :
                     System.Convert.ToDouble(value, culture) * System.Convert.ToDouble(parameter, culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Invert ? System.Convert.ToDouble(value, culture) * System.Convert.ToDouble(parameter, culture) :
                     System.Convert.ToDouble(value, culture) / System.Convert.ToDouble(parameter, culture);
    }
}
