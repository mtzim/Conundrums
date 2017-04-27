using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Game_Manager : MonoBehaviour {
    public int num_of_players;
    public int minigame_probability;
    public GameObject player_prefab;
    public string[] minigame_scene_names;
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
            init.gameObject.SetActive(false);
            players.Add(init);
        }
        players[current_turn].gameObject.SetActive(true);
        players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
    }
	
	void Update () {
        Debug.Log(Random.value * 100);
        if (!players[current_turn].gameObject.GetComponent<Player>().get_turn()) {
            current_turn++;
            if (current_turn == players.Count) {
                current_turn = 0;
            }
            players[current_turn].gameObject.SetActive(true);
            players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
            if (Random.value * 100 > 100-minigame_probability) {
                Debug.Log("in");
                pick_minigame();
            }
        }
	}

    void pick_minigame() {
        int choice = (int)(Random.value * 100 % minigame_scene_names.Length);
        store_player_state();
        SceneManager.LoadScene(minigame_scene_names[choice]);
    }

    void return_from_minigame() {
        for (int i = 0; i < players.Count; i++) {
            players[i].gameObject.SetActive(false);
        }
    }

    void store_player_state() {
        for(int i = 0; i < players.Count; i++) {
            players[i].gameObject.SetActive(false);
        }
    }
}
