using System;
using System.Threading;

namespace HOKServer {
    class ServerStart {
        static void Main(string[] args) {
            ServerRoot.Instance.Init();

            while(true) {
                ServerRoot.Instance.Update();
                Thread.Sleep(10);
            }
        }
    }
}
