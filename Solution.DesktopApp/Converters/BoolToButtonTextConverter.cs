using System.Globalization;

namespace Solution.DesktopApp.Converters
{
    public class BoolToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return value;

            string[] texts = parameter.ToString().Split(';');
            if (texts.Length != 2)
                return value;

            return (bool)value ? texts[0] : texts[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}