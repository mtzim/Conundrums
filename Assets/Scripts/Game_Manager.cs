using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {
    public Player[] players;
    private int turn = 0;
    private int current_turn = 0;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start() {
        players[current_turn].turn = true;
        players[current_turn].gameObject.SetActive(true);
    }
	
	void Update () {
		if(players[current_turn].turn == false) {
            current_turn++;
            if (current_turn == players.Length) {
                current_turn = 0;
                SceneManager.LoadScene(1);
            }
            players[current_turn].turn = true;
            players[current_turn].gameObject.SetActive(true);
        }
	}
}
