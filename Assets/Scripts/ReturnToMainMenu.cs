using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{

    private AudioSource[] allAudioSources;
    private GameObject[] allGameObjects;

    private GameObject gameManager;
    private GameObject volumeManager;

    public void CleanSlate()
    {
        gameManager = GameObject.Find("Game_Manager");
        gameManager.GetComponent<Game_Manager>().setAllActive();
        volumeManager = GameObject.Find("VolumeManager");
        volumeManager.SetActive(false);
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        allGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
        foreach (GameObject objectG in allGameObjects)
        {
            Destroy(objectG);
        }
        volumeManager.SetActive(true);
        Time.timeScale = 1; //restores time because pause menu set it to 0
    }
}
