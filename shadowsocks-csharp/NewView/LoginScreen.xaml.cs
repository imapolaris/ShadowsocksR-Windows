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
using Newtonsoft.Json;
using Shadowsocks.NewModel;
using Newtonsoft.Json.Linq;
using System.IO;

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


        public bool LoginAsGuest
        {
            get => (bool)GetValue(LoginAsGuestProperty);
            set => SetValue(LoginAsGuestProperty, value);
        }
        public static readonly DependencyProperty LoginAsGuestProperty = DependencyProperty.Register(@"LoginAsGuest", typeof(bool), typeof(LoginScreen));


        public LoginScreen(ShadowsocksController controller)
        {
            InitializeComponent();

            _controller = controller;

            this.DataContext = this;

            this.IsLogin = true;
            this.LoginAsGuest = false;
            this.IsRegister = false;
            this.ScreenTitle = "邮箱登录";
            this.LoginButtonTitle = "登录";
            this.Email = "";
            this.Password = "";
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

            if (this.LoginAsGuest)
            {
                try
                {
                    var res = loginAsGuest();
                    if (res != null)
                    {
                        JObject jObject = JObject.Parse(res);
                        int ret = (int)jObject["ret"];
                        if (ret == 1)
                        {
                            JObject data = jObject["data"] as JObject;
                            CurrentUser.IsLogined = true;
                            CurrentUser.User_Id = (string)data["user_id"];
                            CurrentUser.Token = (string)data["token"];
                            CurrentUser.LoginDate = DateTime.Now;
                            CurrentUser.Email = Email;

                            // 登录成功
                            Close();
                            _controller?.ShowMainScreen();
                        }
                        else
                        {
                            MessageBox.Show("登录失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("登录失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("登录失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return;
            }
            // MessageBox.Show("" + email + ":" + pwd + ", " + autoLogin.ToString() + ", " + rememberMe.ToString() + ", " + isLogin.ToString());
            if (isLogin)
            {
                try
                {
                    var res = login(email, pwd);
                    if (res != null)
                    {
                        JObject jObject = JObject.Parse(res);
                        int ret = (int)jObject["ret"];
                        if (ret == 1)
                        {
                            JObject data = jObject["data"] as JObject;
                            CurrentUser.IsLogined = true;
                            CurrentUser.User_Id = (string)data["user_id"];
                            CurrentUser.Token = (string)data["token"];
                            CurrentUser.LoginDate = DateTime.Now;
                            CurrentUser.Email = Email;

                            if (rememberMe || autoLogin)
                            {
                                string content = email + "," + pwd + "," + (rememberMe ? 1 : 0) + "," + (autoLogin ? 1 : 0);
                                writeFile(content);
                            }
                            else
                            {
                                writeFile("");
                            }

                            // 登录成功
                            Close();
                            _controller?.ShowMainScreen();
                        }
                        else
                        {
                            MessageBox.Show("登录失败，请检查输入信息是否正确！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("登录失败，请检查输入信息是否正确！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("登录失败，请检查输入信息是否正确！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    var res = register(email, pwd);
                    if (res != null)
                    {
                        JObject jObject = JObject.Parse(res);
                        int ret = (int)jObject["code"];
                        if (ret == 200)
                        {
                            MessageBox.Show("注册成功！正在进入登录界面", "提示", MessageBoxButton.OK, MessageBoxImage.Information);

                            onToLoginButton_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("注册失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("注册失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("注册失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // 登录
        private string login(string email, string pwd)
        {
            string url = "http://ss.yam.im/api/token";
            string json = "{" + "\"email\":\"" + email + "\", \"passwd\":\"" + pwd + "\"," +"\"device_id\":" + "32231" + "}";
            return WebHelper.Post(url, json);
        }

        // 游客登录
        private string loginAsGuest()
        {
            string url = "http://ss.yam.im/api/guest/token";
            string json = "{" + "\"device_id\":" + "32231" + "}";
            return WebHelper.Post(url, json);
        }

        // 注册
        private string register(string email, string pwd)
        {
            string url = "http://ss.yam.im/api/user/register";
            string json = "{" + "\"email\":\"" + email
                + "\", \"passwd\":\"" + pwd
                + "\", \"repasswd\":\"" + pwd
                + "\", \"imtype\":1"
                + ", \"name\":\"" + email
                + "\", \"code\":1"
                + ", \"wechat\":\"" + Guid.NewGuid().ToString() + "\"}";
            return WebHelper.Post(url, json);
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

            this.LoginAsGuest = false;
        }

        private void onMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void onCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

            init();
        }

        private void init()
        {
            // 格式：用户名，密码，记住密码-0|1，自动登录-0|1
            string content = readFile();
            Console.WriteLine(content);
            string[] str = content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (str == null || str.Length < 4)
                return;
            else
            {
                this.Email = str[0];
                this.Password = str[1];
                this.RememberPassword = int.Parse(str[2]) == 1;
                this.AutoLogin = int.Parse(str[3]) == 1;
            }
        }

        private const string fileName = "appconfig";
        private string readFile()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    Console.WriteLine("file exists");

                    string str = "";
                    using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                    {
                        int lineCount = 0;
                        while (sr.Peek() > 0)
                        {
                            lineCount++;
                            string temp = sr.ReadLine();
                            str += temp;
                        }
                    }
                    return str;
                }
                else
                {
                    Console.WriteLine("file not exists");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void writeFile(string content)
        {
            try
            {
                Byte[] bytes = Encoding.UTF8.GetBytes(content);
                /*string str = "";
                for (int i = 0; i < bytes.Length; i++)
                {
                    str += bytes[i].ToString();
                }*/
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        sw.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private void autoLoginCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.AutoLogin)
            {
                this.RememberPassword = true;
            }
        }

        private void rememberMeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!this.RememberPassword)
            {
                this.AutoLogin = false;
            }
        }

        private void loginAsGuestCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.LoginAsGuest)
            {
                this.ScreenTitle = "游客登录";
                this.Email = "Guest";
                this.Password = "12345678";

                this.rememberPwd.Visibility = Visibility.Collapsed;
                this.autoLogin.Visibility = Visibility.Collapsed;

                this.emailText.IsEnabled = false;
                this.pwdText.IsEnabled = false;
            }
        }

        private void loginAsGuestCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!this.LoginAsGuest)
            {
                this.ScreenTitle = "邮箱登录";
                this.Email = "";
                this.Password = "";

                this.rememberPwd.Visibility = Visibility.Visible;
                this.autoLogin.Visibility = Visibility.Visible;

                this.emailText.IsEnabled = true;
                this.pwdText.IsEnabled = true;
            }
        }
    }
}
