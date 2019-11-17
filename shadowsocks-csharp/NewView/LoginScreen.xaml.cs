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
using Shadowsocks.Controller;

namespace Shadowsocks.NewView
{
    /// <summary>
    /// LoginScreen.xaml 的交互逻辑
    /// </summary>
    public partial class LoginScreen : Window
    {
        // 0-登录，1-注册
        public bool IsLogin
        {
            get => (bool)GetValue(IsLoginProperty);
            set => SetValue(IsLoginProperty, value);
        }

        public static readonly DependencyProperty IsLoginProperty = DependencyProperty.Register(@"IsLogin", typeof(bool), typeof(LoginScreen));
      
        // 0-登录，1-注册
        public bool IsRegister
        {
            get => (bool)GetValue(IsRegisterProperty);
            set => SetValue(IsRegisterProperty, value);
        }

        public static readonly DependencyProperty IsRegisterProperty = DependencyProperty.Register(@"IsRegister", typeof(bool), typeof(LoginScreen));

        public string ScreenTitle
        {
            get => GetValue(ScreenTitleProperty) as string;
            set => SetValue(ScreenTitleProperty, value);
        }

        public static readonly DependencyProperty ScreenTitleProperty = DependencyProperty.Register(@"Title", typeof(string), typeof(LoginScreen));

        public string LoginButtonTitle
        {
            get => GetValue(LoginButtonTitleProperty) as string;
            set => SetValue(LoginButtonTitleProperty, value);
        }

        public static readonly DependencyProperty LoginButtonTitleProperty = DependencyProperty.Register(@"LoginButtonTitle", typeof(string), typeof(LoginScreen));

        public string Email
        {
            get => GetValue(EmailProperty) as string;
            set => SetValue(EmailProperty, value);
        }

        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register(@"Email", typeof(string), typeof(LoginScreen));

        public bool RememberPassword
        {
            get => (bool)GetValue(RememberPasswordProperty);
            set => SetValue(RememberPasswordProperty, value);
        }

        public static readonly DependencyProperty RememberPasswordProperty = DependencyProperty.Register(@"RememberPassword", typeof(bool), typeof(LoginScreen));

        public string Password
        {
            get => GetValue(PasswordProperty) as string;
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(@"Password", typeof(string), typeof(LoginScreen));


        public bool AutoLogin
        {
            get => (bool)GetValue(AutoLoginProperty);
            set => SetValue(AutoLoginProperty, value);
        }
        public static readonly DependencyProperty AutoLoginProperty = DependencyProperty.Register(@"AutoLogin", typeof(bool), typeof(LoginScreen));


        public LoginScreen(ShadowsocksController controller)
        {
            InitializeComponent();

            _controller = controller;

            this.DataContext = this;

            this.IsLogin = true;
            this.IsRegister = false;
            this.ScreenTitle = "邮箱登录";
            this.LoginButtonTitle = "登录";
            this.Email = "请输入您的邮箱";
            this.Password = "请输入正确密码";
        }

        private readonly ShadowsocksController _controller;

        // 登录
        private void onLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = this.Email;
            string pwd = this.Password;
            bool autoLogin = this.AutoLogin;
            bool rememberMe = this.RememberPassword;
            bool isLogin = this.IsLogin;

            // MessageBox.Show("" + email + ":" + pwd + ", " + autoLogin.ToString() + ", " + rememberMe.ToString() + ", " + isLogin.ToString());

            this.Hide();
            _controller?.ShowMainScreen();
        }

        // 去登录
        private void onToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            IsLogin = true;
            IsRegister = false;
            this.ScreenTitle = "邮箱登录";
            this.LoginButtonTitle = "登录";
        }

        // 去注册
        private void onToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            IsLogin = false;
            IsRegister = true;
            this.ScreenTitle = "邮箱注册";
            this.LoginButtonTitle = "注册";
        }

        private void onMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void onCloseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
