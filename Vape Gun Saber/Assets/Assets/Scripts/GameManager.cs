using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public SceneLoading loading;
    public GameObject[] button;
    // Start is called before the first frame update
    void Start()
    {
        button[0].transform.DOMoveX(-2, 0.4f).SetEase(Ease.OutBack);
        button[1].transform.DOMoveX(2, 0.4f).SetEase(Ease.OutBack);
        button[2].transform.DOMoveX(2, 0.4f).SetEase(Ease.OutBack);
        button[3].transform.DOMoveX(-0.5f, 0.4f).SetEase(Ease.OutBack);
        button[4].transform.DOMoveX(-0.5f, 0.4f).SetEase(Ease.OutBack);
        button[5].transform.DOMoveX(-0.5f, 0.4f).SetEase(Ease.OutBack);
        button[6].transform.DOMoveX(-0.5f, 0.4f).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LightSaber()
    {
        loading.gameObject.SetActive(true);
        loading.LoadingTo("SelectSaber");
    }
}
