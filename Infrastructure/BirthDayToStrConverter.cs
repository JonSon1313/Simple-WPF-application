using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp_Practice.Infrastructure
{
    internal class BirthDayToStrConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBirthDateToAge(value);
        }

        private string ConvertBirthDateToAge(object value)
        {
            if (value is DateTime birthDate)
            {
                int age = CalculateAge(birthDate);
                return $"{age} років";
            }

            return string.Empty;
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;

            if (currentDate.Month < birthDate.Month ||
                (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var pattern = @"\d+";
                var regEx = new Regex(pattern);
                var res = regEx.Match(value.ToString() ?? "");
                return int.Parse(res.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
