using PEUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class NetSvc : Singleton<NetSvc> {
        public override void Init() {
            base.Init();

            this.Log("NetSvc Init Done.");
        }

        public override void Update() {
            base.Update();
        }
    }
}
