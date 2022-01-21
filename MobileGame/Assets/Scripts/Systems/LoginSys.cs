using MobileProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSys : SysRoot
{
    public static LoginSys Instance;

    public LoginWnd loginWnd;
    public StartWnd startWnd;

    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        this.Log("Init LoginSys Done.");
    }

    public void EnterLogin()
    {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(NameDefine.MainCityBGMusic);
    }

    /// <summary>
    /// Handle server login response
    /// </summary>
    /// <param name="msg"></param>
    public void RspLogin(HOKMsg msg)
    {
        root.ShowTips("登录成功");
        root.UserData = msg.rspLogin.userData;

        startWnd.SetWndState();
        loginWnd.SetWndState(false);
    }

    public void EnterLobby()
    {
        startWnd.SetWndState(false);
        LobbySys.Instance.EnterLobby();
    }
}
