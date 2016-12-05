using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Zombie))]
public class ZombiePathFinding : MonoBehaviour {

    public Zombie Z;
	// Use this for initialization
	void Start () {
        Z = GetComponent<Zombie>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
