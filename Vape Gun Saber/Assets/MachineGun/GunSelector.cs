using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GunSelector : MonoBehaviour
{
    public SceneLoading loading;
    public GameObject[] button;
    // Start is called before the first frame update
    void Start()
    {
        button[0].transform.DOMoveZ(90f, 0.4f).SetEase(Ease.OutBack);
        button[1].transform.DOMoveZ(90f, 0.4f).SetEase(Ease.OutBack);
        button[2].transform.DOMoveX(-16, 0.4f).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select(int numSaber)
    {
        loading.gameObject.SetActive(true);
        PlayerPrefs.SetInt("GunSelector", numSaber);
        loading.LoadingTo("MachineGun");
    }
    public void SetBack()
    {
        loading.gameObject.SetActive(true);
        loading.LoadingTo("MasterLevel");
    }
}
