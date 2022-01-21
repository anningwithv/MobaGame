using MobileProtocol;
using UnityEngine;
using UnityEngine.UI;

public class LobbyWnd : WindowRoot
{
    public Text txtName;
    public Text txtLevel;
    public Text txtExp;
    public Image fgExp;

    public Text txtCoin;
    public Text txtDiamond;
    public Text txtTicket;

    public Transform transMatchRoot;
    public Text txtPredictTime;
    public Text txtCountTime;


    protected override void InitWnd()
    {
        base.InitWnd();

        SetActive(transMatchRoot, false);
        UserData ud = root.UserData;

        txtName.text = ud.name;
        txtLevel.text = "Lv." + ud.lv;
        txtExp.text = ud.exp + "/100000";
        fgExp.fillAmount = (ud.exp * 1.0f) / 100000;
        txtCoin.text = ud.coin.ToString();
        txtDiamond.text = ud.diamond.ToString();
        txtTicket.text = ud.ticket.ToString();
        txtCountTime.text = "00:00";
    }

    public void ShowMatchInfo(bool isActive, int predictTime = 0)
    {
        if (isActive)
        {
            SetActive(transMatchRoot);
            isMatching = true;
            int min = predictTime / 60;
            int sec = predictTime % 60;
            string minStr = min < 10 ? "0" + min + ":" : min.ToString() + ":";
            string secStr = sec < 10 ? "0" + sec : sec.ToString();
            txtPredictTime.text = "预计匹配时间：" + minStr + secStr;
        }
        else
        {
            SetActive(transMatchRoot, false);
            isMatching = false;
            timeCount = 0;
            deltaSum = 0;
        }
    }


    private int timeCount = 0;
    private float deltaSum = 0;
    private bool isMatching = false;
    private void Update()
    {
        if (isMatching)
        {
            float delta = Time.deltaTime;
            deltaSum += delta;
            if (deltaSum >= 1)
            {
                deltaSum -= 1;
                timeCount += 1;

                SetCountTime();
            }
        }
    }


    private void SetCountTime()
    {
        int min = timeCount / 60;
        int sec = timeCount % 60;
        string minStr = min < 10 ? "0" + min + ":" : min.ToString() + ":";
        string secStr = sec < 10 ? "0" + sec : sec.ToString();
        txtCountTime.text = minStr + secStr;
    }

    public void ClickMatchBtn()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch
            {
                pvpEnum = PVPEnum._1V1
            }
        };
        netSvc.SendMsg(msg);
    }

    public void ClickRankBtn()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch
            {
                pvpEnum = PVPEnum._2V2
            }
        };
        netSvc.SendMsg(msg);
    }

    public void ClickSettngs()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch
            {
                pvpEnum = PVPEnum._5V5
            }
        };
        netSvc.SendMsg(msg);
    }
}
