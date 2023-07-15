using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 0.01f)
        {
            transform.Rotate(new Vector3(0.5f, 1, 0), 500 * Time.deltaTime);
        }
    }
}
