using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBox : MonoBehaviour
{
    private Vector3 offset;

    public void OnStartDrag(PointerEventData eventData)
    {
        offset = transform.position - (Vector3)eventData.position;
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector3) eventData.position + offset;
    }
}
