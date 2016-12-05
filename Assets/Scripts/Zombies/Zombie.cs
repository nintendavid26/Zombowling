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
    public Vector3 TargetPosition;
    public string Name;
    public Renderer rend;
    public static int numberOfZombies = 0;
    public static List<Zombie> AllZombies=new List<Zombie>();
    public GameObject closestTarget;
    public float distToGround;
    bool LookedAtTarget=false;

    // Use this for initialization

    void Start()
    {
        InitializeZombie();
    }

    public virtual Zombie InitializeZombie()
    {
        AllZombies.Add(this);
        fade = GetComponent<FadeObjectInOut>();
        fade.FadeIn();
        rb = gameObject.GetComponent<Rigidbody>();
        center = rb.centerOfMass;
        rb.centerOfMass = Vector3.down * 0.7F;
        numberOfZombies++;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        InvokeRepeating("GetTarget", 1, 2);
        return this;
    }

    void Update()
    {
      
        if (!dead) {      
            float step = speed * Time.deltaTime;
            if (Target!=null&&Target.tag=="Meat Head") { transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step*2); }
            else { transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step); }

        }
    }

    public virtual void Die()
    {
        rb.centerOfMass = center;
        dead = true;
        fade.FadeOut();
        gameObject.layer = 8;
        numberOfZombies--;
        AllZombies.Remove(this);
        Invoke("destroy", 7);
    }

    public void destroy()
    {
        Destroy(gameObject);
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
    public void GetTarget()
    {
        if (Target == null)
        {
           
        }
        else if (Target.GetComponent<MeatHead>()) { }
        if (Level.CurrentLevel.GetComponent<DefendTheBase>())
        {
            if (Vector3.Distance(transform.position,TargetPosition)<1) {
                TargetPosition = UnityEngine.Random.insideUnitCircle * 7;
                TargetPosition = new Vector3(TargetPosition.x,1,TargetPosition.y);
                Target = null; }
            else {
                Target = Level.CurrentLevel.GetComponent<DefendTheBase>().Base.gameObject;
            }
        }
        else {
            Debug.Log("no");
            for (int x = 0; x < AllPlayers.allPlayers.PlayerList.Capacity - 1; x++)
            {
                try
                {
                    if (Vector3.Distance(transform.position, AllPlayers.allPlayers.PlayerList[x].transform.position) < Vector3.Distance(transform.position, Target.transform.position)) { closestTarget = AllPlayers.allPlayers.PlayerList[x].gameObject; }
                }
                catch (Exception e) { Debug.Log(e); }
            }
        }
        if (Target != null)
        {
            TargetPosition = Target.transform.position;
        }
    } 



    public void setStats(float speed,int attack)
    {

    } 
}
