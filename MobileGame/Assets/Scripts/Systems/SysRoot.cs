using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SysRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetSvc netSvc;
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public virtual void InitSys() {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}
