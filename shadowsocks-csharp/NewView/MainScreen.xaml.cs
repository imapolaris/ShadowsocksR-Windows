using Shadowsocks.Controller;
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
    /// MainScreen.xaml 的交互逻辑
    /// </summary>
    public partial class MainScreen : Window
    {
        private Dictionary<string, Uri> allViews = new Dictionary<string, Uri>(); //包含所有页面

        public new int TabIndex
        {
            get => (int)GetValue(TabIndexProperty);
            set => SetValue(TabIndexProperty, value);
        }

        public new static readonly DependencyProperty TabIndexProperty = DependencyProperty.Register(@"TabIndex", typeof(int), typeof(MainScreen));

        public ImageBrush SpeedTabBackground
        {
            get => GetValue(SpeedTabBackgroundProperty) as ImageBrush;
            set => SetValue(SpeedTabBackgroundProperty, value);
        }
        public static readonly DependencyProperty SpeedTabBackgroundProperty = DependencyProperty.Register(@"SpeedTabBackground", typeof(ImageBrush), typeof(MainScreen));
        public Brush SpeedTabForeground
        {
            get => GetValue(SpeedTabForegroundProperty) as Brush;
            set => SetValue(SpeedTabForegroundProperty, value);
        }
        public static readonly DependencyProperty SpeedTabForegroundProperty = DependencyProperty.Register(@"SpeedTabForeground", typeof(Brush), typeof(MainScreen));


        public ImageBrush MeTabBackground
        {
            get => GetValue(MeTabBackgroundProperty) as ImageBrush;
            set => SetValue(MeTabBackgroundProperty, value);
        }
        public static readonly DependencyProperty MeTabBackgroundProperty = DependencyProperty.Register(@"MeTabBackground", typeof(ImageBrush), typeof(MainScreen));
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

            allViews.Add("page1", new Uri("/NewView/SpeedPage.xaml", UriKind.Relative));
            allViews.Add("page2", new Uri("/NewView/MePage.xaml", UriKind.Relative));
        }

        private readonly ShadowsocksController _controller;

        private void onSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            this.TabIndex = 0;
            this.onTabChanged(this.TabIndex);
            mainFrame.Navigate(allViews["page1"], this._controller);
        }

        private void onMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.TabIndex = 1;
            this.onTabChanged(this.TabIndex);
            mainFrame.Navigate(allViews["page2"], this._controller);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.TabIndex = 0;
            this.onTabChanged(this.TabIndex);
            mainFrame.Navigate(allViews["page1"], this._controller);
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
                this.MeTabBackground = null;
            } 
            else if (tabIndex == 1)
            {
                this.SpeedTabForeground = new SolidColorBrush(Color.FromRgb((byte)138, (byte)137, (byte)154)); // #8A899A
                this.SpeedTabBackground = null;

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
