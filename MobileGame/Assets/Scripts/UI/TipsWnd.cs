using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TipsWnd : WindowRoot
{
    public Image bgTips;
    public Text txtTips;
    public Animator ani;

    private bool isTipsShow = false;
    private Queue<string> tipsQue = new Queue<string>();

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(bgTips, false);
        tipsQue.Clear();
    }

    private void Update()
    {
        if(tipsQue.Count > 0 && isTipsShow == false)
        {
            string tips = tipsQue.Dequeue();
            isTipsShow = true;
            SetTips(tips);
        }
    }

    private void SetTips(string tips)
    {
        int len = tips.Length;
        SetActive(bgTips);
        txtTips.text = tips;
        bgTips.GetComponent<RectTransform>().sizeDelta = new Vector2(35 * len + 100, 80);

        ani.Play("TipsWnd", 0, 0);
    }

    public void AddTips(string tips)
    {
        tipsQue.Enqueue(tips);
    }

    public void AniPlayDone()
    {
        SetActive(bgTips, false);
        isTipsShow = false;
    }
}
