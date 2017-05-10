using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPlayers : MonoBehaviour {

    [HideInInspector]
    public int numberOfPlayers;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setNumberOfPlayers(int numPlayers)
    {
        numberOfPlayers = numPlayers;
    }
}
