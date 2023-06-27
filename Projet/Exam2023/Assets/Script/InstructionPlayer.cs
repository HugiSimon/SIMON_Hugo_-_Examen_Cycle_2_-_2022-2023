using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPlayer : MonoBehaviour
{
    public float movementPixel;
    public GameObject code;
    
    private float rotationAngle;
    public Animator animator;
    
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
                            animator.SetBool("Walking", true);
                            transform.LeanMoveLocal(new Vector3(transform.localPosition.x + distanceX, transform.localPosition.y + distanceY, transform.localPosition.z), 1f);
                            Debug.Log("avancer");
                            yield return new WaitForSeconds(1f);
                        }
                        animator.SetBool("Walking", false);
                        break;
                    case "tourner":
                        yield return new WaitForSeconds(0.5f);
                        
                        rotationAngle += moreInfo;
                        if (rotationAngle > 180)
                        {
                            rotationAngle -= 360;
                        } else if (rotationAngle < -180)
                        {
                            rotationAngle += 360;
                        }
                        animator.SetFloat("Rotation", rotationAngle);
                        
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
