using MobileProtocol;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    public InputField iptAcct;
    public InputField iptPass;
    public Toggle togSrv;

    protected override void InitWnd()
    {
        base.InitWnd();

        System.Random rd = new System.Random();
        iptAcct.text = rd.Next(100, 999).ToString();
        iptPass.text = rd.Next(100, 999).ToString();
    }

    public void ClickLoginBtn()
    {
        audioSvc.PlayUIAudio("loginBtnClick");
        if(iptAcct.text.Length >= 3 && iptPass.text.Length >= 3)
        {
            HOKMsg msg = new HOKMsg
            {
                cmd = CMD.ReqLogin,
                reqLogin = new ReqLogin
                {
                    acct = iptAcct.text,
                    pass = iptPass.text
                }
            };
            netSvc.SendMsg(msg, (bool result) => {
                if (result == false)
                {
                    netSvc.InitSvc();
                }
            });
        }
        else
        {
            //POP Tips
            root.ShowTips("账号或密码为空");
        }
    }
}
