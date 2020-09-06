using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackObject : MonoBehaviour
{
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KnockBackObject>())
        {
            if ((other.GetComponent<Rigidbody>().velocity.magnitude > this.body.velocity.magnitude))
            {
                //If other object is faster
                Vector3 hitDirection = (other.transform.position - transform.position).normalized;
                KnockBack(hitDirection, other.GetComponent<Rigidbody>().velocity.magnitude);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Collision with object with Rigidbody");
        }
    }

    public void KnockBack(Vector3 direction, float amount)
    {
        Debug.Log("Object Knocked Back by: " + amount);
        body.AddForce((-direction * amount * 2f) + (Vector3.up * amount * 0.1f), ForceMode.Impulse);
        
    }
}
