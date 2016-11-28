using UnityEngine;
using System.Collections;

public class JumpingZombie : Zombie {


    public float MaxJumpForce,MinJumpForce;
    public override void Start()
    {
        base.Start();
        //texture = Resources.Load("Assets/Textures/Zombie Body1-Jump") as Texture;
       // Debug.Log(texture);
        int x = Random.Range(2, 5);
        int y = Random.Range(2, 5);
        InvokeRepeating("Jump", x, y);
    }
    public override Zombie InitializeZombie()
    {
        MaxJumpForce=4000;
        MinJumpForce = 2000;
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

        if (Grounded()) { rb.AddForce(Vector3.up * Random.Range(MinJumpForce, MaxJumpForce)); }
    }
}
