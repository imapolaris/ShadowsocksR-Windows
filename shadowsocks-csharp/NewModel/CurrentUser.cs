using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.NewModel
{
    static class CurrentUser
    {
        public static bool IsLogined { get; set; } = false;

        // email
        public static string Email { get; set; }

        // user_id
        public static string User_Id { get; set; }

        // token
        public static string Token { get; set; }

        public static DateTime LoginDate { get; set; } = DateTime.Now;

        public static string LoginDateStr
        {
            get
            {
                return LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public static void Reset()
        {
            IsLogined = false;
            Email = "";
            User_Id = "";
            Token = "";
        }
    }
}
