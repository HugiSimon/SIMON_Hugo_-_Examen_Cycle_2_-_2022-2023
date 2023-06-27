using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPlayer : MonoBehaviour
{
    public float movementPixel;
    public GameObject code;
    
    private float rotationAngle;
    
    public void Play()
    {
        List<Transform> listInstruction = code.GetComponent<AddPlaceHolder>().GetPlaceHolders();
        StartCoroutine(PlayCoroutine(listInstruction));
    }
    
    private IEnumerator PlayCoroutine(List<Transform> listInstruction)
    {
        foreach (Transform instruction in listInstruction)
        {
            if (instruction.childCount != 0)
            {
                string commande = instruction.GetChild(0).GetComponent<LineInstruction>().GetInstruction();
                int moreInfo = instruction.GetChild(0).GetComponent<LineInstruction>().GetMoreInfo();

                switch (commande)
                {   
                    case "avancer":
                        float radians = rotationAngle * Mathf.Deg2Rad;
                        float distanceX = movementPixel * Mathf.Cos(radians);
                        float distanceY = movementPixel * Mathf.Sin(radians);

                        for (int i = 0; i < moreInfo; i++)
                        {
                            transform.LeanMoveLocal(new Vector3(transform.localPosition.x + distanceX, transform.localPosition.y + distanceY, transform.localPosition.z), 1f).setEaseInOutCubic();
                            Debug.Log("avancer");
                            yield return new WaitForSeconds(1f);
                        }
                        break;
                    case "tourner":
                        yield return new WaitForSeconds(0.5f);
                        rotationAngle += moreInfo;
                        Debug.Log("tourner");
                        yield return new WaitForSeconds(0.5f);
                        break;
                    default:
                        Debug.Log("Rien");
                        break;
                }
            }
        }
    }
    
    // Quand j'appuie sur espace, appelle Play
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
    }
}
