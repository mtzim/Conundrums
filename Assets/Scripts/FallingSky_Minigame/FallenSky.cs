﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSky : MonoBehaviour {
    public int WaitForSecondsRealTime { get; private set; }

    // Use this for initialization
    void Start () {
        DestroyObject(gameObject, 5);
	}

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player0"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Player1"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Player3"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
    }*/
}
