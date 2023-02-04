using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeBell : MonoBehaviour
{
    public string ToolTipText;

    DragAndDrop drag;
    // Start is called before the first frame update
    void Start()
    {
        drag = gameObject.AddComponent<DragAndDrop>();
        drag.On_ExitHover += OnExitHover_Func;
        drag.On_EnterHover += OnEnterHover_Func;
        drag.On_ClickRelease += OnClickReleased_Func;
        drag.CanDrag = false;
    }

    private void OnEnterHover_Func()
    {
        ToolTip.instance.Show(ToolTipText);
    }
    private void OnExitHover_Func()
    {
        ToolTip.instance.Hide();
    }
    private void OnClickReleased_Func()
    {
        FindObjectOfType<Stove>().ResetStove();
        FindObjectOfType<ServingBowl>().SummitOrder();
    }
}
