using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CodeAvancer;
    public GameObject CodeTourner;
    public GameObject CodeTireCourt;
    public GameObject CodeTireLong;
    public GameObject CodeFrapper;
    public GameObject Ennemie;

    public GameObject Content;
    public GameObject ContentCode;

    private int level;
    
    void Start()
    {
        level = 1;
    }
    
    public void Play()
    {
        switch (level)
        {
            case 1:
                GameObject newCodeAvancer = Instantiate(CodeAvancer, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeAvancer.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                ContentCode.GetComponent<AddPlaceHolder>().maxNumber = 1;
                break;
            default:
                Debug.Log("Rien");
                break;
        }
    }
}
