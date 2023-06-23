using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool firstTime = true;
    [SerializeField] private GameObject Content;

    // Fonction appelée lorsque l'objet est en train d'être déplacé
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // Fonction appelée lorsque l'utilisateur commence à déplacer l'objet
    public void OnPointerDown(PointerEventData eventData)
    {
        // Si c'est la première fois que l'objet est déplacé
        if (firstTime)
        {
            // Déplace l'objet deux niveaux plus haut dans la hiérarchie des objets
            firstTime = false;
            MoveTwoParentsUp();
            // Désactive le HorizontalLayoutGroup du Content
            Content.GetComponent<HorizontalLayoutGroup>().enabled = false;
        }
    }

    // Fonction appelée lorsque l'utilisateur relâche l'objet
    public void OnPointerUp(PointerEventData eventData)
    {
        RectTransform contentRect = Content.GetComponent<RectTransform>();
        // Si l'objet est déposé dans le Content
        if (RectTransformUtility.RectangleContainsScreenPoint(contentRect, eventData.position))
        {
            transform.SetParent(Content.transform);
            firstTime = true;
        }
        else
        {
            if (firstTime) {
                MoveTwoParentsUp();
            }
        }
        // Réactive le HorizontalLayoutGroup du Content
        Content.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    // Fonction pour déplacer l'objet deux niveaux plus haut dans la hiérarchie des objets
    public void MoveTwoParentsUp()
    {
        Transform parent = transform.parent;
        // Si l'objet a un parent, que le parent a un parent et que le parent du parent a un parent
        if (parent != null && parent.parent != null && parent.parent.parent != null)
        {
            transform.SetParent(parent.parent.parent);
        }
    }
}