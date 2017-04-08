using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {
    public int num_of_players;
    public int minigame_probability;
    public GameObject player_prefab;

    private List<GameObject> players = new List<GameObject>();
    private int current_turn = 0;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start() {
        GameObject init;
        for (int i = 0; i < num_of_players; i++) {
            init = Instantiate(player_prefab) as GameObject;
            init.GetComponent<Player>().set_player_num(i);
            init.GetComponent<Player>().set_turn(false);
            init.gameObject.GetComponent<Player>().minigame_prob = minigame_probability;
            init.gameObject.SetActive(false);
            players.Add(init);
        }
        players[current_turn].gameObject.SetActive(true);
        players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
    }
	
	void Update () {
		if(!players[current_turn].gameObject.GetComponent<Player>().get_turn()) {
            current_turn++;
            if (current_turn == players.Count) {
                current_turn = 0;
            }
            players[current_turn].gameObject.SetActive(true);
            players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
        }
	}
}
