using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_generator : MonoBehaviour {
    public int columns = 5;
    public int rows = 5;
    public GameObject[] floorTiles;
    private Transform boardHolder;
    private Dictionary<Vector3, Vector3> gridPositions = new Dictionary<Vector3, Vector3>();



    public int num_of_players;
    public GameObject player_prefab;
    private List<GameObject> players = new List<GameObject>();
    private int current_turn = 0;
    // Use this for initialization
    void Start () {
        BoardSetup();

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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        int i = 0;
        for (int x = 0; x < columns; x++) {
            for (int z = 0; z < rows; z++) {
                gridPositions.Add(new Vector3(x, 0f, z), new Vector3(x, 0f, z));
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
                Renderer rend = instance.gameObject.GetComponent<Renderer>();
                if (i % 2 == 0)
                    rend.material.color = Color.black;
                i++;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private void addTiles(Vector3 tileToAdd) {
        if (!gridPositions.ContainsKey(tileToAdd)) {
            gridPositions.Add(tileToAdd, tileToAdd);
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, new Vector3(tileToAdd.x, 0f, tileToAdd.z), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }
    }
}
