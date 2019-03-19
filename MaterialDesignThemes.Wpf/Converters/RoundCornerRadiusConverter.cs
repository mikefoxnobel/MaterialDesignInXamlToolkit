using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class RoundCornerRadiusConverter : IMultiValueConverter
    {
        public static readonly RoundCornerRadiusConverter Instance = new RoundCornerRadiusConverter();
        public static CornerRadius DefaultCornerRadius { get; set; } = new CornerRadius(2);
        private static char[] __separator = new char[] { ',', ' ' };
        private static FigureLengthConverter __lengthConverter = new FigureLengthConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double minLength = Double.NaN;
            if (!(values[0] is string))
            {
                return DefaultCornerRadius;
            }
            string sourceValue = (string)values[0];

            if (values.Length > 2)
            {
                double actualWidth = (double)values[2];
                double actualHeight = (double)values[1];
                minLength = Math.Min(actualWidth, actualHeight);
            }
            else if (values.Length > 1)
            {
                minLength = (double)values[1];
            }
            else
            {
                throw new NotSupportedException("Please pass in 'ActualWidth' and 'ActualHeight' as the parameter.");
            }

            string[] splitValue = sourceValue.Trim().Split(__separator, StringSplitOptions.RemoveEmptyEntries);
            if (splitValue.Length == 1)
            {
                double radius = ConvertFromString(splitValue[0], minLength);
                CornerRadius result = new CornerRadius(radius);
                return result;
            }
            else if (splitValue.Length == 4)
            {
                double[] dCornerRadius = new double[4];
                for (int i = 0; i < 4; i++)
                {
                    dCornerRadius[i] = ConvertFromString(splitValue[i], minLength);
                }
                CornerRadius result = new CornerRadius(dCornerRadius[0], dCornerRadius[1], dCornerRadius[2], dCornerRadius[3]);
                return result;
            }
            else
            {
                throw new NotSupportedException("Invalid corner radius string, please provide 1 or 4 numbers, with supported unit px, in, cm, pt, % or no unit.");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static double ConvertFromString(string value, double origin = Double.NaN)
        {
            if (value.EndsWith("%"))
            {
                double percent = 0;
                try
                {
                    percent = System.Convert.ToDouble(value.TrimEnd(' ', '%')) / 100.0;
                    if (percent < 0)
                    {
                        percent = 0;
                    }
                    else if (percent > 0.5)
                    {
                        percent = 0.5;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                if (Double.IsNaN(origin))
                {
                    throw new NotSupportedException();
                }

                return origin * percent;
            }
            else
            {
                FigureLength length = (FigureLength)__lengthConverter.ConvertFrom(value);
                if (!Double.IsNaN(origin))
                {
                    if (length.Value > origin)
                    {
                        length = new FigureLength(origin);
                    }
                }
                return length.Value;
            }
        }
    }
}
