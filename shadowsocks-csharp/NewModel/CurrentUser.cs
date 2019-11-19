using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.NewModel
{
    static class CurrentUser
    {
        public static bool IsLogined { get; set; } = false;

        // user_id
        public static string User_Id { get; set; }

        // token
        public static string Token { get; set; }
    }
}
