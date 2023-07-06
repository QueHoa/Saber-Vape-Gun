using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaberPlay : MonoBehaviour
{
    public bool isTouching = false;
    public Transform[] saber;
    public Image powerUp;

    public GameController gameController;

    [SerializeField]
    private float speed;
    private Vector2 startTouchPosition;
    private float lengthSword = 0;   
    
    void Start()
    {      
        for (int i = 0; i < saber.Length; i++)
        {
            saber[i].localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {                
                return;
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || gameController.isSword)
            {
                isTouching = true;
            }                      
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || gameController.isSword)
            {
                isTouching = false;
            }
            if (touch.phase == TouchPhase.Began && gameController.isSword)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && gameController.isSword)
            {
                Vector2 currentTouchPosition = touch.position;
                Vector2 touchDelta = currentTouchPosition - startTouchPosition;

                float rotationX = -touchDelta.y * 0.01f;
                float rotationY = touchDelta.x * 0.01f;

                transform.Rotate(rotationX, rotationY, 0);
            }
            if (!gameController.isSword)
            {
                Vector3 targetDirection = Vector3.zero - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 50);
            }
        }
        if (!gameController.isSword)
        {
            if (isTouching)
            {
                if (lengthSword < 1) lengthSword += Time.deltaTime * speed;
                if (lengthSword > 1) lengthSword = 1;
                powerUp.fillAmount -= Time.deltaTime * 0.12f;
            }
            else
            {
                if (lengthSword > 0) lengthSword -= Time.deltaTime * speed;
                if (lengthSword < 0) lengthSword = 0;
            }
        }
        else
        {
            if (lengthSword < 1) lengthSword += Time.deltaTime * speed;
            if (lengthSword > 1) lengthSword = 1;
        }
        if (powerUp.fillAmount <= 0)
        {            
            isTouching = false;
            if (powerUp.fillAmount < 1) powerUp.fillAmount += Time.deltaTime * 2;
            else
            {
                powerUp.fillAmount = 1;
            }
        }
        if (saber.Length >= 3)
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
}
