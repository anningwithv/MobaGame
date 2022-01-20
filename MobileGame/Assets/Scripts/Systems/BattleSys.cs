using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot
{
    public static BattleSys Instance;

    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        this.Log("Init BattleSys Done.");
    }
}
