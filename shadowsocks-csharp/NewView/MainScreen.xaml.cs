using Shadowsocks.Controller;
using Shadowsocks.NewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// MainScreen.xaml 的交互逻辑
    /// </summary>
    public partial class MainScreen : Window
    {
        public new int TabIndex
        {
            get => (int)GetValue(TabIndexProperty);
            set => SetValue(TabIndexProperty, value);
        }

        public new static readonly DependencyProperty TabIndexProperty = DependencyProperty.Register(@"TabIndex", typeof(int), typeof(MainScreen));

        public Brush SpeedTabBackground
        {
            get => GetValue(SpeedTabBackgroundProperty) as Brush;
            set => SetValue(SpeedTabBackgroundProperty, value);
        }
        public static readonly DependencyProperty SpeedTabBackgroundProperty = DependencyProperty.Register(@"SpeedTabBackground", typeof(Brush), typeof(MainScreen));
        public Brush SpeedTabForeground
        {
            get => GetValue(SpeedTabForegroundProperty) as Brush;
            set => SetValue(SpeedTabForegroundProperty, value);
        }
        public static readonly DependencyProperty SpeedTabForegroundProperty = DependencyProperty.Register(@"SpeedTabForeground", typeof(Brush), typeof(MainScreen));


        public Brush MeTabBackground
        {
            get => GetValue(MeTabBackgroundProperty) as Brush;
            set => SetValue(MeTabBackgroundProperty, value);
        }
        public static readonly DependencyProperty MeTabBackgroundProperty = DependencyProperty.Register(@"MeTabBackground", typeof(Brush), typeof(MainScreen));
        public Brush MeTabForeground
        {
            get => GetValue(MeTabForegroundProperty) as Brush;
            set => SetValue(MeTabForegroundProperty, value);
        }
        public static readonly DependencyProperty MeTabForegroundProperty = DependencyProperty.Register(@"MeTabForeground", typeof(Brush), typeof(MainScreen));

        public ImageBrush SpeedTabIcon
        {
            get => GetValue(SpeedTabIconProperty) as ImageBrush;
            set => SetValue(SpeedTabIconProperty, value);
        }
        public static readonly DependencyProperty SpeedTabIconProperty = DependencyProperty.Register(@"SpeedTabIcon", typeof(ImageBrush), typeof(MainScreen));
        public ImageBrush MeTabIcon
        {
            get => GetValue(MeTabIconProperty) as ImageBrush;
            set => SetValue(MeTabIconProperty, value);
        }
        public static readonly DependencyProperty MeTabIconProperty = DependencyProperty.Register(@"MeTabIcon", typeof(ImageBrush), typeof(MainScreen));


        public MainScreen(ShadowsocksController controller)
        {
            InitializeComponent();

            this.DataContext = this;

            this._controller = controller;
        }

        private readonly ShadowsocksController _controller;

        private void onSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            this.TabIndex = 0;
            this.onTabChanged(this.TabIndex);
            mainFrame.Navigate(speedPage);
        }

        private MePage mePage = null;
        private SpeedPage speedPage = null;
        private void onMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.TabIndex = 1;
            this.onTabChanged(this.TabIndex);
            mainFrame.Navigate(mePage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseDown += (x, y) =>
            {
                if (y.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.TabIndex = 0;
            this.onTabChanged(this.TabIndex);

            mePage = new MePage();
            mePage.CloseMainScreenEvent += MePage_CloseMainScreenEvent;
            mePage.ShareQRCodeEvent += MePage_ShareQRCodeEvent;
            mePage.BindInviteCodeEvent += MePage_BindInviteCodeEvent;
            mePage.VisitOfficialWebsiteEvent += MePage_VisitOfficialWebsiteEvent;
            mePage.VisitGoodWebsiteEvent += MePage_VisitGoodWebsiteEvent;
            mePage.FeedbackEvent += MePage_FeedbackEvent;
            mePage.OnlineSupportEvent += MePage_OnlineSupportEvent;
            mePage.BusinessEvent += MePage_BusinessEvent;

            speedPage = new SpeedPage(_controller);
            mainFrame.Navigate(speedPage);
        }

        private void MePage_BusinessEvent(object sender, EventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/url/Business?access_token=" + CurrentUser.Token;
                VisitWebsite(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MePage_OnlineSupportEvent(object sender, EventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/url/OnlineSupport?access_token=" + CurrentUser.Token;
                VisitWebsite(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MePage_FeedbackEvent(object sender, EventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/url/Ticket?access_token=" + CurrentUser.Token;
                VisitWebsite(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MePage_ShareQRCodeEvent(object sender, EventArgs e)
        {
            
        }

        private void MePage_VisitGoodWebsiteEvent(object sender, EventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/url/Navigation?access_token=" + CurrentUser.Token;
                VisitWebsite(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MePage_VisitOfficialWebsiteEvent(object sender, EventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/url/Website?access_token=" + CurrentUser.Token;
                VisitWebsite(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MePage_BindInviteCodeEvent(object sender, EventArgs e)
        {
            VisitWebsite(@"https://www.365.com");
        }

        private void VisitWebsite(string url)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                string s = key.GetValue("").ToString();
                int blank = s.IndexOf("\" ");
                System.Diagnostics.Process.Start(s.Substring(0, blank + 1), url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("无法访问！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MePage_CloseMainScreenEvent(object sender, EventArgs e)
        {
            _controller.Stop();
            NewModel.CurrentUser.Reset();
            Close();
            _controller.ShowLoginScreen();
        }

        private void onTabChanged(int tabIndex)
        {
            if (tabIndex == 0)
            {
                this.SpeedTabForeground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));
                ImageBrush imageBrush = new ImageBrush();
                Uri uri = new Uri("pack://application:,,,/Resources/tab_bg.png", UriKind.Absolute);
                imageBrush.ImageSource = new BitmapImage(uri);
                this.SpeedTabBackground = imageBrush;

                this.SpeedTabIcon = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/speed.png", UriKind.Absolute)));
                this.MeTabIcon = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/me_disable.png", UriKind.Absolute)));

                this.MeTabForeground = new SolidColorBrush(Color.FromRgb((byte)138, (byte)137, (byte)154)); // #8A899A
                this.MeTabBackground = new SolidColorBrush(Color.FromRgb((byte)29, (byte)30, (byte)47)); // #1D1E2F
            } 
            else if (tabIndex == 1)
            {
                this.SpeedTabForeground = new SolidColorBrush(Color.FromRgb((byte)138, (byte)137, (byte)154)); // #8A899A
                this.SpeedTabBackground = new SolidColorBrush(Color.FromRgb((byte)29, (byte)30, (byte)47)); // #1D1E2F;

                this.MeTabForeground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));
                ImageBrush imageBrush = new ImageBrush();
                Uri uri = new Uri("pack://application:,,,/Resources/tab_bg.png", UriKind.Absolute);
                imageBrush.ImageSource = new BitmapImage(uri);
                this.MeTabBackground = imageBrush;

                this.SpeedTabIcon = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/speed_disable.png", UriKind.Absolute)));
                this.MeTabIcon = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/me.png", UriKind.Absolute)));
            }
        }

        private void onMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void onCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
