using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shadowsocks.NewModel
{
    class SpeedTabIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tabIndex = (int)value;
            if (tabIndex == 0)
            {
                Uri uri = new Uri("pack://application:,,,/Resources/speed.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/Resources/speed_disable.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class MeTabIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tabIndex = (int)value;
            if (tabIndex == 2)
            {
                Uri uri = new Uri("pack://application:,,,/Resources/me.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/Resources/me_disable.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class PayTabIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tabIndex = (int)value;
            if (tabIndex == 1)
            {
                Uri uri = new Uri("pack://application:,,,/Resources/pay.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/Resources/pay_disable.png", UriKind.Absolute);
                return new BitmapImage(uri);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
