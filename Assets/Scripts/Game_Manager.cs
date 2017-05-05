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
    public List<GameObject> players { get; private set; }
    public int current_turn { get; private set; }
    private Board_Generator generator;
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);       
    }

    public int p1Score = 0;
    public int p2Score = 0;
    public int p3Score = 0;
    public int p4Score = 0;

    void Start() {
        players = new List<GameObject>();
        generator = GetComponent<Board_Generator>();
        GameObject init;
        for (int i = 0; i < num_of_players; i++) {
            init = Instantiate(player_prefab) as GameObject;
            init.GetComponent<Player>().set_player_num(i);
            init.GetComponent<Player>().set_turn(false);
            init.GetComponent<Player>().set_board_manager(generator);
            init.gameObject.SetActive(false);
            players.Add(init);
        }
        players[current_turn].gameObject.SetActive(true);
        players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
    }
	
	void Update () {
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
