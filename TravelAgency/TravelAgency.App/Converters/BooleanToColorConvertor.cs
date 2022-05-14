using System;
using System.Windows;
using System.Windows.Data;

namespace TravelAgency.App.Converters
{
    class BooleanToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean && (bool)value)
            {
                return "#E4002B";
            }
            return "#000000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
