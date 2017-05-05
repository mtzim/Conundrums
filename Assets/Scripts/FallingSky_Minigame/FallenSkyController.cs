using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSkyController : MonoBehaviour {

    public GameObject target;
    public GameObject[] players;
    private int numPlayers;

    void Start()
    {
        InvokeRepeating("SpawnObject", 2, 0.5f);
        numPlayers = GameObject.Find("Game_Manager").GetComponent<Game_Manager>().num_of_players;
        for (int i = 0; i < numPlayers; i++)
        {
            players[i].SetActive(true);
        }
    }

    private void Update()
    {
        
    }

    void SpawnObject()
    {
        float x = Random.Range(-2.5f, 2.5f);
        float z = Random.Range(-4.0f, 2.0f);
        Instantiate(target, new Vector3(x, 5, z), Quaternion.identity);
    }
}
