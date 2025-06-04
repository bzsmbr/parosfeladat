using System.Globalization;
using System.Diagnostics;

namespace Solution.DesktopApp.Converters
{
    public class ReferenceEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            try
            {
                if (value is CompetitionModel valueComp && parameter is CompetitionModel paramComp)
                {
                    bool result = valueComp.Id == paramComp.Id;
                    return result;
                }

                if (ReferenceEquals(value, parameter))
                {
                    return true;
                }

                return value.ToString() == parameter.ToString();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}