using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]private GameObject Camera;
    [SerializeField]private GameObject Player;
    [SerializeField]private GameObject FinMessage;
    
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
                newCodeAvancer.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"1"});   
                ContentCode.GetComponent<AddPlaceHolder>().maxNumber = 1;
                ContentCode.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<color=#779d54>#Avance jusqu'à la fin du niveau à droite";
                break;
            case 2:
                newCodeAvancer = Instantiate(CodeAvancer, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeAvancer.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeAvancer.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"1", "2", "3"});  
                ChangeLevel(new Vector3(17.89f, 0, -10));
                ContentCode.GetComponent<AddPlaceHolder>().ResetPlaceHolders();
                ContentCode.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<color=#779d54>#Tu peux avancer de 1 à 3 cases directement";
                break;
            case 3:
                newCodeAvancer = Instantiate(CodeAvancer, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeAvancer.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeAvancer.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"1", "2", "3"});
                GameObject newCodeTourner = Instantiate(CodeTourner, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTourner.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeTourner.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"-90", "0", "90"});
                ChangeLevel(new Vector3(35.89f, 0, -10));
                ContentCode.GetComponent<AddPlaceHolder>().ResetPlaceHolders();
                ContentCode.GetComponent<AddPlaceHolder>().maxNumber = 2;
                ContentCode.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<color=#779d54>#Tourne pour éviter le trou";
                break;
            case 4:
                newCodeAvancer = Instantiate(CodeAvancer, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeAvancer.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeAvancer.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"1", "2", "3"});
                newCodeTourner = Instantiate(CodeTourner, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTourner.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeTourner.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"-90", "0", "90"});
                GameObject newCodeTireCourt = Instantiate(CodeTireCourt, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTireCourt.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                ChangeLevel(new Vector3(54.11f, 0, -10));
                ContentCode.GetComponent<AddPlaceHolder>().ResetPlaceHolders();
                ContentCode.GetComponent<AddPlaceHolder>().maxNumber = 3;
                ContentCode.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<color=#779d54>#Tire sur l'ennemie pour continuer";
                break;
            case 5:
                newCodeAvancer = Instantiate(CodeAvancer, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeAvancer.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeAvancer.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"1", "2", "3"});
                newCodeTourner = Instantiate(CodeTourner, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTourner.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                newCodeTourner.GetComponent<LineInstruction>().UpdateOptionDropdown(new []{"-90", "-45", "-15", "0", "15", "45", "90"});
                newCodeTireCourt = Instantiate(CodeTireCourt, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTireCourt.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                GameObject newCodeTireLong = Instantiate(CodeTireLong, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeTireLong.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                GameObject newCodeFrapper = Instantiate(CodeFrapper, new Vector3(0, 0, 0), Quaternion.identity, ContentCode.transform);
                newCodeFrapper.GetComponent<DragAndDrop>().Intialize(Content, ContentCode, this.gameObject);
                ChangeLevel(new Vector3(72.11f, 0, -10));
                ContentCode.GetComponent<AddPlaceHolder>().ResetPlaceHolders();
                ContentCode.GetComponent<AddPlaceHolder>().maxNumber = 5;
                ContentCode.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<color=#779d54>#Fin de la démo, tu peux tout essayer";
                FinMessage.SetActive(true);
                break;
            default:
                Debug.Log("Rien");
                break;
        }
    }

    private void ChangeLevel(Vector3 cameraPos)
    {
        Camera.transform.LeanMoveLocal(cameraPos, 4f).setEaseOutCubic();
        Player.transform.LeanMoveLocal(new Vector3(cameraPos.x - 0.796f, cameraPos.y -0.48f, 0), 5f).setOnComplete(() => {
            Player.GetComponent<Animator>().SetBool("Walking", false);
            Player.GetComponent<InstructionPlayer>().StopByLevel = false;
        });
        Player.GetComponent<Animator>().SetBool("Walking", true);
    }

    public void ResetPlayerPos()
    {
        LeanTween.cancelAll();
        Player.transform.localPosition = new Vector3(Camera.transform.localPosition.x - 0.796f, Camera.transform.localPosition.y - 0.48f, 0);
    }

    public void NewLevel()
    {
        level++;
        Play();
    }

    private void Update()
    {
        switch (level)
        {
            case 1:
                if (Player.transform.localPosition.x > 8.7f)
                {
                    Player.GetComponent<InstructionPlayer>().StopByLevel = true;
                    if (!Player.GetComponent<Animator>().GetBool("Walking"))
                    {
                        NewLevel();
                    }
                }
                break;
            case 2:
                if (Player.transform.localPosition.x >= 26.6f)
                {
                    Player.GetComponent<InstructionPlayer>().StopByLevel = true;
                    if (!Player.GetComponent<Animator>().GetBool("Walking"))
                    {
                        NewLevel();
                    }
                }
                break;
            case 3:
                if (Player.transform.localPosition.x >= 44.6f)
                {
                    Player.GetComponent<InstructionPlayer>().StopByLevel = true;
                    if (!Player.GetComponent<Animator>().GetBool("Walking"))
                    {
                        NewLevel();
                    }
                }
                break;
            case 4:
                if (Player.transform.localPosition.x >= 63.6f)
                {
                    Player.GetComponent<InstructionPlayer>().StopByLevel = true;
                    if (!Player.GetComponent<Animator>().GetBool("Walking"))
                    {
                        NewLevel();
                    }
                }
                break;
        }
    }
}
