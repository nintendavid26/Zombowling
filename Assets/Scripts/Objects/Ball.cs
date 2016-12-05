using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class Ball : ThrowableObject {
    public Texture[] Textures;
    public int ThrowsLeft;
    // Use this for initialization
    // Update is called once per frame

    public override void Start()
    {
        base.Start();

    }
    void Update () {
        
    }
    public override void OnHitGroundFromThrow()
    {
        base.OnHitGroundFromThrow();
        ThrowsLeft--;
        if (ThrowsLeft == 0) { StartDisapearing(); }
    }
    void OnTriggerEnter(Collider col)
    {
        try
        { if (col.gameObject.tag.Contains("Player") && currentState == State.Rolling && col.gameObject.transform.parent.GetComponent<Player>().heldThrowableObject == null)
            {
                GetPickedUp(col.gameObject.transform.parent.GetComponent<Player>());
            }

        }
        catch (Exception e) {
            Debug.LogWarning(col.gameObject.transform.parent);
        }

    }

    public void OnCollisionEnter(Collision col) {
     //   Debug.Log(col);
        if (col.gameObject.tag=="Ground"&&currentState!=State.Rolling) {
            currentState = State.Rolling;
        }
    }

    public override void Thrown(Player thrower)
    {
        base.Thrown(thrower);
        ThrowsLeft--;
    }
    public void StartDisapearing()
    {
        currentState = State.Unpickupable;
        fade.FadeOut();
        gameObject.layer = 8;
        Invoke("destroy", 7);
    }

    public void destroy()
    {
        Debug.Log("Destroyed");
        Destroy(this.gameObject);
    }
}
