using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPlayer : MonoBehaviour
{
    public float movementPixel;
    public GameObject code;

    private float rotationAngle;
    public Animator animator;

    public GameObject tireCourt;
    public GameObject tireLong;
    public GameObject frappe;

    public bool StopByLevel = false;

    public void Play()
    {
        List<Transform> listInstruction = code.GetComponent<AddPlaceHolder>().GetPlaceHolders();
        StartCoroutine(PlayCoroutine(listInstruction));
    }

    private IEnumerator PlayCoroutine(List<Transform> listInstruction)
    {
        foreach (Transform instruction in listInstruction)
        {
            if (instruction.childCount != 0 && !StopByLevel)
            {
                string commande = instruction.GetChild(0).GetComponent<LineInstruction>().GetInstruction();
                int moreInfo = instruction.GetChild(0).GetComponent<LineInstruction>().GetMoreInfo();
                
                switch (commande)
                {
                    case "avancer":
                        yield return StartCoroutine(Avancer(moreInfo));
                        break;
                    case "tourner":
                        yield return StartCoroutine(Tourner(moreInfo));
                        break;
                    case "tireCourt":
                        yield return StartCoroutine(TireCourt());
                        break;
                    case "tireLong":
                        yield return StartCoroutine(TireLong());
                        break;
                    case "fraper":
                        yield return StartCoroutine(Frappe());
                        break;
                    default:
                        Debug.Log("Rien");
                        break;
                }
            }
        }
    }

    private IEnumerator Avancer(int distance)
    {
        float radians = rotationAngle * Mathf.Deg2Rad;
        float distanceX = movementPixel * Mathf.Cos(radians);
        float distanceY = movementPixel * Mathf.Sin(radians);

        for (int i = 0; i < distance; i++)
        {
            if (!StopByLevel)
            {
                Debug.Log("avancer");
                animator.SetBool("Walking", true);
                transform.LeanMoveLocal(
                    new Vector3(transform.localPosition.x + distanceX, transform.localPosition.y + distanceY,
                        transform.localPosition.z), 1f);
                yield return new WaitForSeconds(1f);
            }
        }
        animator.SetBool("Walking", false);
    }

    private IEnumerator Tourner(int angle)
    {
        Debug.Log("tourner");
        yield return new WaitForSeconds(0.5f);

        rotationAngle += angle;
        if (rotationAngle > 180)
        {
            rotationAngle -= 360;
        }
        else if (rotationAngle < -180)
        {
            rotationAngle += 360;
        }
        animator.SetFloat("Rotation", rotationAngle);
        
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator TireCourt()
    {
        Debug.Log("tireCourt");
        yield return new WaitForSeconds(0.2f);
        
        animator.SetBool("Shoot", true);
        tireCourt.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        
        if (rotationAngle <= 45 & rotationAngle >= -45) // Droite
        {
            tireCourt.transform.localPosition = new Vector3(0.309f, -0.075f);
        } else if (rotationAngle > 45 && rotationAngle < 135) // Haut
        {
            tireCourt.transform.localPosition = new Vector3(0.019f, 0.364f);
        } else if (rotationAngle >= 135 || rotationAngle <= -135) // Gauche
        {
            tireCourt.transform.localPosition = new Vector3(-0.282f, -0.075f);
        } else if (rotationAngle > -135 && rotationAngle < -45) // Bas
        {
            tireCourt.transform.localPosition = new Vector3(-0.0167f, -0.2841f);
        } else
        {
            Debug.Log("Erreur");
        }
        
        tireCourt.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("Shoot", false);
        tireCourt.SetActive(false);
        
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator TireLong()
    {
        Debug.Log("tireLong");
        yield return new WaitForSeconds(0.2f);
        
        animator.SetBool("Crossbow", true);
        tireLong.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        
        if (rotationAngle <= 45 & rotationAngle >= -45) // Droite
        {
            tireLong.transform.localPosition = new Vector3(0.309f, -0.075f);
        } else if (rotationAngle > 45 && rotationAngle < 135) // Haut
        {
            tireLong.transform.localPosition = new Vector3(0.019f, 0.364f);
        } else if (rotationAngle >= 135 || rotationAngle <= -135) // Gauche
        {
            tireLong.transform.localPosition = new Vector3(-0.282f, -0.075f);
        } else if (rotationAngle > -135 && rotationAngle < -45) // Bas
        {
            tireLong.transform.localPosition = new Vector3(-0.0167f, -0.2841f);
        } else
        {
            Debug.Log("Erreur");
        }
        
        tireLong.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("Crossbow", false);
        tireLong.SetActive(false);
        
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator Frappe()
    {  
        Debug.Log("frappe");
        yield return new WaitForSeconds(0.2f);
        
        animator.SetBool("Stab", true);
        frappe.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        
        if (rotationAngle <= 45 & rotationAngle >= -45) // Droite
        {
            frappe.transform.localPosition = new Vector3(0.309f, -0.075f);
        } else if (rotationAngle > 45 && rotationAngle < 135) // Haut
        {
            frappe.transform.localPosition = new Vector3(0, 0.364f);
        } else if (rotationAngle >= 135 || rotationAngle <= -135) // Gauche
        {
            frappe.transform.localPosition = new Vector3(-0.282f, -0.075f);
        } else if (rotationAngle > -135 && rotationAngle < -45) // Bas
        {
            frappe.transform.localPosition = new Vector3(-0, -0.4f);
        } else
        {
            Debug.Log("Erreur");
        }
        
        frappe.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("Stab", false);
        frappe.SetActive(false);
        
        yield return new WaitForSeconds(0.3f);
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