using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SaberPlay : MonoBehaviour
{    
    public Transform[] longSaber;   
    public bool isTouching = false;
    public bool isPower = false;

    private Vector3 saberPosition = new Vector3(0, -0.8f, 9);
    [SerializeField] 
    private Image powerUp;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private float speed;
    private float lengthSword;
    private Vector2 startTouchPosition;

    void Start()
    {
        powerUp.fillAmount = 1;
        for (int i = 0; i < longSaber.Length; i++)
        {
            longSaber[i].localScale = new Vector3(0, 0, 0);
        }
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
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || gameController.is3D || gameController.isColor)
            {
                isTouching = true;
            }                 
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || gameController.is3D || gameController.isColor)
            {
                isTouching = false;
            }
            if (gameController.is3D)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPosition = touch.position;
                    Vector2 touchDelta = currentTouchPosition - startTouchPosition;

                    float rotationX = -touchDelta.y * 0.01f;
                    float rotationY = touchDelta.x * 0.01f;

                    transform.Rotate(rotationX, rotationY, 0);
                }
            }            
        }        
        if (!gameController.is3D)
        {
            if (isTouching && !isPower)
            {
                if (lengthSword < 1) lengthSword += Time.deltaTime * speed;
                if (lengthSword > 1) lengthSword = 1;
                powerUp.fillAmount -= Time.deltaTime * 0.12f;
            }
            if(!isTouching)
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
        if (powerUp.fillAmount <= 0)
        {
            isPower = true;           
        }
        if (isPower)
        {
            if (powerUp.fillAmount < 1) powerUp.fillAmount += Time.deltaTime;
            else
            {
                powerUp.fillAmount = 1;
            }
        }
        if(powerUp.fillAmount == 1)
        {
            isPower = false;
        }
        if (longSaber.Length >= 3)
        {
            longSaber[0].localScale = new Vector3(1, 1, lengthSword);
            for (int i = 1; i < longSaber.Length; i++)
            {
                longSaber[i].localScale = new Vector3(1, lengthSword, 1);
            }
        }
        else
        {
            for (int i = 0; i < longSaber.Length; i++)
            {
                longSaber[i].localScale = new Vector3(1, 1, lengthSword);
            }
        }       
    }
    private void ResetRotate()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);            
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 400);       
    }       
}
