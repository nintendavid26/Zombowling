using UnityEngine;
using System.Collections;

public abstract class Level : MonoBehaviour {
    public static Level CurrentLevel;
    public int MaxPlayers;
    public bool ended = false;
    public void Awake()
    {
        CurrentLevel = this;
    }


	
}
