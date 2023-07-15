using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GunPlay : MonoBehaviour
{
    //[HideInInspector]
    public bool isTouching = false;
    [HideInInspector]
    public bool isReload = false;
    [HideInInspector]
    public bool isShooting = false;

    [SerializeField]
    private bool isBomb = false;
    [SerializeField]
    private bool isSmoke = false;
    [SerializeField]
    private bool isBursting = false;
    public bool noBurst;
    [SerializeField]
    private int numBurst;
    [SerializeField]
    private GameObject shootEffect;
    [SerializeField]
    private GameObject curvySmoke;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shootTran;
    [SerializeField]
    private Transform bulletTran;
    [SerializeField]
    private Vector3 rotationEffect = new Vector3();    
    [SerializeField]
    private float shootCooldown;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private int chamber = 0;
    [SerializeField]
    private Text TextBullet;
    [SerializeField]
    private GameObject noBullet;
    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private Animator anim;
    private float rotateSpeed;
    private int numBullet;
    private float defaultTime;
    private float cooldownTimer = Mathf.Infinity;
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
        numBullet = chamber;
        defaultTime = shootCooldown;
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
            if (gunController.isAuto)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isTouching = false;
                }
            }
            if (gunController.isSingle)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                }
                else
                {
                    isTouching = false;
                }
            }
            if (gunController.isBurst)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                }
                else
                {
                    isTouching = false;
                }
            }
            if (gunController.isShake)
            {
                
            }
            if (gunController.is3D)
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
                if (touch.phase == TouchPhase.Ended)
                {

                }
            }            
        }
        if (!gunController.is3D)
        {           
            if (!gunController.isBurst)
            {
                if (isTouching && cooldownTimer >= shootCooldown && numBullet != 0)
                {
                    if (cooldownTimer >= (shootCooldown + 0.2f) && isSmoke)
                    {
                        GameObject smokeEffect = (GameObject)Instantiate(curvySmoke, shootTran.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                        Destroy(smokeEffect, 2);
                    }
                    Shoot();  
                }
            }
            else
            {
                if (isTouching && !isBursting)
                {
                    isBursting = true;
                    float t = numBurst * defaultTime;
                    while (t >= 0 && cooldownTimer >= shootCooldown && numBullet != 0)
                    {
                        if (cooldownTimer >= (shootCooldown + 0.2f) && isSmoke)
                        {
                            GameObject smokeEffect = (GameObject)Instantiate(curvySmoke, shootTran.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                            Destroy(smokeEffect, 2);
                        }
                        Shoot();
                        t -= Time.deltaTime;
                    }
                    isBursting = false;
                }            
            }         
            ResetRotate();
        }       
        TextBullet.text = numBullet.ToString();
        /*if (numBullet == 0 && !isReload )
        {
            StartCoroutine(NoBullet());
        }*/
        if (!noBullet.activeInHierarchy && shootCooldown == Mathf.Infinity && isReload)
        {
            StartCoroutine(Reload());           
        }
        cooldownTimer += Time.deltaTime;
    }    
    private void Shoot()
    {
        isShooting = true;
        cooldownTimer = 0;
        GameObject effect = (GameObject)Instantiate(shootEffect, shootTran.position, Quaternion.Euler(rotationEffect));        
        Destroy(effect, defaultTime);
        if (!isBomb)
        {
            GameObject effectBullet = (GameObject)Instantiate(bullet, bulletTran.position, bulletTran.rotation);
            Destroy(effectBullet, 5);
        }       
        anim.SetTrigger("shoot");        
        numBullet--;                
        isShooting = false;
        if (numBullet == 0 && !isReload)
        {           
            StartCoroutine(NoBullet());
        }        
    }
    private void ResetRotate()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 400);
    }
    IEnumerator Reload()
    {
        shootCooldown = defaultTime;
        anim.SetTrigger("reload");
        yield return new WaitForSeconds(reloadTime);        
        isReload = false;
        numBullet = chamber;
        yield return 0;
    }   
    IEnumerator NoBullet()
    {
        shootCooldown = Mathf.Infinity;
        yield return new WaitForSeconds(defaultTime);       
        noBullet.SetActive(true);
        isReload = true;

        yield return 0;
    }
}
