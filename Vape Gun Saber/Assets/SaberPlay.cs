using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberPlay : MonoBehaviour
{
    public Renderer renderer;
    [SerializeField]private float speed;
    [SerializeField]private float maxHight;
    private float dissolve = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(dissolve > -maxHight) dissolve -= Time.deltaTime * speed;
        }
        else
        {
            if(dissolve <= 0) dissolve += Time.deltaTime * speed;
        }
        renderer.material.SetFloat("_dissolveamount", dissolve);
    }
}
