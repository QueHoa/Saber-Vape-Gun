using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class SaberController : MonoBehaviour
{   
    [HideInInspector]
    public bool is3D = false;
    [HideInInspector] 
    public bool isColor = false;
    private bool isZoom = false;
    private bool isSaber = false;
    private bool isGround = false;
    
    private int numSaber;
    private int numGround;
    public float shakeThreshold = 2.0f;
    private bool shakeDetected = false;
    public HapticTypes hapticTypes = HapticTypes.HeavyImpact;
    private bool hapticsAllowed = true;

    [SerializeField]
    private FlashlightPlugin flash;
    [SerializeField]
    private SceneLoading loading;
    [SerializeField]
    private GameObject setting;   
    [SerializeField]
    private GameObject noInter;
    [SerializeField]
    private Image powerUp;
    public GameObject[] saber;
    public GameObject[] background;
    public GameObject[] icon;       

    // Start is called before the first frame update
    void Start()
    {
        MMVibrationManager.SetHapticsActive(hapticsAllowed);
        numSaber = PlayerPrefs.GetInt("SaberSelector");
        numGround = 0;
        saber[numSaber].SetActive(true);
        background[numGround].SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Join(icon[0].transform.DOMoveY(51, 0.3f)).SetEase(Ease.OutBack);
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
        if (Input.accelerationEventCount > 0)
        {
            Vector3 acceleration = Input.acceleration;
            float totalAcceleration = acceleration.magnitude;
            if (totalAcceleration > shakeThreshold)
            {
                shakeDetected = true;
                flash.TurnOff();
            }
        }
        if (shakeDetected)
        {
            flash.TurnOn();
            MMVibrationManager.Haptic(hapticTypes, true, true, this);
            shakeDetected = false;
        }
    }
    public void Set3D()
    {
        Sequence sequence = DOTween.Sequence();
        if (!is3D)
        {
            for (int i = 6; i < 9; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(-50, 0.4f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-55, 0.4f));
            for (int i = 1; i < 5; i++)
            {
                sequence.Join(icon[i].transform.DOMoveX(50, 0.4f));
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
                sequence.Join(icon[i].transform.DOMoveX(-50, 0.3f));
            }
            sequence.Join(icon[5].transform.DOMoveX(-55, 0.3f));
            sequence.Join(icon[0].transform.DOMoveY(80, 0.3f));
            sequence.Join(icon[1].transform.DOMoveX(50, 0.3f));
            sequence.Join(icon[3].transform.DOMoveX(50, 0.3f));
            sequence.Join(icon[4].transform.DOMoveX(50, 0.3f));
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
            sequence.Join(icon[0].transform.DOMoveY(51, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[1].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[3].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Join(icon[4].transform.DOMoveX(23, 0.3f)).SetEase(Ease.OutBack);
            sequence.Play();
            saber[numSaber].transform.DOMoveZ(9, 0.3f);
        }
        isZoom = !isZoom;
    }
    public void setColor()
    {
        if (isSaber) setSaber();
        if (isColor)
        {
            icon[9].transform.DOMoveX(65, 0.3f);
        }
        else
        {
            icon[9].transform.DOMoveX(23, 0.5f).SetEase(Ease.OutBack);
        }
        isColor = !isColor;   
    }
    public void setSaber()
    {
        if (isColor) setColor();
        if (isSaber)
        {
            icon[10].transform.DOMoveX(60, 0.3f);
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
            icon[11].transform.DOMoveX(-50, 0.3f);
        }
        else
        {
            icon[11].transform.DOMoveX(-22, 0.3f).SetEase(Ease.OutBack);
        }
        isGround = !isGround;
    }
    public void SetSetting()
    {
        Debug.Log("hehe");
        setting.SetActive(true);
    }
    public void SetEnergy()
    {
        noInter.SetActive(true);
    }
    public void SetBack()
    {
        loading.gameObject.SetActive(true);
        loading.LoadingTo("SelectSaber");
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
