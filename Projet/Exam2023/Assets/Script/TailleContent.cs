using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TailleContent : MonoBehaviour
{
    public void ResizeContent()
    {
        // Récupère le RectTransform du Content
        RectTransform contentRect = GetComponent<RectTransform>();
        
        // Récupère l'espacement entre les enfants du Content
        float spacing = GetComponent<HorizontalLayoutGroup>().spacing;

        // Calcule la largeur totale des enfants
        float totalWidth = 0f;
        foreach (RectTransform child in transform)
        {
            totalWidth += child.rect.width;
        }

        // Ajoute l'espacement entre les enfants
        totalWidth += spacing * (transform.childCount - 1);

        // Ajoute l'espacement entre le premier enfant et le bord gauche du Content
        totalWidth += spacing * 2;

        // Calcule la largeur minimale qui est la largeur du parent
        float minWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        // Met à jour la largeur du Content en prenant la plus grande valeur entre la largeur totale et la largeur minimale
        contentRect.sizeDelta = new Vector2(Mathf.Max(totalWidth, minWidth), contentRect.sizeDelta.y);
    }
}