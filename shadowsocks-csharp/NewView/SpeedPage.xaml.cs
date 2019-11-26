using Newtonsoft.Json.Linq;
using Shadowsocks.Controller;
using Shadowsocks.Model;
using Shadowsocks.NewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class SpeedPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged( string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private NodeModel _currentNode;
        public NodeModel CurrentNode
        {
            get => _currentNode;
            private set
            {
                if (_currentNode != value)
                {
                    _currentNode = value;
                    OnPropertyChanged();
                }
            }
        }

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


        public string Remarks
        {
            get => GetValue(RemarksProperty) as string;
            set => SetValue(RemarksProperty, value);
        }

        public static readonly DependencyProperty RemarksProperty = DependencyProperty.Register(@"Remarks", typeof(string), typeof(SpeedPage));

        public string ButtonTitle
        {
            get => GetValue(ButtonTitleProperty) as string;
            set => SetValue(ButtonTitleProperty, value);
        }

        public static readonly DependencyProperty ButtonTitleProperty = DependencyProperty.Register(@"ButtonTitle", typeof(string), typeof(SpeedPage));


        private readonly ShadowsocksController _controller;
        public SpeedPage(ShadowsocksController controller)
        {
            InitializeComponent();

            this.DataContext = this;

            this.Delay = "--";
            this.Loss = "--";
            this.Promote = "--";

            this.ButtonTitle = "开始加速";

            _controller = controller;
            _controller.ConfigChanged += _controller_ConfigChanged; ;
        }

        private void _controller_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();

            ReadConfig();
        }

        private Configuration _modifiedConfiguration;
        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = _controller.GetConfiguration();
            _modifiedConfiguration.sysProxyMode = ProxyMode.Global;
        }

            // 开始加速
        private void onSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ButtonTitle == "开始加速")
            {
                if (CurrentNode == null)
                    return;

                bool res = SaveConfig();
                if (res)
                {
                    CurrentUser.IsSpeeding = true;
                    this.ButtonTitle = "停止加速";
                }
                else
                {
                    MessageBox.Show("启动加速失败！");
                }
            }
            else
            {
                this.Stop();
                this.ButtonTitle = "开始加速";
                CurrentUser.IsSpeeding = false;
            }
            
        }

        private bool SaveConfig()
        {
            string oldServerId = null;
            if (_modifiedConfiguration.index >= 0 && _modifiedConfiguration.index < _modifiedConfiguration.configs.Count)
            {
                oldServerId = _modifiedConfiguration.configs[_modifiedConfiguration.index].Id;
            }
            _modifiedConfiguration.configs.Clear();
            Server[] servers = new Server[] { CurrentNode.GetServer() };
            _modifiedConfiguration.configs.AddRange(servers);
            if (oldServerId != null)
            {
                var currentIndex = _modifiedConfiguration.configs.FindIndex(server => server.Id == oldServerId);
                if (currentIndex != -1)
                {
                    _modifiedConfiguration.index = currentIndex;
                }
            }

            if (_modifiedConfiguration.configs.Count == 0)
            {
                MessageBox.Show("请选择至少一条加速路线");
                return false;
            }

            _controller.SaveServersConfig(_modifiedConfiguration);
            return true;
        }

        private void Stop()
        {
            _controller.Stop();
        }

        private Server _currentServer;

        public Server CurrentServer
        {
            get => _currentServer;
            private set
            {
                if (_currentServer != value)
                {
                    _currentServer = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ReadConfig()
        {
            var config = _controller.GetCurrentConfiguration();
            if (config != null && config.configs != null && config.configs.Count > 0)
            {
                CurrentServer = config.configs[0];
                if (CurrentNode == null)
                {
                    CurrentNode = new NodeModel();
                    CurrentNode.GetNode(CurrentServer);
                    // traceName.Text = CurrentNode.Remarks;
                    this.Remarks = CurrentNode.Remarks;

                    if (CurrentUser.IsSpeeding)
                    {
                        this.ButtonTitle = "停止加速";
                    }
                }
            }
        }

        // 选择线路
        private void onChooseTraceButton_Click(object sender, RoutedEventArgs e)
        {
            List<NodeModel> nodes = fetchData();
            TracePage page = new TracePage(nodes);
            page.ChooseNodeEvent += Page_ChooseNodeEvent;
            if (nodes != null && nodes.Count > 0)
            {
                NavigationService.Navigate(page);
            }
        }

        private void Page_ChooseNodeEvent(object sender, EventArgs e)
        {
            NodeModel node = sender as NodeModel;
            CurrentNode = node;

            if (CurrentNode != null)
            {
                // traceName.Text = CurrentNode.Remarks;
                this.Remarks = CurrentNode.Remarks;
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
                            nodeModel.Server_Port = (ushort)node["server_port"];
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCurrentConfiguration();

            ReadConfig();
        }
    }
}
