using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Zombie : MonoBehaviour
{
    public bool dead;
    public ZombieHead Head;
    public Rigidbody rb;
    Vector3 center;
    public FadeObjectInOut fade;
    public float speed;
    public int attack;
    public GameObject Target;
    public Texture texture;
    public string Name;
    public Renderer rend;
    public Material mat;
    public static int numberOfZombies = 0;
    public GameObject closestTarget;
    public float distToGround;

    // Use this for initialization

    public virtual void Start()
    {
        fade = GetComponent<FadeObjectInOut>();
        fade.FadeIn();
        rb = gameObject.GetComponent<Rigidbody>();
        center = rb.centerOfMass;
        rb.centerOfMass = Vector3.down * 0.7F;
      //  Debug.Log("Changed Center");
      // texture=Resources.Load("Assets/Textures/Zombie Head1") as Texture;
        //Target = Player.Player1.tra;
        // GetComponent<Renderer>().material.mainTexture = texture;
        //Debug.Log("Zombie Start");
        numberOfZombies++;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        try
        { Target = AllPlayers.allPlayers.PlayerList[0].gameObject; }
        catch (Exception e) { Debug.LogWarning(e+". Player List not set"); }
        InvokeRepeating("checkClosestTarget", 1, 2);
    }

    public virtual Zombie InitializeZombie()
    {
        return this;
    }

    //public Zombie() { }

    // Update is called once per frame
    void Update()
    {
      
        //if () { }
        // transform.LookAt(Player.Player1.transform.);
        //agent.SetDestination(Player.Player1.transform.position);
        if (!dead) {
            
            float step = speed * Time.deltaTime;
            try
            { if (Target == null && AllPlayers.allPlayers.PlayerList[0].gameObject != null) { Target = AllPlayers.allPlayers.PlayerList[0].gameObject; } }
            catch(Exception e)
            {
                Debug.LogWarning(e);
            }
            if (Target.tag=="Meat Head") { transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step*2); }
            else { transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step); }

        }
    }

    public void Die()
    {
        rb.centerOfMass = center;
        //Debug.Log("COM reset");
        dead = true;
        fade.FadeOut();
        gameObject.layer = 8;
        numberOfZombies--;
       // GetComponent<Collider>().isTrigger = true;
        Invoke("destroy", 7);
    }

    public void destroy()
    {
      //  Debug.Log("Destroyed");
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag.Contains("Player") && !dead) {
            //Vector3 direction = new Vector3(Player.Player1.transform.position.x - transform.position.x,5, Player.Player1.transform.position.z - transform.position.z);
            try { col.transform.parent.GetComponent<Player>().GetHit(this); }
            catch(Exception e)
            {
                col.GetComponent<Player>().GetHit(this);
            }
            
        }
    }
    public bool Grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1F) && !dead;
    }

    public string FullName()
    {

        return Name + "-" + Head.Name + "-Zombie";

    }
    public void checkClosestTarget()
    {
        for (int x = 0; x < AllPlayers.allPlayers.PlayerList.Capacity - 1; x++)
        {
            try {
                if (Vector3.Distance(transform.position, AllPlayers.allPlayers.PlayerList[x].transform.position) < Vector3.Distance(transform.position, Target.transform.position)) { closestTarget = AllPlayers.allPlayers.PlayerList[x].gameObject; }
            }
            catch (Exception e) { Debug.Log(e); }
        }
    }
}
