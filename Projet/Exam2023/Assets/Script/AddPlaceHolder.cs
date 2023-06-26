using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlaceHolder : MonoBehaviour
{
    public int maxNumber;
    public GameObject placeHolderPrefab;
    public int spacing;
    public Vector2 firstPosition;

    public void AddPlaceHolders(List<Transform> placeHolders = null)
    {
        if (placeHolders == null)
        {
            placeHolders = GetPlaceHolders();
        }

        // Vérifie si tous les PlaceHolder ont un enfant
        bool allPlaceHoldersHaveChild = true;
        foreach (Transform placeHolder in placeHolders)
        {
            if (placeHolder.childCount == 0)
            {
                allPlaceHoldersHaveChild = false;
                break;
            }
        }

        // Si tous les PlaceHolder ont un enfant et qu'il y a moins de maxNumber PlaceHolder, ajoute un nouveau PlaceHolder
        if (allPlaceHoldersHaveChild && placeHolders.Count < maxNumber)
        {
            // Crée un nouveau PlaceHolder à partir du prefab
            GameObject newPlaceHolder = Instantiate(placeHolderPrefab, transform);

            // Positionne le nouveau PlaceHolder à la fin de la liste des PlaceHolder
            RectTransform lastPlaceHolder = placeHolders[placeHolders.Count - 1].GetComponent<RectTransform>();
            newPlaceHolder.GetComponent<RectTransform>().position = new Vector2(lastPlaceHolder.position.x, lastPlaceHolder.position.y - spacing);
        }
    }

public void RemoveEmptyPlaceHolders()
{
    List<Transform> placeHolders = GetPlaceHolders();

    // Collecte les références des PlaceHolder qui ont un enfant
    List<Transform> remainderPlaceHolder = new List<Transform>();
    foreach (Transform placeHolder in placeHolders)
    {
        if (placeHolder.childCount == 0)
        {
            Destroy(placeHolder.gameObject);
        }
        else
        {
            remainderPlaceHolder.Add(placeHolder);
        }
    }

    // Repositionne les PlaceHolder restants
    // Obligé de faire une nouvelle liste car Destroy n'est pas immédiat
    RepositionPlaceHolders(remainderPlaceHolder);
}

    public void RepositionPlaceHolders(List<Transform> placeHolders)
    {
        int i = 0;
        foreach (Transform placeHolder in placeHolders)
        {
            // Positionne le PlaceHolder à la bonne position
            placeHolder.localPosition = new Vector2(firstPosition.x, firstPosition.y - spacing * i);
            i++;
        }
        
        AddPlaceHolders(placeHolders);
    }
    
    private List<Transform> GetPlaceHolders()
    {
        // Récupère tous les enfants de l'objet
        Transform[] children = GetComponentsInChildren<Transform>();

        // Ajoute dans la liste que les enfants qui sont des PlaceHolder
        List<Transform> placeHolders = new List<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("PlaceHolder"))
            {
                placeHolders.Add(child);
            }
        }

        return placeHolders;
    }
}
