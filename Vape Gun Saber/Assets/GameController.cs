using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{   
    public FlashlightPlugin flash;
    [HideInInspector]public bool isSword = false;
    private bool isZoom = false;

    public SaberPlay saberPlay;
    public Button but3D;
    public Button butSetting;
    public Button butZoom;
    public Button butSaber;
    public Button butColor;
    public Button butBack;
    public Button butEnergy;
    public Button butBackground;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (saberPlay.isTouching)
        {
            flash.TurnOn();
        }
        else
        {
            flash.TurnOff();
        }*/
    }
    public void Set3D()
    {       
        isSword = !isSword;
    }
    public void setZoom()
    {
        if(isZoom == false)
        {
            saberPlay.transform.DOMove(new Vector3(0, -0.8f, 6), 0.8f);
        }
        else
        {
            saberPlay.transform.DOMove(new Vector3(0, -0.8f, 9), 0.8f);
        }
        isZoom = !isZoom;
    }
}
