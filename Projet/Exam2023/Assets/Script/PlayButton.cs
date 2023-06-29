using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject parentObject;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Play()
    {
        parentObject.SetActive(false);
    }
}