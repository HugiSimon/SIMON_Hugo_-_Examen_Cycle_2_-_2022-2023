using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DimensionText : MonoBehaviour
{
    // Marges à appliquer à la largeur et à la hauteur du texte
    public int topAndBottomMargin = 12;
    public int leftAndRightMargin = 18;

    private RectTransform rectTransform;
    private TextMeshProUGUI textComponent;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        // Récupère les composants nécessaires
        rectTransform = GetComponent<RectTransform>();
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Applique les marges à la largeur et à la hauteur du texte
        ApplyMarginToRectTransform();
        // Applique les dimensions du RectTransform au BoxCollider2D
        ApplyDimensionsToBoxCollider();

        // Redimensionne le Content
        GetComponent<DragAndDrop>().Content.GetComponent<TailleContent>().ResizeContent();
    }

    private void ApplyMarginToRectTransform()
    {
        if (textComponent != null)
        {
            // Calcule la largeur et la hauteur préférées du texte en ajoutant les marges
            float preferredWidth = textComponent.preferredWidth + leftAndRightMargin * 2;
            float preferredHeight = textComponent.preferredHeight + topAndBottomMargin * 2;

            // Applique la largeur et la hauteur calculées au RectTransform
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
        }
    }

    private void ApplyDimensionsToBoxCollider()
    {
        if (boxCollider != null)
        {
            // Applique les dimensions du RectTransform au BoxCollider2D
            boxCollider.size = rectTransform.sizeDelta;
        }
    }
}