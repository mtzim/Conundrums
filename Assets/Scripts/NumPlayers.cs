using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPlayers : MonoBehaviour {

    [HideInInspector]
    public int numberOfPlayers;
    [HideInInspector]
    public int numberOfFloors;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setNumberOfPlayers(int numPlayers)
    {
        numberOfPlayers = numPlayers;
    }
    public void setNumberofFloors(int numFloors)
    {
        numberOfFloors = numFloors;
    }
}
