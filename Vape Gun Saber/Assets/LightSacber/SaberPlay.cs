using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaberPlay : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject[] longSaber;
    public bool isTouching = false;
    public bool isPower = false;
    public VolumeProfile[] colorSaber;
    public Slider colorSlider; // Reference đến UI Slider

    [SerializeField]
    private float r;
    [SerializeField]
    private float g;
    [SerializeField]
    private float b;

    [SerializeField]
    private Image powerUp;
    [SerializeField]
    private GameObject noEnergy;
    [SerializeField]
    private SaberController saberController;
    [SerializeField]
    private float speed;
    
    private float lengthSword;
    private Vector2 startTouchPosition;

    void Start()
    {       
        powerUp.fillAmount = 1;
        for (int i = 0; i < longSaber.Length; i++)
        {
            longSaber[i].transform.localScale = new Vector3(0, 0, 0);
        }
        colorSlider.onValueChanged.AddListener(OnColorChanged);
    }
    private void OnEnable()
    {
        lengthSword = 0;
        transform.position = new Vector3(0, 0, 13);
        transform.DOMoveZ(9, 0.5f).SetEase(Ease.OutBack);      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Chặn tiếp xúc khi nhấn vào các button
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {                
                return;
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || saberController.isColor)
            {
                isTouching = true;
            }                 
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || saberController.isColor)
            {
                isTouching = false;
            }
            if (saberController.is3D)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPosition = touch.position;
                    Vector2 touchDelta = currentTouchPosition - startTouchPosition;

                    float rotationX = -touchDelta.y * rotateSpeed * Time.deltaTime;
                    float rotationY = touchDelta.x * rotateSpeed * Time.deltaTime;
                    float rotationZ = 0f;

                    transform.Rotate(rotationX, rotationY, rotationZ, Space.World);

                    startTouchPosition = currentTouchPosition;
                }
                if(touch.phase == TouchPhase.Ended)
                {

                }
            }                      
        }        
        if (!saberController.is3D)
        {
            if (isTouching && !isPower)
            {
                if (lengthSword < 1) lengthSword += Time.deltaTime * speed;
                if (lengthSword > 1) lengthSword = 1;
                powerUp.fillAmount -= Time.deltaTime * 0.1f;
            }
            else
            {
                if (lengthSword > 0) lengthSword -= Time.deltaTime * speed;
                if (lengthSword < 0) lengthSword = 0;
            }
            ResetRotate();
        }
        else
        {
            if (lengthSword < 1) lengthSword += Time.deltaTime * speed;
            if (lengthSword > 1) lengthSword = 1;
        }
        if (powerUp.fillAmount == 0)
        {           
            noEnergy.SetActive(true);
            powerUp.fillAmount += Time.deltaTime * 0.1f;
            isPower = true;
            saberController.setColor();
        }
        if (isPower && !noEnergy.activeInHierarchy)
        {
            if (powerUp.fillAmount < 1) powerUp.fillAmount += Time.deltaTime;
            else
            {
                powerUp.fillAmount = 1;
                isPower = false;
            }
        }        
        if (longSaber.Length >= 3)
        {
            longSaber[0].transform.localScale = new Vector3(1, 1, lengthSword);
            for (int i = 1; i < longSaber.Length; i++)
            {
                longSaber[i].transform.localScale = new Vector3(1, lengthSword, 1);
            }
        }
        else
        {
            for (int i = 0; i < longSaber.Length; i++)
            {
                longSaber[i].transform.localScale = new Vector3(1, 1, lengthSword);
            }
        }       
    }
    private void ResetRotate()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);            
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 400);       
    }
    void OnColorChanged(float value)
    {
        if (value * 6 <= 1)
        {
            b = value * 6;
        }
        else if (value * 6 <= 2)
        {
            r = 2 - value * 6;
        }
        else if (value * 6 <= 3)
        {
            g = value * 6 - 2;
        }
        else if (value * 6 <= 4)
        {
            b = 4 - value * 6;
        }
        else if (value * 6 <= 5)
        {
            r = value * 6 - 4;
        }
        else
        {
            g = 6 - value * 6;
        }
        Color newColor = new Color(r, g, b);
        ChangeBloomColor(newColor);
    }
    private void ChangeBloomColor(Color color)
    {
        for (int i = 0; i < colorSaber.Length; i++)
        {
            Bloom bloom;
            if (colorSaber[i].TryGet<Bloom>(out bloom))
            {
                bloom.tint.value = color;
            }
        }
    }
}
