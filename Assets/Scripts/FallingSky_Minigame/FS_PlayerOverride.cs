using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FS_PlayerOverride : MonoBehaviour {

    private bool fs_SceneLoaded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (fs_SceneLoaded)
        {
            Debug.Log("PlayerOverride Success");
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "FallingSky_Minigame")
        {
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<Light>().enabled = false;
            gameObject.SetActive(true);
            fs_SceneLoaded = true;
        }
    }
}
