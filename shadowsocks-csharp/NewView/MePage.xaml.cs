using Shadowsocks.Controller;
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
    }
}
