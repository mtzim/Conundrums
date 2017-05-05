using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float rotationSpeed = 150f;
    public float moveSpeed = 3f;

    public int p1HP = 100;
    private int p2HP = 100;
    private int p3HP = 100;
    private int p4HP = 100;

    // Use this for initialization
    void Start () {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
    }

    private void movePlayer()
    {
        if (gameObject.CompareTag("Player0"))
        {
            float x = Input.GetAxis("Horizontal0") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical0") * Time.deltaTime * moveSpeed;

            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
        }
    }
}
