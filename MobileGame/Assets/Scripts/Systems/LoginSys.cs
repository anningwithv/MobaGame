using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSys : SysRoot
{
    public static LoginSys Instance;

    public LoginWnd loginWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init LoginSys Done.");
    }

    public void EnterLogin() {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(NameDefine.MainCityBGMusic);
    }
}
