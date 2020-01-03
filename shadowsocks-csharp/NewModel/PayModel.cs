using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Shadowsocks.NewModel
{
    public class PayModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceText {
            get
            {
                return "￥" + this.Price;
            }
        }
        public string Content { get; set; }
        public int Auto_Renew { get; set; }
        public int Auto_Reset_Bandwidth { get; set; }
        public int Status { get; set; }

        public Brush ItemBackground { get; set; } = null;
    }
}
