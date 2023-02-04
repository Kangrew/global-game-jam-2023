using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : Singleton<CookingUI>
{
    public Button ThrowButton;
    // Start is called before the first frame update
    void Start()
    {
        ThrowButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
