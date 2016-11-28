using UnityEngine;
using System.Collections;

public class ElevatorTop : MonoBehaviour {
    public Elevator Parent;
    // Use this for initialization
	void Start () {
        Parent = transform.parent.GetComponent<Elevator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   
}
