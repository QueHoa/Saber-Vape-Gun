using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SaberController : MonoBehaviour
{   
    [HideInInspector]public bool is3D = false;
    [HideInInspector] public bool isColor = false;
    private bool isZoom = false;
    private bool isSaber = false;
    private bool isGround = false;
    
    private int numSaber;
    private int numGround;

    [SerializeField]private FlashlightPlugin flash;
    public GameObject[] saber;
    public GameObject[] background;
    public GameObject[] icon;       

    // Start is called before the first frame update
    void Start()
    {
        numSaber = PlayerPrefs.GetInt("SaberSelector");
        numGround = 0;
        saber[numSaber].SetActive(true);
        background[numGround].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (saber[numSaber].GetComponent<SaberPlay>().isTouching)
        {
            flash.TurnOn();
        }
        else
        {
            flash.TurnOff();
        }
    }
    public void Set3D()
    {
        Sequence sequence = DOTween.Sequence();
        if (!is3D)
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-32, 0.4f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-39, 0.4f));
            for (int i = 1; i < 5; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(32, 0.4f)).SetEase(Ease.OutBack);
            }            
            sequence.Play();
        }
        else
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-23, 0.4f)).SetEase(Ease.OutBack);
            }
            sequence.Join(icon[5].transform.DOMoveX(-16, 0.4f)).SetEase(Ease.OutBack);
            for (int i = 1; i < 5; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(23, 0.4f)).SetEase(Ease.OutBack);
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
                sequence.Join(icon[i].transform.DOMoveX(-32, 0.3f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-39, 0.3f));
            sequence.Join(icon[0].transform.DOMoveY(64, 0.3f));
            sequence.Join(icon[1].transform.DOMoveX(32, 0.3f));
            sequence.Join(icon[3].transform.DOMoveX(32, 0.3f));
            sequence.Join(icon[4].transform.DOMoveX(32, 0.3f));
            sequence.Play();
            saber[numSaber].transform.DOMoveZ(6, 0.6f).SetEase(Ease.OutBack);
        }
        else
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-23, 0.3f)).SetEase(Ease.OutBack);
            }
            sequence.Join(icon[5].transform.DOMoveX(-16, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[0].transform.DOMoveY(52, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[1].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[3].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[4].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Play();
            saber[numSaber].transform.DOMoveZ(9, 0.5f);
        }
        isZoom = !isZoom;
    }
    public void setColor()
    {
        if (isColor)
        {
            icon[9].transform.DOMoveX(32, 0.3f);
        }
        else
        {
            icon[9].transform.DOMoveX(23, 0.5f).SetEase(Ease.OutBack);
        }
        isColor = !isColor;
    }
    public void setSaber()
    {
        if (isSaber)
        {
            icon[10].transform.DOMoveX(35, 0.3f);
        }
        else
        {
            icon[10].transform.DOMoveX(22, 0.3f).SetEase(Ease.OutBack);
        }
        isSaber = !isSaber;
    }
    public void setGround()
    {
        if (isGround)
        {
            icon[11].transform.DOMoveX(-35, 0.3f);
        }
        else
        {
            icon[11].transform.DOMoveX(-22, 0.3f).SetEase(Ease.OutBack);
        }
        isGround = !isGround;
    }
    public void ChangeSaber(int change)
    {
        saber[numSaber].SetActive(false);
        numSaber = change;
        saber[numSaber].SetActive(true);
    }
    public void ChangeBackground(int change)
    {
        background[numGround].SetActive(false);
        numGround = change;
        background[numGround].SetActive(true);
    }
}
