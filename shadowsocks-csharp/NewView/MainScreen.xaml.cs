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


        public MainScreen()
        {
            InitializeComponent();

            allViews.Add("page1", new Uri("/NewView/SpeedPage.xaml", UriKind.Relative));
            allViews.Add("page2", new Uri("/NewView/MePage.xaml", UriKind.Relative));

        }

        private void onSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(allViews["page1"]);
        }

        private void onMeButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(allViews["page2"]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(allViews["page1"]);
        }
    }
}
