using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineInstruction : MonoBehaviour
{
    [SerializeField]private string instruction;
    private int moreInfo;
    [SerializeField]private bool asMoreInfo;
    
    public string GetInstruction()
    {
        return instruction;
    }
    
    public int GetMoreInfo()
    {
        if (asMoreInfo)
        {
            SetMoreInfo();
            return moreInfo;
        } else
        {
            return -1;
        }
    }

    private void SetMoreInfo()
    {
        moreInfo = int.Parse(GetComponentInChildren<TMP_Dropdown>()
            .options[GetComponentInChildren<TMP_Dropdown>().value].text);
    }
}
