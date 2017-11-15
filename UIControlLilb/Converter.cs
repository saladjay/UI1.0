using ExtendedString;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UIControlLib
{

    public sealed class Converter
    {
        public static StateConverter CellStateConverter
        {
            get { return SingleTon<StateConverter>.GetInstance(); }
        }
        public static VisibilityConverterForCommon CellVisibilityConverter
        {
            get { return SingleTon<VisibilityConverterForCommon>.GetInstance(); }
        }
        public static VisibilityConverterForSingle CellVisibilityConverterForSingle
        {
            get { return SingleTon<VisibilityConverterForSingle>.GetInstance(); }
        }
        public static SelectedConverterForCLabel SelectConverter
        {
            get { return SingleTon<SelectedConverterForCLabel>.GetInstance(); }
        }
        public static ScrollBarVisibilityConverter PaddingConverter
        {
            get { return SingleTon<ScrollBarVisibilityConverter>.GetInstance(); }
        }

        public static BooleanOrOperation BooleanOrConverter
        {
            get { return SingleTon<BooleanOrOperation>.GetInstance(); }
        } 
    }

    public sealed class StateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Debug.WriteLine("StateConverter");
            return (bool)values[0] && (bool)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class VisibilityConverterForSingle : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class VisibilityConverterForCommon : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                //Debug.WriteLine("VisibilityConvetter");
                if ((bool)values[0] && (bool)values[1])
                {
                    //Debug.WriteLine("VisibilityConverter return visible");
                    return Visibility.Visible;
                }
                else
                {
                    //Debug.WriteLine("VisibilityConverter return collapsed");
                    return Visibility.Collapsed;
                }
            }
            else
            {
                return ((bool)values[0] && (bool)values[1]);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class SelectedConverterForCLabel : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)values[0] || (bool)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ScrollBarVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Collapsed)
                return Visibility.Hidden;
            else
                return (Visibility)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class BooleanOrOperation : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = (bool)values[0];
            for (int i = 1; i < values.Length; i++)
            {
                result = result | (bool)values[i];
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

