using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public bool IsDragable;

    DragAndDrop drag;
    Vector3 dragStartPos;
    // Start is called before the first frame update
    void Start()
    {
        if (IsDragable)
        {
            drag = gameObject.AddComponent<DragAndDrop>();
            drag.On_DragStart += OnDragStart_Func;
            drag.On_DragEnd += OnDragEnd_Func;
        }

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
                transform.position = hit.collider.transform.position;
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + transform.localScale.y / 2,
                    transform.position.z);
                transform.SetParent(hit.collider.transform);
                if (IsDragable) drag.CanDrag = false;
                return;
            }
        }

        transform.position = dragStartPos;

    }
}
