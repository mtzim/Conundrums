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
        winner();
    }

    void SpawnObject()
    {
        float x = Random.Range(-2.5f, 2.5f);
        float z = Random.Range(-4.0f, 2.0f);
        Instantiate(target, new Vector3(x, 5, z), Quaternion.identity);
    }

    private void winner()
    {
        if ((players[0].activeInHierarchy == true) && (players[1].activeInHierarchy == false) && 
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == false))
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p1Score += 100;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == true) &&
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == false))
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p2Score += 100;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == false) &&
            (players[2].activeInHierarchy == true) && (players[3].activeInHierarchy == false))
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p3Score += 100;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == false) &&
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == true))
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p4Score += 100;
        }

        //return to board --figureout howto
    }
}
