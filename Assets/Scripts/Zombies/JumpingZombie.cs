using UnityEngine;
using System.Collections;

public class JumpingZombie : Zombie {


    public float MaxJumpForce=1,MinJumpForce=4;
    public bool ShouldJump;
    public override Zombie InitializeZombie()
    {
        base.InitializeZombie();
        int x = Random.Range(2, 6);
        int y = Random.Range(1, 6);
        InvokeRepeating("Jump", x, y);
        return this;
    }



    public void Jump(Vector3 direction,float Force) {

        if (Grounded()) { rb.AddForce(direction * Force); }
        Debug.Log("Jump");
    }

    public void Jump(Vector3 direction)
    {

        if (Grounded()) { rb.AddForce(direction * Random.Range(MinJumpForce,MaxJumpForce)); }
        Debug.Log("Jump");
    }

    public void Jump()
    {
        if (rb == null) { Debug.Log("rb is null"); }
        if (Grounded()) { rb.AddForce(Vector3.up * 2000); }
    }
}
