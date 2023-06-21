using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Couleur : MonoBehaviour
{
    private Dictionary<string, string> colorMap = new Dictionary<string, string>()
    {
        {"joueur", "#7cdcf1"},
        {"move", "#3bc9b0"},
        {"turn", "#3bc9b0"}
    };

    private void Start()
    {
        TMP_InputField inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(TextAsBeenUpdate);
    }

    public void TextAsBeenUpdate(string newText)
    {
        TMP_InputField inputField = GetComponent<TMP_InputField>();

        // Désactiver temporairement les événements de changement de texte
        inputField.onValueChanged.RemoveAllListeners();

        // Quand le dernier charactère est un espace
        if (newText.Length > 0 && newText[newText.Length - 1] == ' ')
        {
            // Ajouter une balise à chaque mot
            string[] words = newText.Split(' ');
            string formattedText = "";

            foreach (var word in words)
            {
                Debug.Log(word + " ");
            }

            string color = "#ffffff"; // blanc par défaut
            if (colorMap.ContainsKey(words[words.Length - 2]))
            {
                color = colorMap[words[words.Length - 2]];
            }

            for (int i = 0; i < words.Length - 2; i++)
            {
                formattedText += words[i] + " ";
            }

            formattedText += $"<color={color}>{words[words.Length - 2]}</color> ";

            // Mettre à jour le texte de l'input field
            inputField.text = formattedText;

            // Déplacer le curseur à la fin du texte
            inputField.caretPosition = inputField.text.Length;
        }

        // Réactiver les événements de changement de texte
        inputField.onValueChanged.AddListener(TextAsBeenUpdate);
    }
}