using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll_Manager : MonoBehaviour {
    public GameObject ball_prefab;
    public GameObject win_panel;
    [SerializeField]private int num_of_players;
    private Game_Manager manager;
    private int[] points;
    private Vector3[] spawn_positions = {
        new Vector3(-2f, 2.4f, 2f),
        new Vector3(-2f, 2.4f, -2f),
        new Vector3(2f, 2.4f, 2f),
        new Vector3(2f, 2.4f, -2f)
    };
    private bool countdown;
    private float time;
    private List<int> alive;

    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>();
        num_of_players = manager.num_of_players;
        time = 0;
        points = new int[num_of_players];
        alive = new List<int>();
        init();
    }

    public void init() {
        for(int i = 0; i < num_of_players; i++) {
            GameObject init = Instantiate(ball_prefab);
            init.GetComponent<Rolling>().player_num = i;
            init.GetComponent<Rolling>().manager = this;
            init.transform.position = spawn_positions[i];
            Debug.Log("spawn ball");
            alive.Add(i);
        }
    }

    public void update_points(int player_num){
        int value = 100 / alive.Count;
        alive.Remove(player_num);
        if(alive.Count == 1) {
            int winner = alive[0];
            switch (winner) {
                case 0:
                    manager.p1Score += 100;
                    break;
                case 1:
                    manager.p2Score += 100;
                    break;
                case 2:
                    manager.p3Score += 100;
                    break;
                case 3:
                    manager.p4Score += 100;
                    break;
            }
            winner++;
            win_panel.transform.GetChild(0).GetComponent<Text>().text = "Player " + winner + " is the Winner!!!";
            countdown = true;
        }
    }

    void Update() {
        if (countdown) {
            time += Time.deltaTime;
            win_panel.SetActive(true);
            if (time > 5)
                manager.return_from_minigame();
        }
    }
}
