using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{   
    [HideInInspector]public bool is3D = false;
    private bool isZoom = false;
    private bool isSaber = false;
    private bool isColor = false;
    private bool isGround = false;
    
    private int numberSaber = 0;

    public FlashlightPlugin flash;
    public GameObject[] saber;
    public GameObject[] icon;       

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
        Sequence sequence = DOTween.Sequence();
        if (!is3D)
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-410, 0.5f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-222.5f, 0.5f));
            for (int i = 1; i < 5; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(1210, 0.5f)).SetEase(Ease.OutBack);
            }            
            sequence.Play();
        }
        else
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(85, 1.5f)).SetEase(Ease.InOutBack);
            }
            sequence.Join(icon[5].transform.DOMoveX(222.5f, 1.5f)).SetEase(Ease.OutBack);
            for (int i = 1; i < 5; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(995, 1.5f)).SetEase(Ease.OutBack);
            }           
            sequence.Play();
        }
        is3D = !is3D;
    }
    public void setZoom()
    {
        Sequence sequence = DOTween.Sequence();       

        if (!isZoom)
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-410, 0.5f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-222.5f, 0.5f));
            sequence.Join(icon[0].transform.DOMoveY(2360, 0.5f));
            sequence.Join(icon[1].transform.DOMoveX(1210, 0.5f));
            sequence.Join(icon[3].transform.DOMoveX(1210, 0.5f));
            sequence.Join(icon[4].transform.DOMoveX(1210, 0.5f));
            sequence.Play();
            saber[numberSaber].transform.DOMoveZ(6, 0.8f).SetEase(Ease.OutBack);
        }
        else
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(85, 1.5f)).SetEase(Ease.InOutBack);
            }
            sequence.Join(icon[5].transform.DOMoveX(222.5f, 1.5f)).SetEase(Ease.OutBack);
            sequence.Join(icon[0].transform.DOMoveY(2160, 1.5f)).SetEase(Ease.OutBack);
            sequence.Join(icon[1].transform.DOMoveX(995, 1.5f)).SetEase(Ease.OutBack);
            sequence.Join(icon[3].transform.DOMoveX(995, 1.5f)).SetEase(Ease.OutBack);
            sequence.Join(icon[4].transform.DOMoveX(995, 1.5f)).SetEase(Ease.OutBack);
            sequence.Play();
            saber[numberSaber].transform.DOMoveZ(9, 0.8f);
        }
        isZoom = !isZoom;
    }
    public void setColor()
    {
        if (isColor)
        {
            icon[9].transform.DOMoveX(1165, 0.5f);
        }
        else
        {
            icon[9].transform.DOMoveX(995, 0.5f).SetEase(Ease.OutBack);
        }
        isColor = !isColor;
    }
    public void setSaber()
    {
        if (isSaber)
        {
            icon[10].transform.DOMoveX(1195, 0.5f);
        }
        else
        {
            icon[10].transform.DOMoveX(970, 0.5f).SetEase(Ease.OutBack);
        }
        isSaber = !isSaber;
    }
    public void setGround()
    {
        if (isGround)
        {
            icon[11].transform.DOMoveX(-115, 0.5f);
        }
        else
        {
            icon[11].transform.DOMoveX(110, 0.5f).SetEase(Ease.OutBack);
        }
        isGround = !isGround;
    }
    public void ChangeSaber(int change)
    {
        saber[numberSaber].SetActive(false);
        numberSaber = change;
        saber[numberSaber].SetActive(true);
    }
}
