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
    /// TracePage.xaml 的交互逻辑
    /// </summary>
    public partial class TracePage : Page
    {
        private readonly List<NodeModel> _nodes;

        public event EventHandler ChooseNodeEvent;

        public TracePage(List<NodeModel> nodes)
        {
            InitializeComponent();

            _nodes = nodes;
        }

        private void onBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.nodeList.ItemsSource = _nodes;
        }
        

        private void onNodeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NodeModel node = (sender as Button).DataContext as NodeModel;
            if (node == null)
                return;

            ChooseNodeEvent?.Invoke(node, default);
        }
    }
}
