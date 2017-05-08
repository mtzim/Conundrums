using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll_Manager : MonoBehaviour {
    public GameObject ball_prefab;
    private int num_of_players;
    private int alive;
    private Game_Manager manager;
    private Vector3[] spawn_positions = {
        new Vector3(-2f, 2.4f, 2f),
        new Vector3(-2f, 2.4f, -2f),
        new Vector3(2f, 2.4f, 2f),
        new Vector3(2f, 2.4f, -2f)
    };

    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>();
        num_of_players = manager.num_of_players;
        alive = num_of_players;
        init();
    }

    public void init() {
        for(int i = 0; i < num_of_players; i++) {
            GameObject init = Instantiate(ball_prefab);
            init.GetComponent<Rolling>().player_num = i;
            init.GetComponent<Rolling>().manager = this;
            init.transform.position = spawn_positions[i];
        }
    }

    public void update_points(int player_num){
        int value = 100 / alive;
        alive--;
        switch (player_num) {
            case 0:
                manager.p1Score += value;
                break;
            case 1:
                manager.p2Score += value;
                break;
            case 3:
                manager.p3Score += value;
                break;
            case 4:
                manager.p4Score += value;
                break;
            default:
                break;
        }
        if(alive <= 1) {
            manager.return_from_minigame();
        }
    }
}
