using MobileProtocol;
using PENet;
using PEUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class MsgPack
    {
        public ServerSession session;
        public HOKMsg msg;
        public MsgPack(ServerSession session, HOKMsg msg)
        {
            this.session = session;
            this.msg = msg;
        }
    }

    public class NetSvc : Singleton<NetSvc>
    {
        public static readonly string pkgque_lock = "pkgque_lock";
        private KCPNet<ServerSession, HOKMsg> server = new KCPNet<ServerSession, HOKMsg>();
        private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();

        public override void Init()
        {
            base.Init();

            KCPTool.LogFunc = this.Log;
            KCPTool.WarnFunc = this.Warn;
            KCPTool.ErrorFunc = this.Error;
            KCPTool.ColorLogFunc = (color, msg) => {
                this.ColorLog((LogColor)color, msg);
            };

            server.StartAsServer(ServerConfig.LocalDevInnerIP, ServerConfig.UdpPort);
            this.Log("NetSvc Init Done.");
        }

        public void AddMsgQue(ServerSession session, HOKMsg msg)
        {
            lock (pkgque_lock)
            {
                msgPackQue.Enqueue(new MsgPack(session, msg));
            }
        }

        public override void Update()
        {
            base.Update();

            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    MsgPack msg = msgPackQue.Dequeue();
                    HandleMsg(msg);
                }
            }
        }

        //消息分发
        private void HandleMsg(MsgPack pack)
        {
            switch (pack.msg.cmd)
            {
                case CMD.ReqLogin:
                    LoginSys.Instance.ReqLogin(pack);
                    break;
                case CMD.ReqMatch:
                    MatchSys.Instance.ReqMatch(pack);
                    break;
                case CMD.None:
                default:
                    break;
            }
        }
    }
}
