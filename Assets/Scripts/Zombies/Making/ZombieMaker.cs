using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using System.Collections;

public class ZombieTypeContainer {
    public Type ZombieType;
}
public class ZombieHeadTypeContainer
{
    public Type ZombieHeadType;
}

public class ZombieMaker : MonoBehaviour {

    public static ZombieMaker maker;
    public ZombieMaker nonstaticMaker;
    public GameObject zombiePrefab;
    public void Awake() {
        if (maker == null) { maker = this; }
        
    }

    public void MakeZombie<Z, ZH>( Vector3 Position,float speed=1,int attack=1) where Z : Zombie,new() where ZH : ZombieHead,new() {
        Z newZombie;
        GameObject newZombieObject = Instantiate(zombiePrefab, Position, transform.rotation) as GameObject;
        newZombie = newZombieObject.AddComponent<Z>();
        ZH newHead;
        newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ZH>();
        newZombie.Head = newHead;
        newZombie.speed = speed;
        newZombie.attack = attack;
        newHead.zombie = newZombie;
        newHead.mass = 9;
    }
    public void MakeZombie(Zombie Z,ZombieHead ZH, Vector3 Position, float speed = 1, int attack = 1)
    {
        Zombie newZombie = Instantiate(Z, Position, transform.rotation) as Zombie;
        ZombieHead newHead = Instantiate(ZH,newZombie.transform,false) as ZombieHead;
        newZombie.Head = newHead;
        newZombie.speed = speed;
        newZombie.attack = attack;
        newHead.zombie = newZombie;
        newHead.mass = 9;

    }


    public void Update() {

        if (Input.GetKeyDown("l"))
        {
            

        }
        if (Input.GetKeyDown("p"))
        {
           // MakeZombie(1,0, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Capacity)]);
        }

    }

    public float JumpForce(float force){
        return force;
    }





    public void MakeBowling<Z, ZH>(Z Zom, ZH ZomH, Vector3 spawn,int rows) where Z : Zombie where ZH : ZombieHead
    {
        MakeZombie(Zom, ZomH, spawn);
        int pins = 0;
        for (int r=0; r< rows;r++) {
            float firstInColumn = spawn.x * (-r );
            for (int R=0; R< r;R++) {
                
                    MakeZombie( Zom,ZomH,new Vector3(spawn.x+R*2, spawn.y, spawn.z - r)); pins++;
                
            }
        }
    }


    
}
