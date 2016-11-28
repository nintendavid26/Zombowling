using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedPlayerScript : NetworkBehaviour {
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public Player PlayerScript; 
	// Use this for initialization
	void Start () {
	
	}

    public override void OnStartLocalPlayer()
    {
        fpsCamera.enabled = true;
        fpsController.enabled = true;
        audioListener.enabled = true;
        PlayerScript.enabled = true;
        gameObject.name = "LOCAL Player";
        gameObject.transform.position = GetComponent<Player>().spawnPoint;
        base.OnStartLocalPlayer();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
