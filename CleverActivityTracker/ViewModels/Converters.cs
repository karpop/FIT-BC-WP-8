using System;
using System.Windows;
using System.Windows.Data;

namespace CleverActivityTracker.ViewModels
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Konvertuje true na visible a false na collapsed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Otočí návratovou hodnotu.</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool boolValue = (bool)value;
            bool boolParameter = parameter != null ? bool.Parse(parameter as string) : false;

            if (boolParameter)
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            else
                return boolValue ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibilityValue = (Visibility)value;
            return visibilityValue == Visibility.Visible ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
