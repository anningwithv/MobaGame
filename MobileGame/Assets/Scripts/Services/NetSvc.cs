using MobileProtocol;
using PENet;
using PEUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetSvc : MonoBehaviour
{
    public static NetSvc Instance;

    public static readonly string pkgque_lock = "pkgque_lock";
    private KCPNet<ClientSession, HOKMsg> client = null;
    private Queue<HOKMsg> msgPackQue = null;
    private Task<bool> checkTask = null;

    public void InitSvc()
    {
        Instance = this;
        client = new KCPNet<ClientSession, HOKMsg>();
        msgPackQue = new Queue<HOKMsg>();

        KCPTool.LogFunc = this.Log;
        KCPTool.WarnFunc = this.Warn;
        KCPTool.ErrorFunc = this.Error;
        KCPTool.ColorLogFunc = (color, msg) => {
            this.ColorLog((LogColor)color, msg);
        };
        string srvIP = ServerConfig.LocalDevInnerIP;
        LoginSys login = LoginSys.Instance;
        if (login != null)
        {
            if (login.loginWnd.togSrv.isOn)
            {
                //TODO
            }
        }

        client.StartAsClient(srvIP, ServerConfig.UdpPort);
        checkTask = client.ConnectServer(100);
        this.Log("Init NetSvc Done.");
    }

    public void AddMsgQue(HOKMsg msg)
    {
        lock (pkgque_lock)
        {
            msgPackQue.Enqueue(msg);
        }
    }

    private int counter = 0;
    public void Update()
    {
        if (checkTask != null && checkTask.IsCompleted)
        {
            if (checkTask.Result)
            {
                //GameRoot.Instance.ShowTips("连接服务器成功");
                this.ColorLog(PEUtils.LogColor.Green, "ConnectServer Success.");
                checkTask = null;
                //todo send ping msg
            }
            else
            {
                ++counter;
                if (counter > 4)
                {
                    this.Error(string.Format("Connect Failed {0} Times,Check Your Network Connection.", counter));
                    GameRoot.Instance.ShowTips("无法连接服务器，请检查网络状况");
                    checkTask = null;
                }
                else
                {
                    this.Warn(string.Format("Connect Failed {0} Times,Retry...", counter));
                    checkTask = client.ConnectServer(100);
                }
            }
        }

        if (client != null && client.clientSession != null)
        {
            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    HOKMsg msg = msgPackQue.Dequeue();
                    HandleMsg(msg);
                }
            }
        }
    }

    /// <summary>
    /// Send msg to server
    /// </summary>
    public void SendMsg(HOKMsg msg, Action<bool> cb = null)
    {
        this.ColorLog(PEUtils.LogColor.None, "SendMsg: " + msg.ToString());
        if (client.clientSession != null && client.clientSession.IsConnected())
        {
            client.clientSession.SendMsg(msg);
            cb?.Invoke(true);
        }
        else
        {
            GameRoot.Instance.ShowTips("服务器未连接");
            this.Error("服务器未连接");
            cb?.Invoke(false);
        }
    }

    /// <summary>
    /// Handle msg from server
    /// </summary>
    /// <param name="msg"></param>
    private void HandleMsg(HOKMsg msg)
    {
        if (msg.error != ErrorCode.None)
        {
            switch (msg.error)
            {
                case ErrorCode.AcctIsOnline:
                    GameRoot.Instance.ShowTips("当前账号已经上线");
                    break;
                default:
                    break;
            }
            return;
        }

        switch (msg.cmd)
        {
            case CMD.RspLogin:
                LoginSys.Instance.RspLogin(msg);
                break;
            case CMD.RspMatch:
                LobbySys.Instance.RspMatch(msg);
                break;
            default:
                break;
        }
    }

}
