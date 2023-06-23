using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DimensionText : MonoBehaviour
{
    public int topAndBottomMargin = 12;
    public int leftAndRightMargin = 18;

    private RectTransform rectTransform;
    private TextMeshProUGUI textComponent;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        boxCollider = GetComponent<BoxCollider2D>();

        ApplyMarginToRectTransform();
        ApplyDimensionsToBoxCollider();
    }

    private void ApplyMarginToRectTransform()
    {
        if (textComponent != null)
        {
            float preferredWidth = textComponent.preferredWidth + leftAndRightMargin * 2;
            float preferredHeight = textComponent.preferredHeight + topAndBottomMargin * 2;

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
        }
    }

    private void ApplyDimensionsToBoxCollider()
    {
        if (boxCollider != null)
        {
            boxCollider.size = rectTransform.sizeDelta;
        }
    }
}