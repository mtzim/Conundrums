using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Generator : MonoBehaviour
{
    struct Tile
    {
        public Vector3 position;
        public Color color;

        public Tile(Vector3 pos, Color col)
        {
            position = pos;
            color = col;
        }
    }
    public int columns = 5;
    public int rows = 5;
    public GameObject[] floorTiles;
    public GameObject[] decorationTiles;
    public GameObject ladderTile;
    public GameObject goldenTile;
    public int maxFloors;
    Game_Manager game;
    public Transform boardHolder;
    private Dictionary<Vector3, Tile> gridPositions = new Dictionary<Vector3, Tile>();
    private bool finalGoalFound;

    // Use this for initialization
    void Start()
    {
        game = GetComponent<Game_Manager>();
        maxFloors = game.num_of_floors;
        board_setup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void board_setup()
    {
        boardHolder = new GameObject("Board").transform;
        DontDestroyOnLoad(boardHolder);
        for (int x = -5; x < columns; x++)
        {
            for (int z = -5; z < rows; z++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
                Renderer rend = instance.gameObject.GetComponent<Renderer>();
                if (black_adjacent(instance.transform.position))
                    rend.material.color = Color.white;
                else
                    rend.material.color = Color.grey;
                gridPositions.Add(new Vector3(x, 0f, z), new Tile(new Vector3(x, 0f, z), rend.material.color));
                instance.transform.SetParent(boardHolder);
            }
        }
        finalGoalFound = false;
    }

    private void add_tiles(Vector3 tileToAdd)
    {
        Debug.Log(tileToAdd.y);
        if (!gridPositions.ContainsKey(tileToAdd)) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, new Vector3(tileToAdd.x, tileToAdd.y, tileToAdd.z), Quaternion.identity) as GameObject;
            Renderer rend = instance.gameObject.GetComponent<Renderer>();
            instance.transform.SetParent(boardHolder);
            if (black_adjacent(instance.transform.position))
                rend.material.color = Color.white;
            else
                rend.material.color = Color.grey;
            gridPositions.Add(tileToAdd, new Tile(tileToAdd, rend.material.color));

            if (Random.Range(0, 10) == 1) {
                toInstantiate = decorationTiles[Random.Range(0, decorationTiles.Length)];
                instance = Instantiate(toInstantiate) as GameObject;
                instance.transform.position = new Vector3(tileToAdd.x, tileToAdd.y + .1f, tileToAdd.z);
                instance.transform.SetParent(boardHolder);
            }
            else if(tileToAdd.y < 5*maxFloors && Random.Range(0,25) == 1)
            {
                toInstantiate = ladderTile;
                instance = Instantiate(toInstantiate) as GameObject;
                instance.transform.position = new Vector3(tileToAdd.x, tileToAdd.y + .1f, tileToAdd.z);
                instance.transform.SetParent(boardHolder);
            }
            else if(!finalGoalFound && tileToAdd.y == 5*maxFloors && Random.Range(0,10) == 1)
            {
                finalGoalFound = true;
                toInstantiate = goldenTile;
                instance = Instantiate(toInstantiate) as GameObject;
                instance.transform.position = new Vector3(tileToAdd.x, tileToAdd.y + .1f, tileToAdd.z);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void add_to_board(int floor)
    {
        Transform current_player = game.players[game.current_turn].transform;
        int x = (int)Mathf.Round(current_player.position.x);
        int z = (int)Mathf.Round(current_player.position.z);
        add_tiles(new Vector3(x + 1, floor, z + 1));
        add_tiles(new Vector3(x + 1, floor, z - 1));
        add_tiles(new Vector3(x + 1, floor, z));
        add_tiles(new Vector3(x - 1, floor, z + 1));
        add_tiles(new Vector3(x - 1, floor, z - 1));
        add_tiles(new Vector3(x - 1, floor, z));
        add_tiles(new Vector3(x, floor, z + 1));
        add_tiles(new Vector3(x, floor, z - 1));
        add_tiles(new Vector3(x, floor, z));
    }

    public void new_floor(int floor)
    {
        Transform current_player = game.players[game.current_turn].transform;
        for (int x = -5; x < columns; x++)
        {
            for (int z = -5; z < rows; z++)
            {
                Vector3 tileToAdd = new Vector3(Mathf.Round(current_player.position.x), floor, Mathf.Round(current_player.position.z));
                tileToAdd += new Vector3(x, 0f, z);
                if (!gridPositions.ContainsKey(tileToAdd))
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, tileToAdd, Quaternion.identity) as GameObject;
                    Renderer rend = instance.gameObject.GetComponent<Renderer>();
                    if (black_adjacent(instance.transform.position))
                        rend.material.color = Color.white;
                    else
                        rend.material.color = Color.grey;
                    gridPositions.Add(tileToAdd, new Tile(tileToAdd, rend.material.color));
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    //check neighboor colors and return if it is black
    private bool black_adjacent(Vector3 position)
    {
        Vector3[] displacements = {
            new Vector3(1,0,0),
            new Vector3(-1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,0,-1)
        };
        for (int i = 0; i < displacements.Length; i++)
        {
            Tile color_check;
            Vector3 neighbor = position + displacements[i];
            if (gridPositions.TryGetValue(neighbor, out color_check))
            {
                if (color_check.color == Color.grey)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
