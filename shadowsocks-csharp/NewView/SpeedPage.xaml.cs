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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shadowsocks.NewView
{
    /// <summary>
    /// SpeedPage.xaml 的交互逻辑
    /// </summary>
    public partial class SpeedPage : Page
    {
        public static NodeModel CurrentNode { get; set; }

        // 延迟
        public string Delay
        {
            get => GetValue(DelayProperty) as string;
            set => SetValue(DelayProperty, value);
        }

        public static readonly DependencyProperty DelayProperty = DependencyProperty.Register(@"Delay", typeof(string), typeof(SpeedPage));

        // 丢包
        public string Loss
        {
            get => GetValue(LossProperty) as string;
            set => SetValue(LossProperty, value);
        }

        public static readonly DependencyProperty LossProperty = DependencyProperty.Register(@"Loss", typeof(string), typeof(SpeedPage));

        // 提升
        public string Promote
        {
            get => GetValue(PromoteProperty) as string;
            set => SetValue(PromoteProperty, value);
        }

        public static readonly DependencyProperty PromoteProperty = DependencyProperty.Register(@"Promote", typeof(string), typeof(SpeedPage));

        public SpeedPage()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Delay = "--";
            this.Loss = "--";
            this.Promote = "--";
        }

        // 开始加速
        private void onSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // 选择线路
        private void onChooseTraceButton_Click(object sender, RoutedEventArgs e)
        {
            List<NodeModel> nodes = fetchData();
            if (nodes != null && nodes.Count > 0)
            {
                NavigationService.Navigate(new TracePage(nodes));
            }
        }

        private List<NodeModel> fetchData()
        {
            try
            {
                string url = "http://ss.yam.im/api/node";
                string[] paramList = new string[] { "access_token=" + CurrentUser.Token };
                string res = WebHelper.Get(url, paramList);
                if (res == null)
                {
                    MessageBox.Show("没有查询到节点！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                List<NodeModel> nodes = new List<NodeModel>();
                if (nodes == null)
                {
                    nodes = new List<NodeModel>();
                }
                else
                {
                    nodes.Clear();
                }
                JObject jObject = JObject.Parse(res);
                int ret = (int)jObject["ret"];
                if (ret == 1)
                {
                    JArray jArray = jObject["data"] as JArray;
                    for (int i = 0; i < jArray.Count; ++i)
                    {
                        JObject node = jArray[i] as JObject;
                        if (node != null)
                        {
                            NodeModel nodeModel = new NodeModel();
                            nodeModel.Remarks = (string)node["remarks"];
                            nodeModel.Server = (string)node["server"];
                            nodeModel.Server_Port = (int)node["server_port"];
                            nodeModel.Method = (string)node["method"];
                            nodeModel.Group = (string)node["group"];
                            nodeModel.Obfs = (string)node["obfs"];
                            nodeModel.Obfsparam = (string)node["obfsparam"];
                            nodeModel.Remarks_Base64 = (string)node["remarks_base64"];
                            nodeModel.Password = (string)node["password"];
                            nodeModel.Tcp_Over_Udp = (bool)node["tcp_over_udp"];
                            nodeModel.Udp_Over_Tcp = (bool)node["udp_over_tcp"];
                            nodeModel.Protocol = (string)node["protocol"];
                            nodeModel.Obfs_Udp = (bool)node["obfs_udp"];
                            nodeModel.Enable = (bool)node["enable"];
                            nodeModel.Sublink = (string)node["sublink"];

                            nodes.Add(nodeModel);
                        }
                    }

                    return nodes;
                }
                else
                {
                    MessageBox.Show("没有查询到节点！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有查询到节点！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
