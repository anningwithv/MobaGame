using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PENet;
using System;
using MobileProtocol;

namespace HOKServer
{
    public class ServerSession : KCPSession<HOKMsg>
    {
        protected override void OnConnected()
        {
            this.ColorLog(PEUtils.LogColor.Green, "Client Online,Sid:{0}", m_sid);
        }

        protected override void OnDisConnected()
        {
            this.Warn("Client Offlien,Sid:{0}", m_sid);
        }

        protected override void OnReciveMsg(HOKMsg msg)
        {
            this.Log("RcvPack CMD:{0}", msg.cmd.ToString());
            NetSvc.Instance.AddMsgQue(this, msg);
        }

        protected override void OnUpdate(DateTime now)
        {
        }
    }
}
