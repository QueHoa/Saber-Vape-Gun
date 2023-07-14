using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class GunController : MonoBehaviour
{   
    [HideInInspector]
    public bool is3D = false;    
    private bool isZoom = false;
    private bool isGun = false;
    private bool isGround = false;
    
    private int numGun;
    private int numGround;
    public HapticTypes hapticTypes = HapticTypes.HeavyImpact;
    private bool hapticsAllowed = true;
    private bool shakeDetected = false;

    [SerializeField]
    private FlashlightPlugin flash;
    [SerializeField]
    private SceneLoading loading;
    [SerializeField]
    private GameObject setting;   
    [SerializeField]
    private GameObject noInter;
    [SerializeField]
    private Text Bullet;
    public GameObject[] gun;
    public GameObject[] background;
    public GameObject[] icon;       

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        MMVibrationManager.SetHapticsActive(hapticsAllowed);
        numGun = PlayerPrefs.GetInt("GunSelector");
        numGround = 0;
        gun[numGun].SetActive(true);
        background[numGround].SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Join(icon[0].transform.DOMoveY(52, 0.3f)).SetEase(Ease.OutBack);
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
        if (gun[numGun].GetComponent<GunPlay>().isShooting)
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
            gun[numGun].transform.DOMoveZ(6, 0.6f).SetEase(Ease.OutBack);
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
            gun[numGun].transform.DOMoveZ(9, 0.3f);
        }
        isZoom = !isZoom;
    }    
    public void setGun()
    {
        if (isGun)
        {
            icon[10].transform.DOMoveX(-60, 0.3f);
        }
        else
        {
            icon[10].transform.DOMoveX(-22, 0.3f).SetEase(Ease.OutBack);
        }        
        isGun = !isGun;
    }
    public void setGround()
    {
        if (isGround)
        {
            icon[9].transform.DOMoveX(60, 0.3f);
        }
        else
        {
            icon[9].transform.DOMoveX(22, 0.3f).SetEase(Ease.OutBack);
        }
        isGround = !isGround;
    }
    public void SetSetting()
    {
        Debug.Log("hehe");
        setting.SetActive(true);
    }
    public void SetBullet()
    {
        noInter.SetActive(true);
    }
    public void SetBack()
    {
        loading.gameObject.SetActive(true);
        loading.LoadingTo("SelectGun");
    }
    public void ChangeGun(int change)
    {
        gun[numGun].SetActive(false);
        numGun = change;
        gun[numGun].SetActive(true);
    }
    public void ChangeBackground(int change)
    {
        background[numGround].SetActive(false);
        numGround = change;
        background[numGround].SetActive(true);
    }
}
