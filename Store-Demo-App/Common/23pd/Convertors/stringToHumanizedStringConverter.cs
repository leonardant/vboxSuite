using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Humanizer;
using System.Globalization;

namespace mpeg2_player.Common
{
    public sealed class stringToHumanizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string returnString = value.ToString();

            if ((value == null) || (string.IsNullOrEmpty(value.ToString())))
            {

            }
            else
            {
                // 20150217205600+0000
                // yyyyMMddHHmmsszzz
                string formatString = "yyyyMMddHHmmsszzz";
                DateTime D;
                if (DateTime.TryParseExact(value.ToString(), formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out D))
                {
                    returnString = D.Humanize();
                }
            }
            return returnString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

    }
}
