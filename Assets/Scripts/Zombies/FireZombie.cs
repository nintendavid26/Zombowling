using UnityEngine;
using System.Collections;

public class FireZombie : Zombie {

    public GameObject Fire;
    public float MakeFireRate;

    // Use this for initialization
    public override Zombie InitializeZombie()
    {
        InvokeRepeating("MakeFire", MakeFireRate, MakeFireRate);
        return base.InitializeZombie();
    }

    // Update is called once per frame

    public void MakeFire()
    {
        if (Grounded())
        {
            Destroy(Instantiate(Fire,transform.position, Fire.transform.rotation),15);
        }
    }
    public void AttackZombie()
    {

    }
}
