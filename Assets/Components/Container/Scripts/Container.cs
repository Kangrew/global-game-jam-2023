using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public bool IsDragable;
    public string ToolTipText;
    DragAndDrop drag;
    Vector3 dragStartPos;
    // Start is called before the first frame update
    void Start()
    {
        drag = gameObject.AddComponent<DragAndDrop>();
        drag.On_ExitHover += OnExitHover_Func;
        drag.On_EnterHover += OnEnterHover_Func;

        if (IsDragable)
        {
            drag.CanDrag = true;
            drag.On_DragStart += OnDragStart_Func;
            drag.On_DragEnd += OnDragEnd_Func;
        }else
            drag.CanDrag = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDragStart_Func()
    {
        dragStartPos = transform.position;
    }

    private void OnDragEnd_Func()
    {
        Vector2 touchPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, 1 << LayerMask.NameToLayer("Stove")))
        {
            if (hit.collider.TryGetComponent<Stove>(out Stove contain))
            {
                transform.position = contain.PlaceHolder.position;
                //transform.position = new Vector3(
                //    transform.position.x,
                //    transform.position.y,
                //    transform.position.z);
                transform.SetParent(hit.collider.transform);
                if (IsDragable) drag.CanDrag = false;
                return;
            }
        }

        transform.position = dragStartPos;

    }
    private void OnEnterHover_Func()
    {
        ToolTip.instance.Show(ToolTipText);
    }
    private void OnExitHover_Func()
    {
        ToolTip.instance.Hide();
    }

}
