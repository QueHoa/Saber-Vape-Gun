using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 local = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = local;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
