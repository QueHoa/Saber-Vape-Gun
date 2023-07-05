using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberPlay : MonoBehaviour
{
    public Transform[] saber;
    [SerializeField]private float speed;
    private float lengthSword = 0;
    
    private void Awake()
    {
        for (int i = 0; i < saber.Length; i++)
        {
            saber[i].localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(lengthSword < 1) lengthSword += Time.deltaTime * speed;
        }
        else
        {
            if (lengthSword > 0) lengthSword -= Time.deltaTime * speed;
            if (lengthSword < 0) lengthSword = 0;
        }
        if(saber.Length >= 3)
        {
            saber[0].localScale = new Vector3(1, 1, lengthSword);
            for (int i = 1; i < saber.Length; i++)
            {
                saber[i].localScale = new Vector3(1, lengthSword, 1);
            }
        }
        else
        {
            for (int i = 0; i < saber.Length; i++)
            {
                saber[i].localScale = new Vector3(1, 1, lengthSword);
            }
        }     
    }
    private void Set3D()
    {
            
    }
}
