﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float rotationSpeed = 150f;
    public float moveSpeed = 3f;

    public int p1HP = 100;
    public int p2HP = 100;
    public int p3HP = 100;
    public int p4HP = 100;

    // Use this for initialization
    void Start () {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        checkHealth();
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
        if (gameObject.CompareTag("Player1"))
        {
            float x = Input.GetAxis("Horizontal1") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical1") * Time.deltaTime * moveSpeed;

            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
        }
        if (gameObject.CompareTag("Player2"))
        {
            float x = Input.GetAxis("Horizontal2") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical2") * Time.deltaTime * moveSpeed;

            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
        }
        if (gameObject.CompareTag("Player3"))
        {
            float x = Input.GetAxis("Horizontal3") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical3") * Time.deltaTime * moveSpeed;

            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
        }
    }

    private void checkHealth()
    {
        if (gameObject.CompareTag("Player0") && (p1HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player1") && (p2HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player2") && (p3HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player3") && (p4HP <= 0))
        {
            gameObject.SetActive(false);
        }
    }
}