using Shadowsocks.Controller;
using Shadowsocks.NewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shadowsocks.NewView
{
    /// <summary>
    /// MePage.xaml 的交互逻辑
    /// </summary>
    public partial class MePage : Page
    {
        public event EventHandler CloseMainScreenEvent;
        public event EventHandler ShareQRCodeEvent;
        public event EventHandler BindInviteCodeEvent;
        public event EventHandler VisitOfficialWebsiteEvent;
        public event EventHandler VisitGoodWebsiteEvent;
        public event EventHandler FeedbackEvent;
        public event EventHandler OnlineSupportEvent;
        public event EventHandler BusinessEvent;

        public MePage()
        {
            InitializeComponent();
        }

        private void onLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMainScreenEvent?.Invoke(default, default);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.loginDate.Text = CurrentUser.LoginDateStr;
            this.loginUser.Text = CurrentUser.Email;
        }

        private void onShareQRCodeEventHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ShareQRCodeEvent?.Invoke(default, default);
        }

        private void onBindInviteCodeHyperlink_Click(object sender, RoutedEventArgs e)
        {
            BindInviteCodeEvent?.Invoke(default, default);
        }

        private void onOfficialWebsiteHyperlink_Click(object sender, RoutedEventArgs e)
        {
            VisitOfficialWebsiteEvent?.Invoke(default, default);
        }

        private void onGoodWebsiteHyperlink_Click(object sender, RoutedEventArgs e)
        {
            VisitGoodWebsiteEvent?.Invoke(default, default);
        }

        private void onFeedbackHyperlink_Click(object sender, RoutedEventArgs e)
        {
            FeedbackEvent?.Invoke(default, default);
        }

        private void onOnlineSupportHyperlink_Click(object sender, RoutedEventArgs e)
        {
            OnlineSupportEvent?.Invoke(default, default);
        }

        private void onBusinessHyperlink_Click(object sender, RoutedEventArgs e)
        {
            BusinessEvent?.Invoke(default, default);
        }
    }
}
