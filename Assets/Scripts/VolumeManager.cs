using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

    public GameObject slider;
    public float volumeLevel = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameObject.Find("SF Slider"))
        {
            slider = GameObject.Find("SF Slider");
        }
    }

    public void UpdateVolume()
    {
        volumeLevel = slider.GetComponent<Slider>().value;
        AudioListener.volume = volumeLevel;
    }
}
