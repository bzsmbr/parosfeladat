using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Solution.DesktopApp.Converters
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime datetimeValue)
            {
                string format = parameter as string ?? "d";
                return datetimeValue.ToString(format, culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && DateTime.TryParse(str, culture, DateTimeStyles.None, out var result))
            {
                return result;
            }
            return Microsoft.Maui.Controls.Binding.DoNothing;
        }
    }
}