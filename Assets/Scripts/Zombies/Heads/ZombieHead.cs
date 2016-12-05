using System.Reflection;
using UnityEngine;
using System.Collections;
using System;

public class ZombieHead : ThrowableObject
{

    public Zombie zombie;
    public Texture texture;
    public string texturePath = "Textures/Zombies/Heads/ZombieHead1";
    public string Name;
    public Renderer rend;
    public Material mat;

    // Use this for initialization
   public virtual void Start()
    {
        fade = GetComponent<FadeObjectInOut>();
        zombie = gameObject.transform.parent.gameObject.GetComponent<Zombie>();
        sc = gameObject.GetComponent<SphereCollider>();
        rend = GetComponent<Renderer>();
        InitializeZombieHead();
        //renderer.material.
        //GetComponent<Renderer>().material.mainTexture = texture;
    }
    public virtual ZombieHead InitializeZombieHead()
    {
        return this;
    }
    
    void FixedUpdate()
    {
        //if (Physics.Raycast(transform.position, -Vector3.up, 0.56F) && currentState == State.OnHead) { OnHitGroundFromThrow(); }
        if (Physics.Raycast(transform.position, -Vector3.up, 0.56F)&&currentState==State.InAir) { OnHitGroundFromThrow(); }

    }
    // Update is called once per frame
    void Update()
    {
        if (zombie != null) {
            //float y=zombie.TargetPosition.y;
         //y =(Mathf.Tan(360-zombie.transform.rotation.eulerAngles.x)*Mathf.PI/180)*Vector3.Distance(transform.position,zombie.TargetPosition);
           // transform.LookAt(new Vector3(zombie.TargetPosition.x,y,zombie.TargetPosition.z));
            
        }
    }

        //transform.LookAt(Player.Player1.transform);
        /*if (currentState==State.OnHead) {
            var targetPosition = Player.Player1.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
            //transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }*/


   public virtual void OnTriggerEnter(Collider col)
    {
      //  Debug.Log(col);
        if ((col.gameObject.tag == "Ground") && currentState == State.OnHead)
        {
            //Debug.Log("ground collision");
            currentState = State.Rolling;
            if (zombie != null) { zombie.Die(); zombie = null; }
            sc.isTrigger = false;
            this.gameObject.transform.parent = null;
            //sc.radius = 0.5F;
            if (rb == null) { rb = this.gameObject.AddComponent<Rigidbody>(); }

            try
            { if (rb != null) { rb.AddForce(transform.forward); }
                else { rb = this.gameObject.AddComponent<Rigidbody>(); rb.AddForce(transform.forward); }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }

        if ((col.gameObject.tag == "Ground") && currentState == State.InAir)
        {
            Debug.Log("ground collision");
            currentState = State.Rolling;
            if (zombie != null) { zombie.Die(); zombie = null; }
            sc.isTrigger = false;
            this.gameObject.transform.parent = null;
            //sc.radius = 0.5F;
            if (rb == null) { rb = this.gameObject.AddComponent<Rigidbody>(); }
            Invoke("Die",30);

        }

        if (col.gameObject.tag.Contains("Player") && currentState == State.Rolling&&currentState!=State.OnHead&& col.gameObject.transform.parent.GetComponent<Player>().heldThrowableObject == null)
        {
            GetPickedUp(col.gameObject.transform.parent.GetComponent<Player>());
        }
    }
   public void OnCollissionEnter(Collision col) {
        Debug.Log(col);
        if (col.gameObject.tag == "Ground" && currentState != State.Rolling)
        {
           
            currentState = State.Rolling;
            if (zombie != null) { zombie.Die(); zombie = null; }
            sc.isTrigger = false;
            this.gameObject.transform.parent = null;
            //sc.radius = 0.5F;
            if (rb == null) { rb = this.gameObject.AddComponent<Rigidbody>(); }

            rb.AddForce(transform.forward);

        }

    }

    public override void OnHitGroundFromThrow()
    {
        base.OnHitGroundFromThrow();
        Die();
    }
    public virtual void Die()
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

