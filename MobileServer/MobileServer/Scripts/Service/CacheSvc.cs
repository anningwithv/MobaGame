using MobileProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class CacheSvc : Singleton<CacheSvc>
    {
        //acct-session
        private Dictionary<string, ServerSession> _onLineAcctDic;
        //seesion-userdata
        private Dictionary<ServerSession, UserData> _onLineSessionDic;

        public override void Init()
        {
            base.Init();

            _onLineAcctDic = new Dictionary<string, ServerSession>();
            _onLineSessionDic = new Dictionary<ServerSession, UserData>();

            this.Log("CacheSvc Init Done.");
        }

        public override void Update()
        {
            base.Update();
        }

        public bool IsAcctOnLine(string acct)
        {
            return _onLineAcctDic.ContainsKey(acct);
        }

        public void AcctOnline(string acct, ServerSession session, UserData playerData)
        {
            _onLineAcctDic.Add(acct, session);
            _onLineSessionDic.Add(session, playerData);
        }
    }
}
