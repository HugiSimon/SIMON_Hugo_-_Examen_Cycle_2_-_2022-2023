using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool firstTime = true;
    public GameObject Content;

    public GameObject ContentCode;
    private GameObject[] PlaceHolders;

    public GameObject MainParent;

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
            ChangeToMainParent();
            // Désactive le HorizontalLayoutGroup du Content
            Content.GetComponent<HorizontalLayoutGroup>().enabled = false;
        }
    }

    // Fonction appelée lorsque l'utilisateur relâche l'objet
    public void OnPointerUp(PointerEventData eventData)
    {
        // Réactive le HorizontalLayoutGroup du Content
        Content.GetComponent<HorizontalLayoutGroup>().enabled = true;
        
        // Redimensionne le Content
        Content.GetComponent<TailleContent>().ResizeContent();
        
        // Vérifie si l'objet est proche d'un PlaceHolder
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 50f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("PlaceHolder"))
            {
                // Vérifie si le PlaceHolder a déjà un enfant
                if (collider.transform.childCount == 0)
                {
                    // Si non, prend la position du PlaceHolder et déplace pour que ça parte de la gauche
                    transform.SetParent(collider.transform);
                    transform.position = collider.transform.position + new Vector3(GetComponent<RectTransform>().sizeDelta.x / 2f, 0f, 0f);
                    firstTime = true;
                    transform.parent.parent.GetComponent<AddPlaceHolder>().AddPlaceHolders();
                    return;
                }
            }
        }

        RectTransform contentRect = Content.GetComponent<RectTransform>();
        // Si l'objet est déposé dans le Content
        if (RectTransformUtility.RectangleContainsScreenPoint(contentRect, eventData.position))
        {
            transform.SetParent(Content.transform);
            ContentCode.GetComponent<AddPlaceHolder>().RemoveEmptyPlaceHolders();
            firstTime = true;
        }
        else
        {
            if (firstTime) {
                ChangeToMainParent();
            }
        }
    }

    // Pour déplacer l'objet dans le MainParent
    public void ChangeToMainParent()
    {
        transform.SetParent(MainParent.transform);
    }

    public void UpdatePlaceHolders()
    {
        PlaceHolders = GameObject.FindGameObjectsWithTag("PlaceHolder");
    }
}