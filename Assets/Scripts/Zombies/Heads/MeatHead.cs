using UnityEngine;
using System.Collections;

public class MeatHead : ZombieHead {
    public float smellRadius;
    // Use this for initialization
    public override void OnHitGroundFromThrow()
    {
        base.OnHitGroundFromThrow();
        Collider[] cols = Physics.OverlapSphere(transform.position, smellRadius);
        if (cols.Length == 0) { destroy(); }
        else {
            foreach (Collider c in cols)
            {
                if (c.tag == "Zombie") { c.GetComponent<Zombie>().Target = gameObject; }
            }
            currentState = State.Unpickupable;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public override ZombieHead InitializeZombieHead()
    {
        smellRadius = 40;
        gameObject.tag = "Meat Head";
        return this as MeatHead;
    }

    public override void Die()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Zombie"&&currentState==State.Unpickupable) {

            destroy();
        }
    }
}
