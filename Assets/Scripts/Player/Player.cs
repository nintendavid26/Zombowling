using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject Hand;
    public ThrowableObject heldThrowableObject;
    public Rigidbody rb;
    public float throwingPower;
    public int Health;
    float distToGround;
    public float hitstun=0;
    public Vector3 spawnPoint;
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rbfpc;
    public List<Vector3> SpawnPoints;
    public List<Player> temp;
    public bool CanBeDamaged = true;
    public static Player main;
    // Use this for initialization
    void Start () {
        main = this;
        rbfpc = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        //if (Player1 == null) { Player1 = this; }
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        //transform.position = spawnPoint;
        //transform.position =SpawnPoints[0];
      // AllPlayers.allPlayers.PlayerList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && heldThrowableObject != null) { heldThrowableObject.Thrown(this); }
        if (Input.GetKeyDown("j"))
        {
            Debug.Log("Player Jump");
            
            //rb.AddForce(Vector3.right * 1000);
        }
        if (hitstun > 0) { hitstun -= Time.deltaTime; }
       // temp = AllPlayers.allPlayers.PlayerList;
    }

    public void Throw(ThrowableObject to) {
      
        
       
    }

    public void onCollisionEnter(Collision col) {
        Debug.Log("Player got hit");
        if (col.gameObject.tag == "Zombie"){
            GetHit(col.gameObject.GetComponent<Zombie>());
            
        }
    }
    public void RemoveInvulnerability()
    {
        CanBeDamaged = true;
    }

     public void GetHit(Zombie zom)
    {
        hitstun = 1.5f;
        rb.drag = 0f;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround+0.1F)) { rb.AddForce(transform.up * zom.attack, ForceMode.Impulse); }
        rb.AddForce(-transform.forward * zom.attack*2, ForceMode.Impulse);
        rbfpc.m_Jump = true;
        if (CanBeDamaged) {
            Health -= zom.attack;
            CanBeDamaged = false;
            Invoke("RemoveInvulnerability", 3);
        }

    }
}
