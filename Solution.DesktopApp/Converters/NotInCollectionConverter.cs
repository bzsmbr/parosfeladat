using System.Collections;
using System.Globalization;
using System.Diagnostics;

namespace Solution.DesktopApp.Converters
{
    public class NotInCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return true;
                }

                if (parameter == null)
                {
                    return true;
                }
                if (!(parameter is IEnumerable collection))
                {
                    return true;
                }
                foreach (var item in collection)
                {
                    if (item == null)
                        continue;

                    if (item is CompetitionModel competitionItem && value is CompetitionModel competitionValue)
                    {
                        if (competitionItem.Id == competitionValue.Id)
                        {
                            return false;
                        }
                    }
                    else if (item.Equals(value))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}