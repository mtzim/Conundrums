using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDescend : MonoBehaviour {

    public GameObject[] platforms;
    public GameObject[] displaytiles;
    public float speed = 1;
    float timer = 0;
    float timerMax = 5;
    public Vector3[] initialposition;
    public float platspawntime = 12.0f;

    // Use this for initialization
    void Start () {
        
        for(int x = 0; x < displaytiles.Length; x++)
        {
            displaytiles[x].SetActive(false);
            //displaytiles[x].SetActive(true);
        }

        for (int i = 0; i <= 5; i++)
        {
           initialposition[i] = platforms[i].transform.position;
        }
        StartCoroutine("ChooseNextTile", 12f);
    }

    public void BeginMiniGame()
    {
        Start();
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    IEnumerator ChooseNextTile()
    {
        if (platspawntime >= 4.0f)
        {
            platspawntime = platspawntime - 0.5f;
        }

        int tile = (int)Random.Range(0, 6);
        Debug.Log("tile chosen: " + tile);
        displaytiles[tile].SetActive(true);
        Debug.Log("tile setactive");

        yield return new WaitForSeconds(5.0f);

        if (tile == 0)
        {
            Debug.Log("if 1");
            //move tiles 1-5
            for (int i = 1; i <= 5; i++)
            {
                Debug.Log("for 1");
                Vector3 initialposition = platforms[i].transform.position;
                Debug.Log("position initialized");
                platforms[i].transform.position = new Vector3(initialposition.x, initialposition.y - 7, initialposition.z);
                Debug.Log("moved down");
            }
        }
        else if (tile == 5)
        {
            Debug.Log("if 2");
            //move tiles 0-4
            for (int i = 0; i <= 4; i++)
            {
                Debug.Log("for 2");
                Vector3 initialposition = platforms[i].transform.position;
                Debug.Log("position initialized");
                platforms[i].transform.position = new Vector3(initialposition.x, initialposition.y - 7, initialposition.z);
                Debug.Log("moved down");
            }
        }
        else if (0 < tile && tile < 5)
        {
            Debug.Log("if 3");
            //move tiles 0<tile<5
            for (int i = 0; i < tile; i++)
            {
                Debug.Log("for 3");
                Vector3 initialposition = platforms[i].transform.position;
                Debug.Log("position initialized");
                platforms[i].transform.position = new Vector3(initialposition.x, initialposition.y - 7, initialposition.z);
                Debug.Log("moved down");
            }
            for (int i = 5; i > tile; i--)
            {
                Debug.Log("for 3");
                Vector3 initialposition = platforms[i].transform.position;
                Debug.Log("position initialized");
                platforms[i].transform.position = new Vector3(initialposition.x, initialposition.y - 7, initialposition.z);
                Debug.Log("moved down");
            }

        }
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i <= 5; i++)
        {
            platforms[i].transform.position = initialposition[i];
        }
        displaytiles[tile].SetActive(false);
        StartCoroutine("ChooseNextTile", platspawntime);
        }

}
