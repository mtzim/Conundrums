using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSmack : MonoBehaviour
{
    public float bounce_force;
    public GameObject ownSelf;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject.tag != ownSelf.gameObject.tag)
        {
            Debug.Log("hit " + collision.gameObject.tag);

            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * bounce_force);
        }
    }
}
