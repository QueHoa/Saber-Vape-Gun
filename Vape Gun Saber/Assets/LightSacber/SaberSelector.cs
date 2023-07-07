using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaberSelector : MonoBehaviour
{
    public SceneLoading loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select(int numSaber)
    {
        Debug.Log(numSaber);
        loading.gameObject.SetActive(true);
        PlayerPrefs.SetInt("SaberSelector", numSaber);
        loading.LoadingTo("LightSaber");
    }
}
