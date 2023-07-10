using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject targetObject; // Vật thể bạn muốn thay đổi màu sắc
    public Slider colorSlider; // Reference đến UI Slider   
    public float r = 1;
    public float g = 0;
    public float b = 0;
    // Start is called before the first frame update
    void Start()
    {
        colorSlider.onValueChanged.AddListener(OnColorChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnColorChanged(float value)
    {
        
        if (value * 6 <= 1)
        {
            b = value * 6;
        }else if (value * 6 <= 2)
        {
            r = 2 - value * 6;
        }else if (value * 6 <= 3)
        {
            g = value * 6 - 2;
        }else if (value * 6 <= 4)
        {
            b = 4 - value * 6;
        }else if (value * 6 <= 5)
        {
            r = value * 6 - 4;
        }
        else
        {
            g = 6 - value * 6;
        }
        Color newColor = new Color(r, g, b); // Thay đổi thành các giá trị R, G, B tương ứng
        targetObject.GetComponent<Renderer>().material.color = newColor;
    }
}
