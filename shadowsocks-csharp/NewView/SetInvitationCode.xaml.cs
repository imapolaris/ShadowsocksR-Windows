using Newtonsoft.Json.Linq;
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
    /// SetInvitationCode.xaml 的交互逻辑
    /// </summary>
    public partial class SetInvitationCode : Window
    {
        public string InviteCode
        {
            get => GetValue(InviteCodeProperty) as string;
            set => SetValue(InviteCodeProperty, value);
        }

        public static readonly DependencyProperty InviteCodeProperty = DependencyProperty.Register(@"InviteCode", typeof(string), typeof(SetInvitationCode));


        public SetInvitationCode()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void onCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "http://ss.yam.im/api/user/invitation/bind/" + "?access_token=" + CurrentUser.Token;
                var res = WebHelper.Get(url, null);
                if (res != null)
                {
                    JObject jObject = JObject.Parse(res);
                    int ret = (int)jObject["code"];
                    if (ret == 200)
                    {
                        MessageBox.Show("绑定邀请码成功！" + this.InviteCode);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("绑定邀请码失败！");
                    }
                }
                else
                {
                    MessageBox.Show("绑定邀请码失败！" + this.InviteCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("绑定邀请码失败！");
            }
        }
    }
}
