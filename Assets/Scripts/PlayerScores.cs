using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{

    public GameObject[] playerTitles;
    public Text[] playerScores;

    private int numPlayers;
    private GameObject gameManager;
    private string[] pTitles = new string[] { "P1: ", "P2: ", "P3: ", "P4: " };
    private int[] pScores;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game_Manager");
        pScores = new int[] {gameManager.GetComponent<Game_Manager>().p1Score, gameManager.GetComponent<Game_Manager>().p2Score,
                             gameManager.GetComponent<Game_Manager>().p3Score,gameManager.GetComponent<Game_Manager>().p4Score};

        numPlayers = gameManager.GetComponent<Game_Manager>().num_of_players;
        scoreText(pTitles, pScores); //set scores for each player
        for (int i = 0; i < numPlayers; i++)
        {
            playerTitles[i].SetActive(true); //activate only the player scores that are playing
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void scoreText(string[] player, int[] score)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            playerScores[i].text = player[i] + score[i];
        }
    }
}
