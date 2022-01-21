using MobileProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class PVPRoom
    {
        public uint roomID;
        public PVPEnum pvpEnum = PVPEnum.None;
        public ServerSession[] sessionArr;

        public PVPRoom(uint roomID, PVPEnum pvpEnum, ServerSession[] sessionArr)
        {
            this.roomID = roomID;
            this.pvpEnum = pvpEnum;
            this.sessionArr = sessionArr;
        }
    }
}
