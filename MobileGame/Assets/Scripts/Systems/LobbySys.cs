using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySys : SysRoot
{
    public static LobbySys Instance;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init LobbySys Done.");
    }
}
