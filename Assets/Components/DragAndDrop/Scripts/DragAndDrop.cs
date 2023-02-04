using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    public bool CanDrag = true, NeedHover = true;
    private bool IsDragging;
    private Vector2 currentPos, previousPos;
    private LayerMask TableExection;

    //hover update
    private bool isHovering;

    public event Action On_Drag, On_DragStart, On_DragEnd, On_EnterHover, On_ExitHover, On_ClickRelease;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (IsClickReleaseOnObject())
        {
            On_ClickRelease?.Invoke();
        }
        if (NeedHover)
        {
            if (!isHovering && IsMouseOnObject())
            {
                isHovering= true;
                On_EnterHover?.Invoke();
            }else if(isHovering && !IsMouseOnObject())
            {
                isHovering= false;
                On_ExitHover?.Invoke();
            }
        }
        if (CanDrag)
        {
            currentPos = Input.mousePosition;
            // On start Drag
            if (!IsDragging && IsClickOnObject())
            {
                IsDragging = true;
                OnStartDrag();
            }

            //dragging
            if (IsDragging && IsMouseDragging())
            {
                OnDragging();
            }

            // On end drag
            if (IsDragging && Input.GetMouseButtonUp(0))
            {
                IsDragging = false;
                OnEndDrag();
            } 
        }
    }
    private void LateUpdate()
    {
        if(CanDrag)previousPos= currentPos;
    }
    private bool IsMouseDragging()
    {
        if(currentPos != previousPos) return true;
        return false;
    }

    private bool IsMouseOnObject()
    {
        Vector2 touchPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        int lm = 1 << LayerMask.NameToLayer("Table");
        lm = ~lm;
        if (Physics.Raycast(ray,out RaycastHit hit, 1000, lm))
        {
            if(hit.collider.gameObject == gameObject)
            {
                //Debug.Log("On Click");
                return true;
            }
        }
        return false;
    }
    private bool IsClickOnObject()
    {
        if(Input.GetMouseButtonDown(0))
            return IsMouseOnObject();
        else
            return false;
    }
    private bool IsClickReleaseOnObject()
    {
        if (Input.GetMouseButtonUp(0))
            return IsMouseOnObject();
        else
            return false;
    }
    private void OnDragging()
    {
        Vector2 touchPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if(Physics.Raycast(ray,out RaycastHit hit, 1000, 1 << LayerMask.NameToLayer("Table")))
        {
            //Debug.Log("Dragging");
            float y = transform.position.y;
            transform.position = new Vector3(
                    hit.point.x,
                    hit.point.y,
                    hit.point.z
            );
        }
        On_Drag?.Invoke();

    }
    private void OnStartDrag()
    {
        //Debug.Log("start Dragging");
        On_DragStart?.Invoke();
    }
    private void OnEndDrag()
    {
        //Debug.Log("End Dragging");
        On_DragEnd?.Invoke();
    }

}
