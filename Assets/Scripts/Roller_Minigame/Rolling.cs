using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour {
    private Rigidbody rb;
    public float thrust;
    public float bounce_force;
    public int player_num;
    public float max_speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float vert = Input.GetAxis("Vertical" + player_num);
        float hori = Input.GetAxis("Horizontal" + player_num);
        Vector3 movement = new Vector3(hori, .0f, vert);
        rb.AddForce(movement * thrust * Time.deltaTime);
        if(rb.velocity.magnitude > max_speed){
            rb.velocity = rb.velocity.normalized * max_speed;
        }
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            Debug.Log("hit");
            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction*bounce_force);
        }
    }
}
