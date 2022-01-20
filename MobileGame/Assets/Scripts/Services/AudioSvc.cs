using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioSvc : MonoBehaviour {
    public static AudioSvc Instance;
    public bool TurnOnVoice;
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitSvc() {
        Instance = this;
        this.Log("Init AudioSvc Done.");
    }

    public void PlayBGMusic(string name, bool isLoop = true) {
        if(!TurnOnVoice) {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if(bgAudio.clip == null || bgAudio.clip.name != audio.name) {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name) {
        if(!TurnOnVoice) {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        uiAudio.clip = audio;
        uiAudio.Play();
    }
}
