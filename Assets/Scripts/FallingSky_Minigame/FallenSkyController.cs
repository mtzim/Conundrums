using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSkyController : MonoBehaviour {

    public GameObject target;

    void Start()
    {
        InvokeRepeating("SpawnObject", 2, 0.5f);
    }

    void SpawnObject()
    {
        float x = Random.Range(-2.5f, 2.5f);
        float z = Random.Range(-4.0f, 2.0f);
        Instantiate(target, new Vector3(x, 5, z), Quaternion.identity);
    }
}
