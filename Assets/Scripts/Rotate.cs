using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    // Use this for initialization
    public int speed = 1;
    Rigidbody rb;
    //public Vector3 eulerAngleVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame

	void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Ground")
        {
            Debug.Log("Collision with " + col.tag);
            if (col.tag == "PlayerPickup") { col.transform.parent.transform.parent = transform; }
            else { col.transform.parent = transform; }
        }
    }
    void OnTriggerExit(Collider col)
    {

            Debug.Log("Exit of " + col.transform.tag);
            col.transform.parent.transform.parent = null;
    }
}
