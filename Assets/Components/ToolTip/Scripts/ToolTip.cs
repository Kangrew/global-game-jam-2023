using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ToolTip : MonoBehaviour
{
    public static ToolTip instance;

    public Image BG;
    public TextMeshProUGUI TTText;
    public bool Stop;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    

    public void Show(string mess)
    {
        //DOTween.CompleteAll();
        if(Stop)
        return;
        BG.gameObject.SetActive(true);
        TTText.text = mess;
        // BG.transform.localScale = Vector3.zero;
        // BG.transform.DOScale(1, 0.1f).SetEase(Ease.OutBounce);
    }

    public void Hide()
    {
        if(Stop)
        return;
        //BG.transform.DOScale(0, 0.1f).SetEase(Ease.InFlash).OnComplete(()=> { BG.gameObject.SetActive(false); });
        BG.gameObject.SetActive(false);
    }

    public void SetStop(bool v){
        Stop = v;
    }
}
