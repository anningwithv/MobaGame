using MobileProtocol;
using PENet;
using System;

public class ClientSession : KCPSession<HOKMsg>
{
    protected override void OnConnected()
    {
        GameRoot.Instance.ShowTips("连接服务器成功");
    }

    protected override void OnDisConnected()
    {
        GameRoot.Instance.ShowTips("断开服务器连接");
    }

    protected override void OnReciveMsg(HOKMsg msg)
    {
        NetSvc.Instance.AddMsgQue(msg);
    }

    protected override void OnUpdate(DateTime now)
    {
    }
}
