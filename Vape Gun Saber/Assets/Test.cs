using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public GameObject targetObject; // Vật thể bạn muốn thay đổi màu sắc

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Instance()
    {
        GameObject effect = (GameObject)Instantiate(targetObject, transform.position, transform.rotation);
    }
}
