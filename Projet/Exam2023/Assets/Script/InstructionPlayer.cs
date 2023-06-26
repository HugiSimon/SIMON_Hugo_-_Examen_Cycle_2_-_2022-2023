using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPlayer : MonoBehaviour
{
    public float movementPixel;
    public GameObject code;
    
    /*public void Play()
    {
        List<Transform> listInstruction = code.GetComponent<AddPlaceHolder>().GetPlaceHolders();
        StartCoroutine(PlayCoroutine(listInstruction));
    }
    
    private IEnumerator PlayCoroutine(List<Transform> listInstruction)
    {
        
    }*/
    
    void Start()
    {
        //transform.LeanMoveLocal(new Vector3(transform.localPosition.x + movementPixel, transform.localPosition.y), 1f).setEaseInOutCubic();
    }
}
