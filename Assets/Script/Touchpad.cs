using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.EventSystems;

using UnityEngine.UI; 

public class Touchpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 origin; 
    private Vector2 direction; 

    private void Awake()
    {
        direction = Vector2.zero; 
    }
    public void OnPointerDown(PointerEventData eventData) 
    {
        origin = eventData.position;
    }
    public void OnDrag(PointerEventData eventData) 
    {
        Vector2 currentPosition = eventData.position;
        Vector2 directionRaw = currentPosition - origin;
        direction = directionRaw.normalized;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector3.zero;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }
}
