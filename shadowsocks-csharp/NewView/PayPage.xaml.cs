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
    /// PayPage.xaml 的交互逻辑
    /// </summary>
    public partial class PayPage : Page
    {
        private List<PayModel> _nodes;

        public PayModel CurrentPayModel
        {
            get => GetValue(CurrentPayModelProperty) as PayModel;
            set => SetValue(CurrentPayModelProperty, value);
        }

        public static readonly DependencyProperty CurrentPayModelProperty = DependencyProperty.Register(@"CurrentPayModel", typeof(PayModel), typeof(PayPage));

        public string CurrentPrice
        {
            get => GetValue(CurrentPriceProperty) as string;
            set => SetValue(CurrentPriceProperty, value);
        }

        public static readonly DependencyProperty CurrentPriceProperty = DependencyProperty.Register(@"CurrentPrice", typeof(string), typeof(PayPage));


        public PayPage()
        {
            InitializeComponent();

            this.DataContext = this;

            _nodes = new List<PayModel>();
            CurrentPayModel = new PayModel();
            CurrentPrice = "0";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fetchData();
        }

        private void onPayItemButton_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPayModel = (sender as Button).DataContext as PayModel;
            CurrentPrice = CurrentPayModel.Price;
            string id = CurrentPayModel.Id;
            List<PayModel> tempNodes = new List<PayModel>();
            for (int i = 0; i < _nodes.Count; i++)
            {
                PayModel model = _nodes[i];
                if (id == model.Id)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    Uri uri = new Uri("pack://application:,,,/Resources/pay_bg.png", UriKind.Absolute);
                    imageBrush.ImageSource = new BitmapImage(uri);
                    model.ItemBackground = imageBrush;
                }
                else
                {
                    model.ItemBackground = null;
                }

                tempNodes.Add(model);
            }
            this.nodeList.ItemsSource = tempNodes;
            this._nodes = tempNodes;
        }

        private void onPaySubmitButton_Click(object sender, RoutedEventArgs e)
        {
            pay();
        }

        private void fetchData()
        {
            if (_nodes != null && _nodes.Count > 0)
            {
                return;
            }
            try
            {
                string url = "http://ss.yam.im/api/shop/list?access_token=" + CurrentUser.Token;
                var res = WebHelper.Get(url, null);
                if (res != null)
                {
                    JObject jObject = JObject.Parse(res);
                    int ret = (int)jObject["ret"];
                    if (ret == 1)
                    {
                        JObject arr = jObject["arr"] as JObject;
                        if (arr != null)
                        {
                            JArray shops = arr["shops"] as JArray;
                            if (shops != null && shops.Count > 0)
                            {
                                for (int i = 0; i < shops.Count; ++i)
                                {
                                    JObject shop = shops[i] as JObject;
                                    if (shop != null)
                                    {
                                        PayModel payModel = new PayModel();
                                        payModel.Id = (string)shop["id"];
                                        payModel.Name = (string)shop["name"];
                                        payModel.Price = (string)shop["price"];
                                        payModel.Content = (string)shop["content"];
                                        payModel.Auto_Renew = (int)shop["auto_renew"];
                                        payModel.Auto_Reset_Bandwidth = (int)shop["auto_reset_bandwidth"];
                                        payModel.Status = (int)shop["status"];

                                        _nodes.Add(payModel);
                                    }
                                }
                            }

                            this.nodeList.ItemsSource = _nodes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void pay()
        {
            return;
            string url = "http://ss.yam.im/api/pay/jm/?access_token=" + CurrentUser.Token;
            var res = WebHelper.Get(url, null);
        }
    }
}
