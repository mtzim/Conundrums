using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Generator : MonoBehaviour {
    public int columns = 5;
    public int rows = 5;
    public GameObject[] floorTiles;
    public GameObject[] decorationTiles;
    Game_Manager game;
    private Transform boardHolder;
    private Dictionary<Vector3, Vector3> gridPositions = new Dictionary<Vector3, Vector3>();

    // Use this for initialization
    void Start () {
        game = GetComponent<Game_Manager>();
        BoardSetup();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        int i = 0;
        for (int x = 0; x < columns; x++) {
            for (int z = 0; z < rows; z++) {
                Debug.Log(x + " " + z);
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

            if (Random.Range(0, 10) == 1) {
                toInstantiate = decorationTiles[Random.Range(0, decorationTiles.Length)];
                instance = Instantiate(toInstantiate) as GameObject;
                instance.transform.position = new Vector3(tileToAdd.x, tileToAdd.y+.1f, tileToAdd.z);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void addToBoard() {
        Transform current_player = game.players[game.current_turn].transform;
        int x = (int)Mathf.Round(current_player.position.x);
        int z = (int)Mathf.Round(current_player.position.z);
        addTiles(new Vector3(x + 1, 0f, z + 1));
        addTiles(new Vector3(x + 1, 0f, z - 1));
        addTiles(new Vector3(x + 1, 0f, z));
        addTiles(new Vector3(x - 1, 0f, z + 1));
        addTiles(new Vector3(x - 1, 0f, z - 1));
        addTiles(new Vector3(x - 1, 0f, z));
        addTiles(new Vector3(x, 0f, z + 1));
        addTiles(new Vector3(x, 0f, z - 1));
        addTiles(new Vector3(x, 0f, z));
    }
}
