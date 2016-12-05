using UnityEngine;
using System.Collections.Generic;
using System;

public class CreateZombieEvent : MonoBehaviour {

    public Vector3[] spawnPoints;
    public Collider col;
   // public int ZombieType;
    public int HeadType;
    public int atk;
    public float speed;
    public bool oneTime = true;
    public bool MadeIt = false;
    public Zombie ZombieType;
    public ZombieHead ZombieHeadType;

    // Use this for initialization
    void Start () {
       // ZombieType = new JumpingZombie();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player"))
        {
            foreach(Vector3 point in spawnPoints) { 
                if (!MadeIt && oneTime)
                {
                 //   ZombieMaker.maker.MakeZombie(ZombieType,ZombieHeadType,point);
                    
                }
               // else if (!oneTime) { ZombieMaker.maker.MakeZombie(ZombieType, ZombieHeadType, point); }
            }
            MadeIt = true;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Vector3 point in spawnPoints)
        {
            Gizmos.DrawSphere(point, 1);
        }
    }



}

public class List<T1, T2>
{
}

public class Zdata:MonoBehaviour
{
    public Collider col;
    public int ZombieType;
    public int atk;
    public float speed;



}