using System.Collections.Generic;
using UnityEngine;

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;

    public void InitSvc()
    {
        Instance = this;
        this.Log("Init ResSvc Done.");
    }

    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip au = null;
        if(!adDic.TryGetValue(path, out au)) {
            au = Resources.Load<AudioClip>(path);
            if(cache) {
                adDic.Add(path, au);
            }
        }
        return au;
    }

}
