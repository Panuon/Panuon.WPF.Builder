using System;
using System.Globalization;
using System.Windows.Data;

namespace Panuon.WPF.Builder.Internal.Converters
{
    internal class CustomConverter
        : IValueConverter
    {
        private Func<object, object> _converter;

        public CustomConverter(Func<object, object> converter) 
        {
            _converter = converter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _converter(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
