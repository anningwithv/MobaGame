using PEUtils;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public Transform uiRoot;
    public TipsWnd tipsWnd;

    private NetSvc netSvc;
    private ResSvc resSvc;
    private AudioSvc audioSvc;

    private void Start()
    {
        Instance = this;

        LogConfig cfg = new LogConfig {
            enableLog = true,
            logPrefix = "",
            enableTime = true,
            logSeparate = ">",
            enableThreadID = true,
            enableTrace = true,
            enableSave = true,
            enableCover = true,
            saveName = "HOKClientPELog.txt",
            loggerType = LoggerType.Unity
        };
        PELog.InitSettings(cfg);
        PELog.ColorLog(LogColor.Green, "InitLogger.");
        DontDestroyOnLoad(this);
        InitRoot();
        PELog.Log("Init Root.");
        Init();
        PELog.Log("Init Done.");
    }

    private void Update()
    {

    }

    private void InitRoot()
    {
        for(int i = 0; i < uiRoot.childCount; i++)
        {
            Transform trans = uiRoot.GetChild(i);
            trans.gameObject.SetActive(false);
        }

        tipsWnd.SetWndState();
    }

    private void Init()
    {
        netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
        audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();

        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        LobbySys lobby = GetComponent<LobbySys>();
        lobby.InitSys();
        BattleSys battle = GetComponent<BattleSys>();
        battle.InitSys();

        //login
        PELog.Log("EnterLogin.");
        login.EnterLogin();
    }

    public void AddTips(string tips)
    {
        tipsWnd.AddTips(tips);
    }
}
