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
        public string SpeedTabForeground
        {
            get => GetValue(SpeedTabForegroundProperty) as string;
            set => SetValue(SpeedTabForegroundProperty, value);
        }
        public static readonly DependencyProperty SpeedTabForegroundProperty = DependencyProperty.Register(@"SpeedTabForeground", typeof(string), typeof(MainScreen));


        public MainScreen(ShadowsocksController controller)
        {
            InitializeComponent();

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
                this.SpeedTabForeground = "#fff";
                ImageBrush imageBrush = new ImageBrush();
                Uri uri = new Uri("/Resources/tab_bg.png", UriKind.Relative);
                imageBrush.ImageSource = new BitmapImage(uri);
                this.SpeedTabBackground = imageBrush;

            } 
            else if (tabIndex == 1)
            {
                this.SpeedTabForeground = "#8A899A";
                this.SpeedTabBackground = null;
            }
        }
    }
}
