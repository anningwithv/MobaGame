using MobileProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer
{
    public class RoomSys : SystemRoot<RoomSys>
    {
        private List<PVPRoom> _pvpRoomLst = null;
        private Dictionary<uint, PVPRoom> _pvpRoomDic = null;

        public override void Init()
        {
            base.Init();

            _pvpRoomLst = new List<PVPRoom>();
            _pvpRoomDic = new Dictionary<uint, PVPRoom>();
            this.Log("RoomSys Init Done.");
        }

        public void AddPVPRoom(ServerSession[] sessionArr, PVPEnum pvp)
        {
            uint roomID = GetUniqueRoomID();
            PVPRoom room = new PVPRoom(roomID, pvp, sessionArr);
            _pvpRoomLst.Add(room);
            _pvpRoomDic.Add(roomID, room);
        }

        public override void Update() {
            base.Update();
        }

        uint roomID = 0;
        public uint GetUniqueRoomID()
        {
            roomID += 1;
            return roomID;
        }
    }
}
