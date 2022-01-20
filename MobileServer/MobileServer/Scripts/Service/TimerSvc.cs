using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class TimerSvc : Singleton<TimerSvc> {
        public override void Init() {
            base.Init();

            this.Log("TimeSvc Init Done.");
        }

        public override void Update() {
            base.Update();
        }
    }
}
