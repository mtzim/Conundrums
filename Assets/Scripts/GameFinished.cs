using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinished : MonoBehaviour {

    public Text winnerTitle;
    public Text winnerMessage;

    private int numPlayers;

    //call as game ends to display winner
    public void finished()
    {       
        whoWon();
        
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
        
    private void whoWon()
    {
        int indexOfMax = 0;
        int max = -1;
        bool multipleWinners = false;

        numPlayers = GameObject.Find("Game_Manager").GetComponent<Game_Manager>().num_of_players;
        string[] players = new string[] { "Player1: ", "\nPlayer2: ", "\nPlayer3: ", "\nPlayer4: " };
        int[] scores = new int[] {GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p1Score,
                                  GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p2Score,
                                  GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p3Score,
                                  GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p4Score};

        for (int i = 0; i < scores.Length; i++)
        {
            if (max < scores[i])
            {
                max = scores[i];
                indexOfMax = i;
            }
            else if (max == scores[i])
            {
                multipleWinners = true;
            }
        }

        if (multipleWinners)
        {
            winnerTitle.text = "Tie!!!";
            numPlayerWinMsg(players, scores);
        }
        else if (indexOfMax == 0)
        {
            winnerTitle.text = "Player1 Wins!!!";
            numPlayerWinMsg(players,scores);
        }
        else if (indexOfMax == 1)
        {
            winnerTitle.text = "Player2 Wins!!!";
            numPlayerWinMsg(players, scores);
        }
        else if (indexOfMax == 2)
        {
            winnerTitle.text = "Player3 Wins!!!";
            numPlayerWinMsg(players, scores);
        }
        else
        {
            winnerTitle.text = "Player4 Wins!!!";
            numPlayerWinMsg(players, scores);
        }
    }

    private void numPlayerWinMsg(string[] player, int[] score)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            winnerMessage.text += player[i] + score[i];
        }
    }
}
