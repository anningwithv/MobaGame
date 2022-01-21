using MobileProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySys : SysRoot
{
    public static LobbySys Instance;

    public LobbyWnd lobbyWnd;

    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        this.Log("Init LobbySys Done.");
    }

    public void EnterLobby()
    {
        lobbyWnd.SetWndState();
    }

    public void RspMatch(HOKMsg msg)
    {
        int predictTime = msg.rspMatch.predictTime;
        lobbyWnd.ShowMatchInfo(true, predictTime);
    }
}
