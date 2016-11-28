using UnityEngine;
using System.Collections.Generic;

public class CreateZombieEvent : MonoBehaviour {

    public Vector3[] spawnPoints;
    public Collider col;
    public int ZombieType;
    public int HeadType;
    public int atk;
    public float speed;
    public bool oneTime = true;
    public bool MadeIt = false;

    // Use this for initialization
    void Start () {
	
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
                    ZombieMaker.maker.MakeZombie(ZombieType, HeadType,point);
                    
                }
                else if (!oneTime) { ZombieMaker.maker.MakeZombie(); }
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