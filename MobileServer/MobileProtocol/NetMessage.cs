using PENet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProtocol
{
    [Serializable]
    public class HOKMsg : KCPMsg
    {
        public CMD cmd;
        public ErrorCode error;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqMatch reqMatch;
        public RspMatch rspMatch;
    }

    #region 登录相关
    [Serializable]
    public class ReqLogin
    {
        public string acct;
        public string pass;
    }

    [Serializable]
    public class RspLogin
    {
        public UserData userData;
    }

    [Serializable]
    public class UserData
    {
        public uint id;
        public string name;
        public int lv;
        public int exp;
        public int coin;
        public int diamond;
        public int ticket;
        public List<HeroSelectData> heroSelectData;
    }

    [Serializable]
    public class HeroSelectData
    {
        public int heroID;
        //已拥有
        //本周限名
        //体验卡
    }
    #endregion

    #region 匹配确认
    [Serializable]
    public enum PVPEnum
    {
        None = 0,
        _1V1 = 1,
        _2V2 = 2,
        _5V5 = 3
    }
    [Serializable]
    public class ReqMatch
    {
        public PVPEnum pvpEnum;
    }

    [Serializable]
    public class RspMatch
    {
        public int predictTime;
    }
    #endregion

    //错误码
    public enum ErrorCode
    {
        None,
        AcctIsOnline,
    }

    //通信协议命令号
    public enum CMD
    {
        None = 0,
        //登录
        ReqLogin = 1,
        RspLogin = 2,

        //匹配
        ReqMatch = 3,
        RspMatch = 4,
    }
}
