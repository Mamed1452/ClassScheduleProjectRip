﻿using System;
using System.Globalization;
using Mohajer.ClassScheduleProject.Localization;
using Xamarin.Forms;

namespace Mohajer.ClassScheduleProject.Converters
{
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? L.Localize("Yes") : L.Localize("No");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}