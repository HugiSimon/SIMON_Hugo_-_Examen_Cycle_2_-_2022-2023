using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool firstTime = true;
    [SerializeField] private GameObject Content;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (firstTime)
        {
            firstTime = false;
            MoveTwoParentsUp();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RectTransform contentRect = Content.GetComponent<RectTransform>();
        if (RectTransformUtility.RectangleContainsScreenPoint(contentRect, eventData.position))
        {
            transform.SetParent(Content.transform);
            firstTime = true;
        }
    }

    public void MoveTwoParentsUp()
    {
        Transform parent = transform.parent;
        if (parent != null && parent.parent != null && parent.parent.parent != null)
        {
            transform.SetParent(parent.parent.parent);
        }
    }
}