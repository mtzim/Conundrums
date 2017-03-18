using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
    public int num_of_players;
    public Player[] players;
    private int turn = 0;
	void Start () {
        players = new Player[num_of_players];
	}
	
	void Update () {
		
	}

}
