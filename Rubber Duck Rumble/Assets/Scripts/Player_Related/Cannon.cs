using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cannon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [Range(0.5f, 1.5f)]
    public float fireRate = 1f;
    [Range(1, 10)]
    public int damage = 1;
    private float timer;
    public bool dualCannons;

    [Header("Cannon Ball Fire Locations")]
    public Transform cannonFirePointRight;
    public Transform cannonFirePointLeft;

    [Header("Prefabs")]
    public GameObject cannonballPrefab;
    public GameObject cannonFireEffect;

    [Header("Cannon Models")]
    public GameObject rightCannon;
    public GameObject leftCannon;

    [Header("Audio")]
    public AudioClip shootSND;
    public AudioSource audioSource;

    [Header("Targeting")]
    public Sprite noTarget;
    public Sprite targetSeen;
    public Image targetingIcon;
    public LayerMask raycastLayers;
    public TextMeshProUGUI healthNumberText;

    CharacterInfo info;
    private void Awake()
    {
        info = GetComponent<CharacterInfo>();
    }

    void Update()
    {
        CheckForTarget();
        SetCannonAppearance();
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Jump"))
            {
                timer = 0f;
                FireCannon();
            }
        }
    }
    void SetCannonAppearance()
    {
        if (dualCannons)
        {
            leftCannon.SetActive(true);
        } else
        {
            leftCannon.SetActive(false);
        }
    }
    void CheckForTarget()
    {
        Ray ray = new Ray(cannonFirePointRight.position, cannonFirePointRight.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, raycastLayers))
        {
            //Debug.Log("Raycast hit: " + hitInfo.collider.name);
            var health = hitInfo.collider.GetComponent<Health>();
            if (health != null)
            {
                
                targetingIcon.sprite = targetSeen;
                healthNumberText.text = health.currentHealth.ToString();
            }
            else
            {
                targetingIcon.sprite = noTarget;
                healthNumberText.text = "";
            }
        }
        else
        {
            targetingIcon.sprite = noTarget;
            healthNumberText.text = "";
        }
    }
    private void FireCannon()
    {

        GameObject effect = Instantiate(cannonFireEffect);
        effect.transform.position = cannonFirePointRight.position;
        Destroy(effect, 3f);
        //Debug.Log("Shot Cannon");
        Debug.DrawRay(cannonFirePointRight.position, cannonFirePointRight.forward * 100, Color.red, 2f);
        GameObject cannonball = Instantiate(cannonballPrefab);
        Physics.IgnoreCollision(cannonball.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
        cannonball.transform.position = cannonFirePointRight.position;
        cannonball.transform.rotation = cannonFirePointRight.rotation;
        cannonball.GetComponent<CannonBall>().damage = damage;
        cannonball.GetComponent<CannonBall>().characterName = info.info.characterName;
        audioSource.PlayOneShot(shootSND);

        if (dualCannons)
        {
            GameObject effect2 = Instantiate(cannonFireEffect);
            effect2.transform.position = cannonFirePointLeft.position;
            Destroy(effect2, 3f);
            //Debug.Log("Shot Cannon");
            Debug.DrawRay(cannonFirePointLeft.position, cannonFirePointLeft.forward * 100, Color.red, 2f);
            GameObject cannonball2 = Instantiate(cannonballPrefab);
            Physics.IgnoreCollision(cannonball2.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
            cannonball2.transform.position = cannonFirePointLeft.position;
            cannonball2.transform.rotation = cannonFirePointLeft.rotation;
            cannonball2.GetComponent<CannonBall>().damage = damage;
            audioSource.PlayOneShot(shootSND);
        }
        //Ray ray = new Ray(cannonFirePointRight.position, cannonFirePointRight.forward);
        //RaycastHit hitInfo;

        /*if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.name);
            var health = hitInfo.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }*/
    }
}
