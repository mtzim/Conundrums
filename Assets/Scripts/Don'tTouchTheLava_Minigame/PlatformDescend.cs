using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDescend : MonoBehaviour {

    public GameObject[] platforms;
    public GameObject[] displaytiles;
    public int platformspeed = 10;


	// Use this for initialization
	void Start () {
        ChooseNextTile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseNextTile()
    {
        int tile = (int)Random.Range(0, 6);
        Debug.Log("tile chosen");
        displaytiles[tile].SetActive(true);
        new WaitForSeconds(1);
        if (tile == 0)
        {
            //move tiles 1-5
            for(int i = 1; i <= 5; i++)
            {
                //transform lower
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                //wait 2 seconds
                new WaitForSeconds(1);
                //transform raise
            }
        }
        if(tile == 5)
        {
            //move tiles 0-4
            for (int i = 0; i <= 4; i++)
            {
                //transform lower
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                //wait 2 seconds
                new WaitForSeconds(1);
                //transform raise
            }
        }
        else
        {
            //move tiles 0<tile<5
            for (int i = 1; i <= 4; i++)
            {
                //transform lower
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                platforms[tile].transform.Translate(0, -7 * platformspeed * Time.deltaTime, 0);
                //wait 2 seconds
                new WaitForSeconds(1);
                //transform raise
            }
        }
        displaytiles[tile].SetActive(false);
        ChooseNextTile();
    }

}
