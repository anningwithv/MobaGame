using PEUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class ServerRoot : Singleton<ServerRoot>
    {
        public override void Init() {
            base.Init();

            //日志
            PELog.InitSettings();

            //服务
            CacheSvc.Instance.Init();
            TimerSvc.Instance.Init();
            NetSvc.Instance.Init();

            //业务
            LoginSys.Instance.Init();
            MatchSys.Instance.Init();
            RoomSys.Instance.Init();

            this.ColorLog(LogColor.Green, "ServerRoot Init Done.");
        }

        public override void Update() {
            base.Update();

            //服务
            CacheSvc.Instance.Update();
            TimerSvc.Instance.Update();
            NetSvc.Instance.Update();

            //业务
            LoginSys.Instance.Update();
            MatchSys.Instance.Update();
            RoomSys.Instance.Update();
        }
    }
}
