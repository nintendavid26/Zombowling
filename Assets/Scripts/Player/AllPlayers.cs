using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AllPlayers : MonoBehaviour {
    public static AllPlayers allPlayers;
    public List<Player> PlayerList;
	// Use this for initialization
	void Start () {
        if (allPlayers == null) { allPlayers = this; }
        allPlayers.PlayerList = FindObjectsOfType<Player>().ToList();
      //  Debug.Log("AllPlayers");
    }
	
	// Update is called once per frame
	void Update () {
       // if (allPlayers.PlayerList.Length != FindObjectsOfType<Player>().Length) { allPlayers.PlayerList = FindObjectsOfType<Player>(); }
	}
}
