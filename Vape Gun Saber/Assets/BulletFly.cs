using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    private float force;//lực ném
    private float dir;//góc ném
    private float horiForce;//lực ném ngang
    private float vertiForce;//lực ném dọc
    private float gravitic = -9.8f;
    private float velocity;//tốc độ
    private Vector3 defaultRotation = new Vector3(-90, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(defaultRotation);
        force = Random.Range(8, 13);
        dir = Random.Range(0.7f, 1.3f);
        horiForce = Mathf.Sin(dir) * force;
        vertiForce = Mathf.Cos(dir) * force;
        velocity = horiForce;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += gravitic * Time.deltaTime;
        transform.Translate(new Vector3(velocity, 0, -vertiForce) * Time.deltaTime);
    }
}
