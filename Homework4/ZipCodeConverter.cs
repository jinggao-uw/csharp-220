using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Homework4
{
    class ZipCodeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zip = value as string;

            if (zip != null)
            {
                string USZipPattern5 = @"^([0-9]{5})$";
                string USZipPattern9 = @"^([0-9]{5}-[0-9]{4})$";
                string CanadaZipPattern = @"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[A-Za-z]{1} *\d{1}[A-Za-z]{1}\d{1}$";

                string finalPattern = "(" + USZipPattern5 + ")|(" + USZipPattern9 + ")|(" + CanadaZipPattern + ")";

                Regex regex = new Regex(finalPattern);
                return regex.IsMatch(zip);
            }
            else
            {
                return false;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
