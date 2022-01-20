using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class CacheSvc : Singleton<CacheSvc> {
        public override void Init() {
            base.Init();

            this.Log("CacheSvc Init Done.");
        }

        public override void Update() {
            base.Update();
        }
    }
}
