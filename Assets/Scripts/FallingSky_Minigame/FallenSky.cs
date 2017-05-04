using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSky : MonoBehaviour {
    public int WaitForSecondsRealTime { get; private set; }

    // Use this for initialization
    void Start () {
        DestroyObject(gameObject, 5);
	}
}
