using UnityEngine;
using System.Collections;

public class WarpHead : ZombieHead {

    public Player p;
    public float ExplosionRadius;
    public float ExplosionForce;
    public Color32 BlurColor;

    public override void GetPickedUp(Player picker)
    {
        base.GetPickedUp(picker);
        p = picker;
    }

    public override void OnHitGroundFromThrow()
    {
        p.transform.position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider c in colliders)
        {

            if (c.GetComponent<Rigidbody>() == null) continue;
            if (!c.tag.Contains("Player"))
            {
                c.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
        Debug.Log("Explode");
        p.Blur(BlurColor);
        base.OnHitGroundFromThrow();
    }
}
