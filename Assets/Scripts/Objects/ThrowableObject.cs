using System.Reflection;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ThrowableObject : MonoBehaviour
{

    public enum State { OnHead, Held, Rolling, InAir,Unpickupable,HeldBySportsZombie,ThrownBySportsZombie };
    public State currentState;
    public Rigidbody rb;
    public SphereCollider sc;
    public FadeObjectInOut fade;
    public float mass;
    public GameObject ground;
    // Use this for initialization
   public virtual void Start()
    {
        fade = GetComponent<FadeObjectInOut>();
        sc = gameObject.GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void GetPickedUp(Player picker){

        gameObject.transform.position = picker.Hand.transform.position;
        gameObject.transform.parent = picker.Hand.transform;
        currentState = State.Held;
        sc.enabled = false;
       // Destroy(rb);
        rb.isKinematic = true;
        rb.detectCollisions = false;
        picker.heldThrowableObject = this;
        // transform.eulerAngles = new Vector3(10, 0, 0);
    }
    public void GetPickedUp(SportsZombie picker)
    {

        gameObject.transform.position = picker.Hand.transform.position;
        gameObject.transform.parent = picker.Hand.transform;
        currentState = State.HeldBySportsZombie;
        sc.enabled = false;
        // Destroy(rb);
        rb.isKinematic = true;
        rb.detectCollisions = false;
        picker.heldThrowableObject = this;
        // transform.eulerAngles = new Vector3(10, 0, 0);
    }
    public virtual void Thrown(Player thrower) {
        //transform.position = new Vector3(0,2,0);
        transform.parent = null;
        currentState = ThrowableObject.State.InAir;
       // rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        sc.enabled = true;
        sc.isTrigger = false;
        rb.mass = mass;
       rb.AddForce(Camera.main.transform.forward * thrower.throwingPower);
        thrower.heldThrowableObject = null;

    }
    public virtual void Thrown(SportsZombie thrower)
    {
        //transform.position = new Vector3(0,2,0);
        transform.parent = null;
        currentState = ThrowableObject.State.ThrownBySportsZombie;
        // rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        sc.enabled = true;
        sc.isTrigger = false;
        rb.mass = mass;
        rb.AddForce(Camera.main.transform.forward * thrower.ThrowPower);
        thrower.heldThrowableObject = null;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnHitGroundFromThrow()
    {
        currentState = State.Rolling;

    }
}
