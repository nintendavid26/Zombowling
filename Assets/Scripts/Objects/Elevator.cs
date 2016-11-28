using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public interface ICheckPoints
{
    void AddPoint(Vector3 point);

    GameObject GameObject();
}

public class Elevator : MonoBehaviour,ICheckPoints {

    public List<Vector3> targets;
    public Collider top;
    public int target = 0;
    public bool stopped = false;
    public float Wait=0;
    public float speed;

	// Use this for initialization
	void Start () {
     //   top = transform.GetChild(0).GetComponent<Collider>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Wait > 0) { Wait -= Time.deltaTime; }
        if (!stopped)
        {
         StartCoroutine(Move(targets[target]));
        }
    }

    public IEnumerator Move(Vector3 Target)
    {
        if (!stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
            if (transform.position == targets[target])
            {
                if (target == targets.Count - 1)
                {
                    target = 0;
                }
                else { target++; }
                stopped = true;
                yield return new WaitForSeconds(2);
                stopped = false;
            }
        }
    }

    public GameObject GameObject()
    {
        return gameObject;
    }

    public void AddPoint(Vector3 point)
    {
        targets.Add(point);
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 point in targets)
        {
            Gizmos.DrawSphere(point, 3);
        }
    }
    void OnTriggerstay(Collider col)
    {
        
        if (col.transform.tag.Contains("Player"))
        {
            Debug.Log("Collision with " + col.tag);
            if (col.tag == "PlayerPickup") { col.transform.parent.transform.parent = transform; }
            else { col.transform.parent.transform.parent = transform; }
        }
    }
    void OnTriggerExit(Collider col)
    {
        
        if (col.transform.tag == "PlayerPickup")
        {
            Debug.Log("Exit of " + col.transform.tag);  
            col.transform.parent.transform.parent = null;
            Wait = 0.5f;
        }
    }

}
