using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float moveSpeed = 15.0f;

    Rigidbody body;
    public float deathTime = 5.0f;
    float deathCounter = 0f;

    public int damage;

    public GameObject collisionDestroyEffectPrefab;
    public GameObject timeoutDestroyEffectPrefab;

    [Header("Owner Info")]
    public string characterName;
    public int characterTeamID;

    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathCounter < deathTime)
        {
            deathCounter += Time.deltaTime;
        } else
        {
            if (timeoutDestroyEffectPrefab != null)
            {
                GameObject effect = Instantiate(timeoutDestroyEffectPrefab);
                effect.transform.position = this.transform.position;
                Destroy(effect, 3.0f);
            }
            Destroy(this.gameObject);
        }
        //body.MovePosition(transform.forward * moveSpeed * Time.deltaTime);

        
        
    }

    public void SetOwnerInfo(string ownerName, int ownerTeamID)
    {
        characterName = ownerName;
        characterTeamID = ownerTeamID;
    }
    private void FixedUpdate()
    {
        transform.position += (transform.forward * moveSpeed * Time.deltaTime);
        //body.AddForce(transform.forward * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage, characterName);
            health.CallKnockBack(other.transform.position - this.transform.position);
        }
        if (collisionDestroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(collisionDestroyEffectPrefab);
            effect.transform.position = this.transform.position;
            Destroy(effect, 3.0f);
        }
        Destroy(this.gameObject);
    }
}
