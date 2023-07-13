using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GunPlay : MonoBehaviour
{
    public bool isTouching = false;
    public bool isReload = false;
    public bool isShooting = false;

    [SerializeField]
    private float shootCooldown;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private int numBullet = 0;
    [SerializeField]
    private Text bullet;
    [SerializeField]
    private GameObject noBullet;
    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private Animator anim;
    private int chamber;
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
        chamber = numBullet;
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
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began)
            {
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
        if (isTouching && cooldownTimer >= shootCooldown)
        {
            Shoot();
        }
        bullet.text = numBullet.ToString();
        /*if (numBullet == 0 && !isReload )
        {
            StartCoroutine(NoBullet());
        }*/
        if (!noBullet.activeInHierarchy && shootCooldown == Mathf.Infinity)
        {
            StartCoroutine(Reload());           
        }
        cooldownTimer += Time.deltaTime;
    }    
    private void Shoot()
    {
        isShooting = true;
        cooldownTimer = 0;
        anim.SetTrigger("shoot");        
        numBullet--;      
        if(numBullet == 0 && !isReload)
        {
            shootCooldown = Mathf.Infinity;
            StartCoroutine(NoBullet());
        }   
        isShooting = false;
    }
    IEnumerator Reload()
    {
        Debug.Log("hehe");
        shootCooldown = defaultTime;
        anim.SetTrigger("reload");
        yield return new WaitForSeconds(reloadTime);        
        isReload = false;
        numBullet = chamber;
        yield return 0;
    }   
    IEnumerator NoBullet()
    {
        yield return new WaitForSeconds(defaultTime);
        noBullet.SetActive(true);
        isReload = true;

        yield return 0;
    }
}
