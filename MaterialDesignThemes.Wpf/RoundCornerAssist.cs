using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Helper properties for working with for make round corner.
    /// </summary>
    public static class RoundCornerAssist
    {
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(object), typeof(RoundCornerAssist), new PropertyMetadata("2"));

        public static void SetCornerRadius(DependencyObject element, object value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static object GetCornerRadius(DependencyObject element)
        {
            return (string)element.GetValue(CornerRadiusProperty);
        }
    }
}
