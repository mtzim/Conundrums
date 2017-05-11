using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Game_Manager : MonoBehaviour {
    public int num_of_players;
    public int num_of_floors;
    public int minigame_probability;
    public GameObject player_prefab;
    public static Game_Manager instance = null;
    public string[] minigame_scene_names;
    public List<GameObject> players { get; private set; }
    public int current_turn { get; private set; }
    private Board_Generator generator;
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(transform.gameObject);       
    }

    public int p1Score = 0;
    public int p2Score = 0;
    public int p3Score = 0;
    public int p4Score = 0;
    private bool in_minigame = false;

    private AudioSource music;

    void Start() {
        music = GetComponent<AudioSource>();
        GameObject amtPlayers = GameObject.Find("NumOfPlayers");
        num_of_players = amtPlayers.GetComponent<NumPlayers>().numberOfPlayers;
        num_of_floors = amtPlayers.GetComponent<NumPlayers>().numberOfFloors;
        Destroy(amtPlayers);
        players = new List<GameObject>();
        generator = GetComponent<Board_Generator>();
        GameObject init;
        for (int i = 0; i < num_of_players; i++) {
            init = Instantiate(player_prefab) as GameObject;
            init.GetComponent<Player>().set_player_num(i);
            init.GetComponent<Player>().set_turn(false);
            init.GetComponent<Player>().setPlayerColor();
            init.GetComponent<Player>().set_board_manager(generator);
            init.gameObject.SetActive(false);
            players.Add(init);
        }
        players[current_turn].gameObject.SetActive(true);
        players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
    }
	
	void Update () {
        if (!in_minigame && !players[current_turn].gameObject.GetComponent<Player>().get_turn()) {
            current_turn++;
            if (current_turn == players.Count) {
                current_turn = 0;
            }
            if (Random.value * 100 > 100 - minigame_probability) {
                in_minigame = true;
                music.mute = true;
                pick_minigame();
            }
            else {
                players[current_turn].gameObject.SetActive(true);
                players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
            }
        }
	}

    void pick_minigame() {
        int choice = (int)(Random.value * 100 % minigame_scene_names.Length);
        store_player_state();
        SceneManager.LoadScene(minigame_scene_names[choice]);
    }

    public void return_from_minigame() {
        SceneManager.LoadScene("board");
        for (int i = 0; i < players.Count; i++) {
            players[i].gameObject.SetActive(true);
        }
        generator.boardHolder.gameObject.SetActive(true);
        players[current_turn].gameObject.GetComponent<Player>().set_turn(true);
        in_minigame = false;
        music.mute = false;
    }

    void store_player_state() {
        for(int i = 0; i < players.Count; i++) {
            players[i].gameObject.SetActive(false);
        }
        generator.boardHolder.gameObject.SetActive(false);
    }

    //when returning to main menu via pause menu, make everything active to be destroyed by CleanSlate()
    public void setAllActive()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(true);
        }
        generator.boardHolder.gameObject.SetActive(true);
    }
}
