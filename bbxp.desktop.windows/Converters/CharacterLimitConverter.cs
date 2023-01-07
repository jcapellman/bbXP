using System;
using System.Globalization;
using System.Windows.Data;

namespace bbxp.desktop.windows.Converters
{
    public class CharacterLimitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;

            int charLimit = int.Parse((string)parameter);

            if (input.Length > charLimit)
            {
                return string.Concat(input.AsSpan(0, charLimit), "...");
            }

            return input;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (string)value;
    }
}