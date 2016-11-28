using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class ZombieMaker : MonoBehaviour {

    public static ZombieMaker maker;
    public ZombieMaker nonstaticMaker;
    public GameObject zombiePrefab;
    public List<Vector3> spawnPoints;
    public StandardZombieAssets sza;
    public StandardZombieHeadAssets szha;
    public float speed=3;
    public int attack=3;
    public int spawnRate=8;
    public int speedUpRate=15;
    public bool constantMaking;

    public void Awake() {
        if (maker == null) { maker = this; }
        
    }

    public void MakeZombie(bool random=false)
    {
        if (spawnPoints.Count > 0)
        {
            int x = UnityEngine.Random.Range(0, sza.tex.Length);
            speed = UnityEngine.Random.Range(3, 5);
            attack = UnityEngine.Random.Range(3, 5);
            Zombie newZombie;
            Vector3 spawn=random?ClosestSpawnPoint():spawnPoints.RandomItemConditional();
            GameObject newZombieObject = Instantiate(zombiePrefab, ClosestSpawnPoint(), transform.rotation) as GameObject;
            switch (x)
            {
                case 0: newZombie = newZombieObject.AddComponent<Zombie>(); break;
                case 1: newZombie = newZombieObject.AddComponent<JumpingZombie>().InitializeZombie(); break;
                default: newZombie = newZombieObject.AddComponent<Zombie>(); break;
            }

            int y = UnityEngine.Random.Range(0, szha.tex.Length);
            ZombieHead newHead;
            switch (y)
            {
                case 0: newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ZombieHead>(); break;
                case 1:
                    newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ExplodingHead>().InitializeZombieHead();
                    break;
                case 2:
                    newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<MeatHead>().InitializeZombieHead();
                    break;

                default: newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ZombieHead>(); break;
            }
            newZombie.Head = newHead;
            newHead.zombie = newZombie;
            newHead.mass = 9;
            newZombie.attack = attack;
            newZombie.speed = speed;
            newZombie.texture = sza.tex[x];
            newHead.texture = szha.tex[y];
            newHead.mat = szha.mat[y];
            newHead.gameObject.GetComponent<Renderer>().material.mainTexture = newHead.texture;
            newZombie.gameObject.GetComponent<Renderer>().material.mainTexture = newZombie.texture;
            Debug.Log("madeZombie");
        }
    }

    public void MakeZombie(int x,int y, Vector3 SpawnPoint)// Look at switch statement for types, x=bodyType y=headType
    {
        
        
        speed = UnityEngine.Random.Range(3, 5);
        attack = UnityEngine.Random.Range(3, 5);
        Zombie newZombie;
        GameObject newZombieObject = Instantiate(zombiePrefab, SpawnPoint, transform.rotation) as GameObject;
        switch (x)
        {
            case 0: newZombie = newZombieObject.AddComponent<Zombie>().InitializeZombie(); break;
            case 1: newZombie = newZombieObject.AddComponent<JumpingZombie>().InitializeZombie();  break;
            default: newZombie = newZombieObject.AddComponent<Zombie>(); break;
        }
        szha = zombiePrefab.GetComponent<StandardZombieHeadAssets>();
        ZombieHead newHead;
        switch (y)
        {
            case 0: newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ZombieHead>(); break;
            case 1:
                newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ExplodingHead>().InitializeZombieHead();
                newHead = ((ExplodingHead)newHead);
                ((ExplodingHead)newHead).ExplosionEffect = szha.Explosion;
                ((ExplodingHead)newHead).ExplosionForce = 80;
                ((ExplodingHead)newHead).ExplosionRadius = 15;

                break;
            default: newHead = newZombieObject.transform.GetChild(0).gameObject.AddComponent<ZombieHead>(); break;
        }
        newZombie.Head = newHead;
        newHead.zombie = newZombie;
        newHead.mass = 9;
        newZombie.attack = attack;
        newZombie.speed = speed;
        sza = zombiePrefab.GetComponent<StandardZombieAssets>();
        try { newZombie.texture = sza.tex[x]; } catch (Exception e) { Debug.LogWarning(x); Debug.Log(System.Environment.StackTrace); }
        newZombie.mat = sza.mat[x];
        newHead.texture = szha.tex[y];
        newHead.mat = szha.mat[y];
        newHead.gameObject.GetComponent<Renderer>().material.mainTexture = newHead.texture;
        newZombie.gameObject.GetComponent<Renderer>().material.mainTexture = newZombie.texture;
        newZombie.name = newZombie.FullName();
        Debug.Log("madeZombie");
    }

    public void Update() {

        if (Input.GetKeyDown("l"))
        {
            MakeBowling(2,1,5);

        }
        if (Input.GetKeyDown("p"))
        {
            MakeZombie(1,0, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Capacity)]);
        }

    }

    public float JumpForce(float force){
        return force;
    }

    public void Start()
    {
        if (constantMaking)
        {
            InvokeRepeating("SpeedUp", 0, speedUpRate);
        }
    }

    public void SpeedUp() {
        if (AllPlayers.allPlayers.PlayerList.Capacity>0) {
            CancelInvoke();
            if (spawnRate > 1) { spawnRate--; }
             InvokeRepeating("MakeZombie", 2, spawnRate);
        }
    }
    public Vector3 ClosestSpawnPoint()
    {
        Vector3 closest = spawnPoints[0];
        foreach (Vector3 point in spawnPoints)
        {
            if(Vector3.Distance(point,Player.main.transform.position)< Vector3.Distance(closest, Player.main.transform.position))
            {
                closest = point;
            }
        }
        return closest;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(Vector3 point in spawnPoints){
            Gizmos.DrawSphere(point, 3);
        }
    }

    public void MakeBowling(int x, int y, int rows) {
        Vector3 spawn= spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Capacity)];
        MakeZombie(x, y, spawn);
        int pins = 0;
        for (int r=0; r< rows;r++) {
            float firstInColumn = spawn.x * (-r );
            for (int R=0; R< r;R++) {
                
                    MakeZombie(x, y, new Vector3(spawn.x+R*2, spawn.y, spawn.z - r)); pins++;
                
            }
        }
    }


    
}
