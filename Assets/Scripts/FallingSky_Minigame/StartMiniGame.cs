using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMiniGame : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Camera.main.GetComponent<FallenSkyController>().enabled = true;
        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;
        player3.GetComponent<PlayerController>().enabled = true;
        player4.GetComponent<PlayerController>().enabled = true;
        gameObject.GetComponent<PauseMenuFS>().enabled = true;
    }
}
