using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.5f, 1).SetEase(Ease.InOutElastic).SetLoops(2,LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            transform.DOPunchScale(new Vector3(0.5f, 0.7f, 0.3f), 0.5f);
        }
    }
}
