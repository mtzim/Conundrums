using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoRolling : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust;
    public float bounce_force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("hit");
            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * bounce_force);
        }
    }
}
