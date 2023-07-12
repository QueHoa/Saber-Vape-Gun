using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GunPlay : MonoBehaviour
{
    public bool isTouching = false;
    public bool isPower = false;

    [SerializeField]
    private int numBullet;
    [SerializeField]
    private Text bullet;
    [SerializeField]
    private GameObject noBullet;
    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private Animator anim;
    private Vector2 startTouchPosition;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        transform.position = new Vector3(0, 0, 13);
        transform.DOMoveZ(9, 0.5f).SetEase(Ease.OutBack);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began)
            {
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
        if (isTouching)
        {
            Shoot();
        }
    }    
    private void Shoot()
    {
        anim.SetTrigger("shoot");
    }
}
