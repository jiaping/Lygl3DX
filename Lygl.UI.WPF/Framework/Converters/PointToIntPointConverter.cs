using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Lygl.UI.Framework.Converters
{
    //[ValueConversion()]
    public class PointToIntPointConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point p = Point.Parse((string)value);
            return string.Format("{0:F0},{1:F0}", p.X, p.Y);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value;
        }
    }
}
