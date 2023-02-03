using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    public bool CanDrag = true;
    private bool IsDragging;
    private Vector2 currentPos, previousPos;

    public event Action On_Drag, On_DragStart, On_DragEnd;
    private void Start()
    {

    }
    private void Update()
    {
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

        if(Physics.Raycast(ray,out RaycastHit hit, 1000))
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
    private void OnDragging()
    {
        Vector2 touchPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if(Physics.Raycast(ray,out RaycastHit hit, 1000, 1 << LayerMask.NameToLayer("Table")))
        {
            Debug.Log("Dragging");
            float y = transform.position.y;
            transform.position = new Vector3(
                    hit.point.x,
                    y,
                    hit.point.z
            );
        }
        On_Drag?.Invoke();

    }
    private void OnStartDrag()
    {
        Debug.Log("start Dragging");
        On_DragStart?.Invoke();
    }
    private void OnEndDrag()
    {
        Debug.Log("End Dragging");
        On_DragEnd?.Invoke();
    }

}
