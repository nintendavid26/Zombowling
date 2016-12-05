using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ConstantZombieMaking : MonoBehaviour {
    public static ConstantZombieMaking instance;
    public List<Vector3> spawnPoints;
    public List<Zombie> ZombieTypes;
    public List<ZombieHead> ZombieHeadTypes;
    public List<Type> ZombieTypesT=new List<Type>();
    public float SpawnRate;
    public float SpeedUpRate;
    public float SpeedUpAmount;
    public float MaxSpawnRate;
    public bool RandomSpawn;
    // Use this for initialization
    void Start () {

//        InitializeZombiesAndHeadsTypes();
  //      Call();
        instance = this;
        RandomSpawn = Level.CurrentLevel.GetComponent<DefendTheBase>();
        StartCoroutine(StartMakingZombies());
	}

    public Vector3 ClosestSpawnPoint(Player p) //Spawn a zombie for each player
    {
        Vector3 closest = spawnPoints[0];
            foreach (Vector3 point in spawnPoints)
            {
                if (Vector3.Distance(point, p.transform.position) < Vector3.Distance(closest, Player.main.transform.position))
                {
                    closest = point;
                }
            }
        return closest;
    }

    // Update is called once per frame
    public IEnumerator StartMakingZombies()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(SpeedUp());
        while (true)
        {
            foreach (Player p in AllPlayers.allPlayers.PlayerList)
            {
                if (RandomSpawn)
                {
                    ZombieMaker.maker.MakeZombie(ZombieTypes.RandomItem(), ZombieHeadTypes.RandomItem(), spawnPoints.RandomItem(), 5, 5);
                }
                else {
                    int r = UnityEngine.Random.Range(0, 10);

                    ZombieMaker.maker.MakeZombie(ZombieTypes.RandomItem(), ZombieHeadTypes.RandomItem(), ClosestSpawnPoint(p), 5, 5);
                }
            }
            yield return new WaitForSeconds(SpawnRate);
        }
    }
    public IEnumerator SpeedUp()
    {
        while (SpawnRate > MaxSpawnRate)
        {
            yield return new WaitForSeconds(SpeedUpRate);
            SpawnRate -= SpeedUpAmount;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (spawnPoints== null) { return; }
        foreach (Vector3 point in spawnPoints)
        {
            Gizmos.DrawSphere(point, 3);
        }
    }

    public void TestGeneric(Type T)
    {
        if (!T.IsSubclassOf(typeof(Zombie))) { Debug.LogError("Must pass in a zombie"); return; }
        Debug.Log(T);
    }
    public void Call()
    {
        Type t = typeof(JumpingZombie);
        TestGeneric(t);
    }
}
