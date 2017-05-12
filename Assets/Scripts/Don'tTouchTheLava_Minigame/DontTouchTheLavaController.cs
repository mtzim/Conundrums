using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontTouchTheLavaController : MonoBehaviour {

    public GameObject[] players;
    public Text winnerText;
    public Text winnerMessage;

    private int numPlayers;
    private bool winnerSplash = false;
    private float backToMainGameTimer = 5f;
    public float spawnTime = 0.5f;
    private bool allDied = true;
    private AudioSource sound;
    public AudioClip victorySound;
    private bool played;


    void Start()
    {
        numPlayers = GameObject.Find("Game_Manager").GetComponent<Game_Manager>().num_of_players;
        for (int i = 0; i < numPlayers; i++)
        {
            players[i].SetActive(true);
        }
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        winner();
        if (winnerSplash)
        {
            setText();
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
            CancelInvoke();

            backToMainGameTimer -= Time.deltaTime;
            if (backToMainGameTimer < 0)
            {
                GameObject.Find("Game_Manager").GetComponent<Game_Manager>().return_from_minigame();
            }
        }
    }

    private void setText()
    {

        if ((players[0].activeInHierarchy == true))
        {
            winnerText.text = "Winner Player 1!!!";
            allDied = false;
        }
        else if ((players[1].activeInHierarchy == true))
        {
            winnerText.text = "Winner Player 2!!!";
            allDied = false;
        }
        else if ((players[2].activeInHierarchy == true))
        {
            winnerText.text = "Winner Player 3!!!";
            allDied = false;
        }
        else if ((players[3].activeInHierarchy == true))
        {
            winnerText.text = "Winner Player 4!!!";
            allDied = false;
        }
        else if (allDied) //everyone died at the same time
        {
            winnerText.text = "...YOU ALL DIED!!!";
            winnerMessage.text = "No points for any of you!";
        }
        if (!played)
        {
            sound.volume = 0.5f;
            played = true;
            sound.PlayOneShot(victorySound);
        }
    }

    private void winner()
    {
        if ((players[0].activeInHierarchy == true) && (players[1].activeInHierarchy == false) &&
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == false) && winnerSplash == false)
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p1Score += 100;
            winnerSplash = true;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == true) &&
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == false) && winnerSplash == false)
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p2Score += 100;
            winnerSplash = true;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == false) &&
            (players[2].activeInHierarchy == true) && (players[3].activeInHierarchy == false) && winnerSplash == false)
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p3Score += 100;
            winnerSplash = true;
        }
        if ((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == false) &&
            (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == true) && winnerSplash == false)
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p4Score += 100;
            winnerSplash = true;
        }
        else if((players[0].activeInHierarchy == false) && (players[1].activeInHierarchy == false) &&
                (players[2].activeInHierarchy == false) && (players[3].activeInHierarchy == false) && winnerSplash == false)
        {
            winnerSplash = true;
        }
    } 
}
