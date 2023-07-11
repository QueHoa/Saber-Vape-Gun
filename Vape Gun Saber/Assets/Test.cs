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
    public void Move()
    {
        targetObject.transform.DOMoveX(1.2f, 0.5f).SetEase(Ease.OutBack);
    }
}
