using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.NewModel
{
    public class NodeModel
    {
        public string Remarks { get; set; }
        public string Server { get; set; }
        public ushort Server_Port { get; set; }
        public string Method { get; set; }
        public string Group { get; set; }
        public string Obfs { get; set; }
        public string Obfsparam { get; set; }
        public string Remarks_Base64 { get; set; }
        public string Password { get; set; }
        public bool Tcp_Over_Udp { get; set; }
        public bool Udp_Over_Tcp { get; set; }
        public string Protocol { get; set; }
        public bool Obfs_Udp { get; set; }
        public bool Enable { get; set; }
        public string Sublink { get; set; }

        public bool Used { get; set; } = false;

        public Model.Server GetServer()
        {
            Model.Server server = new Model.Server();
            server.Enable = Enable;
            server.Group = Group;
            server.Method = Method;
            server.obfs = Obfs;
            server.ObfsParam = Obfsparam;
            server.Password = Password;
            server.Protocol = Protocol;
            server.Remarks = Remarks;
            server.Remarks_Base64 = Remarks_Base64;
            server.server = Server;
            server.Server_Port = Server_Port;
            server.ProtocolParam = "18:PEUvBV"; // TODO:让后台添加此参数

            return server;
        }
    }
}
