using Shadowsocks.NewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shadowsocks.NewView
{
    /// <summary>
    /// QRCodeScreen.xaml 的交互逻辑
    /// </summary>
    public partial class QRCodeScreen : Window
    {
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(@"Description", typeof(string), typeof(QRCodeScreen));


        public BitmapImage QRCodeImage
        {
            get => GetValue(QRCodeImageProperty) as BitmapImage;
            set => SetValue(QRCodeImageProperty, value);
        }
        public static readonly DependencyProperty QRCodeImageProperty = DependencyProperty.Register(@"QRCodeImage", typeof(BitmapImage), typeof(QRCodeScreen));


        public QRCodeScreen()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Description = "描述信息";
        }

        private void onCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.downloadQrCode();
            this.fetchInfo();
        }

        private void downloadQrCode()
        {
            string url = "http://ss.yam.im/api/url/PageQRCode?access_token=" + CurrentUser.Token;
            var res = WebHelper.Get(url, null);
            // if (res != null)
            {
                this.QRCodeImage = new BitmapImage(new Uri("http://pic1.win4000.com/pic/7/a8/c4ac1219621.jpg"));

            }
        }

        private void fetchInfo()
        {
            string url = "http://ss.yam.im/api/url/PageInfo?access_token=" + CurrentUser.Token;
            var res = WebHelper.Get(url, null);
            if (res != null)
            {
                this.Description = "描述信息";
            }
        }
    }
}
