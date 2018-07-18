using System;
using System.Globalization;
using Xamarin.Forms;

namespace SignalR.Mobile.ValueConverters
{
    class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) throw new ArgumentException("Value is not the correct type");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) throw new ArgumentException("Value is not the correct type");

            return !(bool)value;
        }
    }
}
