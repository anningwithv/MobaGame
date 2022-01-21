using MobileProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class MatchSys : SystemRoot<MatchSys>
    {
        private Queue<ServerSession> que1V1 = null;
        private Queue<ServerSession> que2V2 = null;
        private Queue<ServerSession> que5V5 = null;

        public override void Init()
        {
            base.Init();
            que1V1 = new Queue<ServerSession>();
            que2V2 = new Queue<ServerSession>();
            que5V5 = new Queue<ServerSession>();

            this.Log("MatchSys Init Done.");
        }

        public override void Update()
        {
            base.Update();

            while (que1V1.Count >= 2)
            {
                ServerSession[] sessionArr = new ServerSession[2];
                for (int i = 0; i < 2; i++)
                {
                    sessionArr[i] = que1V1.Dequeue();
                }
                RoomSys.Instance.AddPVPRoom(sessionArr, PVPEnum._1V1);
            }

            while (que2V2.Count >= 4)
            {
                ServerSession[] sessionArr = new ServerSession[4];
                for (int i = 0; i < 4; i++)
                {
                    sessionArr[i] = que2V2.Dequeue();
                }
                RoomSys.Instance.AddPVPRoom(sessionArr, PVPEnum._2V2);
            }

            while (que5V5.Count >= 10)
            {
                ServerSession[] sessionArr = new ServerSession[10];
                for (int i = 0; i < 10; i++)
                {
                    sessionArr[i] = que5V5.Dequeue();
                }
                RoomSys.Instance.AddPVPRoom(sessionArr, PVPEnum._5V5);
            }
        }

        /// <summary>
        /// Handle match msg from client
        /// </summary>
        public void ReqMatch(MsgPack pack)
        {
            ReqMatch data = pack.msg.reqMatch;
            PVPEnum pvpEnum = data.pvpEnum;
            switch (pvpEnum)
            {
                case PVPEnum._1V1:
                    que1V1.Enqueue(pack.session);
                    break;
                case PVPEnum._2V2:
                    que2V2.Enqueue(pack.session);
                    break;
                case PVPEnum._5V5:
                    que5V5.Enqueue(pack.session);
                    break;
                case PVPEnum.None:
                default:
                    this.Error("PVPType Error:" + pvpEnum.ToString());
                    break;
            }

            HOKMsg msg = new HOKMsg
            {
                cmd = CMD.RspMatch,
                rspMatch = new RspMatch
                {
                    predictTime = GetPredictTime(pvpEnum),
                }
            };
            pack.session.SendMsg(msg);
        }

        private int GetPredictTime(PVPEnum pvpEnum)
        {
            int waitCount;
            switch (pvpEnum)
            {
                case PVPEnum._1V1:
                    waitCount = 2 - que1V1.Count;
                    if (waitCount < 0)
                    {
                        waitCount = 0;
                    }
                    return waitCount * 10 + 5;
                case PVPEnum._2V2:
                    waitCount = 4 - que2V2.Count;
                    if (waitCount < 0)
                    {
                        waitCount = 0;
                    }
                    return waitCount * 10 + 5;
                case PVPEnum._5V5:
                    waitCount = 10 - que5V5.Count;
                    if (waitCount < 0)
                    {
                        waitCount = 0;
                    }
                    return waitCount * 10 + 5;
                default:
                    return 0;
            }
        }
    }
}
