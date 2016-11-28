using UnityEngine;
using System.Collections;

public class ExplodingHead : ZombieHead {

    public float ExplosionForce;
    public float ExplosionRadius;
    public GameObject ExplosionEffect;
    public int oldZombieAttack;
    // Use this for initialization


    public override void OnHitGroundFromThrow() {
        base.OnHitGroundFromThrow();
        Explode();
    }

    public override void Start()
    {
        base.Start();
        oldZombieAttack = zombie.attack;

    }
    public override ZombieHead InitializeZombieHead()
    {
        ExplosionEffect = ZombieMaker.maker.szha.Explosion;
        ExplosionForce = 80;
        ExplosionRadius = 15;
        return this as ExplodingHead;
    }

    public void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider c in colliders)
        {

            if (c.GetComponent<Rigidbody>() == null) continue;
            if (c.tag.Contains("Player")) { c.GetComponent<Player>().rbfpc.m_Jump = true;
                if (Vector3.Distance(transform.position, c.transform.position) < ExplosionRadius / 2)
                {
                    c.GetComponent<Player>().Health -= oldZombieAttack;
                }
            }
            c.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3, ForceMode.Impulse);
        }
        Destroy(gameObject);
        Debug.Log("Explode");
    }


}
