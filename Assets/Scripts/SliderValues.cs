using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValues : MonoBehaviour {

    public Slider slider;
    public Text sliderValue;

    private void Start()
    {
        slider.value = GameObject.Find("VolumeManager").GetComponent<VolumeManager>().volumeLevel;
        sliderValue.text = Mathf.Round(slider.value * 100).ToString();
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        sliderValue.text = Mathf.Round(slider.value * 100).ToString();
        //Displays the value of the slider in the console.
        //Debug.Log(slider.value);
    }
}
