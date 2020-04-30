using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MonkeyChallenger.Converters
{
    public class StringToCultureInfo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return "";

            var cultureInfo = new CultureInfo((string)value);

            if (cultureInfo == null)
                return "";

            return cultureInfo.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return culture.DisplayName;
        }
    }
}
