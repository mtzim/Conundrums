using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManagerSpawner : MonoBehaviour {

    public GameObject VolumeManagerPrefab;

    private GameObject volumeManager;

    private void Start()
    {
        if (volumeManager = GameObject.Find("VolumeManager"))
        {
            //do nothing
        }
        else
        {
            GameObject rename = Instantiate(VolumeManagerPrefab);
            rename.name = "VolumeManager";
            volumeManager = rename;
        }
    }

    public void ManageVolume()
    {
        volumeManager.GetComponent<VolumeManager>().UpdateVolume();
    }

}
