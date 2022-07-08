using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Shirotha.IntervalTimer.MarkupExtensions
{
    [ContentProperty(nameof(Binding))]
    public class Convertable : MarkupExtension
    {
        public Binding? Binding { get; set; }
        public BindingMode BindingMode { get; set; }
        public IValueConverter? Converter { get; set; }
        public Binding? Parameter { get; set; }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            if (Binding is null)
                return null;

            var multiBinding = new MultiBinding();
            Binding.Mode = BindingMode;
            multiBinding.Bindings.Add(Binding);
            if (Parameter is not null)
            {
                Parameter.Mode = BindingMode.OneWay;
                multiBinding.Bindings.Add(Parameter);
            }
            var adapter = new MultiValueConverterAdapter
            {
                Converter = Converter
            };
            multiBinding.Converter = adapter;
            return multiBinding.ProvideValue(serviceProvider);
        }

        [ContentProperty(nameof(Converter))]
        private class MultiValueConverterAdapter : IMultiValueConverter
        {
            public IValueConverter? Converter { get; set; }

            private object? _LastParameter;

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (Converter is null)
                    return values[0];

                if (values.Length > 1)
                    _LastParameter = values[1];

                return Converter.Convert(values[0], targetType, _LastParameter, culture);
            }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                if (Converter is null)
                    return new object[] { value };

                return new object[] { Converter.Convert(value, targetTypes[0], _LastParameter, culture) };
            }
        }
    }
}
