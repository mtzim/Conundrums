using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour {
    private Rigidbody rb;
    public float thrust;
    public float bounce_force;
    public int player_num;
    public float max_speed;
    public Roll_Manager manager;
    private Renderer ballRend;

    void Start() {
        rb = GetComponent<Rigidbody>();
        ballRend = GetComponent<Renderer>();

        Color armorColor;
        switch (player_num)
        {
            case 0:
                armorColor = new Color(1f, 0f, 0f, 1f);
                break;
            case 1:
                armorColor = new Color(0f, 0f, 1f, 1f);
                break;
            case 2:
                armorColor = new Color(0f, 1f, 0f, 1f);
                break;
            case 3:
                armorColor = new Color(1f, 0.92f, 0.016f, 1f);
                break;
            default:
                armorColor = new Color(1f, 1f, 1f, 1f);
                break;
        }
        ballRend.material.color = armorColor;
    }

    void FixedUpdate() {
        float vert = Input.GetAxis("Vertical" + player_num);
        float hori = Input.GetAxis("Horizontal" + player_num);
        Vector3 movement = new Vector3(hori, 0.0f, vert);
        rb.AddForce(movement * thrust * Time.deltaTime);
        if (rb.velocity.magnitude > max_speed) {
            rb.velocity = rb.velocity.normalized * max_speed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("hit");
            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * bounce_force);
        }
    }

    void OnTriggerEnter(Collider collide) {
        if (collide.tag == "Lava") {
            Debug.Log("lava");
            manager.update_points(player_num);
            Destroy(this.gameObject);
        }
    }
}
