using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AggroDetection aggroDetection;
    private Health healthTarget;
    private float attackTimer;
    public float attackRefreshRate = 2.5f;

    public GameObject cannonBallPrefab;
    public int damageAmount = 1;
    public Transform leftCannonFirePoint;
    public Transform rightCannonFirePoint;

    public bool usingDualCannons;

    public GameObject leftCannon;
    public GameObject rightCannon;

    public GameObject cannonFireEffectPrefab;


    private Animator anim;

    public AudioClip cannonFireSND;
    public AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAgrro += AggroDetection_OnAgrro;
    }

    private void AggroDetection_OnAgrro(Transform target)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            healthTarget = health;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCannonAppearance();
        if (healthTarget != null)
        {
            attackTimer += Time.deltaTime;

            if (CanAttack())
            {
                Attack();
            }
        }
    }

    public void SetCannonAppearance()
    {
        if (usingDualCannons)
        {
            rightCannon.SetActive(true);
        } else
        {
            rightCannon.SetActive(false);
        }
    }

    private bool CanAttack()
    {
        return attackTimer >= attackRefreshRate;
    }

    private void Attack()
    {
        attackTimer = 0f;
        transform.LookAt(healthTarget.transform);
        //Quaternion leftCannonBaseRotation = leftCannon.transform.rotation;
        //leftCannon.transform.LookAt(healthTarget.transform);
        InstantiateAtPosition(cannonFireEffectPrefab, leftCannonFirePoint, 2.0f);
        GameObject leftCannonBall = Instantiate(cannonBallPrefab);
        Physics.IgnoreCollision(leftCannonBall.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
        leftCannonBall.transform.position = leftCannonFirePoint.transform.position;
        leftCannonBall.transform.rotation = leftCannonFirePoint.rotation;
        leftCannonBall.GetComponent<CannonBall>().damage = damageAmount;
        leftCannonBall.GetComponent<CannonBall>().characterName = this.GetComponent<CharacterInfo>().info.characterName;
        audioSource.PlayOneShot(cannonFireSND);
        //leftCannon.transform.rotation = leftCannonBaseRotation;
        if (usingDualCannons)
        {
            //Quaternion rightCannonBaseRotation = rightCannon.transform.rotation;
            //rightCannon.transform.LookAt(healthTarget.transform);
            InstantiateAtPosition(cannonFireEffectPrefab, rightCannonFirePoint, 2.0f);
            GameObject rightCannonBall = Instantiate(cannonBallPrefab);
            Physics.IgnoreCollision(rightCannonBall.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(rightCannonBall.GetComponent<Collider>(), leftCannonBall.GetComponent<Collider>(), true);
            rightCannonBall.transform.position = rightCannonFirePoint.transform.position;
            rightCannonBall.transform.rotation = rightCannonFirePoint.transform.rotation;
            rightCannonBall.GetComponent<CannonBall>().damage = damageAmount;
            rightCannonBall.GetComponent<CannonBall>().characterName = this.GetComponent<CharacterInfo>().info.characterName;
            //rightCannon.transform.rotation = rightCannonBaseRotation;
        }



        anim.SetTrigger("Shoot");
    }

    public void InstantiateAtPosition(GameObject obj, Transform position, float deathTime)
    {
        if (obj != null)
        {
            GameObject o = Instantiate(obj);
            o.transform.position = position.position;
            if (deathTime != 0)
            {
                Destroy(o, deathTime);
            }
        }
    }
}
